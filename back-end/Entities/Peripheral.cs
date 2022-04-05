using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayManagingAPI.Entities{
    public class Peripheral{
        public int UID { get; set; }
        public string Vendor { get; set; } = null!;
        public DateOnly CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}