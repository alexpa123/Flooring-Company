using Flooring.Data;
using Flooring.Models;
using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Data.Repositories;
using System.Text.RegularExpressions;

namespace Flooring.BLL
{
    public class OrderManager
    {
        private static IOrdersRepository _ordersRepository;
        public StateTaxManager taxManager = StateTaxManagerFactory.Create();
        public ProductManager productManager = ProductManagerFactory.Create();

        public OrderManager(IOrdersRepository repo)
        {
            _ordersRepository = repo;
        }

        public ValidateOrderResponse ValidateOrder(Order order, bool isEdit)
        {
            ValidateOrderResponse response = new ValidateOrderResponse();

            bool orderDateExists = OrderDateExists(order.Date);

            if (orderDateExists && !isEdit)
            {
                order.OrderNumber = _ordersRepository.LoadOrders(order.Date).Max(x => x.OrderNumber) + 1;
            }
            else if (!orderDateExists)
            {
                order.OrderNumber = 1;
            }
            else //order must be edit
            {
                order = EditValuesForEmptyStrings(response.Order, order);//set values to original order if empty string was entered
                if(order.Area == 0)
                {
                    order.Area = response.Order.Area;
                }
            }

            if (order.Area < 100)
            {
                response.Success = false;
                response.Message = "Error, a valid number was not entered or number was less than 100sqft.";
                return response;
            }

            if (order.Date < DateTime.Now)
            {
                response.Success = false;
                response.Message = "Order must be in the future";
                return response;
            }

            if (!productManager.CheckForProduct(order.ProductType))
            {
                response.Success = false;
                response.Message = "We do not carry this product at this time.";
                return response;
            }

            if (!IsStateServiced(order.State))
            {
                response.Success = false;
                response.Message = "We do not service this state at this time.";
                return response;
            }

            Regex allowCharacter = new Regex("^[0-9A-Za-z.,]*$");
            if (!allowCharacter.IsMatch(order.CustomerName))
            {
                response.Success = false;
                response.Message = "Name is only allowed to contain [a-z][0-9] as well as periods and comma characters.";
                return response;
            }

            var setTaxRate = GetListOfStateTaxes().Where(x => x.StateAbbreviation == order.State).Select(y => y.TaxRate);
            order.TaxRate = setTaxRate.First();

            var setCostPerSqFt = GetListOfAllProducts().Where(x => x.ProductType == order.ProductType).Select(y => y.CostPerSqFt);
            order.CostPerSqFt = setCostPerSqFt.FirstOrDefault();

            var setLaborCostPerSqFt = GetListOfAllProducts().Where(x => x.ProductType == order.ProductType).Select(y => y.LaborCostPerSqFt);
            order.LaborCostPerSqFt = setLaborCostPerSqFt.FirstOrDefault();
            SetPrice(order);

            response.Order = order;
            response.Success = true;
            return response;
        }

        public List<Order> RequestOrdersByDate(DateTime date)
        {
            return _ordersRepository.LoadOrders(date);
        }

        //public Order LoadOrder(int orderNumber, DateTime date)
        //{
        //    return _ordersRepository.LoadOrder(orderNumber, date);
        //}

        public void AddOrder(Order order)//editing parameters and return type for training mode 3:54 6/1/18
        {
             _ordersRepository.AddOrder(order);
        }

        public void EditOrder(int orderNumber, Order newOrder)
        {
             _ordersRepository.EditOrder(orderNumber, newOrder);
        }

        public void DeleteOrder(int orderNumber, DateTime date)
        {
             _ordersRepository.DeleteOrder(orderNumber, date);
        }

        public bool OrderDateExists(DateTime date)
        {
            if(_ordersRepository.LoadOrders(date).Count() == 0)
            {
                return false;
            }
            return true;
        }

        public bool CheckIfOrderNumberExists(DateTime date, int orderNumber)
        {
            return _ordersRepository.LoadOrders(date).Any(x => x.OrderNumber == orderNumber);
        }

        public Order SetPrice(Order order)
        {
            order.MaterialCost = order.Area * order.CostPerSqFt;
            order.LaborCost = order.Area * order.LaborCostPerSqFt;
            order.Tax = (order.MaterialCost + order.LaborCost) * (order.TaxRate / 100);
            order.Total = order.LaborCost + order.LaborCostPerSqFt + order.MaterialCost + order.Tax;
            return order;
        }

        public List<StateTax> GetListOfStateTaxes()
        {
            return taxManager.RequestListOfStateTaxes();
        }

        public List<Products> GetListOfAllProducts()
        {
            return productManager.GetListOfAllProducts();
        }

        public bool IsStateServiced(string state)
        {
            return taxManager.RequestListOfStateTaxes().Any(x => x.StateAbbreviation == state);
        }

        public Order EditValuesForEmptyStrings(Order original, Order orderToEdit)
        {
            if (orderToEdit.CustomerName == "")
            {
                orderToEdit.CustomerName = original.CustomerName;
            }
            if (orderToEdit.State == "")
            {
                orderToEdit.State = original.State.ToUpper();
            }
            if (orderToEdit.ProductType == "")
            {
                orderToEdit.ProductType = original.ProductType.ToUpper(); ;
            }
            return orderToEdit;
        }
    }
}
