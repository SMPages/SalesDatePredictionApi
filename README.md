# API de Predicción de Ventas
📌 Sales Prediction API

🚀 Requisitos previos

Asegúrese de tener instalado lo siguiente:

.NET 8

SQL Server o MySQL (según la base de datos configurada)

Git

⚙️ Instalación y ejecución

Clonar el repositorio:

git clone https://github.com/SMPages/SalesDatePredictionApi.git

cd SalesDatePredictionApi

Configurar la base de datos:

Modificar appsettings.json con las credenciales y cadena de conexión adecuadas.

Restaurar paquetes y compilar:

dotnet restore
dotnet build

Ejecutar la API:

dotnet run

La API estará disponible en http://localhost:5281.

Los metodos estan visibles en: http://localhost:5281/swagger/index.html

📂 Endpoints principales

GET /api/orders → Obtiene todas las órdenes.

POST /api/orders → Crea una nueva orden.