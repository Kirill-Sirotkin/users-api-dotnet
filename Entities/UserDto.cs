namespace users_api_dotnet.Entities {
    public class UserDto {
        public string Login {get;set;} = string.Empty;
        public string Name {get;set;} = string.Empty;
        public int Gender {get;set;}
        public DateTime? Birthday {get;set;}
        public DateTime CreatedOn {get;set;}
        public DateTime ModifiedOn {get;set;}
        public bool isActive {get;set;}
    }
}