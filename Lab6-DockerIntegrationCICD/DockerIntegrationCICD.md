## Implementing and Optimizing Database Queries

#### 1. Review Simulated CI/CD Pipeline Configuration

**Build Stage**
Esta es la parte que detona todo el flujo de integración continua, cuando hacemos merge en alguna de las ramas principales, es decir, develop, qa, stage y main, se genera un commit y aquí es donde corre el proceso, que, entre muchas cosas, encapsula el código y arma todas las dependencias necesesarias en una imagen, a partir del dockerfile, que tiene las instrucciones para construir, sistema operativo y diferentes configuraciones. Permitiendo esperar el mismo resultado cuando se ejecute la aplicación en los ambientes.

**Test Stage**
Docker también nos permite realizar ejecutar pruebas en entornos controlados y en condiciones como producción. Con estas pruebas podemos estar seguros que está todo listo para ser ejecutado en producción, ya que aquí nos podemos dar cuenta si falta alguna dependencia o configuración.

**Deployment Stage**
Las imagenes que superan las pruebas y están listas, se guardan en registro de docker, donde podemos tener una lista de versiones.
Después sigue la administración de nuestros contenedores en herramientas como Kubernetes que nos permite hacer distintas configuraciones como escalar y replicar.

#### 2. Analyze Enhancements and Potential Issues:
**Mejoras**
* En equipos de software de alto nivel, es muy importante la integración continua de nuestras aplicaciones, porque nos permite automatizar resultados de nuestro código en un servicio disponible para todos, además nos garantiza que lo que estamos enviando a ambientes estables está probado y es correcto.
* Las imagenes de docker son independientes del sistema operativo y nos permite implementarlo en diferentes plataformas, lo cual también lo hace consistente.
* Con algunas configuraciones y condiciones podemos hacer escalar el proyecto en diferentes replicas que corren en paralelo y distribuyen la carga.

**Posibles problemas**
* El uso correcto de secretos sería usar algún recurso como AWS secrets manager, no es la mejor práctica guardar en secrets de los contenedores.
* El uso de versiones de imagenes pesadas podría ser un problema, lo que se recomienda usar versiones alpine que contiene el mínimo necesario para funcionar y minimiza el tamaño.
* Requiere experiencia y conocimientos avanzados para gestionar orquestación con Kubernetes, como conocimientos de infraestructura como código con Terraform o para crear instrucciones YAML.

#### 3. Write an Analysis Report:
* Podemos dar merge al código en una rama y se ejecuta el proceso de CICD construyendo nuestro código en una imagen, con todo lo necesario para correr.
* Podemos hacer pruebas del proyecto en algún contenedor.
* Las imagenes que se validan se registran para desplegarlas.
* Podemos ejecutar contenedores en cualquier lugar, azure, aws, gcp, o localmente.
* Nos permite orquestar instancias con kubernetes.
* Definitivamente es un salto de calidad integrar docker junto a una integración continua en un repositorio como github.