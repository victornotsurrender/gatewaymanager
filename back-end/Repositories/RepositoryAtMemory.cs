using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayManagingAPI.Entities;

namespace GatewayManagingAPI.Repositories{

    public class RepositoryAtMemory: IRepository{
        private List<Gateway> gateways = null!;
        private static List<Gateway> remote = new List<Gateway>(){
            new Gateway(){SerialID = "KJJFS-15", Name = "Router John Salsa", IPv4 = "192.168.43.228"},
            new Gateway(){SerialID = "YUSII-05", Name = "TP-Link Gordo", IPv4 = "192.168.43.137"},
            new Gateway(){SerialID = "ALSKD-23", Name = "Wifi03", IPv4 = "192.168.43.144"}
        };

        public RepositoryAtMemory(){
            gateways = remote;
        }

        public async Task<List<Gateway>> getGateways(){
            await Task.Delay(1);
            return gateways;
        }

        public async Task<Gateway> getGatewayBySerialID(string SerialID){
            await Task.Delay(1);
            return gateways.FirstOrDefault(g => g.SerialID.Equals(SerialID))!;
        }

        public async void addGateway(Gateway gateway){
            await Task.Delay(1);
            this.gateways.Add(gateway);
            RepositoryAtMemory.remote = gateways;
        }
    }
}