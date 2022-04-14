using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayManagingAPI.Entities{
    public class GatewayPeripherals{
        [Required]
        [Key]
        public string SerialID { get; set; } = null!;
        [Required]
        [Key]
        public int UID { get; set; }
    }
}