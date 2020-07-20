using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TandemApi.Objects
{
    public class ReturnUser
    {
        public Guid userId { get; set; }
        // public string UserId => Id.ToString();
        public string name {get;set;}
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
    }
}
