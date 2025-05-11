namespace users_api_dotnet.Entities {
    public class CreateUserDto {
        public string Login {get;set;} = string.Empty;
        public string Password {get;set;} = string.Empty;
        public string Name {get;set;} = string.Empty;
        public int Gender {get;set;}
        public DateTime? Birthday {get;set;}
        public bool isAdmin {get;set;}
    }
}