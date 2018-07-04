using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static import.ImportStretegy.ImportContext;

namespace import.ImportStretegy.Strategies
{
    public class CapterraStrategy : IImportStretegy
    {
        bool _isVerbouse;
        string _filePath;
        string _cmdArg;

        public string Command => "capterra";

        public CapterraStrategy(string[] args)
        {
            _filePath = ImportContext.GetCommand(args, "-f");
            _cmdArg = ImportContext.GetCommand(args, "-c");
            _isVerbouse = ImportContext.HasFlag(args, "-v");
        }

        //public IEnumerable<object> Import()
        public void Import(Action<dynamic> callback)
        {
            Console.WriteLine("Importing Capterra");

            if (string.IsNullOrEmpty(_filePath) || !File.Exists(_filePath))
            {
                Console.WriteLine("ERROR: Unexisting file path");
                return;
            }

            var fields = new string[] { "tags:", "name:", "twitter:" };

            using (var reader = File.OpenText(_filePath))
            {
                var newObject = new string[3] { "", "", "" }; //array index [0-2] where 0: tags, 1:name, 2:twitter;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().TrimStart().TrimEnd();
                    if (line == "-")
                    {
                        if (newObject.Any(s => !string.IsNullOrEmpty(s)))
                        {
                            // parse valid object
                            callback(Processing(newObject));
                        }

                        newObject = new string[3] { "", "", "" };
                        //if(_isVerbouse)
                        Console.WriteLine("\nBegin Object: ");
                    }
                    else
                    {
                        Enumerable.Range(0, fields.Length)
                            .ToList()
                            .ForEach(ind => IfThenElse(line.Contains(fields[ind]), () => newObject[ind] = line.Replace(fields[ind], "").TrimEnd().TrimStart(), Noop));
                    }
                }

                callback(Processing(newObject));
            }
        }

        public void PrintHelp()
        {
            Console.WriteLine("\n ### Capterra: ###");
            Console.WriteLine(" $> -c capterra -f filepath \t WHERE -c: capterra -f: file path to parse ");
            Console.WriteLine(" $> -v || verbosity on the import");
        }

        public bool CanImport() => _cmdArg == this.Command;

        Capterra Processing(string[] arrayObject)
        {
            // Parsing to a valid typed object
            var newCapterra = new Capterra { Tags = arrayObject[0], Name = arrayObject[1], Twitter = arrayObject[2] };
            //if (_isVerbouse)
            Console.WriteLine($"Idratiating obj => {newCapterra.ToString()}");

            return newCapterra;
        }


        Action Noop = () => { };

        void IfThenElse(bool eval, Action then, Action otherwise)
        {
            if (eval)
                then();
            else
                otherwise();
        }

        public class Capterra
        {
            public string Tags { get; set; }
            public string Name { get; set; }
            public string Twitter { get; set; }

            public override string ToString()
            {
                return $"Tags:{Tags}, Name:{Name}, Twitter:{Twitter}";
            }
        }
    }
}
