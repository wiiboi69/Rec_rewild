using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Data;

namespace api
{
    class roomdownloader
    {
        public static bool room_find(string data, int skip_int = 0, int take_int = 0)
        {
            listRoot<RoomRoot> rooms = JsonConvert.DeserializeObject<listRoot<RoomRoot>>(data);
            if (take_int == 0)
            {
                take_int = rooms.Results.Count;
            }
            take_int = rooms.Results.Count;

            string input = "";
            int skip = 0;
            int take = 9;
            string data_1 = "";
            string data_2 = "";
            int skippage = 0;
            int maxpage = (int)Math.Floor(take_int / 10f);
        Refreshpage:
            Console.Clear();
        Refreshpage_2:
            Console.WriteLine($"┌─────────────────────────────┤ Room ├─────────────────────────────┐");

            skippage = (int)Math.Floor((take + 1) / 10f);
            if (take > take_int)
                take = take_int;
            string temp = "";
            string temp2 = "";
            for (int i = skip; i < take; i++)
            {
                temp = "";
                temp2 = "";

                RoomRoot root = rooms.Results[i];
                temp = $"│ [{i}]  {root.RoomId}   {root.Name}";
                for (int j = $"│ [{i}]  {root.RoomId}   {root.Name}".Length; j < 67; j++)
                {
                    temp2 = temp2 + " ";
                }
                temp = temp + temp2 + "│";

                Console.WriteLine(temp);

            }
            if (skippage > 1)
            {
                data_1 = "│ Prev ├";
            }
            else
            {
                data_1 = "├───────";
            }

            if (skippage < maxpage)
            {
                data_2 = "┤ Next │";
            }
            else
            {
                data_2 = "───────┤";
            }
            Console.WriteLine($"{data_1}──────────────────────┤ Exit ├──────────────────────{data_2}");
            Console.WriteLine($"└─────────────────────────────┘ Page {skippage} / {maxpage}");
            while (true)
            {
                input = Console.ReadLine();
                if (input == "E")
                {
                    return false;
                }
                else if (input == "P")
                {
                    if (skippage > 1)
                    {
                        skip -= 10;
                        take -= 10;
                        goto Refreshpage;
                    }

                }
                else if (input == "N")
                {
                    if (skippage < maxpage)
                    {
                        skip += 10;
                        take += 10;
                        goto Refreshpage;
                    }
                }
                else
                {
                    if (int.TryParse(input, out int value))
                    {
                        if (value >= 0 || value < take_int)
                        {
                            room_download(data, value);
                            return true;
                        }
                        else
                        {
                            goto error;
                        }
                    }
                    else
                    {
                        goto error;
                    }
                }
            }
        error:
            Console.Clear();
            Console.WriteLine($"{input} was not a valued int or out of range");
            goto Refreshpage_2;
        }
        /// <summary>
        /// this is used to get a rec net room, roomdata, and the info
        /// </summary>
        public static void room_download(string data, int value)
        {
            listRoot<RoomRoot> rooms = JsonConvert.DeserializeObject<listRoot<RoomRoot>>(data);
            RoomRoot roomRoot = rooms.Results[value];
            Console.WriteLine($"downloading room {roomRoot.Name}");
            string webdata = new WebClient().DownloadString("https://rooms.rec.net/rooms?name=" + roomRoot.Name + "&include=297");
            File.WriteAllText($"savedata\\rooms\\Downloaded\\{roomRoot.Name}\\room.json", webdata);
            RoomRoot room = JsonConvert.DeserializeObject<RoomRoot>(webdata);
            foreach (SubRooms subrooms in room.SubRooms)
            {
                if (subrooms.CurrentSave is null)
                    continue;
                if (subrooms.CurrentSave.DataBlob is null)
                    continue;
                Console.WriteLine($"downloading subroom data {subrooms.Name} datablob: {subrooms.CurrentSave.DataBlob} created at {subrooms.CurrentSave.CreatedAt}");

                File.WriteAllBytes($"savedata\\rooms\\cdn\\{subrooms.CurrentSave.DataBlob}",
                    new WebClient().DownloadData("https://cdn.rec.net/room/" + subrooms.CurrentSave.DataBlob)
                    );
            }
            Console.WriteLine($"downloaded room {roomRoot.Name}");
        }

        public class listRoot<T>
        {
            public List<T> Results { get; set; }
            public ulong TotalResults { get; set; }
        }

        public class RoomRoot
        {
            public ulong RoomId { get; set; }
            public bool IsDorm { get; set; }
            public int MaxPlayerCalculationMode { get; set; }
            public int MaxPlayers { get; set; }
            public bool CloningAllowed { get; set; }
            public bool DisableMicAutoMute { get; set; }
            public bool DisableRoomComments { get; set; }
            public bool EncryptVoiceChat { get; set; }
            public bool ToxmodEnabled { get; set; }
            public bool LoadScreenLocked { get; set; }
            public int PersistenceVersion { get; set; }
            public bool AutoLocalizeRoom { get; set; }
            public bool IsDeveloperOwned { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImageName { get; set; }
            public WarningMaskType WarningMask { get; set; }
            public string CustomWarning { get; set; }
            public ulong CreatorAccountId { get; set; }
            public int State { get; set; }
            public int Accessibility { get; set; }
            public bool SupportsLevelVoting { get; set; }
            public bool IsRRO { get; set; }
            public bool SupportsScreens { get; set; }
            public bool SupportsWalkVR { get; set; }
            public bool SupportsTeleportVR { get; set; }
            public bool SupportsVRLow { get; set; }
            public bool SupportsQuest2 { get; set; }
            public bool SupportsMobile { get; set; }
            public bool SupportsJuniors { get; set; }
            public int MinLevel { get; set; }
            public string CreatedAt { get; set; }
            public Stats Stats { get; set; }
            public string? RankedEntityId { get; set; }
            public string? RankingContext { get; set; }
            public List<SubRooms> SubRooms { get; set; }
            public List<Roles> Roles { get; set; }
            public string? DataBlob { get; set; }
            public int UgcVersion { get; set; }
            public int Version { get; set; }
            public List<Tags> Tags { get; set; }
            public List<Dummy> PromoImages { get; set; }
            public List<Dummy> PromoExternalContent { get; set; }
            public List<LoadScreens> LoadScreens { get; set; }
        }

        [Flags]
        public enum WarningMaskType
        {
            None = 0,
            Scary = 1,
            Mature = 2,
            FlashingLights = 4,
            IntenseMotion = 8,
            Violence = 16,
            Custom = 32,
            Reports = 64
        }

        public class Dummy
        {

        }
        public class Stats
        {
            public int CheerCount { get; set; }
            public int FavoriteCount { get; set; }
            public int VisitorCount { get; set; }
            public int VisitCount { get; set; }
        }
        public class Tags
        {
            public string Tag { get; set; }
            public int Type { get; set; }
        }
        public class LoadScreens
        {
            public string ImageName { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }
        }
        public class Roles
        {
            public int AccountId { get; set; }
            public int Role { get; set; }
            public int InvitedRole { get; set; }
        }
        //CurrentSave	
        public class CurrentSave
        {
            public string? DataBlob { get; set; }
            public DateTime CreatedAt { get; set; }

        }
        public class SubRooms
        {
            public ulong SubRoomId { get; set; }
            public ulong RoomId { get; set; }
            public string Name { get; set; }
            public CurrentSave? CurrentSave { get; set; }
            public bool IsSandbox { get; set; }
            public int MaxPlayers { get; set; }
            public int Accessibility { get; set; }
            public string UnitySceneId { get; set; }
            public ulong SavedByAccountId { get; set; }
        }
    }
}