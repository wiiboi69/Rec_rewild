using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Rec_rewild.api
{
    internal class Avatar_Item_Util
    {
        public static string inject_AvatarItem_list(string s)
        {
            List<AvatarItem> AvatarItemlistdata = JsonConvert.DeserializeObject<List<AvatarItem>>(s);

            string[] AvatarItemdir = Directory.GetFiles("SaveData\\custom\\avatar items\\");
            foreach (string AvatarItemdata in AvatarItemdir)
            {
                try
                {
                    AvatarItem avataritem_data = JsonConvert.DeserializeObject<AvatarItem>(File.ReadAllText(AvatarItemdata));
                    AvatarItemlistdata.Add(avataritem_data);

                }
                catch
                {

                    List<AvatarItem> AvatarItem_dir_list = JsonConvert.DeserializeObject<List<AvatarItem>>(File.ReadAllText(AvatarItemdata));
                    foreach (AvatarItem AvatarItem_data_item in AvatarItem_dir_list)
                    {

                        AvatarItemlistdata.Add(AvatarItem_data_item);
                    }
                }
            }

            return JsonConvert.SerializeObject(AvatarItemlistdata);
        }

        public static string inject_gameconfig_list(string s)
        {
            List<gameconfig_item> AvatarItemlistdata = JsonConvert.DeserializeObject<List<gameconfig_item>>(s);
            AvatarItemlistdata.Add(new gameconfig_item
            {
                Key = "Debug.AdditionalLogFlags",
                Value = "CircuitsV2Lifecycle",
                ActiveExperiments = null
            });
            AvatarItemlistdata.Add(new gameconfig_item
            {
                Key = "UGC.AllowUnlistedInventions",
                Value = "1",
                ActiveExperiments = null
            });
            AvatarItemlistdata.Add(new gameconfig_item
            {
                Key = "UGC.InventionSavingEnabled",
                Value = "1",
                ActiveExperiments = null
            });
            AvatarItemlistdata.Add(new gameconfig_item
            {
                Key = "RRUI.MaxActiveHiddenPages",
                Value = "50",
                ActiveExperiments = null
            });
            AvatarItemlistdata.Add(new gameconfig_item
            {
                Key = "UGC.AllowNonBetaInvertedTubeCreation",
                Value = "true",
                ActiveExperiments = null
            });
            AvatarItemlistdata.Add(new gameconfig_item
            {
                Key = "UGC.RoomSavingEnabled",
                Value = "true",
                ActiveExperiments = null
            });
            AvatarItemlistdata.Add(new gameconfig_item
            {
                Key = "UGC.AllowBetaInvertedTubeCreation",
                Value = "true",
                ActiveExperiments = null
            });
            return JsonConvert.SerializeObject(AvatarItemlistdata);
        }

        public class gameconfig_item
        {
            public string Key { get; set; }
            public string Value { get; set; }
            public string ActiveExperiments { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
        }


        public class AvatarItem
        {
            public string AvatarItemDesc { get; set; }
            public int AvatarItemType { get; set; }
            public int PlatformMask { get; set; }
            public string FriendlyName { get; set; }
            public string Tooltip { get; set; }
            public AvatarItemRarity Rarity { get; set; }
        }

        public enum AvatarItemRarity
        {
            Star_0 = 0,
            Star_1 = 10,
            Star_2 = 20,
            Star_3 = 30,
            Star_4 = 40,
            Star_5 = 50,
        }
    }
}
