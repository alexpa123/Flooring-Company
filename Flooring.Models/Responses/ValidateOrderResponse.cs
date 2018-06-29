using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models
{
    public class ValidateOrderResponse
    {
        public Order Order { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
