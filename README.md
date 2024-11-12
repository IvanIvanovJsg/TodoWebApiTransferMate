# Todo Web API TransferMate

## Prerequisites

- [Docker](https://www.docker.com/get-started) installed on your machine.
- [.NET SDK](https://dotnet.microsoft.com/download/dotnet) installed for running EF commands.

## Getting Started

### 1. Build and Start the Application

From the root folder of the project, run the following command to build and start the application:

```bash
docker-compose up --build -d
```

This command will:
  - Build and start the ASP.NET Core application and PostgreSQL database as Docker containers.
  - Map the application to `http://localhost:8080`.

### 2. Update the Database

Once the containers are running, update the database schema using Entity Framework migrations:

```bash
dotnet ef database update
```

**Note**: Make sure you have the `.NET SDK` installed to run this command. This command should be run from the host machine, not from within the Docker container.

### Accessing Swagger UI

After the application is up and the database is updated, you can access the Swagger UI to interact with the API at:

[http://localhost:8080](http://localhost:8080)
