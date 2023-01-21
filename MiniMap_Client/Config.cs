using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Reflection;

namespace MiniMap_Client
{
    [Obfuscation(Exclude = true)]
    public class Config
    {
        public List<AppServersData> Servers { get; set; }

        private static string defaultconfig =
@"{
    'Servers': [
        {
            'Name': 'LocalHost',
            'IP': '127.0.0.1',
            'Port': '5890'
        }
    ]
}";
        [Obfuscation(Exclude = false, Feature = "preset(maximum);")]
        public static void Load(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    using (FileStream fs = File.OpenWrite(path))
                    {
                        Byte[] info =
                            new UTF8Encoding(true).GetBytes(defaultconfig);

                        fs.Write(info, 0, info.Length);

                        Globals.Settings = JsonConvert.DeserializeObject<Config>(defaultconfig);
                        return;
                    }
                }
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;
                using (StreamReader sr = new StreamReader(path))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    Globals.Settings = serializer.Deserialize<Config>(reader);
                }
            }
            catch (Exception ex)
            {
                Globals.Settings = JsonConvert.DeserializeObject<Config>(defaultconfig);
                MainWindow.ShowMessage("Error", "Ошибка загрузки конфига. Возможно у вас не хватает прав или файл поврежден. Загружен стандартный конфиг.");
            }
        }
        [Obfuscation(Exclude = false, Feature = "preset(maximum);")]
        public static bool Save(string path, Config settings)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.Formatting = Formatting.Indented;
                using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, settings);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
