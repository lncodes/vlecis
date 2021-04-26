using System;
using System.Collections;
using System.Collections.Generic;

namespace Lncodes.Module.Collecter
{
    internal sealed class KeyValuePairCollcetionConverter
    {
        /// <summary>
        /// Function to convert dictionary collection
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="data"></param>
        /// <param name="convertedCollectionType"></param>
        /// <returns></returns>
        internal IDictionary ConvertKeyValuePair<TKey, TValue>(IDictionary<TKey, TValue> data, Type convertedCollectionType)
        {
            if (convertedCollectionType.GetInterface(nameof(IDictionary)) is null)
                throw new ArgumentException("Only can convert key value pair collection types");
            var valueType = GetCollectionValueTypes(convertedCollectionType);
            var dictionary = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(valueType));
            foreach (var item in data)
                dictionary.Add(Convert.ChangeType(item.Key, valueType[0]), Convert.ChangeType(item.Value, valueType[1]));
            return (IDictionary)Activator.CreateInstance(convertedCollectionType, dictionary);
        }

        /// <summary>
        /// Function to get collection value types
        /// </summary>
        /// <param name="collectionType"></param>
        /// <returns>Types that assign by the collection type</returns>
        private Type[] GetCollectionValueTypes(Type collectionType)
        {
            if (collectionType.IsGenericType)
                return collectionType.GetGenericArguments();
            return new[] { typeof(object), typeof(object) };
        }
    }
}
