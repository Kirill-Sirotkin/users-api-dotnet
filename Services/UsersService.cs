using users_api_dotnet.Entities;
using users_api_dotnet.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace users_api_dotnet.Services {
    public class UsersService : IUsersService
    {
        char[] _allowedChars = [
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        ];
        char[] _allowedNameChars = [
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 
            'а','б','в','г','д','е','ё','ж','з','и','й','к','л','м','н','о','п',
            'р','с','т','у','ф','х','ц','ч','ш','щ','ъ','ы','ь','э','ю','я',
            'А','Б','В','Г','Д','Е','Ё','Ж','З','И','Й','К','Л','М','Н','О','П',
            'Р','С','Т','У','Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я'
        ];
        private readonly DataBaseContext _database;

        public UsersService(DataBaseContext database) {
            _database = database;
        }

        public User? CreateUser(string login, string password, string name, int gender, DateTime? birthday, bool isAdmin, string editor) {
            var user = _database.Users.SingleOrDefault(u => u.Login == login);
            if (user is not null) { return null; }

            if (!ValidateName(name)) { return null; }
            if (!ValidateLogin(login)) { return null; }
            if (!ValidateGender(gender)) { return null; }
            if (!ValidatePassword(password)) { return null; }
            if (!ValidateBirthday(birthday)) { return null; }

            var newUser = new User(login, password, name, gender, birthday, isAdmin, editor);
            var passwordHash = new PasswordHasher<User>()
                .HashPassword(newUser, password);
            newUser.PasswordHash = passwordHash;

            _database.Users.Add(newUser);
            _database.SaveChanges();

            return newUser;
        }

        public User? UpdateUserData(Guid userId, string? name, int? gender, DateTime? birthday, string editor) {
            var user = _database.Users.SingleOrDefault(u => u.Guid == userId);
            if (user is null) { return null; }

            if (name is not null) {
                if (!ValidateName(name)) { return null; }
                user.Name = name;
            }
            if (gender is not null) {
                if (!ValidateGender((int)gender)) { return null; }
                user.Gender = (int)gender;
            }
            if (birthday is not null) {
                if (!ValidateBirthday(birthday)) { return null; }
                user.Birthday = birthday;
            }
            user.ModifiedBy = editor;
            user.ModifiedOn = DateTime.UtcNow;

            _database.SaveChanges();
            return user;
        }

        public User? UpdateUserPassword(Guid userId, string? password, string editor) {
            var user = _database.Users.SingleOrDefault(u => u.Guid == userId);
            if (user is null) { return null; }

            if (password is null) { return null; }
            if (!ValidatePassword(password)) { return null; }

            var passwordHash = new PasswordHasher<User>()
                .HashPassword(user, password);
            user.PasswordHash = passwordHash;
            user.ModifiedBy = editor;
            user.ModifiedOn = DateTime.UtcNow;

            _database.SaveChanges();

            return user;
        }

        public User? UpdateUserLogin(Guid userId, string? login, string editor) {
            var user = _database.Users.SingleOrDefault(u => u.Guid == userId);
            if (user is null) { return null; }

            if (login is null) { return null; }
            if (!ValidateLogin(login)) { return null; }

            var userWithNewLogin = _database.Users.SingleOrDefault(u => u.Login == login);
            if (userWithNewLogin is not null) { return null; }

            user.Login = login;
            user.ModifiedBy = editor;
            user.ModifiedOn = DateTime.UtcNow;

            _database.SaveChanges();

            return user;
        }

        public IEnumerable<User> GetActiveUsers() {
            List<User> users = _database.Users.Where(u => u.RevokedOn == null)
                .ToList();

            return users;
        }

        public User? GetUserByLogin(string login) {
            return _database.Users.SingleOrDefault(u => u.Login == login);
        }

        public User? GetUserByLoginAndPassword(string login, string password) {
            var user = _database.Users.SingleOrDefault(u => u.Login == login);
            if (user is null) { return null; }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password) 
                == PasswordVerificationResult.Failed
            ) { return null; }

            return user;
        }

        public IEnumerable<User> GetUsersOlderThanAge(int age) {
            List<User> users = _database.Users.Where(
                u => 
                    u.Birthday != null &&
                    DateTime.Today.Year - u.Birthday.Value.Year > age
            ).ToList();

            return users;
        }

        public User? RevokeUserByLogin(string login, string adminLogin, bool isDeleteFully) {
            var user = _database.Users.SingleOrDefault(u => u.Login == login);
            if (user is null) { return null; }

            if (isDeleteFully) { 
                _database.Remove(user); 
                _database.SaveChanges();
                return user;
            }

            user.RevokedOn = DateTime.UtcNow;
            user.RevokedBy = adminLogin;
            user.ModifiedBy = adminLogin;
            user.ModifiedOn = DateTime.UtcNow;
            _database.SaveChanges();
            return user;
        }

        public User? RestoreUserByLogin(string login, string editor) {
            var user = _database.Users.SingleOrDefault(u => u.Login == login);
            if (user is null) { return null; }

            user.RevokedOn = null;
            user.RevokedBy = "";
            user.ModifiedBy = editor;
            user.ModifiedOn = DateTime.UtcNow;
            _database.SaveChanges();
            return user;
        }

        public LoginPasswordDto? HelperAddMockAdmin() {
            var password = RandomNumberGenerator.GetString(_allowedChars, 8);
            var loginAddon = RandomNumberGenerator.GetString(_allowedChars, 8);
            var login = "mockAdmin" + loginAddon;

            var newUser = new User(login, password, "admin", 2, null, true, login);
            var passwordHash = new PasswordHasher<User>()
                .HashPassword(newUser, password);
            newUser.PasswordHash = passwordHash;

            _database.Users.Add(newUser);
            _database.SaveChanges();

            var loginPassword = new LoginPasswordDto {
                Login = login,
                Password = password
            };

            return loginPassword;
        }

        public string GenerateToken(User user) {
            var claims = new List<Claim> {
                new(ClaimTypes.NameIdentifier, user.Guid.ToString()),
                new(ClaimTypes.Role, user.Admin ? "ADMIN" : "USER"),
                new("Active", user.RevokedOn is null ? "ACTIVE" : "REVOKED"),
                new("Login", user.Login)
            };
            
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(DotNetEnv.Env.GetString("JWT_SECRET"))
            );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(DotNetEnv.Env.GetInt("JWT_EXPIRES_IN_MINUTES")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private bool ValidateLogin(string login) {
            foreach (char c in login) {
                if (_allowedChars.Contains(c)) { return false; }
            }
            return true;
        }

        private bool ValidateName(string name) {
            foreach (char c in name) {
                if (_allowedNameChars.Contains(c)) { return false; }
            }
            return true;
        }

        private bool ValidatePassword(string password) {
            foreach (char c in password) {
                if (_allowedChars.Contains(c)) { return false; }
            }
            return true;
        }

        private bool ValidateGender(int gender) {
            if (gender < 0 || gender > 2) { return false; }
            return true;
        }

        private bool ValidateBirthday(DateTime? birthday) {
            if (birthday is null) { return true; }
            return birthday < DateTime.UtcNow;
        }
    }
}