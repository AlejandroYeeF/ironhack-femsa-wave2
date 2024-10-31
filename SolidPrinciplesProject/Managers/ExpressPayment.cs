using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolidPrinciplesProject.Interfaces;

namespace SolidPrinciplesProject.Managers
{
    public class ExpressPayment : IPayment
    {
        public bool ProcessPayment(double amount, string priority)
        {
            // Logic to process express payment
            return true;
        }
    }
}