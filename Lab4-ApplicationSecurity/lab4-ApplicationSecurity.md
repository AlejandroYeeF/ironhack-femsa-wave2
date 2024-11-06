## Pseudo-Code for Authentication System

```csharp
FUNCTION authenticateUser(username, password):
  QUERY database WITH username AND password
  IF found RETURN True
  ELSE RETURN False

```

#### Entregable analisis de vulnerabilidades
* Basado en los principios SAST, analizamos las vulnerabilidades del pseudocódigo y encuentro muchos problemas que van en contra de lo que vimos en clase de SAST:
    * **Inyección SQL**: En el ejemplo se hace una consulta directamente con los parámetros recibidos, un atacante puede meter una consulta más compleja que retorne un resultado que de información al atacante y poder inyectar sentencias que puedan hacer daño.
    * **Las contraseñas están inseguras**: Podemos ver que se hace una consulta directa de password, por lo que la contraseña están en texto plano, se recomienda encriptar.
    * **No se validan campos input**: El atacante podría ingresar cualquier script para intentar hacer daño.
    * **Manejo de fuerza bruta**: El pseudocódigo no valida nada, va directo a la base de datos, no tiene ningún tipo de controlar reintentos para ataque de bots.

#### Entregable plan de mitigación del pseudocódigo original
* Plan de mitigación aplicando principios SAST y DAST.
    * **Inyección SQL**: Para evitar la concatenación directa de parámetros en las consultas. Además de una sanitización al valor de los parámetros, podemos usar bibliotecas que nos permiten evitarlo, como un ORM, en .NET he utilizado EntityFramework.
    * **Las contraseñas están inseguras**: Podemos usar algoritmos para hashear las contraseñas antes de guardarlas y para validarlas, en .NET he utilizado bcrypt que genera un salt en automático.
    * **No se validan campos input**: Necesitamos agregar validación a las propiedades del modelo con decoradores/atributos con DataAnnotations, podemos usar una expresión regular para cumplir con una longitud y caracteres especiales. También SAST me ha alertado y recomendado usar sanitización en los atributos del modelo.
    * **Manejo de fuerza bruta**: Podemos controlar los reintentos en un redis, ya sea por username o por IP, para bloquear temporalmente un posible bot haciendo ataque de fuerza bruta. Además en la creación de la cuenta, cuando creas una contraseña debe ser segura, aplicar longitud min y max, uso de caracter especial y un número. Porque en la clase vimos que se usa diccionarios, con una contraseña segura estamos a salvo de esto.
    
* Al ejecutar pruebas SAST en un pipe de github actions podremos ver que el código ya no marca vulnerabilidades y DAST con ayuda de herramientas podemos probar en tiempo de ejecución enviar payloads dañinos.


## JWT Authentication Schema

```csharp
DEFINE FUNCTION generateJWT(userCredentials):
  IF validateCredentials(userCredentials):
    SET tokenExpiration = currentTime + 3600 // Token expires in one hour
    RETURN encrypt(userCredentials + tokenExpiration, secretKey)
  ELSE:
    RETURN error

```

#### Entregable de propuesta de lineamientos del JWT
* **Firmar con algoritmo RS256**, es el que usamos en Auth0 para firmar el JWT de manera asimétrica, la llave privada la tiene Auth0 que es quien genera el JWT, después nos proporciona un JWKS que es la llave pública que nos ayuda a verificar que no ha sido vulnerada.
* **Uso de refreshToken**, en caso de que se llegue a robar un accessToken, este tiene un tiempo de expiración corto, el refreshToken genera nuevo juego de tokens (accessToken y refreshToken). El refreshToken tiene una familia de tokens, si se llega a usar un refreshToken que ya no es el ultimo generado por Auth0, entonces se revoca toda la sesión. Mientras el sistema mantenga refrescando los tokens y guarde el ultimo refreshToken generado, el usuario debería mantenerse en sesión.
* **Payload del JWT**, no debemos meter atributos sencibles del usuario como contraseña. Los atributos necesarios son sub (id del usuario), iat (hora de emisión) y exp (hora de expiración), scopes (permisos) e issuer (quien lo firmó), con estos atributos también podemos validar el JWT.


## Secure Data Communication Plan

```csharp
PLAN secureDataCommunication:
  IMPLEMENT SSL/TLS for all data in transit
  USE encrypted storage solutions for data at rest
  ENSURE all data exchanges comply with HTTPS protocols

```

#### Entregable de estrategia de protección de datos.
* Para la transmisión de datos creo que vale la pena hablar de como funciona esta comunicación, primero se establece un "canal" con el protocolo TCP que es la capa de transporte y se usa SSL/TLS para dar seguridad a los datos que se transfieren y entre clinete y servidor hacen una negociación para definir como cifran los datos, existen bibliotecas que lo usan en automático, por ejemplo la biblioteca HttpClient de .NET. Con esto evitamos ataques como man in the middle.
* Como hablamos anteriormente en el entregable 1, es importante hashear las contraseñas y datos sencibles. También el uso de infraestructura en la nube nos da seguridad.
* Es importante el uso de MFA para validar el inicio de sesión no solo con la contraseña, puede ser por OTP, magic link, también se pueden usar tokens de semillas como lo hace microsoft authenticator.
* Para el acceso a una base de datos de AWS, por ejemplo, se usa IAM, así que los usuarios tienen roles con ciertas políticas que no les permite hacer cualquier cosa.
* Es importante definir un CICD en el repositorio como github con pipes que ejecuten pruebas SAST, puede ser usado Stackhawk también, así no podemos dar merge en main, por ejemplo, si tenemos vulnerabilidades. Además es importante realizar las pruebas de penetración para usar herramientas que prueben la app o servicio en tiempo real.
* El uso de monitoreo y alertas es importante, aquí usamos Datadog para auditoria, ver la traza de las peticiones a los endpoints, monitores que nos alertan comportamientos raros.