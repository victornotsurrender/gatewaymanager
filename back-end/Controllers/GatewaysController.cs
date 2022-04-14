using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayManagingAPI.Entities;
using GatewayManagingAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GatewayManagingAPI.Controllers{

    [Route("api/gateways")]
    [ApiController]
    public class GatewaysController: ControllerBase{
        private readonly IRepository repo;
        private readonly ILogger<GatewaysController> logger;

        public GatewaysController(IRepository repo,
        ILogger<GatewaysController>logger){
            this.repo = repo;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Gateway>>> Get(){
            return await repo.getGateways();
        }
        
        [HttpGet("{SerialID}")]
        public async Task<ActionResult<Gateway>> Get(string SerialID){
            var gateway = await repo.getGatewayBySerialID(SerialID);
            
            if ( gateway == null ){
                logger.LogError("Not Found SerialID: "+SerialID);
                return NotFound();
            }

            return gateway;
        }
        
        [HttpGet("{SerialID}/{devicesStr}")]
        public async Task<ActionResult<List<Peripheral>>> Get(string SerialID, string devicesStr){
            if ( !devicesStr.Equals("devices") ){
                return NotFound();
            }
            var peripherals = await repo.getGatewayPeripherals(SerialID);
            if ( peripherals == null ){
                return NotFound();
            }
            return peripherals;
        }
        
        [HttpGet("{SerialID}/devices/{UID:int}")]
        [HttpGet("{SerialID}/{UID:int}")]
        public async Task<ActionResult<Peripheral>> Get(string SerialID, int UID){
            var peripheral = await repo.getGatewayPeripheral(SerialID, UID);
            if ( peripheral == null ){
                return NotFound();
            }
            return peripheral;
        }

        [HttpPost]
        public async void Post([FromBody] Gateway gateway){
            if ( gateway == null ){
                logger.LogInformation("Accessed to Post with null gateway");
                return;
            }
            List<Gateway> gateways = await repo.getGateways();
            foreach ( Gateway curr in gateways ){
                if ( curr.SerialID.Equals( gateway.SerialID ) ){
                    logger.LogError("Failed to add the gateway. SerialID "+gateway.SerialID+" is already taken");
                    return;
                }
            }
            await repo.addGateway(gateway);
            logger.LogInformation("Success. Added the gateway with serial "+gateway.SerialID);
        }

        [HttpPost("{SerialID}")]
        public async void Post(string SerialID, [FromBody] Peripheral peripheral){
            if ( peripheral == null ){
                logger.LogInformation("Accessed to Post with null device");
                return;
            }
            List<Peripheral> peripherals = await repo.getGatewayPeripherals(SerialID);
            bool ok = await repo.addPeripheral(peripheral);
            if ( !ok ){
                logger.LogError("Failed to add the device. UID "+peripheral.UID+" is already taken");
                return;
            }
            logger.LogInformation("Successfully added the device "+peripheral.UID);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Gateway gateway){
            bool result = await repo.modifyGateway(gateway);
            if ( !result ){
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("devices")]
        public async Task<ActionResult> Put([FromBody] Peripheral peripheral){
            bool result = await repo.modifyPeripheral(peripheral);
            if ( !result ){
                logger.LogError("Failed to modify the device. Was impossible to find the device "+peripheral.UID);
                return NotFound();
            }
            logger.LogInformation("Successfuly modified the device "+peripheral.UID);
            return Ok();
        }

        [HttpDelete("{SerialID}")]
        public async Task<ActionResult> Delete(string SerialID){
            bool result = await repo.delGateway(SerialID);
            if ( !result ){
                return NotFound();
            }
            return NoContent();
        }
        
        [HttpDelete("{SerialID}/devices/{UID:int}")]
        [HttpDelete("{SerialID}/{UID:int}")]
        public async Task<ActionResult> Delete(string SerialID, int UID){
            bool result = await repo.delPeripheral(SerialID, UID);
            if ( !result ){
                return NotFound();
            }
            return NoContent();
        }
    }
}