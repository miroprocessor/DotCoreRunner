using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static System.Console;

namespace DotCoreRunner
{
    internal class Program
    {
        private static IConfigurationRoot config;

        private static IEnumerable<IConfigurationSection> Applications => config.GetSection("Applications").GetChildren();
        private static string AppName { get; set; }
        private static string ProjectName { get; set; }

        private static List<string> Projects => GetAvailableProjects();

        private static IConfigurationSection AppToRun => Applications.Single(x => x.Key.ToLower() == AppName.ToLower());

        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json");

            config = builder.Build();
            Settings(args);
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = @"cmd.exe";
            startInfo.Arguments = @$"/k dotnet run --project {AppToRun.Value}\{ProjectName}";
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        private static string GetAppToRun()
        {
            WriteLine("Available Applications");
            WriteLine("**********************");
            Applications.ToList().ForEach(x => WriteLine($"- {x.Key}"));
            Write("Enter Appliction Name : ");
            return ReadLine();
        }

        private static string GetProjectsToRun()
        {
            WriteLine("Available .Net Projects");
            WriteLine("***********************");
            Projects.ForEach(x => WriteLine($"- {x.Split(Path.DirectorySeparatorChar).Last()}"));
            Write("Enter Project Name : ");
            return ReadLine();
        }

        private static void Settings(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                AppName = GetAppToRun();
            }
            else
            {
                AppName = args[0];
            }
            if (!Applications.Any(x => x.Key.ToLower() == AppName.ToLower()))
            {
                throw new Exception("Not recognized application");
            }

            if (args.Length > 1)
            {
                ProjectName = args[1];
                ValidateProjectName();
            }
            else
            {
                ProjectName = GetProjectsToRun();
                ValidateProjectName();
            }
        }

        private static void ValidateProjectName()
        {
            if (!Projects.Contains(ProjectName))
            {
                if (Projects.Any(x => x.ToLower().EndsWith($".{ProjectName.ToLower()}")))
                {
                    ProjectName = Projects.Single(x => x.ToLower().EndsWith($".{ProjectName.ToLower()}"));
                }
                else
                {
                    throw new Exception("invalid project name");
                }
            }
        }

        private static List<string> GetAvailableProjects()
        {
            var projectsFullPaths = Directory.GetDirectories(Applications.Single(x => x.Key.ToLower() == AppName.ToLower()).Value)
                                                .Where(x => Directory.GetFiles(x).Any(y => y.EndsWith(".csproj"))).ToList();

            return projectsFullPaths.Select(x => Path.GetFileName(x).ToLower()).ToList();
        }
    }
}