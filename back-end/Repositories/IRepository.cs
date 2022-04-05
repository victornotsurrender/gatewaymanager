using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayManagingAPI.Entities;

namespace GatewayManagingAPI.Repositories {

    public interface IRepository {
        List<Gateway> getGateways();
        public Gateway getGatewayByName(string Name);
    }
}