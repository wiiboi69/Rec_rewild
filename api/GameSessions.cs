using System;
using Newtonsoft.Json;
using api;
using server;
using System.IO;
using static api.GameSessions;
using System.Collections.Generic;
using System.Numerics;
using static api.Roomdata;
using System.Net;
using System.Security.AccessControl;

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
                Console.WriteLine("can't find room named : " + roomname + Environment.NewLine + "room not found");

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

        public static string Createdorm()
        {
            return Createroom("DormRoom");
        }
        public static PlayerStatus StatusSessionInstance()
        {
            return new PlayerStatus
            {
                PlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                IsOnline = true,
                InScreenMode = false,
                GameSession = Config.localGameSession
            };
        }

        public static GameSessions.JoinResult Createnone()
        {
            Console.WriteLine("Rec_Rewild GameSession noroom");
            
			Config.localGameSession = null;
			
            return new GameSessions.JoinResult
            {
                appVersion = APIServer.CachedversionID.ToString(),
                deviceClass = 2,
                errorCode = MatchmakingErrorCode.Success,
                isOnline = true,
                playerId = (long?)APIServer.CachedPlayerID,
                roomInstance = null,
                statusVisibility = 0,
                vrMovementMode = 1
            };
        }
        public static string Createroom(string roomname)
        {
            string scenename = "";
            if (roomname.Contains("/"))
            {
                scenename = roomname.Substring(roomname.IndexOf("/"));
                roomname = roomname.Replace(scenename, "");
                scenename = scenename.Substring(1);
            }
            return Createroom(roomname, scenename);
        }
        public static string Createroom(string roomname, string scenename)
        {
            long gamesessionid = 20161L;
            long gamesessionsubroomid = 20161L;
            string myuuidAsString = "hello";

            if (scenename != "")
            {
                Console.WriteLine("Rec_Rewild finding room : \"" + roomname + "\" with scene id : \"" + scenename + "\"");
            }
            else
            {
                Console.WriteLine("Rec_Rewild finding room : \"" + roomname + "\"");
            }
            myuuidAsString = "hello";

            gamesessionid = new Random().Next(100000000, 0x7fffffff);
            if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Enabled")
            {
                gamesessionsubroomid = new Random().Next(0, 0xffff);
                Guid myuuid = Guid.NewGuid();
                myuuidAsString = myuuid.ToString();
            }


            if (Roomdata.RROS.ContainsKey(roomname))
            {
                Console.WriteLine("rec_rewild: " + roomname + " found! joining...");
                if (File.ReadAllText("SaveData\\App\\privaterooms.txt") != "Enabled")
                {
                    gamesessionid += (long)Roomdata.RROS[roomname].Room.RoomId;

                }
                Config.GameSession = new GameSessions.JoinResult
                {
                    isOnline = true,
                    deviceClass = 0,
                    playerId = long.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                    statusVisibility = 0,
                    vrMovementMode = 1,
                    errorCode = MatchmakingErrorCode.Success,
                    appVersion = APIServer.CachedversionID.ToString(),
                    roomInstance = new GameSessions.SessionInstance
                    {
                        encryptVoiceChat = false,
                        clubId = null,
                        dataBlob = Roomdata.RROS[roomname].Scenes[0].DataBlobName,
                        eventId = 0,
                        isFull = false,
                        isInProgress = false,
                        isPrivate = true,
                        location = Roomdata.RROS[roomname].Scenes[0].RoomSceneLocationId,
                        maxCapacity = Roomdata.RROS[roomname].Scenes[0].MaxPlayers,
                        name = roomname,
                        photonRegionId = "us",
                        photonRegion = "us",
                        photonRoomId = roomname + "-" + myuuidAsString + "-room",
                        roomCode = null,
                        roomId = (long)Roomdata.RROS[roomname].Room.RoomId,
                        roomInstanceId = gamesessionid,
                        roomInstanceType = 0,
                        subRoomId = 0,
                        matchmakingPolicy = 0,
                    }
                };
                if (scenename != "")
                {
                    foreach (Roomdata.Scene scene in Roomdata.RROS[roomname].Scenes)
                    {
                        if (scene.Name == scenename)
                        {
                            if (File.ReadAllText("SaveData\\App\\privaterooms.txt") != "Enabled")
                            {
                                Config.GameSession.roomInstance.roomInstanceId += (10000000 * scene.RoomSceneId);

                            }
                            Config.GameSession.roomInstance.subRoomId = scene.RoomSceneId;
                            Config.GameSession.roomInstance.location = scene.RoomSceneLocationId;
                            Config.GameSession.roomInstance.photonRoomId = roomname + "-" + myuuidAsString + "-room-" + scenename;
                        }
                    }
                }
                return JsonConvert.SerializeObject(Config.GameSession);
            }
            //CrimsonCauldron
            else
            {
                try
                {

                    string roomFilePath = "https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/" + roomname + ".txt";
                    string roomFileContent = new WebClient().DownloadString(roomFilePath);
                    Roomdata.RoomRootv2 roomdata = JsonConvert.DeserializeObject<Roomdata.RoomRootv2>(roomFileContent);


                    Console.WriteLine("rec_rewild: " + roomname + " found! joining...");
                    if (File.ReadAllText("SaveData\\App\\privaterooms.txt") != "Enabled")
                    {
                        gamesessionid += (long)roomdata.RoomId;

                    }



                    Config.GameSession = new GameSessions.JoinResult
                    {
                        isOnline = true,
                        deviceClass = 0,
                        playerId = long.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        statusVisibility = 0,
                        vrMovementMode = 1,
                        errorCode = MatchmakingErrorCode.Success,
                        appVersion = APIServer.CachedversionID.ToString(),
                        roomInstance = new GameSessions.SessionInstance
                        {
                            encryptVoiceChat = false,
                            clubId = null,
                            dataBlob = roomdata.SubRooms[0].DataBlob,
                            eventId = 0,
                            isFull = false,
                            isInProgress = false,
                            isPrivate = true,
                            location = roomdata.SubRooms[0].UnitySceneId,
                            maxCapacity = roomdata.SubRooms[0].MaxPlayers,
                            name = roomname,
                            photonRegionId = "us",
                            photonRegion = "us",
                            photonRoomId = roomname + "-" + myuuidAsString + "-room",
                            roomCode = null,
                            roomId = (long)roomdata.RoomId,
                            roomInstanceId = gamesessionid,
                            roomInstanceType = 0,
                            subRoomId = 0,
                            matchmakingPolicy = 0,
                        }
                    };
                    if (scenename != "")
                    {
                        foreach (Roomdata.SubRooms subroom in roomdata.SubRooms)
                        {
                            if (subroom.Name == scenename)
                            {
                                if (File.ReadAllText("SaveData\\App\\privaterooms.txt") != "Enabled")
                                {
                                    Config.GameSession.roomInstance.roomInstanceId += (10000000 * subroom.RoomId);

                                }
                                Config.GameSession.roomInstance.subRoomId = subroom.SubRoomId;
                                Config.GameSession.roomInstance.location = subroom.UnitySceneId;
                                Config.GameSession.roomInstance.photonRoomId = roomname + "-" + myuuidAsString + "-room-" + scenename;
                            }
                        }
                    }
                    return JsonConvert.SerializeObject(Config.GameSession);
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                {
                
                    try
                    {
                        string roomFilePathLower = "https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/" + roomname.ToLower() + ".txt";
                        string roomFileContentLower = new WebClient().DownloadString(roomFilePathLower);
                        Roomdata.RoomRootv2 roomdata = JsonConvert.DeserializeObject<Roomdata.RoomRootv2>(roomFileContentLower);

                        Console.WriteLine("rec_rewild: " + roomname + " found! joining...");
                        if (File.ReadAllText("SaveData\\App\\privaterooms.txt") != "Enabled")
                        {
                            gamesessionid += (long)roomdata.RoomId;
                        }

                        Config.GameSession = new GameSessions.JoinResult
                        {
                            isOnline = true,
                            deviceClass = 0,
                            playerId = long.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                            statusVisibility = 0,
                            vrMovementMode = 1,
                            errorCode = MatchmakingErrorCode.Success,
                            appVersion = APIServer.CachedversionID.ToString(),
                            roomInstance = new GameSessions.SessionInstance
                            {
                                encryptVoiceChat = false,
                                clubId = null,
                                dataBlob = roomdata.SubRooms[0].DataBlob,
                                eventId = 0,
                                isFull = false,
                                isInProgress = false,
                                isPrivate = true,
                                location = roomdata.SubRooms[0].UnitySceneId,
                                maxCapacity = roomdata.SubRooms[0].MaxPlayers,
                                name = roomname,
                                photonRegionId = "us",
                                photonRegion = "us",
                                photonRoomId = roomname + "-" + myuuidAsString + "-room",
                                roomCode = null,
                                roomId = (long)roomdata.RoomId,
                                roomInstanceId = gamesessionid,
                                roomInstanceType = 0,
                                subRoomId = 0,
                                matchmakingPolicy = 0,
                            }
                        };

                        if (scenename != "")
                        {
                            foreach (Roomdata.SubRooms subroom in roomdata.SubRooms)
                            {
                                if (subroom.Name == scenename)
                                {
                                    if (File.ReadAllText("SaveData\\App\\privaterooms.txt") != "Enabled")
                                    {
                                        Config.GameSession.roomInstance.roomInstanceId += (10000000 * subroom.RoomId);
                                    }
                                    Config.GameSession.roomInstance.subRoomId = subroom.SubRoomId;
                                    Config.GameSession.roomInstance.location = subroom.UnitySceneId;
                                    Config.GameSession.roomInstance.photonRoomId = roomname + "-" + myuuidAsString + "-room-" + scenename;
                                }
                            }
                        }
                        return JsonConvert.SerializeObject(Config.GameSession);
                    }
                    catch
                    {

                        string[] roomlistdir = Directory.GetFiles("SaveData\\Rooms\\custom\\");
                        foreach (string roomdir in roomlistdir)
                        {
                            Roomdata.RoomRootv2 roomdata = JsonConvert.DeserializeObject<Roomdata.RoomRootv2>(File.ReadAllText(roomdir));

                            if (roomdata.Name.Contains(roomname))
                            {
                                Console.WriteLine("found room name: " + roomdir + " using room name: " + roomname);
                                string roomrootdata = File.ReadAllText(roomdir);
                                RoomRootv2 roomRoot = JsonConvert.DeserializeObject<RoomRootv2>(roomrootdata);
                                if (scenename != "")
                                {
                                    Console.WriteLine("rec_rewild: " + roomname + " found! joining...");
                                    if (File.ReadAllText("SaveData\\App\\privaterooms.txt") != "Enabled")
                                    {
                                        gamesessionid += (long)roomdata.RoomId;

                                    }
                                    Config.GameSession = new GameSessions.JoinResult
                                    {
                                        isOnline = true,
                                        deviceClass = 0,
                                        playerId = long.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                                        statusVisibility = 0,
                                        vrMovementMode = 1,
                                        errorCode = MatchmakingErrorCode.Success,
                                        appVersion = APIServer.CachedversionID.ToString(),
                                        roomInstance = new GameSessions.SessionInstance
                                        {
                                            encryptVoiceChat = false,
                                            clubId = null,
                                            dataBlob = roomdata.DataBlob,
                                            eventId = 0,
                                            isFull = false,
                                            isInProgress = false,
                                            isPrivate = true,
                                            location = roomdata.SubRooms[0].UnitySceneId,
                                            maxCapacity = roomdata.MaxPlayers,
                                            name = roomname,
                                            photonRegionId = "us",
                                            photonRegion = "us",
                                            photonRoomId = roomname + "-" + myuuidAsString + "-room",
                                            roomCode = null,
                                            roomId = (long)roomdata.RoomId,
                                            roomInstanceId = gamesessionid,
                                            roomInstanceType = 0,
                                            subRoomId = 0,
                                            matchmakingPolicy = 0,
                                        }
                                    };
                                    foreach (SubRooms scene in roomRoot.SubRooms)
                                    {
                                        if (scene.Name == scenename)
                                        {
                                            if (File.ReadAllText("SaveData\\App\\privaterooms.txt") != "Enabled")
                                            {
                                                Config.GameSession.roomInstance.roomInstanceId += (10000000 * scene.RoomId);

                                            }
                                            Config.GameSession.roomInstance.subRoomId = scene.SubRoomId;
                                            Config.GameSession.roomInstance.location = scene.UnitySceneId;
                                            Config.GameSession.roomInstance.photonRoomId = roomname + "-" + myuuidAsString + "-room-" + scenename;
                                        }
                                    }
                                }
                                return JsonConvert.SerializeObject(Config.GameSession);
                            }
                        }
                    }
                }


                Console.WriteLine("rec_rewild: " + roomname + " doesn't exist.");
                return JsonConvert.SerializeObject(new GameSessions.JoinResult
                {
                    isOnline = true,
                    deviceClass = 0,
                    playerId = long.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                    statusVisibility = 0,
                    vrMovementMode = 1,
                    errorCode = MatchmakingErrorCode.NoSuchRoom,
                    appVersion = APIServer.CachedversionID.ToString(),
                });
            }
        }

        public static string FindRoomid(ulong roomname)
        {
            foreach (KeyValuePair<string, Roomdata.RoomRoot> keyValuePair in Roomdata.RROS)
            {
                bool flag = keyValuePair.Value.Room.RoomId == (ulong)roomname;
                if (flag)
                {
                    return keyValuePair.Value.Room.Name;
                }
            }
            return "";
        }

        public static GameSessions.MatchPresence Presence()
        {
            bool flag = Config.GameSession == null;
            GameSessions.SessionInstance roomInstance1;
            roomInstance1 = null;
            if (!flag) 
            {
                try 
                { 
                    roomInstance1 = Config.GameSession.roomInstance;
                }
                catch 
                { 
                    roomInstance1 = null;
                }

            }
            return new GameSessions.MatchPresence
            {
                isOnline = true,
                deviceClass = 0,
                playerId = long.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                statusVisibility = 0,
                vrMovementMode = 1,
                //roomInstance = Config.GameSession.roomInstance,
                //roomInstance = null,
                roomInstance = roomInstance1,
                platform = -1,
                errorCode = 0,
                lastOnline = APIServer.Cachedservertimestarted,
                appVersion = APIServer.CachedversionID.ToString(),
            };
        }

        public static string GetDetails(string roomid)
        {
            string text;
            try
            {
                foreach (KeyValuePair<string, Roomdata.RoomRoot> room in Roomdata.RROS)
                {
                    Roomdata.RoomRoot root = room.Value;
                    if (root.Room.RoomId == ulong.Parse(roomid))
                    {
                        root.LocalPlayerRole = 4;
                        root.localPlayerRole = 4;
                        root.CoOwners.Add(APIServer.CachedPlayerID);
                        root.Room.creatorPlayerId = APIServer.CachedPlayerID;
                        root.Room.RoomOrPlaylistId = root.Room.RoomId;
                        return JsonConvert.SerializeObject(root);
                    }
                }
                text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                text = "";
            }
            return text;
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
            public ulong PlayerId { get; set; }
            public bool IsOnline { get; set; }
            public bool InScreenMode { get; set; }
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
            public bool encryptVoiceChat { get; set; }
            public long? clubId { get; set; }
            public long? EventId { get; set; }
            public string roomCode { get; set; }
            public string photonRegion { get; set; }
            public int matchmakingPolicy { get; set; }
        }

        public class JoinRandomRequest2
        {
            public string[] ActivityLevelIds { get; set; }
            public ulong[] ExpectedPlayerIds { get; set; }
            public GameSessions.RegionPing[] RegionPings { get; set; }
        }

        public class JoinRoomRequest
        {
            public ulong[] ExpectedPlayerIds { get; set; }
            public GameSessions.RegionPing[] RegionPings { get; set; }
            public string[] RoomTags { get; set; }
            public string RoomName { get; set; }
            public string SceneName { get; set; }
            public int AdditionalPlayerJoinMode { get; set; }
            public bool Private { get; set; }
        }
        public class CreateRequest
        {
            public string ActivityLevelId { get; set; }
            public ulong[] ExpectedPlayerIds { get; set; }
            public GameSessions.RegionPing[] RegionPings { get; set; }
            public bool IsSandbox { get; set; }
        }
        public class RegionPing
        {
            public string Region { get; set; }
            public int Ping { get; set; }
        }
        public class JoinResult
        {
            public string appVersion { get; set; }
            public int deviceClass { get; set; }
            public MatchmakingErrorCode errorCode { get; set; } //todo: errorCode list
            public bool isOnline { get; set; }
            public long? playerId { get; set; }
            public GameSessions.SessionInstance roomInstance { get; set; }
            public int statusVisibility { get; set; }
            public int vrMovementMode { get; set; }
        }

        public enum MatchmakingErrorCode
        {
            // Token: 0x04008963 RID: 35171
            Success,
            // Token: 0x04008964 RID: 35172
            NoSuchGame,
            // Token: 0x04008965 RID: 35173
            PlayerNotOnline,
            // Token: 0x04008966 RID: 35174
            InsufficientSpace,
            // Token: 0x04008967 RID: 35175
            EventNotStarted,
            // Token: 0x04008968 RID: 35176
            EventAlreadyFinished,
            // Token: 0x04008969 RID: 35177
            EventCreatorNotReady,
            // Token: 0x0400896A RID: 35178
            BlockedFromRoom,
            // Token: 0x0400896B RID: 35179
            ProfileLocked,
            // Token: 0x0400896C RID: 35180
            NoBirthday,
            // Token: 0x0400896D RID: 35181
            MarkedForDelete,
            // Token: 0x0400896E RID: 35182
            JuniorNotAllowed,
            // Token: 0x0400896F RID: 35183
            Banned,
            // Token: 0x04008970 RID: 35184
            AlreadyInBestInstance,
            // Token: 0x04008971 RID: 35185
            InsufficientRelationship,
            // Token: 0x04008972 RID: 35186
            UpdateRequired = 16,
            // Token: 0x04008973 RID: 35187
            AlreadyInTargetInstance,
            // Token: 0x04008974 RID: 35188
            RegistrationRequired,
            // Token: 0x04008975 RID: 35189
            UGCNotAllowed,
            // Token: 0x04008976 RID: 35190
            NoSuchRoom,
            // Token: 0x04008977 RID: 35191
            RoomCreatorNotReady,
            // Token: 0x04008978 RID: 35192
            RoomIsNotActive,
            // Token: 0x04008979 RID: 35193
            RoomBlockedByCreator,
            // Token: 0x0400897A RID: 35194
            RoomBlockingCreator,
            // Token: 0x0400897B RID: 35195
            RoomIsPrivate,
            // Token: 0x0400897C RID: 35196
            RoomInstanceIsPrivate,
            // Token: 0x0400897D RID: 35197
            DeviceClassNotSupported = 30,
            // Token: 0x0400897E RID: 35198
            DeviceClassNotSupportedByRoomOwner,
            // Token: 0x0400897F RID: 35199
            VRMovementModeNotSupportedByRoomOwner,
            // Token: 0x04008980 RID: 35200
            EventIsPrivate = 35,
            // Token: 0x04008981 RID: 35201
            RoomInviteExpired = 40,
            // Token: 0x04008982 RID: 35202
            NoAvailableRegion = 45,
            // Token: 0x04008983 RID: 35203
            NotorietyTooPoor = 50,
            // Token: 0x04008984 RID: 35204
            BannedFromRoom = 55
        }

        public class MatchPresence
        {
            public long? playerId { get; set; }
            public int statusVisibility { get; set; }
            public int platform { get; set; }
            public int deviceClass { get; set; }
            public int vrMovementMode { get; set; }
            public SessionInstance? roomInstance { get; set; }
            public bool isOnline { get; set; }
            public int? errorCode { get; set; } //todo: errorCode list
            public ulong? lastOnline { get; set; }
            public string appVersion { get; set; }
        }
    }
}
