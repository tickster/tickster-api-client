# tickster-api-client
Client and sample application for using Tickster's API

# Sample app
The console app fetches data from Tickster's CRM API and writes the data into an SQLite database.

## Run the app 

### - with setup script

#### Prerequisites
- Python - https://www.python.org/downloads/

The sample app is configured to fetch data from a test-EOG and needs no manual configuration to get started.
You can run the "setup.py"-script to install dependencies and run the app.

`python setup.py`

### - without setup script

#### Prerequisites
- EF Core Tools
  - `dotnet tool install -g dotnet-ef`

#### Create Sqlite database

From the console project run the following to create a new migration

`dotnet ef migrations add <your name for the migration> -p ..\TicksterSampleApp.Infrastructure\TicksterSampleApp.Infrastructure.csproj`

To update the database run the following

`dotnet ef database update -p ..\TicksterSampleApp.Infrastructure\TicksterSampleApp.Infrastructure.csproj`

On a windows machine, the Sqlite database file is created at (SqliteDbName configured in appsettings - Default "sampleapp.db")
`C:\Users\<user>\AppData\Local\<SqliteDbName>`

#### Run the app

You can run the app by running `dotnet run` from within the console app folder

## Finally

1. You can visualize the data with DB Browser for SQLite - https://sqlitebrowser.org/
1. Open DB Browser and open the SQLite DB-file. The file is saved to "C:\Users\<username>\AppData\Local\sampleapp.db"

