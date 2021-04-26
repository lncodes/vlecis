using System;
using System.Collections;
using System.Collections.Generic;

namespace Lncodes.Module.Collecter
{
    public sealed class CollectionConverterFacade
    {
        private RegulerCollectionConverter _regulerCollectionConverter = new RegulerCollectionConverter();
        private KeyValuePairCollcetionConverter _dictionaryCollectionConverter = new KeyValuePairCollcetionConverter();

        /// <summary>
        /// Function to convert reguler collection
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="data"></param>
        /// <param name="convertedType"></param>
        /// <returns></returns>
        public IEnumerable ConvertRegulerCollection<TValue>(IEnumerable<TValue> data, Type convertedType) =>
           _regulerCollectionConverter.ConvertCollection(data, convertedType);

        /// <summary>
        /// Function to convert dictionary collection
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="data"></param>
        /// <param name="convertedType"></param>
        /// <returns></returns>
        public IDictionary ConvertDictionaryCollection<TKey, TValue>(IDictionary<TKey, TValue> data, Type convertedType) =>
            _dictionaryCollectionConverter.ConvertKeyValuePair(data, convertedType);
    }
}
