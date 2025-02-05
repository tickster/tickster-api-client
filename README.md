# tickster-api-client
Client and sample application for using Tickster's API

### Install Entity Framework
`dotnet tool install --global dotnet-ef`

### Create Sqlite database

From the console project run the following to create a new migration

`dotnet ef migrations add <your name for the migration> -p ..\TicksterSampleApp.Infrastructure\TicksterSampleApp.Infrastructure.csproj`

To update the database run the following

`dotnet ef database update -p ..\TicksterSampleApp.Infrastructure\TicksterSampleApp.Infrastructure.csproj`

On a windows machine, the Sqlite database file is created at (SqliteDbName configured in appsettings)
`C:\Users\<user>\AppData\Local\<SqliteDbName>`
