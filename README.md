# API de Predicción de Ventas
📌 Sales Prediction API

🚀 Requisitos previos

Importante: Ejecutar el archivo adjunto  DBProcedures.sql dentro de la base de datos DBSetup.sql

Asegúrese de tener instalado lo siguiente:

.NET 8

SQL Server

Git

⚙️ Instalación y ejecución

⚙️ Clonar el repositorio:

git clone https://github.com/SMPages/SalesDatePredictionApi.git

cd SalesDatePredictionApi

⚙️ Configurar la base de datos:

Modificar appsettings.json con las credenciales  DefaultConnection con la cadena del sitio.

⚙️ Restaurar paquetes y compilar:

dotnet restore

dotnet build

⚙️ Ejecutar la API:

dotnet run

La API estará disponible en http://localhost:5281

Los metodos estan visibles en: http://localhost:5281/swagger/index.html


📂 Endpoints principales

GET /api/customers → Obtiene todos los clientes o el listado de coincidencia con el name ingresado.

GET /api/employees → Obtiene el listado de empleados.

GET /api/products → Obtiene el listado de productos.

GET /api/shippers → Obtiene el listado de transportistas

GET /api/orders/{customerId} → Obtiene el listado de las órdenes de un cliente.

POST /api/orders/create → Crea una nueva orden.