using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using start;
using static api.AccountAuth;

namespace Rec_rewild.api
{
    public class player_config
    {
        public class playerSetting
        {
            public int AccountId { get; set; }
            public string Username { get; set; }
            public string DisplayName { get; set; }
            public string Bio { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime Birthday { get; set; }
            public string Email { get; set; }
            public int Level { get; set; }
            public int XP { get; set; }
            public Balances Balances { get; set; } = new Balances();
            public List<RoomKey> Roomkeys { get; set; } = new List<RoomKey>();
            public Rep Reputation { get; set; } = new Rep();
            public string ProfileImage { get; set; }
            public string BannerImage { get; set; }
            public bool IsJunior { get; set; }
            public bool IsDeveloper { get; set; }
            public DateTime? LastLogin { get; set; } = null;
            public List<int> recent_rooms { get; set; } = new();
            public List<int> cheered_rooms { get; set; } = new();
            public List<int> favorited_rooms { get; set; } = new();
        }

        public class Balances
        {
            public int Tokens { get; set; }
            public int Tickets { get; set; }
            public int Gold { get; set; }
            public int Silver { get; set; }
            public int RecRoyale_Season1 { get; set; }

            public List<RoomCurrency> RoomCurrency { get; set; } = new List<RoomCurrency>();
        }

        public class RoomCurrency
        {
            public int room_id { get; set; }
            public List<CurrencyItem> Currency { get; set; } = new List<CurrencyItem>();
        }

        public class CurrencyItem
        {
            public string id { get; set; } = "";
            public int Balance { get; set; }
        }

        public class RoomKey
        {
            public string id { get; set; } = "";
            public bool have_key { get; set; }
        }

        public static void save_setting()
        {
            File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(_setting));
        }
        public static void load_setting()
        {
            try
            {
                if (!File.Exists(SettingsPath))
                {
                    Setup.setup_profile(); 
                }

                _setting = JsonConvert.DeserializeObject<playerSetting>(File.ReadAllText(SettingsPath));
            }
            catch
            {
                Setup.setup_profile();
                _setting = JsonConvert.DeserializeObject<playerSetting>(File.ReadAllText(SettingsPath));
            }
        }


        public static string SettingsPath = "SaveData/Profile/player.json";

        public static playerSetting _setting;

        internal static playerSetting Setting
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