# AvtoNetScraper
Scrapes car information from website avto.net and stores it into a local sqlite database.

## How to run
Go to project directory and run commands: 

1. `dotnet tool install --global dotnet-ef` - this will install dotnet entity framework tool, this is optional if you already have it installed
2. `dotnet restore` - this will restore all project packages
3. `dotnet ef database update` - this will create a local sqlite database file
4. `dotnet run` - this will run the console application and fill up your local sqlite database

