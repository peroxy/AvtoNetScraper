# AvtoNetScraper
Scrapes car information from website avto.net and stores it into a local sqlite database.

## How to run
Before you can run this project you will need these:
- [.NET core SDK](https://dotnet.microsoft.com/download)
- [dotnet ef](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet) (can be installed via `dotnet tool install --global dotnet-ef`)

Open `appsettings.template.json` and setup custom urls and request intervals in milliseconds. URL values should be copied from avto.net after you have specified search filters. After you are done customizing the file, rename or save it as `appsettings.json`.

Go to project directory and run commands: 
1. `dotnet restore` - this will restore all project packages
2. `dotnet ef database update` - this will create a local sqlite database file, make sure the build action is set to copy to output directory
3. `dotnet run` - this will run the console application and fill up your local sqlite database

