using System;
using Newtonsoft.Json;
using api;
using server;
using System.IO;
using static api.GameSessions;
using System.Collections.Generic;

namespace api
{
    public class GameSessions
    {
        /*
        public static string Createdorm()
        {
            GameSessions.gamesessionid = 20161L;
            gamesessionsubroomid = 20161L;
            Console.WriteLine("Rec_Rewild GameSession dormroom");
            if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Enabled")
            {
                gamesessionid = new Random().Next(0, 99);
                gamesessionsubroomid = new Random().Next(0, 0xffff);
            }
            Guid myuuid = Guid.NewGuid();
            myuuidAsString = myuuid.ToString();

            Config.localGameSession = new GameSessions.SessionInstance
            {
                EncryptVoiceChat = false,
                clubId = null,
                dataBlob = "",
                EventId = null,
                isFull = false,
                isInProgress = false,
                isPrivate = false,
                location = "76d98498-60a1-430c-ab76-b54a29b7a163",
                MaxCapacity = 20,
                Name = "dormroom",
                photonRegionId = "us",
                photonRoomId = "dormroom" + "-" + myuuidAsString + "-room",
                roomCode = "",
                roomId = 1,
                roomInstanceId = gamesessionid,
                roomInstanceType = 0,
                subRoomId = 1,
                name = "dormroom",
                maxCapacity = 20,
                eventId = 0,

            };
            return JsonConvert.SerializeObject(new GameSessions.JoinResultv3
            {
                appVersion = APIServer.CachedversionID.ToString(),
                deviceClass = 2,
                errorCode = null,
                isOnline = true,
                playerId = (long?)APIServer.CachedPlayerID,
                roomInstance = Config.localGameSession,
                statusVisibility = 0,
                vrMovementMode = 1
            });
        }

        public static string Createroom(string roomname)
        {
            amesessionid = 20161L;
            gamesessionsubroomid = 20161L;
            Console.WriteLine("Rec_Rewild GameSession room : " + roomname);
            if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Enabled")
            {
                gamesessionid = new Random().Next(0, 99);
                gamesessionsubroomid = new Random().Next(0, 0xffff);
            }
            Guid myuuid = Guid.NewGuid();
            myuuidAsString = myuuid.ToString();
            gameroomlocation = "";
            gameroomId = 0;
            if (!FindRoomData(roomname.ToLower()))
            {
                Console.WriteLine("can't find room named : " + roomname + Environment.NewLine + "are you using a unsported room?");

                gameroomlocation = "76d98498-60a1-430c-ab76-b54a29b7a163";
                gameroomId = 1;
            }

            Config.localGameSession = new GameSessions.SessionInstance
            {
                EncryptVoiceChat = false,
                clubId = null,
                dataBlob = "",
                EventId = null,
                isFull = false,
                isInProgress = false,
                isPrivate = false,
                location = gameroomlocation,
                MaxCapacity = 20,
                Name = roomname,
                photonRegionId = "us",
                photonRoomId = roomname + "-" + myuuidAsString + "-room",
                roomCode = null,
                roomId = gameroomId,
                roomInstanceId = gamesessionid,
                roomInstanceType = 0,
                subRoomId = 1,

            };
            return JsonConvert.SerializeObject(new GameSessions.JoinResult
            {
                appVersion = APIServer.CachedversionID.ToString(),
                deviceClass = 2,
                errorCode = null,
                isOnline = true,
                playerId = (long?)APIServer.CachedPlayerID,
                roomInstance = Config.localGameSession,
                statusVisibility = 0,
                vrMovementMode = 1
            });
        }

        public static string Createnone()
        {

            Console.WriteLine("Rec_Rewild GameSession noroom");

            GameSessions.gamesessionid = 20161L;
            gamesessionsubroomid = 20161L;

            if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Enabled")
            {
                gamesessionid = new Random().Next(0, 99);
                gamesessionsubroomid = new Random().Next(0, 0xffff);
            }
            Guid myuuid = Guid.NewGuid();
            myuuidAsString = myuuid.ToString();
            /*
            Config.localGameSessionv3 = new GameSessions.SessionInstancev3
            {
                EncryptVoiceChat = false,
                clubId = null,
                dataBlob = "",
                EventId = null,
                isFull = false,
                isInProgress = false,
                isPrivate = false,
                location = "76d98498-60a1-430c-ab76-b54a29b7a163",
                MaxCapacity = 20,
                Name = "dormroom",
                photonRegionId = "us",
                photonRoomId = "dormroom-" + myuuidAsString + "-room",
                roomCode = null,
                roomId = 1,
                roomInstanceId = gamesessionid,
                roomInstanceType = 0,
                subRoomId = gamesessionsubroomid,

            };
            
            Config.localGameSessionv3 = new GameSessions.SessionInstancev3
            {
                EncryptVoiceChat = false,
                clubId = null,
                dataBlob = "",
                EventId = null,
                isFull = false,
                isInProgress = false,
                isPrivate = false,
                location = "",
                MaxCapacity = 20,
                Name = "",
                photonRegionId = "us",
                photonRoomId = "",
                roomCode = null,
                roomId = 0,
                roomInstanceId = 1,
                roomInstanceType = 2,
                subRoomId = 1,

            };
			Config.localGameSessionv3 = null;
			
            return JsonConvert.SerializeObject(new GameSessions.JoinResultv3
            {
                appVersion = APIServer.CachedversionID.ToString(),
                deviceClass = 2,
                errorCode = null,
                isOnline = true,
                playerId = (long?)APIServer.CachedPlayerID,
                roomInstance = null,
                //roomInstance = Config.localGameSessionv3,
                statusVisibility = 0,
                vrMovementMode = 1
            });
        }

        public static GameSessions.PlayerStatus StatusSessionInstance()
        {
            return new GameSessions.PlayerStatus
            {
                PlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                IsOnline = true,
                InScreenMode = false,
                GameSession = Config.localGameSession
            };
        }

        public static bool FindRoomData(String roomname)
        {

            foreach (KeyValuePair<string, c00005d.rooms_details> keyValuePair in c00005d.rooms_details_list)
            {
                bool flag = keyValuePair.Value.Name == roomname;
                if (flag)
                {
                    gameroomlocation = keyValuePair.Value.RoomSceneLocationId;
                    gameroomId = (long)keyValuePair.Value.RoomId;
                    return true;
                }
            }
            return false;
        }

        */
        public static string FindRoomid(ulong roomname)
        {
            foreach (KeyValuePair<string, roomdata.RoomRoot> keyValuePair in roomdata.RROS)
            {
                bool flag = keyValuePair.Value.Room.RoomId == (ulong)roomname;
                if (flag)
                {
                    return keyValuePair.Value.Room.Name;
                }
            }
            return "";
        }
        public enum JoinResultIDs
        {
            Success,
            NoSuchGame,
            PlayerNotOnline,
            InsufficientSpace,
            EventNotStarted,
            EventAlreadyFinished,
            EventCreatorNotReady,
            BlockedFromRoom,
            ProfileLocked,
            NoBirthday,
            MarkedForDelete,
            JuniorNotAllowed,
            Banned,
            NoSuchRoom = 20,
            RoomCreatorNotReady,
            RoomIsNotActive,
            RoomBlockedByCreator,
            RoomBlockingCreator,
            RoomIsPrivate
        }
        public class PlayerStatus
        {
            // Token: 0x1700003F RID: 63
            // (get) Token: 0x060000C3 RID: 195 RVA: 0x000025A6 File Offset: 0x000007A6
            // (set) Token: 0x060000C4 RID: 196 RVA: 0x000025AE File Offset: 0x000007AE
            public ulong PlayerId { get; set; }

            // Token: 0x17000040 RID: 64
            // (get) Token: 0x060000C5 RID: 197 RVA: 0x000025B7 File Offset: 0x000007B7
            // (set) Token: 0x060000C6 RID: 198 RVA: 0x000025BF File Offset: 0x000007BF
            public bool IsOnline { get; set; }

            // Token: 0x17000041 RID: 65
            // (get) Token: 0x060000C7 RID: 199 RVA: 0x000025C8 File Offset: 0x000007C8
            // (set) Token: 0x060000C8 RID: 200 RVA: 0x000025D0 File Offset: 0x000007D0
            public bool InScreenMode { get; set; }

            // Token: 0x17000042 RID: 66
            // (get) Token: 0x060000C9 RID: 201 RVA: 0x000025D9 File Offset: 0x000007D9
            // (set) Token: 0x060000CA RID: 202 RVA: 0x000025E1 File Offset: 0x000007E1
            public GameSessions.SessionInstance GameSession { get; set; }
        }

        public class SessionInstance
        {
            public int maxCapacity { get; set; }
            public string name { get; set; }
            public long? eventId { get; set; }
            public long? roomId { get; set; }
            public long? roomInstanceId { get; set; }
            public long? subRoomId { get; set; }
            public string dataBlob { get; set; }
            public bool isFull { get; set; }
            public bool isInProgress { get; set; }
            public bool isPrivate { get; set; }
            public string location { get; set; }
            public int MaxCapacity { get; set; }
            public string Name { get; set; }
            public string photonRegionId { get; set; }
            public string photonRoomId { get; set; }
            public int roomInstanceType { get; set; } //todo: roomInstanceType
            public bool EncryptVoiceChat { get; set; }
            public long? clubId { get; set; }
            public long? EventId { get; set; }
            public string roomCode { get; set; }

        }

        public class JoinRandomRequest
        {
            public string[] ActivityLevelIds { get; set; }

            public ulong[] ExpectedPlayerIds { get; set; }

            public GameSessions.RegionPing[] RegionPings { get; set; }
        }

        public class JoinRoomRequest2
        {
            // Token: 0x17000022 RID: 34
            // (get) Token: 0x06000060 RID: 96 RVA: 0x00002345 File Offset: 0x00000545
            // (set) Token: 0x06000061 RID: 97 RVA: 0x0000234D File Offset: 0x0000054D
            public ulong[] ExpectedPlayerIds { get; set; }

            // Token: 0x17000023 RID: 35
            // (get) Token: 0x06000062 RID: 98 RVA: 0x00002356 File Offset: 0x00000556
            // (set) Token: 0x06000063 RID: 99 RVA: 0x0000235E File Offset: 0x0000055E
            public GameSessions.RegionPing[] RegionPings { get; set; }

            // Token: 0x17000024 RID: 36
            // (get) Token: 0x06000064 RID: 100 RVA: 0x00002367 File Offset: 0x00000567
            // (set) Token: 0x06000065 RID: 101 RVA: 0x0000236F File Offset: 0x0000056F
            public string[] RoomTags { get; set; }

            // Token: 0x17000025 RID: 37
            // (get) Token: 0x06000066 RID: 102 RVA: 0x00002378 File Offset: 0x00000578
            // (set) Token: 0x06000067 RID: 103 RVA: 0x00002380 File Offset: 0x00000580
            public string RoomName { get; set; }

            // Token: 0x17000026 RID: 38
            // (get) Token: 0x06000068 RID: 104 RVA: 0x00002389 File Offset: 0x00000589
            // (set) Token: 0x06000069 RID: 105 RVA: 0x00002391 File Offset: 0x00000591
            public string SceneName { get; set; }

            // Token: 0x17000027 RID: 39
            // (get) Token: 0x0600006A RID: 106 RVA: 0x0000239A File Offset: 0x0000059A
            // (set) Token: 0x0600006B RID: 107 RVA: 0x000023A2 File Offset: 0x000005A2
            public int AdditionalPlayerJoinMode { get; set; }

            // Token: 0x17000028 RID: 40
            // (get) Token: 0x0600006C RID: 108 RVA: 0x000023AB File Offset: 0x000005AB
            // (set) Token: 0x0600006D RID: 109 RVA: 0x000023B3 File Offset: 0x000005B3
            public bool Private { get; set; }
        }
        // Token: 0x02000025 RID: 37

        // Token: 0x02000025 RID: 37
        public class CreateRequest
        {
            // Token: 0x17000055 RID: 85
            // (get) Token: 0x060000F2 RID: 242 RVA: 0x0000271C File Offset: 0x0000091C
            // (set) Token: 0x060000F3 RID: 243 RVA: 0x00002724 File Offset: 0x00000924
            public string ActivityLevelId { get; set; }

            // Token: 0x17000056 RID: 86
            // (get) Token: 0x060000F4 RID: 244 RVA: 0x0000272D File Offset: 0x0000092D
            // (set) Token: 0x060000F5 RID: 245 RVA: 0x00002735 File Offset: 0x00000935
            public ulong[] ExpectedPlayerIds { get; set; }

            // Token: 0x17000057 RID: 87
            // (get) Token: 0x060000F6 RID: 246 RVA: 0x0000273E File Offset: 0x0000093E
            // (set) Token: 0x060000F7 RID: 247 RVA: 0x00002746 File Offset: 0x00000946
            public GameSessions.RegionPing[] RegionPings { get; set; }

            // Token: 0x17000058 RID: 88
            // (get) Token: 0x060000F8 RID: 248 RVA: 0x0000274F File Offset: 0x0000094F
            // (set) Token: 0x060000F9 RID: 249 RVA: 0x00002757 File Offset: 0x00000957
            public bool IsSandbox { get; set; }
        }

        // Token: 0x02000026 RID: 38
        public class RegionPing
        {
            public string Region { get; set; }
            public int Ping { get; set; }
        }

        public class JoinResult
        {
            public string appVersion { get; set; }
            public int deviceClass { get; set; }
            public int? errorCode { get; set; } //todo: a custom thing?
            public bool isOnline { get; set; }
            public long? playerId { get; set; }
            public GameSessions.SessionInstance roomInstance { get; set; }
            public int statusVisibility { get; set; }
            public int vrMovementMode { get; set; }
        }
    }
}
