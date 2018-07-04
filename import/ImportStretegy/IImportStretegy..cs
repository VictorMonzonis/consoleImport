using System;
using System.Collections.Generic;
using System.Text;
using static import.ImportStretegy.ImportContext;
using static import.ImportStretegy.Strategies.CapterraStrategy;

namespace import.ImportStretegy
{
    public interface IImportStretegy
    {

        /// <summary>
        /// Determines whether this instance can import.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can import; otherwise, <c>false</c>.
        /// </returns>
        bool CanImport();

        /// <summary>
        /// Imports data from a generic source.
        /// </summary>
        void Import(Action<dynamic> callback);

        /// <summary>
        /// Prints the help.
        /// </summary>
        void PrintHelp();
    }
}
