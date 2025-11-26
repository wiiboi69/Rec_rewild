using api;
using Newtonsoft.Json;
using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rec_rewild.api
{
    public static class RoomCache
    {
        public static Dictionary<ulong, RoomInfo> _roomMap = new();
        public static List<RoomInfo> _roomList = new();

        public static void Clear()
        {
            _roomMap.Clear();
            _roomList.Clear();
        }

        public static void DownloadRooms()
        {
            try
            {
                var client = new HttpClient();
                Console.WriteLine("Setting up Rooms");
                var json = client.GetStringAsync("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/RRORooms.json").Result;

                json = room_util.room_inject_CustomRooms_list(json);
                if (APIServer.CachedversionID > 20210899)
                {
                    json = room_util.room_fix_Rooms_list(json);
                }

                Rec_rewild.api.RoomCache.Initialize(json);
                Console.WriteLine($"Downloaded {Rec_rewild.api.RoomCache.Count} rooms");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public static void Initialize(string json)
        {
            var parsed = JsonConvert.DeserializeObject<RoomListWrapper>(json);
            if (parsed?.Results != null)
            {
                _roomList = parsed.Results;
                _roomMap = parsed.Results.ToDictionary(r => r.RoomId, r => r);
            }
        }

        public static bool TryGetRoomByName(string name, out RoomInfo roomInfo)
        {
            roomInfo = _roomList.FirstOrDefault(r => string.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));
            return roomInfo != null;
        }


        public static RoomInfo GetRoomInfo(ulong roomId)
        {
            _roomMap.TryGetValue(roomId, out var room);
            return room;
        }

        public static int Count => _roomMap.Count;
    }
    public class RoomListWrapper
    {
        public List<RoomInfo> Results { get; set; } = new();
        public int TotalResults { get; set; }
    }

    public class RoomInfo
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
        public object RankedEntityId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageName { get; set; } = "";
        public int WarningMask { get; set; }
        public string CustomWarning { get; set; } = "";
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
        public DateTime CreatedAt { get; set; }
        public RoomStats Stats { get; set; } = new();
        public object RankingContext { get; set; }
        public List<SubRoom> SubRooms { get; set; } = new();
        public List<RoomRole> Roles { get; set; } = new();
        public object DataBlob { get; set; }
        public int UgcVersion { get; set; }
        public List<object> Tags { get; set; } = new();
        public List<object> PromoImages { get; set; } = new();
        public List<object> PromoExternalContent { get; set; } = new();
        public List<object> LoadScreens { get; set; } = new();
    }

    public class RoomStats
    {
        public int CheerCount { get; set; }
        public int FavoriteCount { get; set; }
        public int VisitorCount { get; set; }
        public int VisitCount { get; set; }
    }

    public class SubRoom
    {
        public int SubRoomId { get; set; }
        public ulong RoomId { get; set; }
        public string Name { get; set; } = "";
        public string DataBlob { get; set; } = "";
        public bool IsSandbox { get; set; }
        public int MaxPlayers { get; set; }
        public int Accessibility { get; set; }
        public string UnitySceneId { get; set; } = "";
        public long SavedByAccountId { get; set; }
    }

    public class RoomRole
    {
        public ulong AccountId { get; set; }
        public int Role { get; set; }
        public int InvitedRole { get; set; }
    }
}
