# **User Management App**

Una aplicación de gestión de usuarios construida con **.NET 8** y **SQL Server**, diseñada para ofrecer una administración eficiente para las cuentas de usuarios.

---

## **Requisitos del Sistema**

Antes de comenzar con la instalación y el desarrollo de este proyecto, asegúrate de contar con las siguientes herramientas y programas:

### **Herramientas Necesarias:**
- **[SDK .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0):** Requerido para ejecutar y desarrollar el proyecto.
- **[Visual Studio 2022 o superior](https://visualstudio.microsoft.com/vs/):** Editor recomendado para editar y ejecutar el proyecto de manera eficiente.
- **[SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads):** Base de datos utilizada en este proyecto para almacenar los datos de los usuarios.

## **Credenciales de Login**

Para iniciar sesión en el sistema, utiliza las siguientes credenciales de administrador predeterminadas:

- **Nombre de usuario:** `User Admin`
- **Correo electrónico:** `useradmin@gmail.com`
- **Contraseña:** `Admin009008++`
- 
Una vez que inicies sesión, recibirás un Token JWT en la respuesta.
Para poder autenticarte y acceder a los demás endpoints disponibles en el sistema, es necesario que incluyas este token en el encabezado de autorización como un Bearer Token en Swagger, utilizando el siguiente formato:

 Bearer {token}
    
### **Requisitos Adicionales:**
- Si ejecutas el proyecto en un entorno local, asegúrate de tener **SQL Server** correctamente instalado y configurado.
- Si prefieres usar **Docker** para ejecutar el proyecto, asegúrate de contar con **[Docker Desktop](https://www.docker.com/products/docker-desktop)** instalado en tu máquina.

---

## **Instrucciones para Clonar el Proyecto**

Sigue estos pasos para obtener el código fuente del proyecto y configurarlo localmente:

1. Abre una terminal en tu computadora.
2. Clona el repositorio ejecutando el siguiente comando:

    ```bash
    git clone https://github.com/vicjose007/User-Management-App.git
    ```

3. Una vez clonado el repositorio, navega hasta el directorio del proyecto:

    ```bash
    cd User-Management-App
    ```

---

## **Configuración de la Conexión a la Base de Datos**

El Script de la base de datos esta dentro del repositorio

De igual manera puedes hacer un update-database en el Package Manager Console una vez hayas puesto tu connectionString en el AppSettings.Development

Para conectar el proyecto con **SQL Server**, actualiza el archivo `appsettings.json.Development` con la cadena de conexión adecuada:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your connection string"
  }
}
