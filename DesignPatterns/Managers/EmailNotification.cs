using System;
using DesignPatterns.Interfaces;

namespace DesignPatterns.Managers
{
    public class EmailNotification : INotification
    {
        public Task SendAsync(string message)
        {
            return Task.Run(() =>
            {
                // Realizamos el envío de la notificación
                Console.WriteLine("Sending email: " + message);
            });
        }
    }
}

