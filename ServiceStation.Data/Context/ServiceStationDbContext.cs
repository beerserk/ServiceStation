using ServiceStation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Data.Context
{
    public class ServiceStationDbContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        
        public DbSet<Car> Car { get; set; }

        public DbSet<Order> Order { get; set; }
    }
}
