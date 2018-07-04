using import.ImportStretegy.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace import.ImportStretegy
{
    public class ImportContext
    {
        Dictionary<ImportSelectors, IImportStretegy> _strategies;

        public ImportContext(string[] args)
        {
            _strategies = new Dictionary<ImportSelectors, IImportStretegy>();

            _strategies.Add(ImportSelectors.Capterra, new CapterraStrategy(args));
            _strategies.Add(ImportSelectors.SoftwareAdvice, new SoftwareAdviceStrategy(args));
        }

        public void Import()
        {
            int count = 0;
            foreach(var strategy in _strategies.Values)
            {
                if (strategy.CanImport())
                {
                    strategy.Import((_)=> { });
                    count++;
                }
            }

            if (count == 0)
                Console.WriteLine("ERROR: Invalid command passed");
        }

        public void PrintHelp()
        {
            _strategies.Values.ToList().ForEach(strategy => strategy.PrintHelp());
        }

        /// <summary>
        /// Gets the argument.
        /// e.g. 
        /// if args is "any -c comd01"
        ///   GetArgs(args, "-c") //comd01 as return
        ///   GetArgs(args, "-l") //empry string as return
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="cmd">The command.</param>
        /// <returns>The command value.</returns>
        public static string GetCommand(string[] args, string cmd)
        {
            var cmdIndex = Enumerable.Range(0, args.Length)
                .Where(i => args[i].ToLower() == cmd).Select(i => i).DefaultIfEmpty(-1).First();

            if (cmdIndex < 0)
                return string.Empty;

            return args[cmdIndex + 1];
        }

        /// <summary>
        /// Withes the flag.
        /// e.g. 
        /// if args is "any -v"
        ///    WithFlag(args, "-v")  // it return true
        ///    WithFlag(args, "-f")  // it return false
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="flag">The flag.</param>
        /// <returns></returns>
        public static bool HasFlag(string[] args, string flag) =>
            args.Any(arg => arg.ToLower() == flag);
    }
}
