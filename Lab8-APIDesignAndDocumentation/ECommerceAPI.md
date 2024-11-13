#### API Design and Documentation Workshop

**Reflection Report**

Después de haber desarrollado el API para un sistema E-Commerce para administrar usuarios, pedidos y tickets para soporte al cliente. Fue una práctica interesante porque puse a prueba los conocimientos que tenía y los que nos enseñó el profesor y sobre la práctica me fue quedando mucho más claro definir un API First, estando seguro que cumple con estándares y buenas prácticas.
Los desafíos encontrados son los siguientes:

- Descripción de paths (endpoints) para tener un nombre claro y descriptivo, aplicando los métodos HTTP como POST, GET, PATCH, DELETE, en función del propósito que quisimos cubrir, tratando de poder administrar desde front haciendo peticiones al back.
- También había que representar los modelos, modelos de solicitud para crear una entidad, modelo de respuesta, modelo de errores también, junto al status code correspondiente al caso. También me alinee al estándar de los códigos de respuesta de HTTP.
- Tuvimos que determinar si usar PUT o PATCH, decidí PATCH porque nos permite hacer una actualización parcial en lugar de la entidad completa, como en los casos de Order y Ticket donde teníamos que modificar unicamente el estatus.

Considero que es importante diseñar el API previamente a la implementación porque podemos ofrecer al frontend un contrato de la interacción con el API, puede conocer los modelos de request, response, tipos de estatus de error.
Open API nos permite leer de mejor manera los endpoints del API e incluso si lo ponemos en un servidor seremos capaces e hacer pruebas como con postman pero todo preparado.
Además nos permite gestionar versiones de APIs.