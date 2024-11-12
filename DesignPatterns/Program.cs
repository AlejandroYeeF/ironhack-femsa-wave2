// See https://aka.ms/new-console-template for more information
using DesignPatterns.Interfaces;
using DesignPatterns.Managers;

// Así podemos probar SINGLETON PATTERN,
// con la lógica dentro de GetInstance de la clase GlobalConfigManager nos aseguramos que solo existirá una única instancia.
var config = GlobalConfigManager.GetInstance();
Console.WriteLine(config.ApplicationName);

// Hacemos uso de FACTORY METHOD
// De esta manera podemos crear objetos basados en el canal que se requiera
// Incluso después podemos agregar una canal más como Push notifications con la misma interfaz y agregar el comportamiento específico
INotification smsNotification = NotificationFactory.SendNotification("sms");
INotification emailNotification = NotificationFactory.SendNotification("email");
INotification whatsappNotification = NotificationFactory.SendNotification("whatsapp");

// OBSERVER PATTERN
// Instancia de NotificationManager, que gestionará el envío de notificaciones a los suscriptores
NotificationManager notificationManager = new NotificationManager();

// Creamos instancias de usuarios que recibirán notificaciones
// Sólo suscribimos los primeros dos, por lo que DoesNothing, al no estar suscrito, no recibe notificación
UserManager firstUser = new UserManager("Alejandro");
UserManager secondUser = new UserManager("David");
UserManager thirdUser = new UserManager("DoesNothing"); // Usuario no suscrito

// Aquí suscribimos usuarios para recibir notificaciones a NotificationManager que es quien envía las notificaciones.
// De tal manera que cuando enviamos la notificación asíncrona, le llegará el mensaje a los usuarios suscritos (firstUser y secondUser).
notificationManager.NotificationSent += firstUser.ReceiveNotification;
notificationManager.NotificationSent += secondUser.ReceiveNotification;

//ASYNCHRONOUS METHOD
// Aquí realizamos el envío de la notificación sin esperar el resultado a los usuarios suscritos.
// El hilo principal no se bloquea y puede continuar ejecutando los siguientes pasos.
notificationManager.SendNotificationAsync(smsNotification, "This is a sms notification");
notificationManager.SendNotificationAsync(emailNotification, "This is a email notification");
notificationManager.SendNotificationAsync(whatsappNotification, "This is a whatsapp notification");

Console.WriteLine("Notifications are being sent asynchronously");
Console.WriteLine("Main thread is not blocked and continues execution");

Console.ReadKey();