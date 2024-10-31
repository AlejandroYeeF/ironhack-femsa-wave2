# Violaciones de los principios SOLID en el código original

## Codigo original
```csharp
class SystemManager {
    processOrder(order) {
        if (order.type == "standard") {
            verifyInventory(order);
            processStandardPayment(order);
        } else if (order.type == "express") {
            verifyInventory(order);
            processExpressPayment(order, "highPriority");
        }
        updateOrderStatus(order, "processed");
        notifyCustomer(order);
    }

    verifyInventory(order) {
        // Checks inventory levels
        if (inventory < order.quantity) {
            throw new Error("Out of stock");
        }
    }

    processStandardPayment(order) {
        // Handles standard payment processing
        if (paymentService.process(order.amount)) {
            return true;
        } else {
            throw new Error("Payment failed");
        }
    }

    processExpressPayment(order, priority) {
        // Handles express payment processing
        if (expressPaymentService.process(order.amount, priority)) {
            return true;
        } else {
            throw new Error("Express payment failed");
        }
    }

    updateOrderStatus(order, status) {
        // Updates the order status in the database
        database.updateOrderStatus(order.id, status);
    }

    notifyCustomer(order) {
        // Sends an email notification to the customer
        emailService.sendEmail(order.customerEmail, "Your order has been processed.");
    }
}

```

## Violaciones en el código original

### Single responsability principle
- La clase SystemManager maneja toda la lógica y tiene toda la responsabilidad, se recomienda hacer más clases para separar responsabilidades.

### Open/Closed principle
- El método ProcessOrder tiene toda la lógica para manejar distintos tipos de ordenes y si salen nuevos tipos de ordenes tenemos que modificar el método y estaríamos incumpliendo este principio, ya que debe estar cerrado para su modificación y abierto para extender.

### Liskov substitution principle
- El código original no aplica ningún tipo de interfaz o clase abstracta, por lo que no sigue el principio. Se requiere hacer uso de interfaces y/o clases abstractas para darle diferentes comportamiento desde las clases derivadas.

### Interface segregation principle
- No hay interfaces, todo el código se trabaja en una misma clase SystemManager. Se requiere hacer uso de interfaces sin forzarnos a usar una única interfaz.

### Dependency inversion principle
- La clase que tiene toda la lógica SystemManager depende de lo que está implementado en los métodos de la misma clase. Este principio nos dice que necesitamos depender de interfaces o abstracciones, solo de contratos y no de implementaciones concretas. Y que podemos inyectar diferentes comportamientos a través de una misma interfaz como contrato.




# Refactorización aplicando SOLID

En esta sección vamos a explicar cómo le dimos solución al flujo implementando SOLID

Creamos 3 folders para ordenar un poco, 
- Interfaces: para manejar interfaces y clase abstracta y tener contratos. 
- Managers: para implementar comportamientos (manejar lógica) de los contratos de las abstracciones.
- Models: para la clase Order.

## Aplicación de SOLID

### Single responsability principle
- Tenemos diferentes clases que tienen sus propias responsabilidades. Tenemos inventario, notificacione, pagos y procesamiento de ordenes.

### Open/Closed principle
- Creamos la clase abstracta OrderProcessor que tiene su lógica y está abierta para ser extendida por si queremos agregar nuevos tipos ordenes (nuevas clases como StandardOrderProcessor y ExpressOrderProcessor), por lo que podríamos tener un manejo de procesamiento de órdenes premium heredando de OrderProcessor, por ejemplo. Y no necesitamos modificar OrderProcessor.

### Liskov substitution principle
- Este principio también lo aplicamos en OrderProcessor, porque podemos usar cualquiera de sus derivadas para ejecutar una lógica u otra, ya que cumplimos con el contrato de la clase abstracta. En SystemManager usamos el método ProcessOrder de OrderProcessor y no hace falta cambiarlo, porque este es una instancia de cualquiera de sus clases derivadas, que tiene un diferente comportamiento.

### Interface segregation principle
- Con esta refactorización creamos 3 interfaces (IInventory, INotification, IPayment), si fuese necesario podríamos crear más interfaces. No tenemos una sola interfaz obligándonos a usar una sola. Bien podríamos crear más interfaces si fuese necesario.

### Dependency inversion principle
- La clase principal SystemManager depende de abstracciones, ahí podemos tener diferentes comportamientos en función del tipo de orden que se trate. IPayment paymentType puede ser cualquiera de las clases que heredan de la interfaz en función del tipo de pago del contexto. Y OrderProcessor orderProcessor puede tener inyectado el tipo de pago que requiere la orden.