using System;
using System.IO;
using System.Threading.Tasks;
using Lncodes.Pacakge.Vlecis.Reader;
using Lncodes.Pacakge.Vlecis.Writer;
using Lncodes.Pacakge.Vlecis.Configuration;

namespace Lncodes.Pacakge.Vlecis
{
    public sealed class Vlecis<T> where T : class
    {
        internal readonly VlecisConfig _baseConfig;

        public Vlecis(VlecisConfig baseConfig) => _baseConfig = baseConfig;

        /// <summary>
        /// Method used for export csv file from some instance
        /// </summary>
        /// <param name="instance"></param>
        public void ExportFrom(T instance)
        {
            using (var streamWriter = new StreamWriter(_baseConfig.FilePath))
            {
                var _vlecisWriter = new VlecisWriter(_baseConfig.Delimiter);
                Parallel.ForEach(instance.GetType().GetFields(_baseConfig.BindingFlags), info =>
                    _vlecisWriter.WriteRow(instance, info)
                );
                streamWriter.WriteAsync(_vlecisWriter.Result);
            }
        }

        /// <summary>
        /// Method to import csv file into some class instance
        /// </summary>
        /// <param name="instance"></param>
        public void ImportTo(T instance)
        {
            if (!File.Exists(_baseConfig.FilePath)) throw new Exception("Data Not Found");
            using (var streamReader = new StreamReader(_baseConfig.FilePath))
            {
                var _vlecisReader = new VlecisReader(_baseConfig.Delimiter, streamReader.ReadToEnd());
                Parallel.ForEach(instance.GetType().GetFields(_baseConfig.BindingFlags), info => {
                    var value = _vlecisReader.ReadRowValue(instance, info);
                    if (value is null) return;
                    info.SetValue(instance, value);
                });
            }
        }
    }
}