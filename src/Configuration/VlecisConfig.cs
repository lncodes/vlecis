using System.Reflection;
using Lncodes.Module.Fathgen;

namespace Lncodes.Pacakge.Vlecis
{
    public sealed class VlecisConfig
    {
        internal readonly string FilePath;
        internal readonly string Delimiter = ";";
        internal readonly BindingFlags BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePathGenerator"></param>
        public VlecisConfig(FilePathGenerator<CsvFileExtension> filePathGenerator) =>
           FilePath = filePathGenerator.Result;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePathGenerator"></param>
        /// <param name="delimiter"></param>
        public VlecisConfig(FilePathGenerator<CsvFileExtension> filePathGenerator, string delimiter) =>
            (FilePath, Delimiter) = (filePathGenerator.Result, delimiter);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePathGenerator"></param>
        /// <param name="delimiter"></param>
        /// <param name="bindingFlags"></param>
        public VlecisConfig(FilePathGenerator<CsvFileExtension> filePathGenerator, string delimiter, BindingFlags bindingFlags) =>
            (FilePath, Delimiter, BindingFlags) = (filePathGenerator.Result, delimiter, bindingFlags);
    }
}