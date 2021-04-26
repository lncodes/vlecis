using System;

namespace Lncodes.Module.Fathgen
{
    public struct FilePathGenerator<T> where T : Enum
    {
        internal readonly string Result;

        /// <summary>
        /// Constructor for the file path generator
        /// </summary>
        /// <param name="name"></param>
        /// <param name="format"></param>
        public FilePathGenerator(string name, T format) =>
            Result = $"{AppContext.BaseDirectory}/{name}.{format}";

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="format"></param>
        public FilePathGenerator(string path, string name, T format) =>
            Result = $"{path}/{name}.{format}";
    }
}