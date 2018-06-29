using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Models.Interfaces
{
    public interface IOrdersRepository
    {
        List<Order> LoadOrders(DateTime date);
        void AddOrder(Order order);
        void EditOrder(int orderNumber, Order newOrder);
        void DeleteOrder(int orderNumber, DateTime date);
        //void LoadOrder(int orderNumber, DateTime date);
    }
}
