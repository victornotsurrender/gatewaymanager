using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayManagingAPI.Entities;

namespace GatewayManagingAPI.Repositories{

    public class RepositoryAtMemory: IRepository{
        private List<Gateway> gateways = null!;
        private List<GatewayPeripheral> gatewayPeripherals = null!;
        private List<Peripheral> peripherals = null!;

        static private List<int> pUID = null!;
        private static List<Gateway> remoteGateways = new List<Gateway>(){
            new Gateway(){SerialID = "KJJFS-15", Name = "Router John Salsa", IPv4 = "192.168.43.228"},
            new Gateway(){SerialID = "YUSII-05", Name = "TP-Link Gordo", IPv4 = "192.168.43.137"},
            new Gateway(){SerialID = "ASKOF-05", Name = "Wifi Etecsa", IPv4 = "192.168.43.138"},
            new Gateway(){SerialID = "ALSKD-23", Name = "Wifi03", IPv4 = "192.168.43.144"}
        };
        private static List<GatewayPeripheral> remoteGatewaysPeripherals = new List<GatewayPeripheral>(){
            new GatewayPeripheral(){SerialID = "YUSII-05", UID = 1},
            new GatewayPeripheral(){SerialID = "ASKOF-05", UID = 5},
            new GatewayPeripheral(){SerialID = "KJJFS-15", UID = 23},
            new GatewayPeripheral(){SerialID = "KJJFS-15", UID = 22}
        };
        private static List<Peripheral> remotePeripherals = new List<Peripheral>(){
            new Peripheral(){UID = 1, Vendor = "That Guy", CreatedDate = new DateTime(2000, 4, 6), Status = true},
            new Peripheral(){UID = 5, Vendor = "My Neighbour", CreatedDate = new DateTime(2015, 12, 31), Status = false},
            new Peripheral(){UID = 23, Vendor = "Obviously Me", CreatedDate = new DateTime(), Status = true},
            new Peripheral(){UID = 22, Vendor = "Obviously Me", CreatedDate = new DateTime(), Status = true}
        };

        public RepositoryAtMemory(){
            gateways = remoteGateways;
            peripherals = remotePeripherals;
            gatewayPeripherals = remoteGatewaysPeripherals;
            if ( pUID == null ){
                pUID = new List<int>();
                foreach ( Peripheral p in peripherals ){
                    pUID.Add(p.UID);
                }
            }
        }

        public async Task<List<Gateway>> getGateways(){
            await Task.Delay(1);
            return gateways;
        }

        public async Task<Gateway> getGatewayBySerialID(string SerialID){
            await Task.Delay(1);
            return gateways.FirstOrDefault(g => g.SerialID.Equals(SerialID))!;
        }
        public async Task<List<Peripheral>> getGatewayPeripherals(string SerialID){
            List<Peripheral> devices = new List<Peripheral>();
            foreach ( GatewayPeripheral curr in gatewayPeripherals ){
                if ( curr.SerialID.Equals( SerialID ) ){
                    devices.Add( peripherals.First( d => d.UID == curr.UID ) );
                    await Task.Delay(1);
                }
            }
            return devices;
        }
        public async Task<Peripheral> getGatewayPeripheral(string SerialID, int UID){
            await Task.Delay(1);
            bool ok = false;
            foreach ( GatewayPeripheral curr in gatewayPeripherals ){
                if ( curr.SerialID.Equals( SerialID ) && curr.UID == UID ){
                    ok = true;
                }
            }
            if ( ok ){
                return peripherals.FirstOrDefault(p => p.UID == UID)!;
            }
            return null!;
        }

        public async Task<bool> addGateway(Gateway gateway){
            bool result = true;
            int before, after;
            before = RepositoryAtMemory.remoteGateways.Count;
            //if the new gateway shares a SerialID or a IPv4 becomes invalid
            foreach ( Gateway curr in gateways ){
                result &= curr.SerialID.Equals(gateway.SerialID);
                result &= curr.IPv4.Equals(gateway.IPv4);
            }
            if ( result ){
                await Task.Delay(1);
                this.gateways.Add(gateway);
                RepositoryAtMemory.remoteGateways = gateways;
            }
            after = RepositoryAtMemory.remoteGateways.Count;
            result &= before == after-1;
            return result;
        }

        public async Task<List<Peripheral>> getPeripherals(){
            await Task.Delay(1);
            return peripherals;
        }
        public async Task<bool> addPeripheral(Peripheral peripheral){
            bool result = true;
            int before, after;
            before = RepositoryAtMemory.remotePeripherals.Count;
            //if the new peripheral shares a UID becomes invalid
            foreach ( Peripheral curr in peripherals ){
                result &= curr.UID.Equals(peripheral.UID);
            }
            if ( result ){
                await Task.Delay(1);
                this.peripherals.Add(peripheral);
                pUID.Add( peripheral.UID );
                RepositoryAtMemory.remotePeripherals = peripherals;
            }
            after = RepositoryAtMemory.remotePeripherals.Count;
            result &= before == after-1;
            return result;
        }

        public async Task<bool> delGateway(string SerialID){
            bool found = false;
            for ( int i = 0; !found && i < gateways.Count; ++i ){
                Gateway curr = gateways[i];
                if ( curr.SerialID.Equals(SerialID) ){
                    this.gateways.Remove(curr);
                    await Task.Delay(1);
                    RepositoryAtMemory.remoteGateways = gateways;
                    found = true;
                }
            }
            return found;
        }
        public async Task<bool> delPeripheral(string SerialID, int UID){
            bool found = false;
            for ( int i = 0; !found && i < gatewayPeripherals.Count; ++i ){
                GatewayPeripheral curr = gatewayPeripherals[i];
                if ( curr.SerialID.Equals(SerialID) && curr.UID == UID ){
                    foreach ( Peripheral rem in peripherals ){
                        if ( rem.UID == UID ){
                            foreach ( int id in pUID ){
                                if ( id == UID ){
                                    pUID.Remove( id );
                                }
                            }
                            this.peripherals.Remove(rem);
                            RepositoryAtMemory.remotePeripherals = peripherals;
                            await Task.Delay(1);
                            found = true;
                        }
                    }
                }
            }
            return found;
        }

        public async Task<bool> modifyGateway(Gateway gateway){
            bool found = false;
            for ( int i = 0; !found && i < gateways.Count; ++i ){
                Gateway curr = gateways[i];
                if ( curr.SerialID.Equals(gateway.SerialID) ){
                    this.gateways.Remove(curr);
                    this.gateways.Add( gateway );
                    await Task.Delay(1);
                    RepositoryAtMemory.remoteGateways = gateways;
                    found = true;
                }
            }
            return found;
        }
        
        public async Task<bool> modifyPeripheral(Peripheral peripheral){
            bool found = false;
            for ( int i = 0; !found && i < peripherals.Count; ++i ){
                Peripheral curr = peripherals[i];
                if ( curr.UID == peripheral.UID ){
                    peripherals.Remove( curr );
                    peripherals.Add( peripheral );
                    await Task.Delay(1);
                    RepositoryAtMemory.remotePeripherals = peripherals;
                    found = true;
                }
            }
            return found;
        }

        public static int genUID(){
            List<int> tmp = new List<int>(); 
            foreach ( int curr in pUID ){
                tmp.Add(curr);
            }
            int found = 0;
            for ( int i = 1; found == 0; ++i ){
                bool not_taked = true;
                foreach ( int curr in tmp ){
                    not_taked &= curr != i;
                }
                if ( not_taked ){
                    found = i;
                }
            }
            return found;
        }
    }
}