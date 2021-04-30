using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using Lncodes.Module.Collecter;
using System.Collections.Generic;

namespace Lncodes.Pacakge.Vlecis
{
    internal sealed class VlecisReader
    {
        private readonly Dictionary<string, string> _rawVariabelData;
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
            _rawVariabelData = _vlecisReaderFormarter.FormatFile(csvFile);
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
            var fieldValue = info.GetValue(instance);
            var nameAttribute = info.GetCustomAttribute<NameAttribute>();
            var fieldName = nameAttribute is null ? info.Name : nameAttribute.Name;
            var rawCsvFieldValue = _vlecisReaderFormarter.FormatRow(_rawVariabelData[fieldName]);
            switch (fieldValue)
            {
                case IEnumerable _ when !(fieldValue is string || fieldValue is IDictionary):
                    return _collectionConverterFacade.ConvertRegulerCollection(rawCsvFieldValue, fieldType);
                case IDictionary _:
                    var rowDictionary = _vlecisReaderFormarter.FormatDictionaryField(rawCsvFieldValue);
                    return _collectionConverterFacade.ConvertDictionaryCollection(rowDictionary, fieldType);
                default:
                    return Convert.ChangeType(rawCsvFieldValue.First(), fieldType);
            }
        }
    }
}