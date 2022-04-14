using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayManagingAPI.Entities{
    public class Peripheral{
        [Key]
        [Required]
        public int UID { get; set; }
        [Required]
        public string Vendor { get; set; } = null!;
        [Required]
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; } = false!;
    }
}