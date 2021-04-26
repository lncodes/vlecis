using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Lncodes.Module.Collecter
{
    internal sealed class RegulerCollectionConverter
    {
        /// <summary>
        /// Function to convert an collection
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="data"></param>
        /// <param name="convertedCollectionType"></param>
        /// <returns>New collection</returns>
        internal IEnumerable ConvertCollection<TValue>(IEnumerable<TValue> data, Type convertedCollectionType)
        {
            if (convertedCollectionType.Equals(typeof(string)) || !(convertedCollectionType.GetInterface(nameof(IDictionary)) is null))
                throw new ArgumentException("Cant convert string or dictionary types");
            var collectionIndex = 0;
            var valueType = GetCollectionValueTypes(convertedCollectionType);
            var newCollectionInstance = Array.CreateInstance(valueType, data.Count());
            foreach (var item in data)
                newCollectionInstance.SetValue(Convert.ChangeType(item, valueType), collectionIndex++);
            if (convertedCollectionType.IsArray) return newCollectionInstance;
            return (IEnumerable)Activator.CreateInstance(convertedCollectionType, newCollectionInstance);
        }

        /// <summary>
        /// Function to get collection value types
        /// </summary>
        /// <param name="collectionType"></param>
        /// <returns>Type of collection</returns>
        private Type GetCollectionValueTypes(Type collectionType)
        {
            if (collectionType.IsArray)
                return collectionType.GetElementType();
            else if (collectionType.IsGenericType)
                return collectionType.GetGenericArguments()[0];
            else return typeof(object);
        }
    }
}
