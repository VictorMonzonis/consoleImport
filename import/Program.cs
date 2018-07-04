using System;
using import.ImportStretegy;

namespace import
{
    class Program
    {
        static void Main(string[] args)
        {
            var importContext = new ImportContext(args);

            var helpCmd = new[] { "-h", "-help", "--help" };
            if (ImportContext.HasFlag(args, "-h") ||
                    ImportContext.HasFlag(args, "-help") || 
                    ImportContext.HasFlag(args, "--help"))
            {
                importContext.PrintHelp();
                return;
            }

            Console.WriteLine("Start importing...");
            importContext.Import();
            Console.WriteLine("Importing finished!");
            return;
        }
    }
}

