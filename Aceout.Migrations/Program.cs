using DbUp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Aceout.Migrations
{
    public class Program
    {
        static void Main(string[] args)
        {
            UpdateDb(args[0], args[1]);

            //var upgrader = DeployChanges.To.MySqlDatabase(connectionString)
            //    .WithScriptsAndCodeEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            //    .LogToConsole()
            //    .Build();

            //var result = upgrader.PerformUpgrade();

            //if (!result.Successful)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine(result.Error);
            //    Console.ResetColor();
            //    return;
            //}

            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("DbUp Done!");
            //Console.ResetColor();
        }

        public static void UpdateDb(string path, string enviroment)
        {
            var filePath = Path.Combine(path, $"aceout.{enviroment}.json");
            var content = File.ReadAllText(filePath);

            var config = JsonConvert.DeserializeObject<dynamic>(content)["AppSettings"];

            var type = (string)config.Database["Type"];
            var connectionString = (string)config.Database["ConnectionString"];

            var result = DeployChanges.To.MySqlDatabase(connectionString)
            .WithScriptsAndCodeEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .WithTransactionPerScript()
            .LogToConsole()
            .Build()
            .PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("DbUp Done!");
                Console.ResetColor();
            }
        }

    }

}
