using users_api_dotnet.Entities;
using users_api_dotnet.DatabaseContext;
using Microsoft.AspNetCore.Identity;

namespace users_api_dotnet.Services {
    public class UsersService : IUsersService
    {
        private readonly DataBaseContext _database;

        public UsersService(DataBaseContext database) {
            _database = database;
        }

        public User? CreateUser(string login, string password, string name, int gender, DateTime? birthday, bool isAdmin) {
            var user = _database.Users.SingleOrDefault(u => u.Login == login);
            if (user is not null) { return null; }

            var newUser = new User(login, password, name, gender, birthday, isAdmin);
            var passwordHash = new PasswordHasher<User>()
                .HashPassword(newUser, password);
            newUser.PasswordHash = passwordHash;

            _database.Users.Add(newUser);
            _database.SaveChanges();

            return newUser;
        }

        public User? UpdateUserData(Guid userId, string name, int gender, DateTime birthday) {
            throw new NotImplementedException();
        }

        public User? UpdateUserPassword(Guid userId, string password) {
            throw new NotImplementedException();
        }

        public User? UpdateUserLogin(Guid userId, string login) {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetActiveUsers() {
            throw new NotImplementedException();
        }

        public User? GetUserByLogin(string login) {
            throw new NotImplementedException();
        }

        public User? GetUserByLoginAndPassword(string login, string password) {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersOlderThanAge(int age) {
            throw new NotImplementedException();
        }

        public User? RevokeUserByLogin(string login, bool isDeleteFully) {
            throw new NotImplementedException();
        }

        public User? RestoreUserByLogin(string login) {
            throw new NotImplementedException();
        }

        public User? HelperAddMockAdmin() {
            throw new NotImplementedException();
        }
    }
}