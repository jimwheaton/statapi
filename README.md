# STAT Api

This was built using .NET Core which is cross platform, however it's using SQL Server as the DB and the DB scripts as written may not run on a different DB platform.

To review the code, I recommend downloading [Visual Studio Code](https://code.visualstudio.com/). It's cross platform and will make it easier to review C# code. Once installed, you will need to install a C# language extension. Follow the instructions [here](https://code.visualstudio.com/docs/languages/csharp).

## Project overview
This is the api that the client app connects to. It is built using [ASP.NET Core](https://github.com/aspnet/Home) and connects to a SQL Server Express database. 

### General project structure
There are 4 folders in this project:

* Stat - this is the web api. It has two controllers that handle requests:
  * StatController - endpoints for general functionality
    * things get kicked off in Startup.cs. This is where middleware to ASP.NET Core is added
    * [GET] /api/stat/lookups - gets lookup data that drives the client app
    * [POST] /api/stat/import - imports a CSV file **file must be tab delimited**, as this is the format that you sent to me. I can change the format to comma separated if you like. Has param:
      * file - the file to upload
  * RankingsController - endpoints to get ranking data
    * Both endpoint accept these search params:
      * site:string - filter on site
      * market:string - filter on market
      * device:string - filter on device
      * keyword:string - filter on keyword
      * start:date - filter on start date
      * end:date - filter on end date
      * weighted:boolean - return weighted rankings
    * [GET] /api/rankings - gets ranking data. Search param:
    * [GET] /api/rankings.csv - gets ranking data as CSV using query string to filter data. **result uses commas as delimeter**
* Models - model objects
* Services - app services:
  * CsvService - imports CSV data to the database. As mentioned above, CSV must be tab delimited
  * RankingService - gets ranking data using filters
* Data - data layer. Abstracts database. Uses EF Core to generate DB schema and handle any updates to schema. StatContext is used to interact with the DB using model objects.

### DB Schema
Since this is a "reporting" app, I elected to go with a star schema. The dimension tables can be used to drive the UI. For performance reasons, imported data from the .csv file is first dumped to the staging table. From there, stored procedures populate the dimension tables and fact table (KeywordRanks).

![STAT DB Schema](/statmodel.png?raw=true "STAT DB Schema")

### Build instructions
For convenience, this API is running on a VM on Azure. If you're feeling brave and want to build this locally (I believe you're on Macs?) then follow the instructions [here](https://docs.microsoft.com/en-us/aspnet/core/tutorials/your-first-mac-aspnet). You may run into issues with generating the database as the scripts are specific to SQL Server. Let me know if you want to try to build/run this locally and I'll assist you.

You can use [Postman](https://chrome.google.com/webstore/detail/postman/fhbjgbiflinjbdggehcddcbncdddomop?hl=en) to test out the API running on Azure. I've sent you the URL in an email

### Kanban board
For simplicity, I used a Kanban board instead of a Scrum board. 

The board can be found [here](https://trello.com/b/sHqoK2N6/stat-api)

The stories are prioritized by their position in the column. Stories at the top are higher priority than ones at the bottom
The columns in the boards are:
Backlog - backlog of stories for the project. These can be fully-formed stories, half-baked ideas, or epics. Stories in this column are not scheduled to be worked on
Approved - stories that have been fully-specified and ready to be worked on.
In progress - stories currently being developed and tested
Done - stories have been fully tested. All acceptance criteria met. Ready for deployment.
In the interest of time, I have only fleshed out a few of the stories on the STAT Client board. You can open the stories for "Visualize rankings as line chart", "Filter rankings", "Import raw ranking data", and "Client shell" to see some examples.  I have also purposely put stories into the different columns so that you get a sense for how they would flow through a Kanban board (the same would apply to Scrum).


