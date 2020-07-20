using System;
using Microsoft.Azure.Cosmos.Linq;

namespace TandemApi.Objects
{
    public class TandemUser
    {
        public Guid id { get; set; }
        // public string UserId => Id.ToString();
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        // public string Name => $"{firstName} {middleName} {lastName}";
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
        
    }
}
