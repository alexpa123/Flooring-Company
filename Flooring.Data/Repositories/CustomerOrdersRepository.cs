using Flooring.Data.Helpers;
using Flooring.Models;
using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Flooring.Data
{
    public class CustomerOrdersRepository : IOrdersRepository
    {

        //what do we want this class to do? store all our orders, Load orders(read them from the text file) and save them(write it out to the file)

        public List<Order> LoadOrders(DateTime orderDate)
        {
            string n = string.Format($"C:\\Data\\FlooringData\\Orders_{orderDate.ToString("MM'\'dd'\'yyyy")}.txt");
            List<Order> list = new List<Order>();
            bool orderDateExists = CheckOrderDate(orderDate);
            if (!orderDateExists)
                return list;
            using (StreamReader sr = new StreamReader(n))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Order newOrder = new Order();
                    string[] columns = line.Split(',');
                    newOrder.OrderNumber = int.Parse(columns[0]);
                    newOrder.CustomerName = columns[1].Replace('@',',');
                    newOrder.State = columns[2].ToUpper();
                    newOrder.TaxRate = decimal.Parse(columns[3]);
                    newOrder.ProductType = columns[4].ToUpper();
                    newOrder.Area = decimal.Parse(columns[5]);
                    newOrder.CostPerSqFt = decimal.Parse(columns[6]);
                    newOrder.LaborCostPerSqFt = decimal.Parse(columns[7]);
                    newOrder.MaterialCost = decimal.Parse(columns[8]);
                    newOrder.LaborCost = decimal.Parse(columns[9]);
                    newOrder.Tax = decimal.Parse(columns[10]);
                    newOrder.Total = decimal.Parse(columns[11]);
                    list.Add(newOrder);
                }
                return list;
            }
        }

        private bool CheckOrderDate(DateTime date)
        {
            string n = string.Format($"C:\\Data\\FlooringData\\Orders_{date.ToString("MM'\'dd'\'yyyy")}.txt");
            if (!File.Exists(n))
            {
                return false;
            }
            return true;
        }

        public void AddOrder(Order order)
        {
            bool orderDateExists = CheckOrderDate(order.Date);
            if (orderDateExists)
            {
                List<Order> tempList = LoadOrders(order.Date);
                tempList.Add(order);
                UploadFile(tempList, order.Date);
            }
            else
            {
                List<Order> newList = new List<Order>();
                newList.Add(order);
                UploadFile(newList, order.Date);
            }
        }

        private void UploadFile(List<Order> orders, DateTime date)
        {
            string n = string.Format($"C:\\Data\\FlooringData\\Orders_{date.ToString("MM'\'dd'\'yyyy")}.txt");
            if (File.Exists(n))
                File.Delete(n);
            using (StreamWriter sw = new StreamWriter(n))
            {
                sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                foreach (var o in orders)
                {
                    sw.WriteLine($"{o.OrderNumber},{o.CustomerName.Replace(',','@')},{o.State},{o.TaxRate.ToString("F2")},{o.ProductType},{o.Area},{o.CostPerSqFt.ToString("F2")},{o.LaborCostPerSqFt.ToString("F2")}," +
                        $"{o.MaterialCost.ToString("F2")},{o.LaborCost.ToString("F2")},{o.Tax.ToString("F2")},{o.Total.ToString("F2")}");
                }
            }
        }

        public void EditOrder(int orderNumber, Order newOrder)
        {
            List<Order> tempList = LoadOrders(newOrder.Date);
            int i = tempList.FindIndex(x => x.OrderNumber == orderNumber);
            tempList[i] = newOrder;
            UploadFile(tempList, newOrder.Date);
        }

        public void DeleteOrder(int orderNumber, DateTime date)
        {
            string n = string.Format($"C:\\Data\\FlooringData\\Orders_{date.ToString("MM'\'dd'\'yyyy")}.txt");
            List<Order> tempList = LoadOrders(date);
            int i = tempList.FindIndex(x => x.OrderNumber == orderNumber);
            tempList.RemoveAt(i);
            if (tempList.Count() == 0)
            {
                File.Delete(n);
            }
            UploadFile(tempList, date);
        }

        public Order LoadOrder(int orderNumber, DateTime date)
        {
            List<Order> tempList = LoadOrders(date);
            bool orderExists = tempList.Any(x => x.OrderNumber == orderNumber);
            int i = tempList.FindIndex(x => x.OrderNumber == orderNumber);
            if (orderExists)
                return tempList[i];
            else
                return null;
        }
    }
}


