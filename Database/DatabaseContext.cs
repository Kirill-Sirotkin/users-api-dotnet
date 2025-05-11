using Microsoft.EntityFrameworkCore;
using users_api_dotnet.Entities;

namespace users_api_dotnet.DatabaseContext {
    public class DataBaseContext : DbContext {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) {}

        public DbSet<User> Users {get; set;}
    }
}