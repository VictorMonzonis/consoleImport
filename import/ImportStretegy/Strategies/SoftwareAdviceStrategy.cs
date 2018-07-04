using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static import.ImportStretegy.ImportContext;
using static import.ImportStretegy.Strategies.CapterraStrategy;

namespace import.ImportStretegy.Strategies
{
    public class SoftwareAdviceStrategy : IImportStretegy
    {
        bool _isVerbouse;
        string _filePath;
        string _cmdArg;

        public string Command => "softadvice";

        public SoftwareAdviceStrategy(string[] args)
        {
            _filePath = ImportContext.GetCommand(args, "-f");
            _cmdArg = ImportContext.GetCommand(args, "-c");
            _isVerbouse = ImportContext.HasFlag(args, "-v");
        }

        public void Import(Action<dynamic> callback)
        {
            if (string.IsNullOrEmpty(_filePath) || !File.Exists(_filePath))
            {
                Console.WriteLine("ERROR: Unexisting file path");
                return;
            }

            using (var reader = new StreamReader(_filePath))
            {
                var dto = reader.ReadToEnd();
                var products = JsonConvert.DeserializeObject<SoftwareAdvice>(dto);
                callback(products);

                products.Products.ToList().ForEach(p => Console.WriteLine($"Process {p.ToString()}"));
            }
        }

        public void PrintHelp()
        {
            Console.WriteLine("\n ### SoftAdvice: ###");
            Console.WriteLine(" $> -c softadvice -f filepath \t WHERE -c: softadvice -f: file path to parse ");
            Console.WriteLine(" $> -v || verbosity on the import");
        }

        public bool CanImport() => _cmdArg == this.Command;

        public class SoftwareAdvice
        {
            public SoftwareAdviceProducts[] Products { get; set; }
        }

        public class SoftwareAdviceProducts
        {
            public string[] Categories { get; set; }

            public string Twitter { get; set; }

            public string Title { get; set; }

            public override string ToString()
            {
                return $"Twitter {Twitter},  Title {Title}";
            }
        }
    }
}
