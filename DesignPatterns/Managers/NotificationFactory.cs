using System;
using DesignPatterns.Interfaces;

namespace DesignPatterns.Managers
{
    public class NotificationFactory
    {
        public static INotification SendNotification(string type)
        {
            switch (type)
            {
                case "email":
                    return new EmailNotification();
                case "sms":
                    return new SmsNotification();
                case "whatsapp":
                    return new WhatsAppNotification();
                default:
                    throw new Exception("Tipo de notificación inválido");
            }
        }
    }
}

