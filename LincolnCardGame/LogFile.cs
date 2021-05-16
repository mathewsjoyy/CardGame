using System;
using System.IO;

namespace LincolnCardGame
{
    internal class LogFile : IDisplayable
    {
        // Fields
        public string FileName { get; private set; }

        // Generate a text log file with a unique name
        public LogFile()
        {
            string fileName = "CardGameLog" + Guid.NewGuid().ToString().Substring(0, 6) + ".txt";

            FileName = fileName;

            // Create a file to write to in bin > debug
            using (StreamWriter sw = File.CreateText(FileName))
            {
                sw.WriteLine($"= Lincoln Card Game Log - {DateTime.Now:dd-MM-yyyy}");
            }

            LogFileInfo();
        }

        public void LogFileInfo()
        {
            Console.WriteLine("(Game Log File Has Been Generated To Access The File " +
                $"Visit Bin > Debug > {FileName})");
        }

        // Adds a message to log file
        public void AddLogMessage(string message)
        {
            if (File.Exists(FileName))
            {
                using (StreamWriter sw = File.AppendText(FileName))
                {
                    sw.WriteLine($"{DateTime.Now:hh:mm:ss} - {message}");
                }
            }
            else
            {
                Console.WriteLine($"=== Looks Like Log File {FileName} Doesn't Exist (Check Bin > Debug) ===");
            }
        }

        // Open the file to read from and return its contents
        public void Display()
        {
            string readText = File.ReadAllText(FileName);
            Console.WriteLine(readText);
        }
    }
}
