using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolidPrinciplesProject.Interfaces;
using SolidPrinciplesProject.Models;

namespace SolidPrinciplesProject.Managers
{
    public class InventoryManager : IInventory
    {
        public bool HaveInventory(Order order)
        {
            // Compare quantity ordered to quantity in stock
            if (order.Quantity > 10)
            {
                return false;
            }

            return true;
        }
    }
}