using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolidPrinciplesProject.Interfaces;
using SolidPrinciplesProject.Models;

namespace SolidPrinciplesProject.Managers
{
    public class SystemManager
    {
        private IInventory _inventory { get; set; }
        private IPayment _payment { get; set; }
        private INotification _notification { get; set; }
        public SystemManager(IInventory inventory, IPayment payment, INotification notification)
        {
            _inventory = inventory;
            _payment = payment;
            _notification = notification;
        }
        public void ProcessOrder(Order order)
        {
            IPayment paymentType = order.Type == "express" ? new ExpressPayment() : new StandardPayment();
            OrderProcessor orderProcessor = order.Type == "express" 
                ? new ExpressOrderProcessor(paymentType, _inventory, _notification) 
                : new StandardOrderProcessor(paymentType, _inventory, _notification);

            orderProcessor.ProcessOrder(order);
        }
    }
}