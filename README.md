# 📚 BiblioAwoo — Sistema de Biblioteca Digital Municipal

**BiblioAwoo** es una aplicación web desarrollada en **ASP.NET Core MVC** con **Entity Framework Core**, diseñada para transformar la gestión tradicional de bibliotecas municipales en un sistema digital moderno, eficiente y escalable. El proyecto surge como respuesta a los problemas comunes derivados del uso de libretas físicas y hojas de cálculo para el registro de préstamos, tales como duplicidad de registros, falta de trazabilidad, y errores en el control de inventario.

Este sistema permite registrar usuarios, administrar libros, gestionar préstamos y consultar historiales, todo desde una interfaz web intuitiva y segura.

---

## 🎯 Objetivo del Proyecto

El objetivo principal de BiblioAwoo es digitalizar el proceso de préstamo de libros en bibliotecas públicas, garantizando:

- Un control preciso sobre qué usuario tiene qué libro y hasta cuándo.
- La imposibilidad de prestar libros sin ejemplares disponibles.
- La trazabilidad completa del historial de préstamos por usuario y por libro.
- La validación automática de datos para evitar duplicados y errores comunes.
- Una arquitectura limpia y mantenible basada en el patrón MVC.

---

## 🛠️ Tecnologías Utilizadas

- **Lenguaje:** C# (.NET 7 o superior)
- **Framework:** ASP.NET Core MVC
- **ORM:** Entity Framework Core
- **Base de Datos:** MySQL

## 🏗️ Estructura del Proyecto

La estructura del proyecto sigue el patrón MVC (Modelo-Vista-Controlador), con una clara separación de responsabilidades:

```
BiblioAwoo/
│
├── Controllers/               # Lógica de control y manejo de rutas
│   ├── HomeController.cs
│   ├── LibroController.cs
│   ├── PrestamoController.cs
│   └── UsuarioController.cs
│
├── Infrastructure/            # Configuración de EF Core y conexión a BD
│   └── AppDbContext.cs
│
├── Models/                    # Clases que representan las entidades del sistema
│   ├── Libro.cs
│   ├── Usuario.cs
│   └── ErrorViewModel.cs
│
├── Migrations/                # Migraciones generadas por EF Core
│
├── Views/                     # Archivos Razor para la interfaz de usuario
│   ├── Home/
│   │   ├── Index.cshtml
│   │   └── Privacy.cshtml
│   ├── Libro/
│   │   └── Index.cshtml
│   ├── Prestamo/
│   │   ├── Index.cshtml
│   │   ├── HistorialUsuario.cshtml
│   │   └── PrestamosPorLibro.cshtml
│   └── Usuario/
│       ├── Create.cshtml
│       ├── Details.cshtml
│       ├── Index.cshtml
│       ├── _ViewImports.cshtml
│       └── _ViewStart.cshtml
│
├── wwwroot/                   # Archivos estáticos (CSS, JS, imágenes)
│
├── appsettings.json           # Configuración general del proyecto
├── appsettings.Development.json
└── Program.cs                 # Punto de entrada de la aplicación
```

---

## 📋 Funcionalidades del Sistema

### 👤 Gestión de Usuarios

- **Registro de nuevos usuarios** con los campos: Nombre, Documento, Correo y Teléfono.
- **Validación de documento único** para evitar duplicados.
- **Listado completo de usuarios** registrados en la base de datos.
- **Visualización de detalles** de cada usuario.

### 📘 Gestión de Libros

- **Registro de libros** con los campos: Título, Autor, Código y número de ejemplares disponibles.
- **Validación de código único** para evitar duplicados.
- **Listado completo de libros** disponibles en la biblioteca.
- **Visualización de disponibilidad** de ejemplares por libro.

### 🔄 Gestión de Préstamos

- **Creación de préstamos** asociando un usuario, un libro y una fecha de devolución.
- **Validación de disponibilidad**: no se permite prestar libros sin ejemplares disponibles.
- **Actualización automática del stock**:
  - Al prestar un libro, se descuenta un ejemplar.
  - Al devolverlo, se incrementa nuevamente.
- **Consulta de historial de préstamos por usuario**, mostrando todos los libros que ha solicitado.
- **Consulta de préstamos activos por libro**, mostrando qué usuarios lo tienen actualmente.

---

## 🧠 Validaciones y Manejo de Errores

El sistema implementa validaciones tanto en el modelo como en los controladores, y maneja errores en tiempo de ejecución mediante bloques `try-catch` para garantizar la integridad de los datos:

- **Usuarios duplicados**: se lanza una excepción si se intenta registrar un documento ya existente.
- **Libros duplicados**: se impide registrar códigos repetidos.
- **Préstamos inválidos**: se bloquea el préstamo si no hay ejemplares disponibles.
- **Devoluciones mal registradas**: se valida que el préstamo exista y no haya sido devuelto previamente.

---

## ⚙️ Configuración Inicial del Proyecto

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/jeropq16/GestionBiblioteca.git
   ```

2. **Configurar la cadena de conexión en `appsettings.json`:**
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=BiblioAwooDB;User=root;Password=Jeronimo11;"
   }
   ```

3. **Ejecutar las migraciones para crear la base de datos:**
   ```bash
   dotnet ef database update
   ```

4. **Ejecutar la aplicación:**
   ```bash
   dotnet run
   ```

5. **Acceder desde el navegador:**
   ```
   http://localhost:5000
   ```

---

## 📌 Consideraciones Técnicas

- El proyecto está preparado para entornos de desarrollo y producción mediante archivos de configuración separados.
- Las vistas Razor están organizadas por entidad, facilitando la navegación y el mantenimiento.
- Se recomienda utilizar migraciones para cualquier cambio en el modelo de datos.
- El contexto de base de datos (`AppDbContext`) está centralizado en la carpeta `Infrastructure`, siguiendo buenas prácticas de arquitectura.
