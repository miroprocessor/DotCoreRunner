# DotCoreRunner
.net application to run other .net core web application.

As your .Net solution keep grouwing and contains many projects you want to run. Instead of run each project manually or creating shortcuts
for each this will allow you to run different and many instances of your .Net core projects with one command.

# Installation
1. Clone the repo
2. Open the soluion, build
3. Open `appsettings.json` and add your application to `Applications` section
4. Open `Environment Variables` and select `Path` variable
5. Add the full phyiscal path of `DotCoreRunner.exe` to Path environment variable
6. Open cmd/powershell and type dotcorerunner 
7. Enjoy
