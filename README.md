# tickster-api-client
Client and sample application for using Tickster's API

### Create Sqlite database

From the console project run the following to create a new migration

`dotnet ef migrations add <your name for the migration> -p ..\TicksterSampleApp.Infrastructure\TicksterSampleApp.Infrastructure.csproj`

To update the database run the following

`dotnet ef database update -p ..\TicksterSampleApp.Infrastructure\TicksterSampleApp.Infrastructure.csproj`