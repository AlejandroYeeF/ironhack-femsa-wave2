### Designing and Implementing a Microservices Architecture

#### Migration Roadmap and Architecture Documentation

**Analisis**
* Analizar las funcionalidades del backend monolito para determinar cuales son las responsabilidades de cada dominio.

* Identificar y definir las operaciones de cada uno de los microservicios identificados.

**Implementación**
* Configuración de API Gateway que será responsable de la orquestación y enrutamiento de peticiones hacia los 4 microservicios.
* Creación del microservicio Users (AWS EKS) que se encarga de la autenticación y autorización de usuarios, además de la administración de perfil de usuarios.
    * Implementación de AWS Cognito para la gestión de inicio de sesión generando un JWT, el refreshToken para mantener la sesión activa, logout para revocar las sesiones.
    * Creación de base de datos relacional AWS RDS Maria DB que contiene la información del usuarios.
* Continuamos con el microservicio Products para gestionar el catálogo de productos e inventario de productos.
    * Requiere una base de datos relacional con dos tablas, una de productos y otra que tenga relación con el inventario de estos productos.
* Seguimos con el microservicio Orders que gestiona los pedidos y transacciones de pagos, pero para poder acceder a los endpoints de este microservicio, en API Gateway necesitamos validar que el usuario tiene un JWT haciendo una validación con la llave pública jwks.
    * Requiere una base de datos relacional con tres tablas relacionadas entre sí, Order, OrderDetail, OrderPaymentDetail.
* Por ultimo la creación del microservicio Soporte para la atención al cliente que genera tickets para que los agentes puedan tomar estos casos, este microservicio también necesita que el usuario haya iniciado sesión para tener el ID del usuario y como medida de seguridad.
    * Se requiere una base de datos relacional para administrar los tickets.
* Finalmente se realizan las pruebas funcionales, pruebas automatizadas, durante los despliegues a Develop, QA, Stage y Main vamos haciendo pruebas de penetración y hemos creado una integración continua CICD con github actions con Docker, Kubernetes gestionado por AWS con EKS, sus respectivas pruebas SAST y DAST. Además de la creación de la infraestructura como código con Terraform.