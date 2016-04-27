# IT_Part_02
Assignment for course; Internet Technology on Software engineering, 6th semester

## Requirements
* ASP.NET Core 1.0 RC1 Update 1 must be installed: [Download here](https://get.asp.net/OtherDownloads)
* Enable the ASP.NET Core 1.0 command-line tools. Open a command-prompt and run: 'dnvm upgrade'
* Install DNX for .NET Core. In command-prompt run: 'dnvm upgrade -r coreclr'

The project is a ASP.NET Core 1.0 webapplication, and is running on a Microsoft IIS Express server, and all above must therefor be meet, in order to run the project.

### Restore dependencies
If all dependencies are not restored, use the DNX Utility to restore and download all dependencies.
* Navigate to the source folder of the project in command-prompt, most commonly: 'Documents\Visual Studio 2015\IT_Part_02\src\IT_Part_02'
* Run following command in command-prompt when in the source folder: 'dnu restore'
* Build solution in Visual Studio 2015

The project should be able to run, but with no functionality, as no database has been created or updated. Press the 'IIS Express' button in Visual Studio 2015. The default browser should now open and navigate to 'localhost:xxxx', and a webapplication is displayed. If an error is displayed when navigating to the "image" tab, or a user is tried to register, a database has not been created or update.

### Create or update database with Entity Framework
* Navigate to the source folder of the project in command-prompt, most commonly: 'Documents\Visual Studio 2015\IT_Part_02\src\IT_Part_02'
* Run following command in command-prompt when in the source folder: 'dnu restore'
* When complete, run following command in same folder: 'dnx ef database update'

The webapplication should now be able to register, login, CRUD images etc.