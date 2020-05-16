# AvtoNetScraper
Scrapes car information from Slovenian car website [AVTO.NET](https://avto.net) and stores it into a local sqlite database. 
This was developed to store all avto.net car ads into a database and analyze them. This was not meant to be used when buying a car and getting notifications when a new car pops up. It can definitely be modified for that purpose though, would not require much work.

## How to run
### Requirements
Before you can run this project you will need these:
- [.NET core SDK](https://dotnet.microsoft.com/download)
- [dotnet ef](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet) (can be installed via `dotnet tool install --global dotnet-ef`)

### Setting up config
Open `appsettings.template.json` and setup custom urls and request intervals in milliseconds. URL values should be copied from avto.net after you have specified search filters. After you are done customizing the file, rename or save it as `appsettings.json`.

Please be aware of avto.net limitation that only shows a maximum of 1000 car ads in their search - even if more should be included in the filter. It is suggested to limit your search in those cases to be always under 1000 or they won't get scraped.

### Restoring packages and database
Go to project directory and run commands: 
1. `dotnet restore` - this will restore all project packages
2. `dotnet ef database update` - this will create a local sqlite database file, make sure the build action is set to copy to output directory

### Running application
`dotnet run` - this will run the console application with default configuration and fill up your local sqlite database.
Optional arguments: 
1. `dotnet run -- -urls` - will only parse car urls and insert them into database, will not make a request to each url and parse each car's info
2. `dotnet run -- -cars` - will read the `Url` table from local database and make a request to each car url and parse each car's info into database
3. `dotnet run -- -urls -cars` - this will perform both actions as described above - this is the default behavior if you run the application without any arguments

## Development
### Updating database model
Modify Database/Model.cs file and run commands:
1. `dotnet ef migrations add Changelog`
2. `dotnet ef database update`


