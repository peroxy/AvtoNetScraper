# AvtoNetScraper
Scrapes car information from Slovenian car website [AVTO.NET](https://avto.net) and stores it into a local sqlite database. 
This was developed to store all avto.net car ads into a database and analyze them. This was not meant to be used when buying a car and getting notifications when a new car pops up. It can definitely be modified for that purpose though, would not require much work.

## How to run
### Requirements
Before you can run this project you will need these:
- [.NET core SDK](https://dotnet.microsoft.com/download)
- [dotnet ef](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet) (can be installed via `dotnet tool install --global dotnet-ef`)

### Setting up config
Open `appsettings.template.json` and setup custom urls, request intervals in milliseconds and local image download directory. URL values should be copied from avto.net after you have specified search filters. After you are done customizing the file, rename or save it as `appsettings.json`.

Please be aware of avto.net limitation that only shows a maximum of 1000 car ads in their search - even if more should be included in the filter. It is suggested to limit your search in those cases to be always under 1000 or they won't get scraped.

Here is an example of appsettings.json file:

```
{
  "appSettings": {
    "searchFilterUrls": [
      "https://www.avto.net/Ads/results.asp?znamka=Ford&model=&modelID=&tip=katerikoli%20tip&znamka2=&model2=&tip2=katerikoli%20tip&znamka3=&model3=&tip3=katerikoli%20tip&cenaMin=0&cenaMax=999999&letnikMin=0&letnikMax=2090&bencin=0&starost2=999&oblika=0&ccmMin=0&ccmMax=99999&mocMin=&mocMax=&kmMin=0&kmMax=9999999&kwMin=0&kwMax=999&motortakt=&motorvalji=&lokacija=0&sirina=&dolzina=&dolzinaMIN=&dolzinaMAX=&nosilnostMIN=&nosilnostMAX=&lezisc=&presek=&premer=&col=&vijakov=&EToznaka=&vozilo=&airbag=&barva=&barvaint=&EQ1=1000000000&EQ2=1000000000&EQ3=1000000000&EQ4=100000000&EQ5=1000000000&EQ6=1000000000&EQ7=1110100120&EQ8=1010000001&EQ9=100000000&KAT=1010000000&PIA=&PIAzero=&PSLO=&akcija=&paketgarancije=&broker=&prikazkategorije=&kategorija=&zaloga=&arhiv=&presort=&tipsort=&stran=",
      "https://www.avto.net/Ads/results.asp?znamka=Ford&model=&modelID=&tip=katerikoli%20tip&znamka2=&model2=&tip2=katerikoli%20tip&znamka3=&model3=&tip3=katerikoli%20tip&cenaMin=0&cenaMax=999999&letnikMin=0&letnikMax=2090&bencin=0&starost2=999&oblika=0&ccmMin=0&ccmMax=99999&mocMin=&mocMax=&kmMin=0&kmMax=9999999&kwMin=0&kwMax=999&motortakt=&motorvalji=&lokacija=0&sirina=&dolzina=&dolzinaMIN=&dolzinaMAX=&nosilnostMIN=&nosilnostMAX=&lezisc=&presek=&premer=&col=&vijakov=&EToznaka=&vozilo=&airbag=&barva=&barvaint=&EQ1=1000000000&EQ2=1000000000&EQ3=1000000000&EQ4=100000000&EQ5=1000000000&EQ6=1000000000&EQ7=1110100120&EQ8=1010000001&EQ9=100000000&KAT=1010000000&PIA=&PIAzero=&PSLO=&akcija=&paketgarancije=&broker=&prikazkategorije=&kategorija=&zaloga=&arhiv=&presort=&tipsort=&stran="
    ],
    "requestIntervalMs": 10000,
    "imagesDirectory": "C:\\Pictures"
  }
}
```

### Restoring packages and database
Go to project directory and run commands: 
1. `dotnet restore` - this will restore all project packages
2. `dotnet ef database update` - this will create a local sqlite database file, make sure the build action is set to copy to output directory

### Running application
`dotnet run` - this will run the console application with default configuration (by using `-urls -cars` arguments) and fill up your local sqlite database.
Optional arguments: 
1. `dotnet run -- -urls` - will only parse car urls and insert them into database, will not make a request to each url and parse each car's info
2. `dotnet run -- -cars` - will read the `Url` table from local database and make a request to each car url and parse each car's info into database
3. `dotnet run -- -images` - will read the `Car` table from local database and make a request to each car url image and download it to a local path specified in appsettings

## Development
### Updating database model
Modify Database/Model.cs file and run commands:
1. `dotnet ef migrations add Changelog`
2. `dotnet ef database update`
