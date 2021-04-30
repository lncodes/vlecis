using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lncodes.Pacakge.Vlecis
{
    internal sealed class VlecisReaderFormater
    {
        private readonly string _delimiter;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="delimiter"></param>
        internal VlecisReaderFormater(string delimiter) => _delimiter = delimiter;

        /// <summary>
        /// Function to format csv file
        /// </summary>
        /// <param name="csvFile"></param>
        /// <returns>return variabel name and the value</returns>
        internal Dictionary<string, string> FormatFile(string csvFile)
        {
            var csvData = csvFile.Split(new[] { '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var dictionary = new Dictionary<string, string>();
            foreach (var item in csvData)
            {
                var splitedRow = item.Split(_delimiter, 2);
                dictionary.Add(splitedRow[0], splitedRow[1]);
            }
            return dictionary;
        }

        /// <summary>
        /// Method to formating the row 
        /// </summary>
        /// <param name="csvRow"></param>
        /// <returns></returns>
        internal IEnumerable<string> FormatRow(string csvRow)
        {
            var format = $"(?:^|{_delimiter})(\"(?:[^\"])*\"|[^{_delimiter}]*)";
            return Regex.Split(csvRow, format)
                .Where(val => !string.IsNullOrEmpty(val))
                .Select(val => val.Trim('"'));
        }

        /// <summary>
        /// Method to format csv data that has dictionary type
        /// </summary>
        /// <param name="csvValue"></param>
        /// <returns></returns>
        internal Dictionary<string, string> FormatDictionaryField(IEnumerable<string> csvValue)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var item in csvValue)
            {
                var row = item.Split($"{_delimiter} ");
                dictionary.Add(row.First().Trim('[').Trim(), row.Last().Trim(']').Trim());
            }
            return dictionary;
        }
    }
}
