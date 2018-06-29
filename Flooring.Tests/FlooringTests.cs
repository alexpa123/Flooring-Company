using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.BLL;
using Flooring.Models;
using NUnit.Framework;

namespace Flooring.Tests
{
    [TestFixture]
    public class FlooringTests
    {
        [TestCase("01/01/2019", "test", "OH", "Carpet", "90", 90, false)]//area < 100
        [TestCase("01/01/2019", "test", "FL", "Carpet", "250", 250, false)]//out of state
        [TestCase("01/01/2017", "test", "OH", "Carpet", "250", 250, false)]//date in the past
        [TestCase("01/01/2020", "test", "OH", "Carpet", "250", 250, true)]
        public void AddOrderTest(DateTime date, string customerName, string state, string productType, 
                                 string areaString, decimal area, bool expectedResult)
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order testOrder = new Order()
            {
                Date = date,
                CustomerName = customerName,
                State = state.ToUpper(),
                ProductType = productType.ToUpper(),
                AreaString = areaString,
                Area = area
            };
            bool notAnEdit = false;
            ValidateOrderResponse response = manager.ValidateOrder(testOrder, notAnEdit);
            manager.AddOrder(testOrder);

            Assert.AreEqual(expectedResult, response.Success);
        }


        [TestCase("01/01/2020", 1, "editName", "OR", "Carpet", "250", 250, false)]//out of state
        [TestCase("01/01/2020", 1, "editName", "OH", "Carpet", "250", 250, true)]
        [TestCase("01/01/2017", 1, "editName", "OH", "Carpet", "250", 250, false)]//past date
        [TestCase("01/01/2020", 8, "editName", "OH", "Carpet", "250", 250, false)]//order number doesn't exist
        public void EditOrderTest(DateTime editDate, int orderNumber, string editName, string editState, 
                                  string editProductType, string areaString, decimal area, bool expectedResult)
        {
            OrderManager manager = OrderManagerFactory.Create();
           
            Order orderToEdit = new Order()
            {
                Date = editDate,
                OrderNumber = orderNumber,
                CustomerName = editName,
                State = editState.ToUpper(),
                ProductType = editProductType.ToUpper(),
                AreaString = areaString,
                Area = area
            };
            bool isEdit = true;
            ValidateOrderResponse response = manager.ValidateOrder(orderToEdit, isEdit);
            manager.EditOrder(orderNumber, orderToEdit);

            Assert.AreEqual(expectedResult, response.Success);
        }


        [TestCase(1, "01/01/2020", true)]
        [TestCase(2, "01/01/2021", false)]//no orders on this date
        [TestCase(5, "01/01/2021", false)]//order number doesn't exists
        [TestCase(2, "01/01/2025", false)]//no orders on this date
        public void DeleteOrderTest(int orderNumber, DateTime date, bool expectedResult)
        {
            OrderManager manager = OrderManagerFactory.Create();
            ValidateOrderResponse response = new ValidateOrderResponse();
            manager.DeleteOrder(orderNumber, date);

            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}