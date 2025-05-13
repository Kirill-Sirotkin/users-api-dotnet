namespace users_api_dotnet.Entities {
    public class UpdateUserPasswordDto {
        public Guid Guid {get;set;}
        public string? Password {get;set;}
    }
}