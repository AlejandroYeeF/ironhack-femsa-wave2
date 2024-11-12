using System;
using DesignPatterns.Interfaces;

namespace DesignPatterns.Managers
{
	public class NotificationManager
	{
		public delegate void NotificationSentEventHandler(object sender, string message);

		public event NotificationSentEventHandler? NotificationSent;

		public void SendNotificationAsync(INotification notification, string message)
		{
			var task = notification.SendAsync(message);

			task.ContinueWith(t =>
			{
                NotifySubscriber(message);
			});
		}

		protected virtual void NotifySubscriber(string message)
		{
			if (NotificationSent != null)
			{
				NotificationSent(this, message);
			}
		}
    }
}

