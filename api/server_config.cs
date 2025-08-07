using Newtonsoft.Json;
using start;
using System.IO;


namespace Rec_rewild.api
{
    class server_config
    {
        public class App_Setting
        {
            public bool ConsoleSound { get; set; }
        }

        public static void save_setting()
        {
            File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(_setting));
        }

        public static void Update_server_setting()
        {
            load_setting();
        }

        public static void load_setting()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    _setting = JsonConvert.DeserializeObject<App_Setting>(File.ReadAllText(SettingsPath));
                }
                else
                {
                    Setup.setup_server();
                    _setting = JsonConvert.DeserializeObject<App_Setting>(File.ReadAllText(SettingsPath));

                }
            }
            catch
            {
                Setup.setup_server();
                _setting = JsonConvert.DeserializeObject<App_Setting>(File.ReadAllText(SettingsPath));
            }
        }

        public static string SettingsPath = "SaveData/App/server_setting.json";

        public static App_Setting _setting;

        internal static App_Setting Setting
        {
            set
            {
                _setting = value;
                save_setting();
            }
            get
            {
                if (_setting == null)
                    load_setting();
                return _setting;
            }
        }
    }
}
