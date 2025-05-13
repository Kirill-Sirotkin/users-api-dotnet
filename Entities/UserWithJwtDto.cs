namespace users_api_dotnet.Entities {
    public class UserWithJwtDto : UserDto {
        public string Jwt {get;set;} = string.Empty;

        public UserWithJwtDto(User user, string jwt) : base(user) {
            Jwt = jwt;
        }
    }
}