using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GatewayManagingAPI.Validations;
using GatewayManagingAPI.DTOs;

namespace GatewayManagingAPI.Entities{
    public class Gateway{
        [Key]
        [Required]
        public string SerialID { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [IPv4]
        public string IPv4 { get; set; } = null!;

        public GatewayDTO getGatewayDTO(){
            GatewayDTO ret = new GatewayDTO();
            ret.SerialID = this.SerialID;
            ret.Name = this.Name;
            ret.IPv4 = this.IPv4;
            return ret;
        }
    }
}