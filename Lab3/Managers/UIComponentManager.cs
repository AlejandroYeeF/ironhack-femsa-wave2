using System;
using Lab3.Interfaces;

namespace Lab3.Managers
{
	public class UIComponentManager : IUIComponent
	{
        private int currentWidth;

		public UIComponentManager(int initialWidth)
		{
            currentWidth = initialWidth;
		}

        public bool AdjustsToScreenSize(int width)
        {
            if (width <= 0)
            {
                return false;
            }

            currentWidth = width;
            return true;
        }

        public int GetCurrentWidth()
        {
            return currentWidth;
        }
    }
}

