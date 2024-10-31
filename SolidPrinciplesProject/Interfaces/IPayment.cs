using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolidPrinciplesProject.Interfaces
{
    public interface IPayment
    {
        bool ProcessPayment(double amount, string priority);
    }
}