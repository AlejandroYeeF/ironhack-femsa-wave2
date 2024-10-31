using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolidPrinciplesProject.Interfaces;
using SolidPrinciplesProject.Models;

namespace SolidPrinciplesProject.Managers
{
    public class NotificationManager : INotification
    {
        public void NotifyCustomer(Order order)
        {
            Console.WriteLine($"Email sent to customer for order: {order.Id}");
        }
    }
}