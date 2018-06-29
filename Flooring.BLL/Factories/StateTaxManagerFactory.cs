using Flooring.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL
{
    public class StateTaxManagerFactory
    {
        public static StateTaxManager Create()
        {
            //we added a reference to System.Configuration has a configuration manager class that 
            //reads your app config files
            string[] mode = ConfigurationManager.AppSettings["Mode"].Split(',');

            while (true)
            {
                for (int i = 0; i < mode.Length; i++)
                {
                    if (mode[i] == "StateTaxRepository")
                    {
                        return new StateTaxManager(new StateTaxRepository());
                    }
                }
            }


        }
    }
}
