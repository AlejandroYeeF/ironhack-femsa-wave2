using System;
namespace Lab3.Interfaces
{
	public interface IUIComponent
	{
		bool AdjustsToScreenSize(int width);
		int GetCurrentWidth();
    }
}

