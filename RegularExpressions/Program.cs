using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace RegularExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:1\1.txt";
            string moveFilePath = @"D:\Backup";

            DirectoryInfo moveFileDir = new DirectoryInfo(moveFilePath);
            if (!moveFileDir.Exists)
            {
                moveFileDir.Create();
            }

            ILog consoleLog = new ConsoleLog();
            FileScaner file = new FileScaner(filePath, moveFilePath, consoleLog);
            file.PrintFileInfo();

            bool isExist = file.IsExistOtherSymbols();

            if (isExist)
            {
                Console.WriteLine("Press Y to save changes or N - to leave as is");
                string selection = Console.ReadLine();
                switch (selection)
                {
                    case "Y":
                        file.MoveFile();
                        file.SaveChanges();
                        Console.WriteLine("The fail was changed");
                        break;
                    case "N":
                        Console.WriteLine("The fail was not changed");
                        break;
                    default:
                        Console.WriteLine("you press unknown letter");
                        break;
                }
            }
        }
    }
}
