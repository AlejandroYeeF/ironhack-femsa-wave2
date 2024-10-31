using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolidPrinciplesProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public string CustomerEmail { get; set; }

    }
}