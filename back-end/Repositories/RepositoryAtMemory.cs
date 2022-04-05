using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayManagingAPI.Entities;

namespace GatewayManagingAPI.Repositories{
    
    public class RepositoryAtMemory: IRepository{
        private List<Gateway> gateways = null!;

        public RepositoryAtMemory(){
            gateways = new List<Gateway>(){
                new Gateway(){SerialID = "KJJFS-15", Name = "Router John Salsa", IPv4 = "192.168.43.228"},
                new Gateway(){SerialID = "YUSII-05", Name = "TP-Link Gordo", IPv4 = "192.168.43.137"},
                new Gateway(){SerialID = "ALSKD-23", Name = "Wifi03", IPv4 = "192.168.43.144"}
            };
        }

        public List<Gateway> getGateways(){
            return gateways;
        }

        public Gateway getGatewayByName(string Name){
            return gateways.FirstOrDefault(g => g.Name.Equals(Name));
        }
    }
}