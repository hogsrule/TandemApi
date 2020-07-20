using TandemApi.Objects;

namespace TandemApi.Services
{
    public class CosmosService
    {
        public void CreateUser(User user)
        {
            //Save to Cosmos
        }


        public User GetUser(string emailAddress)
        {
            return new User()
            {
                UserId = "randomId",
                FirstName = "First",
                MiddleName = "Middle",
                LastName = "Last",
                PhoneNumber = "555-555-5555",
                EmailAddress = emailAddress
            };
        }
    }
}