CRUD Movies & Director — Manual para correr el proyecto

Proyecto de evaluación técnica: CRUD de Movies y Director con backend en C# (.NET / ASP.NET Core) y frontend en Angular.

Requisitos previos
.NET SDK (8.0 o superior)
SQL Server (Express o superior) con una instancia local (SQLEXPRESS)
Node.js (LTS) y npm
Angular CLI: npm install -g @angular/cli
Git


1. Base de datos
Abrir el SQL Server Management Studio  y conéctarse a tu instancia local.
Ejecuta el script SQL que está en la carpeta BaseDatos/ de este repo. Esto crea la base MoviesDirectorDB con las tablas Director y Movies, y los datos de ejemplo.
Si tu instancia de SQL Server no se llama SQLEXPRESS, o usas otro método de autenticación, ajusta la cadena de conexión en Backend/Crud/appsettings.json:
json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=MoviesDirectorDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }

2. Backend (API en C#)
powershell
cd Backend\Crud
dotnet run

Deberías ver algo como:

Now listening on: http://localhost:5258

Si sale otro puerto, nomas usar ese.

Verifica que funcione abriendolo en el navegador:

http://localhost:5258/swagger

Prueba el endpoint GET /api/directors — debería devolver los 3 directores de ejemplo.

Deja esta terminal corriendo y abre una nueva para el frontend.

3. Frontend (Angular)

Instala dependencias (primera vez únicamente):

powershell
cd Frontend\crud-app
npm install --legacy-peer-deps

El flag --legacy-peer-deps es necesario por un bug conocido de npm 10.x al resolver dependencias de vitest en proyectos nuevos de Angular 21. Sin este flag, la instalación puede fallar con el error Cannot read properties of null (reading 'edgesOut').

Si el puerto del backend (paso 2) es distinto a 5258, ajústalo en src/environments/environment.ts:

typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5258/api'
};

Corre la app:

para correr el front:

powershell
ng serve

Abre en el navegador:

http://localhost:4200

Deberías ver el menú (Películas / Directores), con las tablas cargadas y la posibilidad de crear, editar y eliminar en ambas.

Notas y solución de problemas:

ng no se reconoce como comando: reinicia la terminal después de instalar Angular CLI (npm install -g @angular/cli), o agrega manualmente %AppData%\npm a la variable de entorno PATH.
La tabla no se llena aunque la API responda 200 OK: Angular 21 usa detección de cambios zoneless por default. Las listas que se llenan desde una suscripción async deben ser signal<T[]>([]) (no propiedades normales), y en el HTML se leen como lista() en vez de lista.
npm install falla en carpetas sincronizadas con OneDrive: si ves errores intermitentes al instalar, considera pausar temporalmente la sincronización de OneDrive durante la instalación.
