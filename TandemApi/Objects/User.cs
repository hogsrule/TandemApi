namespace TandemApi.Objects
{
    public class User
    {
        public string UserId { get; set; }
        public string FirstName { private get; set; }
        public string MiddleName { private get; set; }
        public string LastName { private get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Name => $"{FirstName} {MiddleName} {LastName}";
    }
}
