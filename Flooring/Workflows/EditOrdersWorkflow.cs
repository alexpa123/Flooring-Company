using Flooring.BLL;
using Flooring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Workflows
{
    public class EditOrdersWorkflow
    {
        public void Execute()
        {
            ConsoleIO consoleIO = new ConsoleIO();
            OrderManager manager = OrderManagerFactory.Create();
            consoleIO.DisplayMessage("Edit Order");
            DateTime orderDate = consoleIO.GetOrderDate();
            bool orderDateExists = manager.OrderDateExists(orderDate);
            if (orderDateExists)
            {
                List<Order> tempList = manager.RequestOrdersByDate(orderDate);
                consoleIO.DisplayOrders(tempList);
                int orderNumber = consoleIO.GetOrderNumberTo("edit.");
                bool orderNumberExists = manager.CheckIfOrderNumberExists(orderDate, orderNumber);
                Order editedOrder = new Order();
                if (orderNumberExists)
                {
                    bool isAnEdit = true;
                    editedOrder.OrderNumber = orderNumber;
                    editedOrder.Date = orderDate;
                    editedOrder.CustomerName = consoleIO.GetCustomerName(isAnEdit);
                    consoleIO.DisplayStatesCurrentlyServiced(manager.GetListOfStateTaxes());
                    editedOrder.State = consoleIO.GetCustomerState();
                    consoleIO.DisplayProductTypes(manager.GetListOfAllProducts());
                    editedOrder.ProductType = consoleIO.GetProductFromCustomer();
                    editedOrder.Area = consoleIO.GetAreaFromCustomer(isAnEdit);
                    ValidateOrderResponse response = manager.ValidateOrder(editedOrder, isAnEdit);
                    if (response.Success)
                    {
                        bool placeOrder = consoleIO.ConfirmOrderPlacement(editedOrder);
                        if (placeOrder)
                        {
                            manager.EditOrder(orderNumber, editedOrder);
                            consoleIO.DisplayOrderReponseMessage(response);
                        }
                        else
                        {
                            consoleIO.DisplayOrderReponseMessage(response);
                        }
                    }
                    else //response.success = false, displays a different message - order did not meet requirements
                    {
                        consoleIO.DisplayOrderReponseMessage(response);
                    }
                }
                else
                {
                    consoleIO.DisplayMessage("Order number was not found.");
                }
            }
            else
            {
                consoleIO.DisplayMessage("Order date was not found.");
            }
            consoleIO.PressAnyKeyToContinue();
        }
    }
}
