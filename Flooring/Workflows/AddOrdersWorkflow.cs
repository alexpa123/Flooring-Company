using Flooring.BLL;
using Flooring.Models;
using System;

namespace Flooring
{
    public class AddOrdersWorkflow
    {
        public void Execute()
        {
            ConsoleIO consoleIO = new ConsoleIO();
            OrderManager manager = OrderManagerFactory.Create();
            consoleIO.DisplayMessage("Add Order");
            DateTime orderDate = consoleIO.GetOrderDate();
            Order newOrder = new Order();
            newOrder.Date = orderDate;

            bool notAnEdit = false;
            newOrder.CustomerName = consoleIO.GetCustomerName(notAnEdit);
            consoleIO.DisplayStatesCurrentlyServiced(manager.GetListOfStateTaxes());
            newOrder.State = consoleIO.GetCustomerState();
            consoleIO.DisplayProductTypes(manager.GetListOfAllProducts());
            newOrder.ProductType = consoleIO.GetProductFromCustomer();
            newOrder.Area = consoleIO.GetAreaFromCustomer(notAnEdit);
            ValidateOrderResponse response = manager.ValidateOrder(newOrder, notAnEdit);
            if (response.Success)
            {
                consoleIO.DisplayOrderDetails(newOrder);
                bool placeOrder = consoleIO.ReturnYesOrNo("Would you like to add this order?");
                if (placeOrder)
                {
                    manager.AddOrder(response.Order);
                    consoleIO.DisplayOrderReponseMessage(response);
                }
                else
                {
                    consoleIO.DisplayMessage("Order has been cancelled");
                }
            }
            else //response.success = false, displays a different message - order did not meet requirements
            {
                consoleIO.DisplayOrderReponseMessage(response);
            }
            consoleIO.PressAnyKeyToContinue();
        }
    }
}
