## Implementing and Optimizing Database Queries

#### Optimización de consultas SQL
**Consulta de pedidos**: recupera pedidos con muchos artículos y calcula el precio total
```
SELECT Orders.OrderID, SUM(OrderDetails.Quantity * OrderDetails.UnitPrice) AS TotalPrice
FROM Orders
JOIN OrderDetails ON Orders.OrderID = OrderDetails.OrderID
WHERE OrderDetails.Quantity > 10
GROUP BY Orders.OrderID;
```
* No se ve que haya indices para optimizar las consultas, primero necesitamos identificar cuales son las columnas que se requiere indexar, las columnas importantes son:
    * OrderId y Quantity de la tabla OrderDetails y OrderId de Orders, las 3 son necesarias para optimizar el join y el where.
* Otro error que podemos encontrar es el where al final, es recomendable aprovechar a filtrar lo antes posible, para resumir los resultados que nos interesan.
    * Por lo que podríamos hacer un Select a OrderDetails para obtener solo las órdenes que tienen más de 10 articulos, el resultado de esta subconsulta, es como una tabla nueva con registros relevantes, a la que podemos hacer join con Orders donde empaten OrderId de una tabla con la otra. Ejemplo:
    ```
    SELECT Orders.OrderID, SUM(OrderDetails.Quantity * OrderDetails.UnitPrice) AS TotalPrice
    FROM Orders o
    JOIN (
        SELECT OrderId, Quantity, UnitPrice
        FROM OrderDetails
        WHERE Quantity > 10
    ) AS ResumeDetails ON o.OrderId = ResumeDetails.OrderId
    GROUP BY Orders.OrderID;
    ```


**Consulta de cliente**: encuentre todos los clientes de Londres y ordene por nombre de cliente.
```
SELECT CustomerName FROM Customers WHERE City = 'London' ORDER BY CustomerName;
```
* En este ejercicio, analizando el query necesitamos agregar indices también para optimizar la consulta, podemos ver que los campos importantes son City y CustomerName.
* No hace falta modificar la consulta porque no hay joins ni condiciones complejas, pero el uso de indices sobre estos campos nos ayudará a encontrar el resultado más rápido.

#### NoSQL Query Implementation
**Consulta de publicaciones de usuario**: recupera las publicaciones activas más populares y muestra su título y número de “Me gusta”.

```
db.posts
  .find({ status: "active" }, { title: 1, likes: 1 })
  .sort({ likes: -1 });
```
* En principio necesitamos indexar los campos 'status' (que nos ayuda a reducir la cantidad de resultados con una condición, status 'active') y 'likes' para hacer la ordenación por más likes.
* Estos indices nos ayudarán a conseguir los registros más rápido en este query.

**Agregación de datos de usuario**: resuma la cantidad de usuarios activos por ubicación.

```
db.users.aggregate([
  { $match: { status: "active" } },
  { $group: { _id: "$location", totalUsers: { $sum: 1 } } },
]);
```
* En este ejercicio tampoco vemos señales de que haya indices en la tabla users. Por lo que podemos asumir que la consulta, no está optimizada de la mejor manera.
* Necesitamos crear como indices los campos status y location, que son los usados para condicionar y agrupar.