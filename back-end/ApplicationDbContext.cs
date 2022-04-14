using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GatewayManagingAPI.Entities;

namespace GatewayManagingAPI{
    public class ApplicationDbContext: DbContext{

        public ApplicationDbContext(DbContextOptions options): base(options){
        }

        public DbSet<Gateway> gateways { get; set; } = null!;

    }
}