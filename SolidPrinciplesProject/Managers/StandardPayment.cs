using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolidPrinciplesProject.Interfaces;

namespace SolidPrinciplesProject.Managers
{
    public class StandardPayment : IPayment
    {
        public bool ProcessPayment(double amount, string priority)
        {
            // Logic to process standard payment
            return true;
        }
    }
}