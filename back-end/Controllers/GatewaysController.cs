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

        [HttpPost]
        public async void Post([FromBody] Gateway gateway){
            if ( gateway == null ){
                logger.LogInformation("Accessed to Post with null gateway");
                return;
            }
            List<Gateway> gateways = await repo.getGateways();
            foreach ( Gateway curr in gateways ){
                if ( curr.SerialID.Equals( gateway.SerialID ) ){
                    logger.LogError("Failed to add the gateway. SerialID is already taken");
                    return;
                }
            }
            repo.addGateway(gateway);
            logger.LogInformation("Success. Added a new gateway");
        }

        [HttpPut]
        public void Put([FromBody] Gateway gateway){
            Console.WriteLine("Mermelada at Put");
        }

        [HttpDelete]
        public void Delete(){
            Console.WriteLine("Mermelada at Delete");
        }
    }
}