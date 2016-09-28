using ServiceStation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Data.API
{
    public interface IOrderRepository : IDisposable
    {
        IEnumerable<Order> GetOrders();

        Order FindOrderById(int? orderId);

        void AddOrder(Order order);

        void DeleteOrder(Order order);

        void UpdateOrder(Order order);

        void Save();
    }
}
