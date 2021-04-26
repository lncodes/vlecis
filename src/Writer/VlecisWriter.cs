using System;
using System.Text;
using System.Reflection;
using System.Collections;

namespace Lncodes.Pacakge.Vlecis.Writer
{
    internal sealed class VlecisWriter
    {
        private readonly VlecisWriterFormater _vlecisWriterFormater;

        /// <summary>
        /// constructor
        /// </summary>
        internal StringBuilder Result { get; } = new StringBuilder();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="delimiter"></param>
        internal VlecisWriter(string delimiter) =>
            _vlecisWriterFormater = new VlecisWriterFormater(delimiter);

        /// <summary>
        /// To write row of csv file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="info"></param>
        internal void WriteRow<T>(T instance, FieldInfo info) where T : class
        {
            if (!info.IsDefined(typeof(IgnoreAttribute)))
            {
                var builder = new StringBuilder();
                WriteName(info, ref builder);
                WriteValue(info.GetValue(instance), ref builder);
                Result.Append(builder.AppendLine());
            }
        }

        /// <summary>
        /// To write variabel name
        /// </summary>
        /// <param name="info"></param>
        /// <param name="stringBuilder"></param>
        private void WriteName(MemberInfo info, ref StringBuilder stringBuilder)
        {
            var nameAttribute = (NameAttribute)Attribute.GetCustomAttribute(info, typeof(NameAttribute));
            if (nameAttribute is null) stringBuilder.Append(_vlecisWriterFormater.FormatHeader(info.Name));
            else stringBuilder.Append(_vlecisWriterFormater.FormatHeader(nameAttribute.Name));
        }
        
        /// <summary>
        /// To write variabel value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="stringBuilder"></param>
        private void WriteValue(object value, ref StringBuilder stringBuilder)
        {
            switch (value)
            {
                case IEnumerable _ when !(value is string || value is IDictionary):
                    foreach (var item in value as IEnumerable)
                        stringBuilder.Append(_vlecisWriterFormater.FormatNormalValue(item));
                    break;
                case IDictionary _:
                    foreach (DictionaryEntry item in value as IDictionary)
                        stringBuilder.Append(_vlecisWriterFormater.FormatDictionaryValue(item.Key, item.Value));
                    break;
                default:
                    stringBuilder.Append(_vlecisWriterFormater.FormatNormalValue(value));
                    break;
            }
        }
    }
}
