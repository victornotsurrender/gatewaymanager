using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayManagingAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GatewayManagingAPI.Entities{

    [Route("api/gateways")]
    public class GatewaysController: ControllerBase{
        private readonly IRepository repo;

        public GatewaysController(IRepository repo){
            this.repo = repo;
        }

        [HttpGet]
        public List<Gateway> Get(){
            return repo.getGateways();
        }
        
        [HttpGet("{Name}")]
        public ActionResult<Gateway> Get(string Name){
            var gateway = repo.getGatewayByName(Name);
            
            if ( gateway == null ){
                return NotFound();
                // Console.WriteLine( "Found mermelada "+Name );
            }

            return gateway;
        }

        [HttpPost]
        public void Post(){
        }

        [HttpPut]
        public void Put(){
        }

        [HttpDelete]
        public void Delete(){
        }
    }
}