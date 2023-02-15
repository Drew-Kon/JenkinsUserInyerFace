using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTAPITASK.Util
{
    public class ReadUtils
    {
        private static readonly string DataPath = AppDomain.CurrentDomain.BaseDirectory;
        public static T ReadDataFromFile<T>(string filepath)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText($"{DataPath}{filepath}"));
        }
    }
}