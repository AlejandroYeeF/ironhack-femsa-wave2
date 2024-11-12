using System;
namespace DesignPatterns.Interfaces
{
	public interface INotification
	{
        Task SendAsync(string message);
    }
}

