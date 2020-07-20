using System;
using System.Threading.Tasks;
using AutoMapper;
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
        public async Task<ReturnUser> Get(string emailAddress)
        {
            try
            {
                var cosmosService = new CosmosService(Configuration);
                var tandemUser = await cosmosService.GetUser(emailAddress);
                var mapper = new Mapper(Config);
                var returnUser = mapper.Map<ReturnUser>(tandemUser);
                return returnUser;
            }
            catch (Exception e)
            {
                throw;
            }
            
        }
        [HttpPost]
        public async Task<bool> Create([FromBody] TandemUser user)
        {
            try
            {
                var cosmosService = new CosmosService(Configuration);
                await cosmosService.CreateUser(user);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
