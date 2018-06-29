using Flooring.Data.Helpers;
using Flooring.Models;
using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data
{
    public class StateTaxRepository : IStateTaxes
    {
        //public static List<StateTax> statesList = List(Settings.statesFilePath);

        public List<StateTax> ListOfStateTaxes()
        {
            List<StateTax> states = new List<StateTax>();
            using (StreamReader sr = new StreamReader(Settings.statesFilePath))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    StateTax state = new StateTax();

                    string[] columns = line.Split(',');
                    state.StateAbbreviation = columns[0].ToUpper();
                    state.StateName = columns[1].ToUpper();
                    state.TaxRate = decimal.Parse(columns[2]);
                    states.Add(state);
                }
                return states;
            }
        }
    }
}
