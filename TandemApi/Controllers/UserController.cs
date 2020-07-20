using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TandemApi.Objects;
using TandemApi.Services;

namespace TandemApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public MapperConfiguration Config { get; set; }
        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
            Config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TandemUser, ReturnUser>()
                    .ForMember(dest => dest.name, act => act.MapFrom(src => $"{src.firstName} {src.middleName} {src.lastName}"))
                    .ForMember(dest => dest.userId, act => act.MapFrom(src => src.id)));
        }

        [HttpGet]
        public async Task<IActionResult> Get(string emailAddress)
        {
            try
            {
                var cosmosService = new CosmosService(Configuration);
                var tandemUser = await cosmosService.GetUser(emailAddress);
                if (tandemUser == null)
                {
                    throw new Exception($"No User found with Email Address {emailAddress}");
                }
                var mapper = new Mapper(Config);
                var returnUser = mapper.Map<ReturnUser>(tandemUser);
                return Ok(returnUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.GetFullMessage());
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TandemUser user)
        {
            try
            {
                var cosmosService = new CosmosService(Configuration);
                var userId = await cosmosService.CreateUser(user);
                return Ok($"User successfully created with id {userId}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.GetFullMessage());
            }
        }
    }
}
