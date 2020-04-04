# DotCoreRunner
.Net core application to run other .net core applications.

As your .Net solution keep growing and contains many projects you want to run. Instead of run each project manually or creating shortcuts
for each, this will allow you to run different and many instances of your .net core projects with one command.

# Installation
1. Clone the repo
2. Open the soluion, build
3. Open `appsettings.json` and add your application to `Applications` section
4. Open `Environment Variables` and select `Path` variable
5. Add the full phyiscal path of `DotCoreRunner.exe` directory to Path environment variable
   - **Don't include the file name**
   - _you may need to restart your machine_.
6. Open cmd/powershell and type dotcorerunner <application_name> <project_name>
   - application_name : the application name is the key name in your appsettings.json file
   - project_name : the name of your .net core project (.csproj)
   - if you didn't specify the application_name, a list of defined application in appsettings.json file will be displayed.
7. Enjoy
