# DataAccess.MsSql Project

Used for manage database on MS SQL Server.
Uses EntityFramework for managing migrations.

## Setup config

Please set up DbEngine = MsSqls and respected connection string to your database in appsettings.json

## Commands reminder

For manage migrations please use following commands.

__Ensure `dotnet ef` tool installed__
`dotnet tool install --global dotnet-ef`

Optional keys for specifying parameters:
- ` --project ApiSample\DataAccess.MsSql`
- ` --startup-project ApiSample\ApiSample` 
- ` --connection 'your_connection_string_here'`

Creating initial migration:
- `dotnet ef migrations add InitialCreate`
or from root:
- `dotnet ef migrations add InitialCreate --project ApiSample\DataAccess.MsSql --startup-project ApiSample\ApiSample`

Creating or updating database:
- `dotnet ef database update`
or from root:
- `dotnet ef database update --project ApiSample\DataAccess.MsSql --startup-project ApiSample\ApiSample`

Add new migration:
- `dotnet ef migrations add MigrationName`
or from root:
- `dotnet ef migrations add MigrationName --project ApiSample\DataAccess.MsSql --startup-project ApiSample\ApiSample`

Remove last migration:
- `dotnet ef migrations remove`
or from root:
- `dotnet ef migrations remove --project ApiSample\DataAccess.MsSql --startup-project ApiSample\ApiSample`

Rollback migration in local dev db:
- `dotnet ef database update LastGoodMigrationName`
or from root:
- `dotnet ef database update LastGoodMigrationName --project ApiSample\DataAccess.MsSql --startup-project ApiSample\ApiSample`
