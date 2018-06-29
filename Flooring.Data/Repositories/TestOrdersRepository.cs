using Flooring.Models;
using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data.Repositories
{
    public class TestOrdersRepository : IOrdersRepository
    {
        public List<Order> AllOrders { get; set; }

        public List<Order> LoadOrders(DateTime date)
        {
            List<Order> temporaryList = new List<Order>();
            temporaryList = AllOrders;
            temporaryList.Add(new Order()
            {
                OrderNumber = 1,
                CustomerName = "test1",
                State = "OH",
                TaxRate = 6.25m,
                ProductType = "carpet",
                Area = 100,
                CostPerSqFt = 2.25m,
                LaborCostPerSqFt = 2.10m,
                MaterialCost = 225.00m,
                LaborCost = 210.00m,
                Tax = 27.19m,
                Total = 464.29m,
                Date = new DateTime(2019, 01, 01)
            });
            temporaryList.Add(new Order()
            {
                OrderNumber = 2,
                CustomerName = "test2",
                State = "OH",
                TaxRate = 6.25m,
                ProductType = "carpet",
                Area = 120,
                CostPerSqFt = 2.25m,
                LaborCostPerSqFt = 2.10m,
                MaterialCost = 225.00m,
                LaborCost = 210.00m,
                Tax = 27.19m,
                Total = 464.29m,
                Date = new DateTime(2019, 01, 01)
            });
            temporaryList.Add(new Order()
            {
                OrderNumber = 1,
                CustomerName = "test3",
                State = "OH",
                TaxRate = 6.25m,
                ProductType = "carpet",
                Area = 120,
                CostPerSqFt = 2.25m,
                LaborCostPerSqFt = 2.10m,
                MaterialCost = 225.00m,
                LaborCost = 210.00m,
                Tax = 27.19m,
                Total = 464.29m,
                Date = new DateTime(2020, 01, 01)
            });
            temporaryList.Add(new Order()
            {
                OrderNumber = 2,
                CustomerName = "test1",
                State = "OH",
                TaxRate = 6.25m,
                ProductType = "carpet",
                Area = 100,
                CostPerSqFt = 2.25m,
                LaborCostPerSqFt = 2.10m,
                MaterialCost = 225.00m,
                LaborCost = 210.00m,
                Tax = 27.19m,
                Total = 464.29m,
                Date = new DateTime(2020, 01, 01)
            });
            List<Order> listByDate = temporaryList.Where(x => x.Date == date).ToList();
            return listByDate;
        }

        public void AddOrder(Order order)
        {
            List<Order> tempList = LoadOrders(order.Date);
            tempList.Add(order);
            AllOrders = tempList;
        }

        public void EditOrder(int orderNumber, Order newOrder)
        {
            int i = AllOrders.FindIndex(x => x.OrderNumber == orderNumber);
            AllOrders[i] = newOrder;
        }

        public void DeleteOrder(int orderNumber, DateTime date)
        {
            int i = AllOrders.FindIndex(x => x.OrderNumber == orderNumber);
            AllOrders.Remove(AllOrders[i]);
        }

        //public Order LoadOrder(int orderNumber, DateTime date)
        //{
        //    List<Order> tempList = LoadOrders(date);
        //    bool orderExists = tempList.Any(x => x.OrderNumber == orderNumber);
        //    int i = tempList.FindIndex(x => x.OrderNumber == orderNumber);
        //}
    }
}
