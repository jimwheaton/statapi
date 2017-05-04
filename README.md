# STAT Api

This was built using .NET Core which is cross platform, however it's using SQL Server as the DB and the DB scripts as written may not run on a different DB platform.

To review the code, I recommend downloading Visual Studio Code (https://code.visualstudio.com/). It's cross platform and will make it easier to review C# code. Once installed, you will need to install a C# language extension. Follow the instructions here (https://code.visualstudio.com/docs/languages/csharp).

## Project overview
This is the api that the client app connects to. It is built using ASP.NET Core (https://github.com/aspnet/Home) and connects to a SQL Server Express database. 

### General project structure
There are 4 folders in this project:

* Stat - this is the web api. It has two controllers that handle requests:
  * StatController - endpoints for general functionality
    * things get kicked off in Startup.cs. This is where middleware to ASP.NET Core is added
    * /api/stat/lookups - gets lookup data that drives the client app
    * /api/stat/import - imports a CSV file **file must be tab delimited**, as this is the format that you sent to me. I can change the format to comma separated if you like.
  * RankingsController - endpoints 
* Models
* Services
* Data
