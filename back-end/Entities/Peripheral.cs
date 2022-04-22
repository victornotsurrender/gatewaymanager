using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GatewayManagingAPI.DTOs;

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

        public PeripheralDTO getPeripheralDTO(){
            PeripheralDTO ret = new PeripheralDTO();
            ret.UID = this.UID;
            ret.Vendor = this.Vendor;
            ret.CreatedDate = this.CreatedDate;
            ret.Status = this.Status;
            return ret;
        }

        public PeripheralCreationDTO getPeripheralCreationDTO(){
            PeripheralCreationDTO ret = new PeripheralCreationDTO();
            ret.Vendor = this.Vendor;
            ret.CreatedDate = this.CreatedDate;
            ret.Status = this.Status;
            return ret;
        }
    }
}