using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using Lncodes.Module.Collecter;
using System.Collections.Generic;

namespace Lncodes.Pacakge.Vlecis.Reader
{
    internal sealed class VlecisReader
    {
        private readonly Dictionary<string, string> _rawData;
        private readonly VlecisReaderFormater _vlecisReaderFormarter;
        private readonly CollectionConverterFacade _collectionConverterFacade = new CollectionConverterFacade();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="delimiter"></param>
        /// <param name="csvFile"></param>
        internal VlecisReader(string delimiter, string csvFile)
        {
            _vlecisReaderFormarter = new VlecisReaderFormater(delimiter);
            _rawData = _vlecisReaderFormarter.FormatFile(csvFile);
        }

        /// <summary>
        /// Function to row value of the variabel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        internal object ReadRowValue<T>(T instance, FieldInfo info) where T : class
        {
            if (info.IsDefined(typeof(IgnoreAttribute))) return default;
            var fieldType = info.FieldType;
            var infoValue = info.GetValue(instance);
            var formatedRow = _vlecisReaderFormarter.FormatRow(_rawData[info.Name]);
            switch (infoValue)
            {
                case IEnumerable _ when !(infoValue is string || infoValue is IDictionary):
                    return _collectionConverterFacade.ConvertRegulerCollection(formatedRow, fieldType);
                case IDictionary _:
                    var rowDictionary = _vlecisReaderFormarter.FormatDictionaryValue(formatedRow);
                    return _collectionConverterFacade.ConvertDictionaryCollection(rowDictionary, fieldType);
                default:
                    return Convert.ChangeType(formatedRow.First(), fieldType);
            }
        }
    }
}
