using ServiceStation.Data.Context;
using ServiceStation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Data.Helpers
{
    public static class DbUpdate
    {
        public static void PreCreateUsers(bool create)
        {
            if (!create) return;

            var context = new ServiceStationDbContext();
            if (context.Client.Count() != 0) return;

            context.Client.AddRange( new List<Client> {
                new Client
                {
                    ClientId = 1,
                    FirstName = "John",
                    LastName = "Carter",
                    FullName = "John Carter",
                    DateOfBirth = new DateTime(1980, 6, 12),
                    Email = "jc@mail.com",
                    Phone = "+375 29 2343555",
                    Address = "Mars"
                }, 

                new Client
                {
                    ClientId = 2,
                    FirstName = "James",
                    LastName = "Perry",
                    FullName = "James Perry",
                    DateOfBirth = new DateTime(1983, 3, 5),
                    Email = "",
                    Phone = "+375 25 5151577",
                    Address = "Some address"
                }, 

                new Client
                {
                    ClientId = 3,
                    FirstName = "John",
                    LastName = "McPherson",
                    FullName = "John McPherson",
                    DateOfBirth = new DateTime(1975, 10, 20),
                    Email = "mymail@mail.com",
                    Phone = "+375 44 7788555",
                    Address = "17-10, Wall Street, NY"
                }
            });


            context.Car.AddRange(new List<Car> {
                new Car
                {
                    ClientId = 1,
                    CarId = 1,
                    Make = "Honda",
                    Model = "Accord",
                    VIN = "3fddsf49845fgh90p",
                    Year = 2000
                }, 

                new Car
                {
                    ClientId = 1,
                    CarId = 2,
                    Make = "Toyota",
                    Model = "Avensis",
                    VIN = "fdrtyur4583458dff",
                    Year = 2008
                }, 

                new Car
                {
                    ClientId = 2,
                    CarId = 3,
                    Make = "Daewoo",
                    Model = "Lanos",
                    VIN = "gantl93478fdfg001",
                    Year = 1997
                }, 

                new Car
                {
                    ClientId = 2,
                    CarId = 4,
                    Make = "Audi",
                    Model = "100",
                    VIN = "ausf0008943hhj345",
                    Year = 1980
                }, 

                new Car
                {
                    ClientId = 3,
                    CarId = 5,
                    Make = "Tesla",
                    Model = "Model S",
                    VIN = "tss90002dfsdfjkll",
                    Year = 2015
                }, 
            });

            context.Order.AddRange(new List<Order> {
                new Order
                {
                    OrderId = 1,
                    CarId = 1,
                    Status = OrderStatus.InProgress,
                    Date = new DateTime(2016, 08, 20),
                    Amount = 50
                }, 

                new Order
                {
                    OrderId = 2,
                    CarId = 3,
                    Status = OrderStatus.InProgress,
                    Date = new DateTime(2016, 08, 19),
                    Amount = 20
                }, 

                new Order
                {
                    OrderId = 3,
                    CarId = 3,
                    Status = OrderStatus.Completed,
                    Date = new DateTime(2016, 07, 25),
                    Amount = 37
                }, 

                new Order
                {
                    OrderId = 4,
                    CarId = 4,
                    Status = OrderStatus.Cancelled,
                    Date = new DateTime(2016, 06, 13),
                    Amount = 10
                }, 

                new Order
                {
                    OrderId = 5,
                    CarId = 4,
                    Status = OrderStatus.InProgress,
                    Date = new DateTime(2016, 08, 01),
                    Amount = 120
                }, 

                new Order
                {
                    OrderId = 6,
                    CarId = 5,
                    Status = OrderStatus.InProgress,
                    Date = new DateTime(2016, 09, 02),
                    Amount = 5000
                }, 
            });


            context.SaveChanges();
            context.Dispose();
        }
    }
}
