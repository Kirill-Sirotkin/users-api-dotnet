using users_api_dotnet.Entities;

namespace users_api_dotnet.Services
{
    public interface IUsersService
    {
        public User? CreateUser(string login, string password, string name, int gender, DateTime? birthday, bool isAdmin);
        public User? UpdateUserData(Guid userId, string name, int gender, DateTime birthday);
        public User? UpdateUserPassword(Guid userId, string password);
        public User? UpdateUserLogin(Guid userId, string login);
        public IEnumerable<User> GetActiveUsers();
        public User? GetUserByLogin(string login);
        public User? GetUserByLoginAndPassword(string login, string password);
        public IEnumerable<User> GetUsersOlderThanAge(int age);
        public User? RevokeUserByLogin(string login, bool isDeleteFully);
        public User? RestoreUserByLogin(string login);
        public User? HelperAddMockAdmin();
    }
}