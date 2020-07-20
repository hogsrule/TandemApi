using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TandemApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        
        [HttpGet]
        public User Get(string emailAddress)
        {

            return new User
            {
                Name = "Test"
            };

        }
    }
}
