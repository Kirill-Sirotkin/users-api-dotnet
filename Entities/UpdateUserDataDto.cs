namespace users_api_dotnet.Entities {
    public class UpdateUserDataDto {
        public Guid Guid {get;set;}
        public string? Name {get;set;}
        public int? Gender {get;set;}
        public DateTime? Birthday {get;set;}
    }
}