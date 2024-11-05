## Analisis de las pruebas originales

### Autenticación de usuarios
- La clase tenía una dependencia como se comprueban las credenciales, podemos hacer un mock y depender de una abstracción (interfaz) y probar únicamente authenticate.
- No hay suficientes pruebas y los nombres no eran nada precisos.
- Tiene dos pruebas en una misma prueba unitaria y es mejor probar funcionalidades separadas.

### Procesamiento de datos
- No había pruebas suficientes, los nombres de las pruebas eran incorrectos 
- En una misma prueba estábamos probando flujo normal y excepciones lo cual es un error.
- Estaba ejecutando directamente fetchData y es mejor tener un mock.

### Componente UI
- No había pruebas suficientes, los nombres de las pruebas eran incorrectos y solo se está probando el caso de éxito.





## Refactorización de las pruebas

### Autenticación de usuarios
- Se genera una interfaz para la validación de credenciales, de forma que UserAuthenticationManager depende de una abstracción. 
- Usamos mocks que vimos en la clase para simular el comportamiento de la interfaz IUserRepository y así podemos probar solamente la autenticación, mockeando la validación de credenciales y su respuesta.
- Mejoramos los nombres de las pruebas y separamos las funcionalidades.

### Procesamiento de datos
- Separamos responsabilidades, ahora es IDataManager quien se encarga de obtener los datos y al ser una interfaz es todavía más flexible.
- Al igual que el ejemplo anterior usamos mock para simular un comportamiento.
- Cada prueba se centra en probar una única funcionalidad, tenemos una prueba específica para excepciones.
- Mejoramos los nombres de las pruebas

### Componente UI
- Agregamos más pruebas, los nombres describen perfectamente la funcionalidad que se está probando.
- Maneja funcionalidades separadas, incluso agregamos un escenario de error.