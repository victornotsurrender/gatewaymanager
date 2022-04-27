using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayManagingAPI.Entities;

namespace GatewayManagingAPI.Repositories {

    public interface IRepository {
        Task<List<Gateway>> getGateways();
        Task<Gateway> getGatewayBySerialID(string SerialID); 
        Task<bool> addGateway(Gateway gateway);
        Task<bool> delGateway(string SerialID);
        Task<bool> modifyGateway(Gateway gateway);
        Task<List<Peripheral>> getPeripherals();
        Task<List<Peripheral>> getGatewayPeripherals(string SerialID);
        Task<Peripheral> getGatewayPeripheral(string SerialID, int UID);
        Task<bool> addPeripheral(Peripheral peripheral);
        Task<bool> delPeripheral(string SerialID, int UID);
        Task<bool> modifyPeripheral(Peripheral peripheral);
    }
}