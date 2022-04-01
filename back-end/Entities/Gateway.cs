using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayManagingAPI.Entities{
    public class Gateway{
        public string SerialID { get; set; }
        public string Name { get; set; }
        public string IPv4 { get; set; }

    }
}