using System;
using Microsoft.AspNetCore.Mvc;
using TandemApi.Objects;
using TandemApi.Services;

namespace TandemApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public User Get(string emailAddress)
        {
            var cosmosService = new CosmosService();
            return cosmosService.GetUser(emailAddress);

            
        }
        [HttpPost]
        public bool Create([FromBody] User user)
        {
            try
            {
                var cosmosService = new CosmosService();
                cosmosService.CreateUser(user);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
