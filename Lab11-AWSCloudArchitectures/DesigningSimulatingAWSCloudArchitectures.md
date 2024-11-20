### Designing and Simulating AWS Cloud Architectures

**Part 1: Designing Cloud Infrastructure**
* Para la creación de la infraestructura básica de nube en AWS para una aplicación web, necesitamos crear un AWS EC2, S3 y VPC.
*   VPC: VPC es una red virtual privada que nos permite ejecutar recursos de AWS de manera segura a través de networking.
    * Necesitamos definir un rango de IPs, para tener una mejor disponibilidad podemos tener 2 subredes públicas (Internet gateway para salir a internet) y 2 subredes privadas.
    * Las subredes públicas nos ayudan a que los balanceadores de carga (AWS ELB) estén expuestos a internet y pueda ser consumido por clientes.
    Las subredes privadas donde vivirán las instancias EC2 para no exponerlas a internet.
    * AWS Elastic load balancing (ELB) no está especificado pero nos viene bien para controlar la entrada y configuramos enrutamiento seguro hacia el microservicio EC2 mediante Security Groups.
    * Para S3 se recomienda un VPC endpoint para no exponer el servicio y EC2 pueda conectarse de manera segura sin pasar por internet.


**Part 2: IAM Configuration**
* Necesitamos 3 roles, 1 para el administrador, 1 para los desarrolladores, 1 para las instancias EC2.
* Rol para administradores requiere las siguientes políticas:
    * Gestión de toda la infraestructura de AWS, configuración de networking, seguridad, EC2, VPC, S3 y con capacidad para crear y administrar roles y políticas (IAM). Normalmente los administradores pueden tener full access, podría delimitarse políticas específicas para los recursos de este desarrollo.
* Rol para desarrolladores:
    * Acceso limitado a S3 como PUT y GET objetos en el bucket creado para este desarrollo de S3. 
    Poder iniciar, detener instancias EC2 en ambientes bajos, además de lectura para cloudwatch.
* Rol para instancias EC2
    * El microservicio necesita permisos para conectarse a S3, hacer PUT, GET, Delete de objetos en el bucket creado para este desarrollo de S3.
Finalmente se requier asignar estos roles a usuarios determinados (desarrolladores y administrador), y asignar el rol para las instancias, a las mismas EC2.


**Part 3: Resource Management Strategy**
Es importante la creación de estrategias de gestión de recursos para ofrecer una mejor disponibilidad y escalado.
* Auto Scaling Group para las instancias EC2 se requiere 2 zonas de disponibilidad con el uso de las subredes privadas que especifiqué en la parte 1 y se podrá observar en el diagrama de la parte 4. El tamaño del grupo se puede configurar para tener un mínumo número de instancias y un máximo, dependerá del consumo esperado y crece en automático, por ejemplo cuando aumenta el tráfico.
Podemos identificar patrones, horas pico y durante la noche para accionar cuantos recursos tener.
* AWS Elastic load balancing, necesitamos definir una revisión de salud mediante un endpoint health. El mismo Auto Scaling puede mejora el rendimiento del balanceador de carga.
* AWS Budgets, podemos monitorear los comportamientos de los recursos, por ejemplo, si estamos creando demasiados objetos en S3, podemos monitorear y definir alertas. También se puede definir un límite de gasto mensual.

**Part 4: Theoretical Implementation**
* Los usuarios acceden al servicio web mediante un cliente y realiza una petición HTTP hacia el API.
* En primera instancia la petición llega a la VPC expuesta a internet gracias a Internet Gateway y es dirigido al Load Balancer que encontramos en las subredes públicas.
* El load balancer dirige el tráfico a las instancias EC2 donde están ejecutados los microservicios, donde únicamente a través de reglas que definimos para que únicamente el load balancer alcance las subredes privadas de las EC2.
* Las instancias realizan las operaciones solicitadas por el usuario y se conectan al VPC endpoint de S3 para administrar archivos. Esto se hace de manera segura internamente sin salir a internet.

**Se anexa imagen en la misma carpeta de este laboratorio del diagrama propuesto.**

**Part 5: Discussion and Evaluation**
* Decidí usar una VPC para poder aislar toda nuestra infraestructura del internet. A través de configuraciones de red, subredes, enrutamientos, grupos de seguridad mantenemos el tráfico de manera segura.
* Se requieren 4 subredes, 2 públicas para el balanceador de carga y 2 privadas para las instancias EC2, 4 instancias en lugar de dos para poder disponibilizar el servicio en diferentes regiones.
* Se requiere implementar Internet Gateway para permitir que los clientes puedan comunicarse con nuestra VPC, entrando por el balanceador de carga.
* Se requieren configuraciones de Auto Scaling para escalar de manera automática el número de instancias y optimizar los costos en función de las necesidades.
* Se decide usar VPC endpoint para S3 para permitir la comunicación entre nuestros microservicios que corren en las EC2 hacia S3, de tal forma que el trafico de información no se exponga a internet.
* Además se usa correctamente las políticas mediante el principio del mínimo privilegio para tener cuidado y evitar malos manejos en la infraestructura.