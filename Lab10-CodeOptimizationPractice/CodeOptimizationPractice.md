## Code Optimization Practice

#### JavaScript Snippet
``` javascript
// Inefficient loop handling and excessive DOM manipulation
function updateList(items) {
  let list = document.getElementById("itemList");
  list.innerHTML = "";
  for (let i = 0; i < items.length; i++) {
    let listItem = document.createElement("li");
    listItem.innerHTML = items[i];
    list.appendChild(listItem);
  }
}
```

**Analisis del código**
* El uso de innerHTML es lento, sobre todo si lo tenemos dentro de un foreach, para este caso es más recomendable usar textContent que se trata de una operación más simple.
* También para un foreach el uso de appendChild va a provocar que en cada iteración, el navegador actualizará el DOM por lo que afectará el rendimiento.
* Finalmente el foreach no se está usando de la mejor forma, existe otra forma que aplicaremos para iterar de manera más optima.

#### JavaScript código mejorado
``` javascript
function updateList(items) {
    const list = document.getElementById("itemList");
    list.innerHTML = ""; // Vacía el contenido de la lista
    // Creamos un fragmento para reducir las manipulaciones del DOM
    const fragment = document.createDocumentFragment();
    // Los array tienen su propio método forEach para iterar
    items.forEach(item => {
        const listItem = document.createElement("li");
        // Usamos textContent en lugar de innerHTML
        listItem.textContent = item;
        // Agregamos el item actual dentro de un document fragment
        fragment.appendChild(listItem);
    });
    // Agregamos el fragmento al DOM una sola vez
    list.appendChild(fragment);
}
```
* textContent tiene menos capacidad, pero para este caso nos conviene usarlo por su rapidez. 
* Al final agregamos el fragmento que contiene cada item una sola vez al DOM.
* Usamos el método forEach del array porque me parece más facil de leer y tiene un mejor rendimiento.

#### Java Snippet
``` java
// Redundant database queries
public class ProductLoader {
    public List<Product> loadProducts() {
        List<Product> products = new ArrayList<>();
        for (int id = 1; id <= 100; id++) {
            products.add(database.getProductById(id));
        }
        return products;
    }
}
```

**Analisis del código**
* El código realiza 100 llamadas a la base de datos, una por cada id que iteramos, lo que agrega demasiada latencia al flujo.
* El punto anterior junto al ciclo for, incrementamos el tiempo de respuesta. Podemos eliminar el for y realizar una sola consulta a la base de datos.
* El código tampoco maneja errores, es una buena practica para poder ver en los logs el comportamiento real y poder devolver un mensaje personalizado al front.

#### Java código mejorado
``` java
// Recibimos conexión de la base de datos
public List<Product> loadProducts(Connection conn) {
    // Inicializamos una lista para almacenar los productos
    List<Product> products = new ArrayList<>();
    // Creamos un query SQL para obtener productos los productos con ID de 1 a 100
    String query = "SELECT id, name, price FROM Products WHERE id BETWEEN 1 AND 100";
    try {
        // Preparamos la sentencia para evitar inyección SQL y ejecutamos la consulta
        PreparedStatement stmt = conn.prepareStatement(query);
        ResultSet rs = stmt.executeQuery();
        // Iteramos sobre los resultados y vamos agregando los objetos dentro de products
        while (rs.next()) {
            products.add(new Product(
                rs.getInt("id"),
                rs.getString("name"),
                rs.getDouble("price")
            ));
        }
        // Cerramos el ResultSet y PreparedStatement para liberar recursos
        rs.close();
        stmt.close();
    } catch (SQLException e) {
        // Captura y muestra cualquier error que ocurra durante la consulta
        System.out.println("Error al cargar productos: " + e.getMessage());
    }
    // Retornamos la lista de productos obtenida
    return products;
}
```

* Ejecutamos una consulta que obtiene todos los productos que necesitamos, a diferencia del código anterior que realizábamos una consulta por cada item del for, hacemos la petición una sola vez a la base de datos.
* Además usamos PreparedStatement para evitar que nos hagan una inyección SQL, anteriormente ID venía de lo que ingresó el usuario.
* Agregamos un try y catch para conocer el tipo de error.
* Con estos ajustes vamos a ver una mejora notable en el rendimiento.

#### C# Snippet

``` csharp
// Unnecessary computations in data processing
public List<int> ProcessData(List<int> data) {
    List<int> result = new List<int>();
    foreach (var d in data) {
        if (d % 2 == 0) {
            result.Add(d * 2);
        } else {
            result.Add(d * 3);
        }
    }
    return result;
}
```

**Analisis del código**
* Este código es bastante más sencillo, aparentemente no se ve mal, no tenemos bucles anidados, sin embargo, el foreach y los if else hace que el código no se vea tan limpio, c# tiene LINQ que además de simplificar el código, tiene un rendmiento mejor.
* No estamos validando que data tiene registros y no es nulo.
* Con otra estrategia podemos evitar agregar items a result por cada iteración.

#### C# código mejorado

``` csharp
public List<int> ProcessData(List<int> data)
{
    // Validamos que la lista no sea null o vacía antes de procesar
    if (data == null || data.Count == 0)
    {
        Console.WriteLine("La lista está vacía.");
        return new List<int>();
    }
    // Utilizamos LINQ para simplificar la lógica y mejorar la legibilidad
    return data.Select(d => d % 2 == 0 ? d * 2 : d * 3).ToList();
}

```

* Utilizamos LINQ para simplificar la iteración de los registros, con un select podemos, a través de una expresión lambda, obtener todos los items de data, aplicando un if que detemina si el número es par.
* Aplicamos los operadores ternarios ? : para que reemplaza el if y else que teníamos anteriormente.
* El código ahora es más facil de entender y es más rápido.