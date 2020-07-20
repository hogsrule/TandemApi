using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using TandemApi.Objects;

namespace TandemApi.Services
{
    public class CosmosService
    {
        public CosmosService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public async Task<Guid> CreateUser(TandemUser user)
        {
            if (await GetUser(user.emailAddress) == null)
            {
                user.id = Guid.NewGuid();
                var container = GetContainer();
                await container.CreateItemAsync(user);
                return user.id;
            }
            throw new Exception($"User with Email Address : {user.emailAddress} already exists");
        }

        public async Task<TandemUser> GetUser(string emailAddress)
        {
            var container = GetContainer();
            var sqlQueryText = $"SELECT * FROM c WHERE c.emailAddress = '{emailAddress}'";
            var queryDefinition = new QueryDefinition(sqlQueryText);
            var queryResultSetIterator = container.GetItemQueryIterator<TandemUser>(queryDefinition);

            var users = new List<TandemUser>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<TandemUser> currentResultSet = await queryResultSetIterator.ReadNextAsync();
               foreach (TandemUser user in currentResultSet)
                {
                    users.Add(user);
                }
            }
            return users.FirstOrDefault();
            // return new TandemUser()
            // {
            //     UserId = "randomId",
            //     FirstName = "First",
            //     MiddleName = "Middle",
            //     LastName = "Last",
            //     PhoneNumber = "555-555-5555",
            //     EmailAddress = emailAddress
            // };
        }

        private Container GetContainer()
        {
            var accountEndpoint = Configuration.GetSection("CosmosConnection").Value;
            var authKeyOrResourceToken = Configuration.GetSection("AuthKey").Value;
            var databaseName = Configuration.GetSection("DatabaseName").Value;
            var containerName = Configuration.GetSection("UserContainerName").Value;
            var cosmosClient = new CosmosClient(accountEndpoint, authKeyOrResourceToken);
            var database = cosmosClient.GetDatabase(databaseName);
            var container = database.GetContainer(containerName);
            return container;
        }
    }
}