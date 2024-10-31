using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolidPrinciplesProject.Interfaces;
using SolidPrinciplesProject.Models;

namespace SolidPrinciplesProject.Interfaces
{
    public abstract class OrderProcessor
    {
        protected IPayment _payment;
        protected IInventory _inventory;
        protected INotification _notification;

        protected OrderProcessor(IPayment payment, IInventory inventory, INotification notification)
        {
            _payment = payment;
            _inventory = inventory;
            _notification = notification;
        }
        
        public void ProcessOrder(Order order)
        {
            if (!_inventory.HaveInventory(order))
            {
                Console.WriteLine($"Order {order.Id} was not processed.");
                return;
            }
            if (!ProcessPayment(order))
            {
                Console.WriteLine($"Order {order.Id} was not processed.");
                return;
            }
            UpdateOrderStatus(order);
            _notification.NotifyCustomer(order);
        }

        protected abstract bool ProcessPayment(Order order);

        private void UpdateOrderStatus(Order order)
        {
            Console.WriteLine($"Order {order.Id} was processed.");
        }
    }
}