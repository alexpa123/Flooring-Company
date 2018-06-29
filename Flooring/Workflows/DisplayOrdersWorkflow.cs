using Flooring.BLL;
using Flooring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Workflows
{
    public class DisplayOrdersWorkflow
    {
        public void Execute()
        {
            ConsoleIO consoleIO = new ConsoleIO();
            OrderManager manager = OrderManagerFactory.Create();
            consoleIO.DisplayMessage("Display Orders");
            DateTime orderDate = consoleIO.GetOrderDate();
            bool orderDateExists = manager.OrderDateExists(orderDate);
            if (orderDateExists)
            {
                List<Order> ordersByDate = manager.RequestOrdersByDate(orderDate);
                consoleIO.DisplayOrders(ordersByDate);
            }
            else
            {
                consoleIO.DisplayMessage("Order date was not found.");
            }
            consoleIO.PressAnyKeyToContinue();
        } 
    }
}
