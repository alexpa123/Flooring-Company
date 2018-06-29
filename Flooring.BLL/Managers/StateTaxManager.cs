using Flooring.Models;
using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL
{
    public class StateTaxManager
    {
        IStateTaxes _StateTaxRepository;

        public StateTaxManager(IStateTaxes repo)
        {
            _StateTaxRepository = repo;
        }

        public List<StateTax> RequestListOfStateTaxes()
        {
            return _StateTaxRepository.ListOfStateTaxes();
        }

        
    }
}
