using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Flooring.Data;
using Flooring.Data.Repositories;

namespace Flooring.BLL
{
    public class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            //we added a reference to System.Configuration has a configuration manager class that 
            //reads your app config files
            string[] mode = ConfigurationManager.AppSettings["Mode"].Split(',');

            while (true)
            {
                for (int i = 0; i < mode.Length; i++)
                {
                    if(mode[i] == "CustomerOrdersRepository")
                    {
                        return new OrderManager(new CustomerOrdersRepository());
                    }
                    if (mode[i] == "TestRepository")
                    {
                        return new OrderManager(new TestOrdersRepository());
                    }
                }
            }


        }
    }
}
