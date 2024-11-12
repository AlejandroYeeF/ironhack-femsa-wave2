using System;
using DesignPatterns.Interfaces;

namespace DesignPatterns.Managers
{
	public class UserManager
	{
        public string Username { get; set; }

		public UserManager(string username)
		{
            Username = username;
		}

        public void ReceiveNotification(object sender, string message)
		{
			Console.WriteLine($"{Username} received notification: {message}");
		}
    }
}

