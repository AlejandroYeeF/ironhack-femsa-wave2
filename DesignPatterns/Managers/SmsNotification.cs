using System;
using DesignPatterns.Interfaces;

namespace DesignPatterns.Managers
{
    public class SmsNotification : INotification
    {
        public Task SendAsync(string message)
        {
            return Task.Run(() =>
            {
                // Realizamos el envío de la notificación
                Thread.Sleep(1000);
                Console.WriteLine("Sending sms: " + message);
            });
        }
    }
}

