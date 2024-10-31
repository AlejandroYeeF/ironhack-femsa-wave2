using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolidPrinciplesProject.Models;

namespace SolidPrinciplesProject.Interfaces
{
    public interface INotification
    {
        void NotifyCustomer(Order order);
    }
}