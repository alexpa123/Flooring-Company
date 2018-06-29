using Flooring.BLL;
using Flooring.Data;
using Flooring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring
{
    public class ConsoleIO
    {
        public OrderManager orderManager = OrderManagerFactory.Create();
        public string seperator = "==================================";


        public DateTime GetOrderDate()
        {
            Console.WriteLine(seperator);
            while (true)
            {
                string stringDate = "";
                DateTime orderDate;
                Console.WriteLine("Please enter a date for your order. mm/dd/yyyy");
                stringDate = Console.ReadLine();
                if (DateTime.TryParse(stringDate, out orderDate) && orderDate > DateTime.Now)
                {
                    return orderDate;
                }
            }
        }

        public void DisplayStatesCurrentlyServiced(List<StateTax> states)
        {
            Console.WriteLine(seperator);
            Console.WriteLine("States we currently service.");
            foreach (var item in states)
            {
                Console.WriteLine($"{item.StateAbbreviation}");
            }
        }

        public int GetOrderNumberTo(string editordelete)
        {
            string input = "";
            int orderNumber = 0;
            while (true)
            {
                Console.WriteLine($"Please enter the order number you wish to {editordelete}.");
                input = Console.ReadLine();
                if (int.TryParse(input, out orderNumber) && orderNumber > 0)
                {
                    return orderNumber;
                }
            }
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine(seperator);
        }

        public void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }

        public string GetCustomerName(bool isEdit)
        {
            Console.WriteLine(seperator);
            string name = "";
            if (isEdit)
            {
                Console.WriteLine("Please enter your name.");
                name = Console.ReadLine();
                return name;
            }
            while (true)
            {
                Console.WriteLine("Please enter your name.");
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    return name;
                }
            }
        }

        public void DisplayStateNotServiced()
        {
            Console.WriteLine(seperator);
            Console.WriteLine("We do not currently service this state.");
        }

        public bool ReturnYesOrNo(string message)
        {
            Console.WriteLine(message);
            while (true)
            {
                string answer = Console.ReadLine().ToUpper();
                if (answer == "Y")
                {
                    return true;
                }
                else if (answer == "N")
                {
                    return false;
                }
                Console.WriteLine("Please enter Y or N as yes or no.");
            }
        }

        public bool ConfirmOrderPlacement(Order order)
        {
            Console.WriteLine(seperator);
            orderManager.SetPrice(order);
            DisplayOrderDetails(order);
            return ReturnYesOrNo("Would you like to place this order?");
        }

        public string GetCustomerState()
        {
            Console.WriteLine(seperator);
            Console.WriteLine("Please enter your abbreviated state. Ex. KY");
            string state = Console.ReadLine().ToUpper();
            return state;
        }

        public void DisplayOrderReponseMessage(ValidateOrderResponse response)
        {
            Console.WriteLine($"{response.Message}");
        }

        //here we will need to do a linq query to list the products
        public void DisplayProductTypes(List<Products> products)
        {
            Console.WriteLine(seperator);
            foreach (var item in products)
            {
                Console.WriteLine($"Product : {item.ProductType} \n Cost per sq ft : {item.CostPerSqFt} \n Labor cost per sq ft : {item.LaborCostPerSqFt}");
                Console.WriteLine(seperator);
            }
        }

        public void DisplayOrders(List<Order> orders)
        {
            Console.WriteLine(seperator);
            foreach (var order in orders)
            {
                DisplayOrderDetails(order);
            }
        }

        public void DisplayOrderDetails(Order order)
        {
            Console.WriteLine(seperator);
            Console.WriteLine($"Order number: {order.OrderNumber} \nname: {order.CustomerName}, \nstate: {order.State}, " +
                $"\nproduct: {order.ProductType},\narea: {order.Area},\ncost per sq ft: {order.CostPerSqFt.ToString("F2")}, " +
                $"\nlabor cost per sq ft: {order.LaborCostPerSqFt.ToString("F2")},\nmaterial cost: {order.MaterialCost.ToString("F2")}" +
                $"\nlabor cost: {order.LaborCost.ToString("F2")}, \ntax: {order.Tax.ToString("F2")}, \ntotal: {order.Total.ToString("F2")}");
            Console.WriteLine(seperator);
        }

        public string GetProductFromCustomer()
        {
            Console.WriteLine(seperator);
            while (true)
            {
                string currentProduct = "";
                Console.WriteLine("Please enter a product of your choice.");
                currentProduct = Console.ReadLine().ToUpper();
                return currentProduct;
            }
        }

        public decimal GetAreaFromCustomer(bool isEdit)
        {
            decimal area = 0;
            string areaString = "";
            Console.WriteLine(seperator);
            while (isEdit)
            {
                Console.WriteLine("Please enter a number over 100 to edit your order. Hit enter to keep the size of your original order.");
                areaString = Console.ReadLine();
                if(areaString == "")
                {
                    return area;
                }
                else if(decimal.TryParse(areaString, out area))
                {
                    return area;
                }
                else
                {
                    Console.WriteLine("That is not a valid number. Please try again.");
                }
            }

            while (!isEdit)
            {
                Console.WriteLine("Please enter a number. 100 sq ft is the minimum at this time.");
                areaString = Console.ReadLine();
                if (decimal.TryParse(areaString, out area))
                {
                    return area;
                }
            }
            return area;
        }


        public static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Pacheco's Flooring Shop");
            Console.WriteLine();
            Console.WriteLine("1. Display Orders");
            Console.WriteLine("2. Add an Order");
            Console.WriteLine("3. Edit an Order");
            Console.WriteLine("4. Delete an Order");

            Console.WriteLine("\nQ  Quit");
        }
    }
}
