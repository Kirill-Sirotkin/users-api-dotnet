using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace users_api_dotnet.Entities {
    public class User {
        [Key]
        public Guid Guid {get;set;}
        public string Login {get;set;}
        public string PasswordHash {get;set;}
        public string Name {get;set;}
        public int Gender {get;set;}
        public DateTime? Birthday {get;set;}
        public bool Admin {get;set;} = false;
        public DateTime CreatedOn {get;set;}
        public string CreatedBy {get;set;}
        public DateTime ModifiedOn {get;set;}
        public string ModifiedBy {get;set;}
        public DateTime? RevokedOn {get;set;}
        public string RevokedBy {get;set;} = string.Empty;

        public User(
            string login, 
            string passwordHash, 
            string name, 
            int gender, 
            DateTime? birthday, 
            bool admin,
            string editor
        ) {
            Guid = Guid.NewGuid();
            Login = login;
            PasswordHash = passwordHash;
            Name = name;
            Gender = gender;
            Birthday = birthday;
            Admin = admin;
            CreatedOn = DateTime.UtcNow;
            CreatedBy = editor;
            ModifiedOn = DateTime.UtcNow;
            ModifiedBy = editor;
        }
    }
}