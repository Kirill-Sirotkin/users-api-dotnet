namespace users_api_dotnet.Entities {
    public class UserDto {
        public Guid Guid {get;set;}
        public string Login {get;set;} = string.Empty;
        public string Name {get;set;} = string.Empty;
        public int Gender {get;set;}
        public DateTime? Birthday {get;set;}
        public DateTime CreatedOn {get;set;}
        public DateTime ModifiedOn {get;set;}
        public bool IsActive {get;set;}

        public UserDto(User user) {
            Guid = user.Guid;
            Login = user.Login;
            Name = user.Name;
            Gender = user.Gender;
            Birthday = user.Birthday;
            CreatedOn = user.CreatedOn;
            ModifiedOn = user.ModifiedOn;
            IsActive = user.RevokedOn is null ? true : false;
        }
    }
}