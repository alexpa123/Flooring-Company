using Flooring.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring
{
    public static class Menu
    {
        public static void Start()
        {
            while (true)
            {
                ConsoleIO.DisplayMenu();
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        DisplayOrdersWorkflow displayOrdersWorkflow = new DisplayOrdersWorkflow();
                        displayOrdersWorkflow.Execute();
                        break;
                    case "2":
                        AddOrdersWorkflow addOrdersWorkflow = new AddOrdersWorkflow();
                        addOrdersWorkflow.Execute();
                        break;
                    case "3":
                        EditOrdersWorkflow editOrdersWorkflow = new EditOrdersWorkflow();
                        editOrdersWorkflow.Execute();
                        break;
                    case "4":
                        DeleteOrdersWorkflow deleteOrdersWorkflow = new DeleteOrdersWorkflow();
                        deleteOrdersWorkflow.Execute();
                        break;
                    case "Q":
                    case "q":
                        return;
                    default:
                        break;
                }
            }
        }

    }
}
