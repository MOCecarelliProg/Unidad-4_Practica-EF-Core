# Unidad-4_Practica-EF-Core
Repositorio que contiene la plantilla inicial y la consigna a resolver para la ejercitación de EF Core.

# Objetivo general
Instalar en la capa de Infraestructura los paquetes nuget necesarios para poder utilizar EF Core con el motor de DB de MySQL. Luego configurar correctamente el ORM. Y una vez hecho eso, de acuerdo al enfoque "code first" y aprovechando la entidad Product existente, se debe crear la migración inicial y aplicarla para que se cree automáticamente la base de datos y la tabla de productos correspondiente.

# Secuencia de pasos a realizar. (puedes guiarte de los ejemplos del apunte de la unidad)
* Instalar los paquetes de EF Core y de Pomelo, para poder utilizar el ORM con MySQL.
* Crear la clase ApplicationDbContext en la capa de infraestructura y agregarle una propiedad DbSet de producto.
* Agregar el connectionString en el appSettings.Development.json
* Agregar en la clase Program.cs las instrucciones necesarias para registrar el ApplicationDbContext en el contenedor de servicios
* Intentar crear la migración inicial para luego aplicarla y crear la DB con la tabla de productos.

# EXTRA:
Luego de haber conseguido aplicar la migración con éxito puedes continuar programando todo lo necesario para crear un CRUD de producto:
. Crear IProductRepository y ProductRepository;
. Crear IProductService y ProductService;
. Crear ProductController.
(Asegúrate de aplicar DI para:
. Inyectar el ApplicationDbContext en el ProductRepository;
. Inyectar el IProductRepository en el ProductService;
. Inyectar el IProductService en el ProductController;
. No olvides registrar todo en el contenedor de servicios en la clase Program.cs
. Por último crea todos lo métodos necesarios para conseguir un CRUD de producto funcional.
