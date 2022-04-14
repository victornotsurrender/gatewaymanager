using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayManagingAPI.Entities;

namespace GatewayManagingAPI.Repositories {

    public interface IRepository {
        Task<List<Gateway>> getGateways();
        Task<Gateway> getGatewayBySerialID(string SerialID);
        void addGateway(Gateway gateway);
    }
}