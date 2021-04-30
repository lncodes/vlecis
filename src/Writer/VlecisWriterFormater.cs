namespace Lncodes.Pacakge.Vlecis
{
    internal sealed class VlecisWriterFormater
    {
        private readonly string _delimiter;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="delimiter"></param>
        internal VlecisWriterFormater(string delimiter) => _delimiter = delimiter;

        /// <summary>
        /// Method to format header
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        internal string FormatHeader<T>(T name) =>
            $"{name}{_delimiter}";

        /// <summary>
        /// MEthod to format  variabel normal value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        internal string FormatNormalField<T>(T value) =>
            $"\"{value}\"{_delimiter}";

        /// <summary>
        /// Method to format dictionary value
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal string FormatDictionaryField<TKey, TValue>(TKey key, TValue value) =>
            $"\"[{key}, {value}]\"{_delimiter}";
    }
}
