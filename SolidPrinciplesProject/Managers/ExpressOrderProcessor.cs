using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolidPrinciplesProject.Interfaces;
using SolidPrinciplesProject.Models;

namespace SolidPrinciplesProject.Managers
{
    public class ExpressOrderProcessor : OrderProcessor
    {
        public ExpressOrderProcessor(IPayment payment, IInventory inventory, INotification notification) 
            : base(payment, inventory, notification)
        {
        }

        protected override bool ProcessPayment(Order order)
        {
            return _payment.ProcessPayment(order.Amount, "express");
        }
    }
}