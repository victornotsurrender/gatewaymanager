using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayManagingAPI.Validations;
using GatewayManagingAPI.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GatewayManagingAPI.DTOs{
    public class GatewayDTO{
        [Key]
        [Required]
        public string SerialID { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [IPv4]
        public string IPv4 { get; set; } = null!;

        public Gateway getGateway(){
            Gateway ret = new Gateway();
            ret.SerialID = this.SerialID;
            ret.Name = this.Name;
            ret.IPv4 = this.IPv4;
            return ret;
        }
    }
}