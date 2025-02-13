from pathlib import Path
import shutil
import subprocess
import os

def run_command(command):
    try:
        result = subprocess.run(command, capture_output=True, text=True, check=True)        
        return result
    except (subprocess.CalledProcessError, FileNotFoundError) as e:
        print(f"Error running command '{command}': {e}")
        return None         

def is_package_installed(command, package_name):
    result = run_command(command)

    if result is None:
        raise RuntimeError(f"{package_name} is not installed or not in PATH.")

    print(f"{package_name} ({result.stdout.strip()}) is already installed")
    return True
    
def install_ef_core_tools():        
    result = run_command(["dotnet", "tool", "install", "-g", "dotnet-ef"])

    if result is None:
        raise RuntimeError("Failed to install EF Core tools.")
    
    print("EF Core tools was installed")

def remove_migrations_folder():
    folder_path = Path("../TicksterSampleApp.Infrastructure/Migrations")

    try:
        if os.path.exists(folder_path):
            print("Migration folder exists. Removing it...")
            shutil.rmtree(folder_path)
            print("Migration folder was removed.")
    except PermissionError:
        raise PermissionError("Permission denied. Close any programs using the migrations folder and try again.")
    except Exception as e:
        raise RuntimeError(f"Unexpected error while removing the migrations folder: {e}")

def create_migrations():
    result = run_command(["dotnet", "ef", "migrations", "add", "InitialCreate", "--project", "../TicksterSampleApp.Infrastructure"])

    if result is None:
        raise RuntimeError("Failed to create migrations.")
    
    print("Migrations were created.")

def update_database():
    # Reset migrations and database to initial state
    remove_migrations_folder()

    print("Creating migrations...")
    create_migrations()

    remove_sqlite_db_file()

    # Rebuild the database
    print("Updating database...")
    result = run_command(["dotnet", "ef", "database", "update", "-p", "../TicksterSampleApp.Infrastructure"])

    if result is None:
        raise RuntimeError("Failed to update the database.")
    
    print("Database was updated.")

def remove_sqlite_db_file():
    file_path = Path(os.getenv('LOCALAPPDATA') + "/sampleapp.db")

    try:
        if os.path.exists(file_path):
            print("SQLite database exists. Removing it...")
            os.remove(file_path)
            print("SQLite database was removed")
    except PermissionError:
        raise PermissionError("Permission denied. Close any programs using the database and try again.")
    except Exception as e:
        raise RuntimeError(f"Unexpected error while removing the database: {e}")

def run_sampleapp():
    process = subprocess.Popen(["dotnet", "run"],
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            text=True,
            bufsize=1,
            universal_newlines=True)
    
    for line in process.stdout:
        print(line, end="")

    process.wait()

def setup_sampleapp():
    try:
        if not is_package_installed(["dotnet", "ef", "--version"], "EF Core"):
            print("Installing EF Core tools...")
            install_ef_core_tools()

        update_database()
    except Exception as e:
        print(f"Error: {e}")
        return
        
    print("Setup completed.")

    print("Running sample app...")
    run_sampleapp()



setup_sampleapp()             