using Flooring.BLL;
using Flooring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Workflows
{
    public class DeleteOrdersWorkflow
    {
        public void Execute()
        {
            ConsoleIO consoleIO = new ConsoleIO();
            OrderManager manager = OrderManagerFactory.Create();
            consoleIO.DisplayMessage("Delete Order");
            DateTime orderDate = consoleIO.GetOrderDate();
            bool orderDateExists = manager.OrderDateExists(orderDate);
            if (orderDateExists)
            {
                List<Order> tempList = manager.RequestOrdersByDate(orderDate);
                consoleIO.DisplayOrders(tempList);
                int orderNumber = consoleIO.GetOrderNumberTo("delete.");
                bool orderNumberExists = manager.CheckIfOrderNumberExists(orderDate, orderNumber);
                if (orderNumberExists)
                {
                    bool placeOrder = consoleIO.ReturnYesOrNo("Enter Y/N to delete order.");
                    if (placeOrder)
                    {
                       ValidateOrderResponse response = new ValidateOrderResponse();
                       manager.DeleteOrder(orderNumber, orderDate);
                       consoleIO.DisplayOrderReponseMessage(response);
                    }
                    else
                    {
                        consoleIO.DisplayMessage($"Your order is still in place for {orderDate}");
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
