using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GatewayManagingAPI.Validations;
using GatewayManagingAPI.Entities;

namespace GatewayManagingAPI.DTOs{
    public class PeripheralDTO{
        [Required]
        public int UID { get; set; }
        [Required]
        public string Vendor { get; set; } = null!;
        [Required]
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; } = false!;

        public Peripheral getPeripheral(){
            Peripheral ret = new Peripheral();
            ret.Vendor = this.Vendor;
            ret.CreatedDate = this.CreatedDate;
            ret.Status = this.Status;
            ret.UID = this.UID;
            return ret;
        }
    }
}