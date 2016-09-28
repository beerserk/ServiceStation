using ServiceStation.Data.API;
using ServiceStation.Data.Context;
using ServiceStation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Data.Repositories
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private ServiceStationDbContext context;

        public OrderRepository(ServiceStationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> GetOrders()
        {
            var orders = context.Order.Include(o => o.Car).Include(o => o.Car.Owner);
            return orders.ToList();
        }

        public Order FindOrderById(int? orderId)
        {
            return context.Order.Find(orderId);
        }

        public void AddOrder(Order order)
        {
            context.Order.Add(order);
        }

        public void DeleteOrder(Order order)
        {
            context.Order.Remove(order);
        }

        public void UpdateOrder(Order order)
        {
            context.Entry(order).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
