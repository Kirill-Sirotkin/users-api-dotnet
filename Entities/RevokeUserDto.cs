namespace users_api_dotnet.Entities {
    public class RevokeUserDto {
        public string Login {get;set;} = string.Empty;
        public bool IsDeleteFully {get;set;} = false;
    }
}