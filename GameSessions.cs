using System;
using Newtonsoft.Json;
using api;
using server;
using System.IO;
using static gamesesh.GameSessions;
using System.Collections.Generic;
using vaultgamesesh;
namespace gamesesh

{
	// Token: 0x02000020 RID: 32
	public class GameSessions
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004C08 File Offset: 0x00002E08
		public static string JoinRandom(string jsonData)
		{
			long? creatorid = 1243409L;
			long gamesessionid = long.Parse(start.Program.version + "1");
			Console.WriteLine("OpenRec GameSession Room");
			GameSessions.JoinRandomRequest joinRandomRequest = JsonConvert.DeserializeObject<GameSessions.JoinRandomRequest>(jsonData);
			if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Enabled")
			{
				gamesessionid = new Random().Next(0, 99);
			}
			if (start.Program.version == "2017")
            {
				creatorid = (long?)APIServer.CachedPlayerID;
			}
			if (start.Program.bannedflag == true)
			{
				gamesessionid = 100L;
			}
			Config.localGameSession = new GameSessions.SessionInstance
			{
				GameSessionId = gamesessionid,
				RegionId = "us",
				RoomId = joinRandomRequest.ActivityLevelIds[0],
				RecRoomId = null,
				EventId = null,
				CreatorPlayerId = creatorid,
				Name = "OpenRec Room",
				ActivityLevelId = joinRandomRequest.ActivityLevelIds[0],
				Private = false,
				Sandbox = false,
				SupportsScreens = true,
				SupportsVR = true,
				GameInProgress = false,
				MaxCapacity = 20,
				IsFull = false
			};
			
			return JsonConvert.SerializeObject(new GameSessions.JoinResult
			{
				Result = 0,
				GameSession = Config.localGameSession
			});
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000254B File Offset: 0x0000074B
		public static string StatusSession()
		{
			return JsonConvert.SerializeObject(new GameSessions.PlayerStatusold
			{
				PlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
				IsOnline = true,
				InScreenMode = false,
				GameSession = Config.localGameSession
			});
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004D24 File Offset: 0x00002F24
		public static string Create(string jsonData)
		{
			long gamesessionid = 20161L;
			Console.WriteLine("OpenRec GameSession Custom Room");
			if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Enabled")
			{
				gamesessionid = new Random().Next(0, 99);
			}
			GameSessions.CreateRequest createRequest = JsonConvert.DeserializeObject<GameSessions.CreateRequest>(jsonData);
			Config.localGameSession = new GameSessions.SessionInstance
			{
				GameSessionId = gamesessionid,
				RegionId = "us",
				RoomId = createRequest.ActivityLevelId,
				RecRoomId = null,
				EventId = null,
				CreatorPlayerId = (long?)APIServer.CachedPlayerID,
				Name = "OpenRec Custom Room",
				ActivityLevelId = createRequest.ActivityLevelId,
				Private = false,
				Sandbox = true,
				SupportsScreens = true,
				SupportsVR = true,
				GameInProgress = false,
				MaxCapacity = 20,
				IsFull = false
			};
			return JsonConvert.SerializeObject(new GameSessions.JoinResult
			{
				Result = 0,
				GameSession = Config.localGameSession
			});
		}
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
                photonRoomId = "dormroom" + "-" + myuuidAsString + "-room",
                roomCode = "",
                roomId = 1,
                roomInstanceId = gamesessionid,
                roomInstanceType = 0,
                subRoomId = 0,

                name = "dormroom",
                maxCapacity = 20,
                eventId = 0,

            };
            Config.localGameSessionv2 = new GameSessions.SessionInstancev2
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
                subRoomId = 0,

            };

            return JsonConvert.SerializeObject(new GameSessions.JoinResultv3
            {
                errorCode = 0,
                roomInstance = Config.localGameSessionv3,
            });
        }
        public static string Createdormold()
		{
            GameSessions.gamesessionid = 20161L;
            gamesessionsubroomid = 20161L;
            Console.WriteLine("Rec_Rewild GameSession dormroom" );
            if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Enabled")
            {
                gamesessionid = new Random().Next(0, 99);
                gamesessionsubroomid = new Random().Next(0, 0xffff);
            }
            Guid myuuid = Guid.NewGuid();
            myuuidAsString = myuuid.ToString();

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
                photonRoomId = "dormroom" + "-" + myuuidAsString + "-room",
                roomCode = "",
                roomId = 1,
                roomInstanceId = gamesessionid,
                roomInstanceType = 0,
                subRoomId = 0,

                name = "dormroom",
                maxCapacity =20,
                eventId = 0,

            };
            Config.localGameSessionv2 = new GameSessions.SessionInstancev2
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
                subRoomId = 0,

            };

			return JsonConvert.SerializeObject(new GameSessions.JoinResultv3old
			{
				appVersion = APIServer.CachedversionID.ToString(),
				deviceClass = 2,
				errorCode = null,
				isOnline = true,
				playerId = (long?)APIServer.CachedPlayerID,
				roomInstance = Config.localGameSessionv3,
				statusVisibility = 0,
				vrMovementMode = 1
			});
        }

        public static string Createroom(string roomname)
        {
            return Createroom(roomname, "");
        }
        public static string Createroom(string roomname, string scenename)
        {
            GameSessions.gamesessionid = 20161L;
            GameSessions.gamesessionsubroomid = 20161L;
            if (scenename != "") 
            { 
                Console.WriteLine("Rec_Rewild finding room : \"" + roomname + "\" in scene id : \"" + scenename + "\"");
            }
            else
            {
                Console.WriteLine("Rec_Rewild finding room : \"" + roomname + "\"");
            }
            if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Enabled")
            {
                GameSessions.gamesessionid = new Random().Next(100000000, 999999999);
                GameSessions.gamesessionsubroomid = new Random().Next(0, 0xffff);
            }
            Guid myuuid = Guid.NewGuid();
            myuuidAsString = myuuid.ToString();
            if (GameSessions.RROS.ContainsKey(roomname))
            {
                Console.WriteLine("rec_rewild: " + roomname + " found! joining...");
                Config.GameSession = new GameSessions.JoinResultv3
                {
                    errorCode = 0,
                    roomInstance = new GameSessions.SessionInstancev3
                    {

                        encryptVoiceChat = false,
                        clubId = null,
                        dataBlob = GameSessions.RROS[roomname].Scenes[0].DataBlobName,
                        eventId = 0,
                        isFull = false,
                        isInProgress = false,
                        isPrivate = false,
                        location = GameSessions.RROS[roomname].Scenes[0].RoomSceneLocationId,
                        maxCapacity = GameSessions.RROS[roomname].Scenes[0].MaxPlayers,
                        name = roomname,
                        photonRegionId = "us",
                        photonRoomId = roomname + "-" + myuuidAsString + "-room",
                        roomCode = null,
                        roomId = (long)GameSessions.RROS[roomname].Room.RoomId,
                        roomInstanceId = (int)GameSessions.gamesessionid,
                        roomInstanceType = 0,
                        subRoomId = 0,
                    }
                };
                if (scenename != "")
                {
                    foreach (GameSessions.Scene scene in GameSessions.RROS[roomname].Scenes)
                    {
                        if (scene.Name == scenename)
                        {
                            Config.GameSession.roomInstance.subRoomId = scene.RoomSceneId;
                            Config.GameSession.roomInstance.location = scene.RoomSceneLocationId;
                        }
                    }
                }
                return JsonConvert.SerializeObject(GameSessions.match);
            }
            Console.WriteLine("rec_rewild: " + roomname + " doesn't exist.");
            return "aaaaaaaa";
        }

        // Token: 0x04000055 RID: 85
        public static GameSessions.MatchRoot match;


        // Token: 0x0200001B RID: 27
        public class MatchRoot
        {
            // Token: 0x17000085 RID: 133
            // (get) Token: 0x0600013B RID: 315 RVA: 0x000029F7 File Offset: 0x00000BF7
            // (set) Token: 0x0600013C RID: 316 RVA: 0x000029FF File Offset: 0x00000BFF
            public int errorCode { get; set; }

            // Token: 0x17000086 RID: 134
            // (get) Token: 0x0600013D RID: 317 RVA: 0x00002A08 File Offset: 0x00000C08
            // (set) Token: 0x0600013E RID: 318 RVA: 0x00002A10 File Offset: 0x00000C10
            public GameSessions.RoomInstance roomInstance { get; set; }
        }


        // Token: 0x0200001A RID: 26
        public class RoomInstance
        {
            // Token: 0x17000076 RID: 118
            // (get) Token: 0x0600011C RID: 284 RVA: 0x000028F8 File Offset: 0x00000AF8
            // (set) Token: 0x0600011D RID: 285 RVA: 0x00002900 File Offset: 0x00000B00
            public int roomInstanceId { get; set; }

            // Token: 0x17000077 RID: 119
            // (get) Token: 0x0600011E RID: 286 RVA: 0x00002909 File Offset: 0x00000B09
            // (set) Token: 0x0600011F RID: 287 RVA: 0x00002911 File Offset: 0x00000B11
            public long roomId { get; set; }

            // Token: 0x17000078 RID: 120
            // (get) Token: 0x06000120 RID: 288 RVA: 0x0000291A File Offset: 0x00000B1A
            // (set) Token: 0x06000121 RID: 289 RVA: 0x00002922 File Offset: 0x00000B22
            public int subRoomId { get; set; }

            // Token: 0x17000079 RID: 121
            // (get) Token: 0x06000122 RID: 290 RVA: 0x0000292B File Offset: 0x00000B2B
            // (set) Token: 0x06000123 RID: 291 RVA: 0x00002933 File Offset: 0x00000B33
            public string location { get; set; }

            // Token: 0x1700007A RID: 122
            // (get) Token: 0x06000124 RID: 292 RVA: 0x0000293C File Offset: 0x00000B3C
            // (set) Token: 0x06000125 RID: 293 RVA: 0x00002944 File Offset: 0x00000B44
            public object dataBlob { get; set; }

            // Token: 0x1700007B RID: 123
            // (get) Token: 0x06000126 RID: 294 RVA: 0x0000294D File Offset: 0x00000B4D
            // (set) Token: 0x06000127 RID: 295 RVA: 0x00002955 File Offset: 0x00000B55
            public int eventId { get; set; }

            // Token: 0x1700007C RID: 124
            // (get) Token: 0x06000128 RID: 296 RVA: 0x0000295E File Offset: 0x00000B5E
            // (set) Token: 0x06000129 RID: 297 RVA: 0x00002966 File Offset: 0x00000B66
            public string photonRegionId { get; set; }

            // Token: 0x1700007D RID: 125
            // (get) Token: 0x0600012A RID: 298 RVA: 0x0000296F File Offset: 0x00000B6F
            // (set) Token: 0x0600012B RID: 299 RVA: 0x00002977 File Offset: 0x00000B77
            public string photonRoomId { get; set; }

            // Token: 0x1700007E RID: 126
            // (get) Token: 0x0600012C RID: 300 RVA: 0x00002980 File Offset: 0x00000B80
            // (set) Token: 0x0600012D RID: 301 RVA: 0x00002988 File Offset: 0x00000B88
            public string name { get; set; }

            // Token: 0x1700007F RID: 127
            // (get) Token: 0x0600012E RID: 302 RVA: 0x00002991 File Offset: 0x00000B91
            // (set) Token: 0x0600012F RID: 303 RVA: 0x00002999 File Offset: 0x00000B99
            public int maxCapacity { get; set; }

            // Token: 0x17000080 RID: 128
            // (get) Token: 0x06000130 RID: 304 RVA: 0x000029A2 File Offset: 0x00000BA2
            // (set) Token: 0x06000131 RID: 305 RVA: 0x000029AA File Offset: 0x00000BAA
            public bool isFull { get; set; }

            // Token: 0x17000081 RID: 129
            // (get) Token: 0x06000132 RID: 306 RVA: 0x000029B3 File Offset: 0x00000BB3
            // (set) Token: 0x06000133 RID: 307 RVA: 0x000029BB File Offset: 0x00000BBB
            public bool isPrivate { get; set; }

            // Token: 0x17000082 RID: 130
            // (get) Token: 0x06000134 RID: 308 RVA: 0x000029C4 File Offset: 0x00000BC4
            // (set) Token: 0x06000135 RID: 309 RVA: 0x000029CC File Offset: 0x00000BCC
            public bool isInProgress { get; set; }

            // Token: 0x17000083 RID: 131
            // (get) Token: 0x06000136 RID: 310 RVA: 0x000029D5 File Offset: 0x00000BD5
            // (set) Token: 0x06000137 RID: 311 RVA: 0x000029DD File Offset: 0x00000BDD
            public int roomInstanceType { get; set; }

            // Token: 0x17000084 RID: 132
            // (get) Token: 0x06000138 RID: 312 RVA: 0x000029E6 File Offset: 0x00000BE6
            // (set) Token: 0x06000139 RID: 313 RVA: 0x000029EE File Offset: 0x00000BEE
            public string roomCode { get; set; }
            public bool encryptVoiceChat { get; set; }
            public int? clubId { get; set; }
        }

        // Token: 0x0200001C RID: 28
        public class RoomRoot
        {
            // Token: 0x17000087 RID: 135
            // (get) Token: 0x06000140 RID: 320 RVA: 0x00002A19 File Offset: 0x00000C19
            // (set) Token: 0x06000141 RID: 321 RVA: 0x00002A21 File Offset: 0x00000C21
            public GameSessions.Room Room { get; set; }

            // Token: 0x17000088 RID: 136
            // (get) Token: 0x06000142 RID: 322 RVA: 0x00002A2A File Offset: 0x00000C2A
            // (set) Token: 0x06000143 RID: 323 RVA: 0x00002A32 File Offset: 0x00000C32
            public List<GameSessions.Scene> Scenes { get; set; }

            // Token: 0x17000089 RID: 137
            // (get) Token: 0x06000144 RID: 324 RVA: 0x00002A3B File Offset: 0x00000C3B
            // (set) Token: 0x06000145 RID: 325 RVA: 0x00002A43 File Offset: 0x00000C43
            public List<int> CoOwners { get; set; }

            // Token: 0x1700008A RID: 138
            // (get) Token: 0x06000146 RID: 326 RVA: 0x00002A4C File Offset: 0x00000C4C
            // (set) Token: 0x06000147 RID: 327 RVA: 0x00002A54 File Offset: 0x00000C54
            public List<int> InvitedCoOwners { get; set; }

            // Token: 0x1700008B RID: 139
            // (get) Token: 0x06000148 RID: 328 RVA: 0x00002A5D File Offset: 0x00000C5D
            // (set) Token: 0x06000149 RID: 329 RVA: 0x00002A65 File Offset: 0x00000C65
            public List<int> InvitedModerators { get; set; }

            // Token: 0x1700008C RID: 140
            // (get) Token: 0x0600014A RID: 330 RVA: 0x00002A6E File Offset: 0x00000C6E
            // (set) Token: 0x0600014B RID: 331 RVA: 0x00002A76 File Offset: 0x00000C76
            public List<int> Moderators { get; set; }

            // Token: 0x1700008D RID: 141
            // (get) Token: 0x0600014C RID: 332 RVA: 0x00002A7F File Offset: 0x00000C7F
            // (set) Token: 0x0600014D RID: 333 RVA: 0x00002A87 File Offset: 0x00000C87
            public List<int> Hosts { get; set; }

            // Token: 0x1700008E RID: 142
            // (get) Token: 0x0600014E RID: 334 RVA: 0x00002A90 File Offset: 0x00000C90
            // (set) Token: 0x0600014F RID: 335 RVA: 0x00002A98 File Offset: 0x00000C98
            public List<int> PlayerIdsWithModPower { get; set; }

            // Token: 0x1700008F RID: 143
            // (get) Token: 0x06000150 RID: 336 RVA: 0x00002AA1 File Offset: 0x00000CA1
            // (set) Token: 0x06000151 RID: 337 RVA: 0x00002AA9 File Offset: 0x00000CA9
            public List<int> InvitedHosts { get; set; }

            // Token: 0x17000090 RID: 144
            // (get) Token: 0x06000152 RID: 338 RVA: 0x00002AB2 File Offset: 0x00000CB2
            // (set) Token: 0x06000153 RID: 339 RVA: 0x00002ABA File Offset: 0x00000CBA
            public int CheerCount { get; set; }

            // Token: 0x17000091 RID: 145
            // (get) Token: 0x06000154 RID: 340 RVA: 0x00002AC3 File Offset: 0x00000CC3
            // (set) Token: 0x06000155 RID: 341 RVA: 0x00002ACB File Offset: 0x00000CCB
            public int LocalPlayerRole { get; set; }

            // Token: 0x17000092 RID: 146
            // (get) Token: 0x06000156 RID: 342 RVA: 0x00002AD4 File Offset: 0x00000CD4
            // (set) Token: 0x06000157 RID: 343 RVA: 0x00002ADC File Offset: 0x00000CDC
            public int localPlayerRole { get; set; }

            // Token: 0x17000093 RID: 147
            // (get) Token: 0x06000158 RID: 344 RVA: 0x00002AE5 File Offset: 0x00000CE5
            // (set) Token: 0x06000159 RID: 345 RVA: 0x00002AED File Offset: 0x00000CED
            public int FavoriteCount { get; set; }

            // Token: 0x17000094 RID: 148
            // (get) Token: 0x0600015A RID: 346 RVA: 0x00002AF6 File Offset: 0x00000CF6
            // (set) Token: 0x0600015B RID: 347 RVA: 0x00002AFE File Offset: 0x00000CFE
            public int VisitCount { get; set; }

            // Token: 0x17000095 RID: 149
            // (get) Token: 0x0600015C RID: 348 RVA: 0x00002B07 File Offset: 0x00000D07
            // (set) Token: 0x0600015D RID: 349 RVA: 0x00002B0F File Offset: 0x00000D0F
            public List<GameSessions.Tags> Tags { get; set; }

            // Token: 0x17000096 RID: 150
            // (get) Token: 0x0600015E RID: 350 RVA: 0x00002B18 File Offset: 0x00000D18
            // (set) Token: 0x0600015F RID: 351 RVA: 0x00002B20 File Offset: 0x00000D20
            public bool beta { get; set; }
        }

        // Token: 0x0200001D RID: 29
        public class Room
        {
            // Token: 0x17000097 RID: 151
            // (get) Token: 0x06000161 RID: 353 RVA: 0x00002B29 File Offset: 0x00000D29
            // (set) Token: 0x06000162 RID: 354 RVA: 0x00002B31 File Offset: 0x00000D31
            public int RoomId { get; set; }

            // Token: 0x17000098 RID: 152
            // (get) Token: 0x06000163 RID: 355 RVA: 0x00002B3A File Offset: 0x00000D3A
            // (set) Token: 0x06000164 RID: 356 RVA: 0x00002B42 File Offset: 0x00000D42
            public string Name { get; set; }

            // Token: 0x17000099 RID: 153
            // (get) Token: 0x06000165 RID: 357 RVA: 0x00002B4B File Offset: 0x00000D4B
            // (set) Token: 0x06000166 RID: 358 RVA: 0x00002B53 File Offset: 0x00000D53
            public string Description { get; set; }

            // Token: 0x1700009A RID: 154
            // (get) Token: 0x06000167 RID: 359 RVA: 0x00002B5C File Offset: 0x00000D5C
            // (set) Token: 0x06000168 RID: 360 RVA: 0x00002B64 File Offset: 0x00000D64
            public string ImageName { get; set; }

            // Token: 0x1700009B RID: 155
            // (get) Token: 0x06000169 RID: 361 RVA: 0x00002B6D File Offset: 0x00000D6D
            // (set) Token: 0x0600016A RID: 362 RVA: 0x00002B75 File Offset: 0x00000D75
            public ulong CreatorPlayerId { get; set; }

            // Token: 0x1700009C RID: 156
            // (get) Token: 0x0600016B RID: 363 RVA: 0x00002B7E File Offset: 0x00000D7E
            // (set) Token: 0x0600016C RID: 364 RVA: 0x00002B86 File Offset: 0x00000D86
            public ulong creatorPlayerId { get; set; }

            // Token: 0x1700009D RID: 157
            // (get) Token: 0x0600016D RID: 365 RVA: 0x00002B8F File Offset: 0x00000D8F
            // (set) Token: 0x0600016E RID: 366 RVA: 0x00002B97 File Offset: 0x00000D97
            public int State { get; set; }

            // Token: 0x1700009E RID: 158
            // (get) Token: 0x0600016F RID: 367 RVA: 0x00002BA0 File Offset: 0x00000DA0
            // (set) Token: 0x06000170 RID: 368 RVA: 0x00002BA8 File Offset: 0x00000DA8
            public int Accessibility { get; set; }

            // Token: 0x1700009F RID: 159
            // (get) Token: 0x06000171 RID: 369 RVA: 0x00002BB1 File Offset: 0x00000DB1
            // (set) Token: 0x06000172 RID: 370 RVA: 0x00002BB9 File Offset: 0x00000DB9
            public bool SupportsLevelVoting { get; set; }

            // Token: 0x170000A0 RID: 160
            // (get) Token: 0x06000173 RID: 371 RVA: 0x00002BC2 File Offset: 0x00000DC2
            // (set) Token: 0x06000174 RID: 372 RVA: 0x00002BCA File Offset: 0x00000DCA
            public bool IsAGRoom { get; set; }

            // Token: 0x170000A1 RID: 161
            // (get) Token: 0x06000175 RID: 373 RVA: 0x00002BD3 File Offset: 0x00000DD3
            // (set) Token: 0x06000176 RID: 374 RVA: 0x00002BDB File Offset: 0x00000DDB
            public bool IsDormRoom { get; set; }

            // Token: 0x170000A2 RID: 162
            // (get) Token: 0x06000177 RID: 375 RVA: 0x00002BE4 File Offset: 0x00000DE4
            // (set) Token: 0x06000178 RID: 376 RVA: 0x00002BEC File Offset: 0x00000DEC
            public bool CloningAllowed { get; set; }

            // Token: 0x170000A3 RID: 163
            // (get) Token: 0x06000179 RID: 377 RVA: 0x00002BF5 File Offset: 0x00000DF5
            // (set) Token: 0x0600017A RID: 378 RVA: 0x00002BFD File Offset: 0x00000DFD
            public bool SupportsVRLow { get; set; }

            // Token: 0x170000A4 RID: 164
            // (get) Token: 0x0600017B RID: 379 RVA: 0x00002C06 File Offset: 0x00000E06
            // (set) Token: 0x0600017C RID: 380 RVA: 0x00002C0E File Offset: 0x00000E0E
            public bool SupportsScreens { get; set; }

            // Token: 0x170000A5 RID: 165
            // (get) Token: 0x0600017D RID: 381 RVA: 0x00002C17 File Offset: 0x00000E17
            // (set) Token: 0x0600017E RID: 382 RVA: 0x00002C1F File Offset: 0x00000E1F
            public bool SupportsWalkVR { get; set; }

            // Token: 0x170000A6 RID: 166
            // (get) Token: 0x0600017F RID: 383 RVA: 0x00002C28 File Offset: 0x00000E28
            // (set) Token: 0x06000180 RID: 384 RVA: 0x00002C30 File Offset: 0x00000E30
            public bool SupportsTeleportVR { get; set; }

            // Token: 0x170000A7 RID: 167
            // (get) Token: 0x06000181 RID: 385 RVA: 0x00002C39 File Offset: 0x00000E39
            // (set) Token: 0x06000182 RID: 386 RVA: 0x00002C41 File Offset: 0x00000E41
            public bool AllowsJuniors { get; set; }

            // Token: 0x170000A8 RID: 168
            // (get) Token: 0x06000183 RID: 387 RVA: 0x00002C4A File Offset: 0x00000E4A
            // (set) Token: 0x06000184 RID: 388 RVA: 0x00002C52 File Offset: 0x00000E52
            public int RoomWarningMask { get; set; }

            // Token: 0x170000A9 RID: 169
            // (get) Token: 0x06000185 RID: 389 RVA: 0x00002C5B File Offset: 0x00000E5B
            // (set) Token: 0x06000186 RID: 390 RVA: 0x00002C63 File Offset: 0x00000E63
            public string CustomRoomWarning { get; set; }

            // Token: 0x170000AA RID: 170
            // (get) Token: 0x06000187 RID: 391 RVA: 0x00002C6C File Offset: 0x00000E6C
            // (set) Token: 0x06000188 RID: 392 RVA: 0x00002C74 File Offset: 0x00000E74
            public bool DisableMicAutoMute { get; set; }

            // Token: 0x170000AB RID: 171
            // (get) Token: 0x06000189 RID: 393 RVA: 0x00002C7D File Offset: 0x00000E7D
            // (set) Token: 0x0600018A RID: 394 RVA: 0x00002C85 File Offset: 0x00000E85
            public int Type { get; set; }

            // Token: 0x170000AC RID: 172
            // (get) Token: 0x0600018B RID: 395 RVA: 0x00002C8E File Offset: 0x00000E8E
            // (set) Token: 0x0600018C RID: 396 RVA: 0x00002C96 File Offset: 0x00000E96
            public int ListOrder { get; set; }

            // Token: 0x170000AD RID: 173
            // (get) Token: 0x0600018D RID: 397 RVA: 0x00002C9F File Offset: 0x00000E9F
            // (set) Token: 0x0600018E RID: 398 RVA: 0x00002CA7 File Offset: 0x00000EA7
            public int RoomOrPlaylistId { get; set; }

            // Token: 0x170000AE RID: 174
            // (get) Token: 0x0600018F RID: 399 RVA: 0x00002CB0 File Offset: 0x00000EB0
            // (set) Token: 0x06000190 RID: 400 RVA: 0x00002CB8 File Offset: 0x00000EB8
            public int RoomPlaylistId { get; set; }

            // Token: 0x170000AF RID: 175
            // (get) Token: 0x06000191 RID: 401 RVA: 0x00002CC1 File Offset: 0x00000EC1
            // (set) Token: 0x06000192 RID: 402 RVA: 0x00002CC9 File Offset: 0x00000EC9
            public bool DisableRoomComments { get; set; }

            // Token: 0x170000B0 RID: 176
            // (get) Token: 0x06000193 RID: 403 RVA: 0x00002CD2 File Offset: 0x00000ED2
            // (set) Token: 0x06000194 RID: 404 RVA: 0x00002CDA File Offset: 0x00000EDA
            public bool EncryptVoiceChat { get; set; }

            // Token: 0x170000B1 RID: 177
            // (get) Token: 0x06000195 RID: 405 RVA: 0x00002CE3 File Offset: 0x00000EE3
            // (set) Token: 0x06000196 RID: 406 RVA: 0x00002CEB File Offset: 0x00000EEB
            public bool SupportsMobile { get; set; }

            // Token: 0x170000B2 RID: 178
            // (get) Token: 0x06000197 RID: 407 RVA: 0x00002CF4 File Offset: 0x00000EF4
            // (set) Token: 0x06000198 RID: 408 RVA: 0x00002CFC File Offset: 0x00000EFC
            public int SetType { get; set; }
        }

        // Token: 0x0200001E RID: 30
        public class Scene
        {
            // Token: 0x170000B3 RID: 179
            // (get) Token: 0x0600019A RID: 410 RVA: 0x00002D05 File Offset: 0x00000F05
            // (set) Token: 0x0600019B RID: 411 RVA: 0x00002D0D File Offset: 0x00000F0D
            public int RoomSceneId { get; set; }

            // Token: 0x170000B4 RID: 180
            // (get) Token: 0x0600019C RID: 412 RVA: 0x00002D16 File Offset: 0x00000F16
            // (set) Token: 0x0600019D RID: 413 RVA: 0x00002D1E File Offset: 0x00000F1E
            public int SubRoomId { get; set; }

            // Token: 0x170000B5 RID: 181
            // (get) Token: 0x0600019E RID: 414 RVA: 0x00002D27 File Offset: 0x00000F27
            // (set) Token: 0x0600019F RID: 415 RVA: 0x00002D2F File Offset: 0x00000F2F
            public int RoomId { get; set; }

            // Token: 0x170000B6 RID: 182
            // (get) Token: 0x060001A0 RID: 416 RVA: 0x00002D38 File Offset: 0x00000F38
            // (set) Token: 0x060001A1 RID: 417 RVA: 0x00002D40 File Offset: 0x00000F40
            public string RoomSceneLocationId { get; set; }

            // Token: 0x170000B7 RID: 183
            // (get) Token: 0x060001A2 RID: 418 RVA: 0x00002D49 File Offset: 0x00000F49
            // (set) Token: 0x060001A3 RID: 419 RVA: 0x00002D51 File Offset: 0x00000F51
            public string Location { get; set; }

            // Token: 0x170000B8 RID: 184
            // (get) Token: 0x060001A4 RID: 420 RVA: 0x00002D5A File Offset: 0x00000F5A
            // (set) Token: 0x060001A5 RID: 421 RVA: 0x00002D62 File Offset: 0x00000F62
            public string Name { get; set; }

            // Token: 0x170000B9 RID: 185
            // (get) Token: 0x060001A6 RID: 422 RVA: 0x00002D6B File Offset: 0x00000F6B
            // (set) Token: 0x060001A7 RID: 423 RVA: 0x00002D73 File Offset: 0x00000F73
            public bool IsSandbox { get; set; }

            // Token: 0x170000BA RID: 186
            // (get) Token: 0x060001A8 RID: 424 RVA: 0x00002D7C File Offset: 0x00000F7C
            // (set) Token: 0x060001A9 RID: 425 RVA: 0x00002D84 File Offset: 0x00000F84
            public string? DataBlobName { get; set; }

            // Token: 0x170000BB RID: 187
            // (get) Token: 0x060001AA RID: 426 RVA: 0x00002D8D File Offset: 0x00000F8D
            // (set) Token: 0x060001AB RID: 427 RVA: 0x00002D95 File Offset: 0x00000F95
            public int MaxPlayers { get; set; }

            // Token: 0x170000BC RID: 188
            // (get) Token: 0x060001AC RID: 428 RVA: 0x00002D9E File Offset: 0x00000F9E
            // (set) Token: 0x060001AD RID: 429 RVA: 0x00002DA6 File Offset: 0x00000FA6
            public bool CanMatchmakeInto { get; set; }

            // Token: 0x170000BD RID: 189
            // (get) Token: 0x060001AE RID: 430 RVA: 0x00002DAF File Offset: 0x00000FAF
            // (set) Token: 0x060001AF RID: 431 RVA: 0x00002DB7 File Offset: 0x00000FB7
            public DateTime DataModifiedAt { get; set; }
        }

        // Token: 0x0200001F RID: 31
        public class Tags
        {
            // Token: 0x170000BE RID: 190
            // (get) Token: 0x060001B1 RID: 433 RVA: 0x00002DC0 File Offset: 0x00000FC0
            // (set) Token: 0x060001B2 RID: 434 RVA: 0x00002DC8 File Offset: 0x00000FC8
            public string Tag { get; set; }

            // Token: 0x170000BF RID: 191
            // (get) Token: 0x060001B3 RID: 435 RVA: 0x00002DD1 File Offset: 0x00000FD1
            // (set) Token: 0x060001B4 RID: 436 RVA: 0x00002DD9 File Offset: 0x00000FD9
            public int Type { get; set; }
        }

        public static Dictionary<string, GameSessions.RoomRoot> RROS = new Dictionary<string, GameSessions.RoomRoot>
        {
            {
                "DormRoom",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 1,
                        Name = "DormRoom",
                        Description = "Your own private dorm.",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "DormRoom.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        DisableMicAutoMute = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        AllowsJuniors = false,
                        IsDormRoom = true,
                        SupportsVRLow = true,
                        CustomRoomWarning = "",
                        RoomWarningMask = 0,
                        EncryptVoiceChat = false,
                        DisableRoomComments = true,
                        SupportsMobile = true,
                        SetType = 0,
                        Type = 0,
                        creatorPlayerId = 0UL
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 1,
                            RoomId = 1,
                            Location = "76d98498-60a1-430c-ab76-b54a29b7a163",
                            Name = "Home",
                            IsSandbox = true,
                            DataBlobName = null,
                            MaxPlayers = 4,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now,
                            RoomSceneLocationId = "76d98498-60a1-430c-ab76-b54a29b7a163"
                        }
                    },
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    CoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    InvitedHosts = new List<int>(),
                    InvitedModerators = new List<int>(),
                    Moderators = new List<int>(),
                    Tags = new List<GameSessions.Tags>(),
                    LocalPlayerRole = 3
                }
            },
            {
                "RecCenter",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 2,
                        Name = "RecCenter",
                        Description = "Rec Center.",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "RecCenter.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        DisableMicAutoMute = true,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        AllowsJuniors = true,
                        IsDormRoom = false,
                        SupportsVRLow = true,
                        CustomRoomWarning = "",
                        RoomWarningMask = 0
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 1,
                            RoomId = 2,
                            Location = "cbad71af-0831-44d8-b8ef-69edafa841f6",
                            Name = "Home",
                            IsSandbox = false,
                            DataBlobName = "",
                            MaxPlayers = 4,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now,
                            RoomSceneLocationId = "cbad71af-0831-44d8-b8ef-69edafa841f6"
                        }
                    },
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    CoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    InvitedHosts = new List<int>(),
                    InvitedModerators = new List<int>(),
                    Moderators = new List<int>(),
                    LocalPlayerRole = 3,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "Rec Center",
                            Type = 1
                        }
                    }
                }
            },
            {
                "Paddleball",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 3,
                        Name = "Paddleball",
                        Description = "A simple rally game between two players in a plexiglass tube with a zero-g ball.",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "PaddleBall.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 3,
                            RoomId = 3,
                            RoomSceneLocationId = "d89f74fa-d51e-477a-a425-025a891dd499",
                            Name = "paddleball",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 2,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        },
                        new GameSessions.Tags
                        {
                            Tag = "sport",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Dodgeball",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 4,
                        Name = "Dodgeball",
                        Description = "Throw dodgeballs to knock out your friends in this gym classic!",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "Dodgeball.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 4,
                            RoomId = 4,
                            RoomSceneLocationId = "3d474b26-26f7-45e9-9a36-9b02847d5e6f",
                            Name = "dodgeball",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        },
                        new GameSessions.Tags
                        {
                            Tag = "sport",
                            Type = 2
                        }
                    }
                }
            },
            {
                "DiscGolfPropultion",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 5,
                        Name = "DiscGolfPropultion",
                        Description = "Disk Golf with a twist!",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "DiscGolfPropultion.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 2,
                            RoomId = 1,
                            RoomSceneLocationId = "d9378c9f-80bc-46fb-ad1e-1bed8a674f55",
                            Name = "Propultion",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 4,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Paintball",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 6,
                        Name = "Paintball",
                        Description = "there are paints everyware.",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "Paintball.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = true,
                        IsAGRoom = true,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 0,
                            RoomId = 6,
                            RoomSceneLocationId = "e122fe98-e7db-49e8-a1b1-105424b6e1f0",
                            Name = "River",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new GameSessions.Scene
                        {
                            RoomSceneId = 1,
                            RoomId = 7,
                            RoomSceneLocationId = "a785267d-c579-42ea-be43-fec1992d1ca7",
                            Name = "Homestead",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new GameSessions.Scene
                        {
                            RoomSceneId = 2,
                            RoomId = 8,
                            RoomSceneLocationId = "ff4c6427-7079-4f59-b22a-69b089420827",
                            Name = "Quarry",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new GameSessions.Scene
                        {
                            RoomSceneId = 3,
                            RoomId = 9,
                            RoomSceneLocationId = "380d18b5-de9c-49f3-80f7-f4a95c1de161",
                            Name = "Clearcut",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new GameSessions.Scene
                        {
                            RoomSceneId = 4,
                            RoomId = 10,
                            RoomSceneLocationId = "58763055-2dfb-4814-80b8-16fac5c85709",
                            Name = "Spillway",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new GameSessions.Scene
                        {
                            RoomSceneId = 5,
                            RoomId = 11,
                            RoomSceneLocationId = "65ddbb48-5a01-4e3e-972d-e5c7419e2bc3",
                            Name = "DriveIn",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "LaserTag",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 12,
                        Name = "LaserTag",
                        Description = "(bots included)",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "LaserTag.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = true,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 0,
                            RoomId = 12,
                            RoomSceneLocationId = "239e676c-f12f-489f-bf3a-d4c383d692c3",
                            Name = "Hanger",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new GameSessions.Scene
                        {
                            RoomSceneId = 1,
                            RoomId = 13,
                            RoomSceneLocationId = "9d6456ce-6264-48b4-808d-2d96b3d91038",
                            Name = "CyberJunkCity",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Bowling",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 14,
                        Name = "Bowling",
                        Description = "Wii Sports Bowling",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "bowling.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 20,
                            RoomId = 14,
                            RoomSceneLocationId = "ae929543-9a07-41d5-8ee9-dbbee8c36800",
                            Name = "wii",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "RecRoyaleSquads",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 15,
                        Name = "RecRoyaleSquads",
                        Description = "Squad up with 3 other players in this battle royale!",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "RecRoyale.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = true,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 22,
                            RoomId = 15,
                            RoomSceneLocationId = "253fa009-6e65-4c90-91a1-7137a56a267f",
                            Name = "home",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "RecRoyaleSolos",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 16,
                        Name = "RecRoyaleSolos",
                        Description = "fortnite",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "RecRoyale.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = true,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 23,
                            RoomId = 16,
                            RoomSceneLocationId = "b010171f-4875-4e89-baba-61e878cd41e1",
                            Name = "home",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "RecRoyaleSandbox",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 17,
                        Name = "RecRoyaleSandbox",
                        Description = "Use the maker pen on a very big map",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "RecRoyale.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 23,
                            RoomId = 17,
                            RoomSceneLocationId = "b010171f-4875-4e89-baba-61e878cd41e1",
                            Name = "home",
                            IsSandbox = true,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    LocalPlayerRole = 3,
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Lounge",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 18,
                        Name = "Lounge",
                        Description = "A small, cozy lounge to hang out with your friends",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "Lounge.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 24,
                            RoomId = 18,
                            RoomSceneLocationId = "a067557f-ca32-43e6-b6e5-daaec60b4f5a",
                            Name = "Lounge",
                            IsSandbox = true,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Regestration",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 19,
                        Name = "Regestration",
                        Description = "Regester your account and make your game die",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "Regestration.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = true,
                        IsAGRoom = true,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 27,
                            RoomId = 19,
                            RoomSceneLocationId = "cf61556d-68fd-4288-9ae5-7a512621e569",
                            Name = "Regestration",
                            IsSandbox = true,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Orientation",
                new GameSessions.RoomRoot
                {
                    Room = new GameSessions.Room
                    {
                        RoomId = 20,
                        Name = "Orientation",
                        Description = "welcome to orientation",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "Orientation.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = true,
                        IsAGRoom = false,
                        CloningAllowed = true,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<GameSessions.Scene>
                    {
                        new GameSessions.Scene
                        {
                            RoomSceneId = 27,
                            RoomId = 20,
                            RoomSceneLocationId = "c79709d8-a31b-48aa-9eb8-cc31ba9505e8",
                            Name = "intro",
                            IsSandbox = true,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<GameSessions.Tags>
                    {
                        new GameSessions.Tags
                        {
                            Tag = "rro",
                            Type = 2
                        },
                        new GameSessions.Tags
                        {
                            Tag = "Orientation",
                            Type = 2
                        },
                        new GameSessions.Tags
                        {
                            Tag = "welcome center",
                            Type = 2
                        },
                        new GameSessions.Tags
                        {
                            Tag = "hell",
                            Type = 2
                        }
                    }
                }
            }
        };

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

            };*/
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
                location = "",
                MaxCapacity = 20,
                Name = "",
                photonRegionId = "us",
                photonRoomId = "",
                roomCode = null,
                roomId = 0,
                roomInstanceId = 1,
                roomInstanceType = 2,
                subRoomId = 0,

            };
			Config.localGameSessionv3 = null;
			*/
            return JsonConvert.SerializeObject(new GameSessions.JoinResultnone
            {
                errorCode = 0,
                roomInstance = null,
                //roomInstance = Config.localGameSessionv3,

            });
        }

        // Token: 0x060000C1 RID: 193 RVA: 0x0000257B File Offset: 0x0000077B
        public static GameSessions.PlayerStatus StatusSessionInstance()
		{
			return new GameSessions.PlayerStatus
			{
				PlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
				IsOnline = true,
				InScreenMode = false,
				GameSession = GameSessions.match.roomInstance
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

        public static string FindRoomid(ulong roomname)
        {

            foreach (KeyValuePair<string, c00005d.rooms_details> keyValuePair in c00005d.rooms_details_list)
            {
                bool flag = keyValuePair.Value.RoomId == (ulong)roomname;
                if (flag)
                {

                    return keyValuePair.Value.Name;
                }
            }
            return "";
        }

        // Token: 0x02000021 RID: 33
        public enum JoinResultIDs
		{
			// Token: 0x0400005E RID: 94
			Success,
			// Token: 0x0400005F RID: 95
			NoSuchGame,
			// Token: 0x04000060 RID: 96
			PlayerNotOnline,
			// Token: 0x04000061 RID: 97
			InsufficientSpace,
			// Token: 0x04000062 RID: 98
			EventNotStarted,
			// Token: 0x04000063 RID: 99
			EventAlreadyFinished,
			// Token: 0x04000064 RID: 100
			EventCreatorNotReady,
			// Token: 0x04000065 RID: 101
			BlockedFromRoom,
			// Token: 0x04000066 RID: 102
			ProfileLocked,
			// Token: 0x04000067 RID: 103
			NoBirthday,
			// Token: 0x04000068 RID: 104
			MarkedForDelete,
			// Token: 0x04000069 RID: 105
			JuniorNotAllowed,
			// Token: 0x0400006A RID: 106
			Banned,
			// Token: 0x0400006B RID: 107
			NoSuchRoom = 20,
			// Token: 0x0400006C RID: 108
			RoomCreatorNotReady,
			// Token: 0x0400006D RID: 109
			RoomIsNotActive,
			// Token: 0x0400006E RID: 110
			RoomBlockedByCreator,
			// Token: 0x0400006F RID: 111
			RoomBlockingCreator,
			// Token: 0x04000070 RID: 112
			RoomIsPrivate
		}

        // Token: 0x02000022 RID: 34
        public class PlayerStatusold
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
            public GameSessions.RoomInstance GameSession { get; set; }
        }


        public class SessionInstanceold
        {
            // Token: 0x17000043 RID: 67
            // (get) Token: 0x060000CC RID: 204 RVA: 0x000025EA File Offset: 0x000007EA
            // (set) Token: 0x060000CD RID: 205 RVA: 0x000025F2 File Offset: 0x000007F2
            public long GameSessionId { get; set; }

            // Token: 0x17000044 RID: 68
            // (get) Token: 0x060000CE RID: 206 RVA: 0x000025FB File Offset: 0x000007FB
            // (set) Token: 0x060000CF RID: 207 RVA: 0x00002603 File Offset: 0x00000803
            public string RegionId { get; set; }

            // Token: 0x17000045 RID: 69
            // (get) Token: 0x060000D0 RID: 208 RVA: 0x0000260C File Offset: 0x0000080C
            // (set) Token: 0x060000D1 RID: 209 RVA: 0x00002614 File Offset: 0x00000814
            public string RoomId { get; set; }

            // Token: 0x17000046 RID: 70
            // (get) Token: 0x060000D2 RID: 210 RVA: 0x0000261D File Offset: 0x0000081D
            // (set) Token: 0x060000D3 RID: 211 RVA: 0x00002625 File Offset: 0x00000825
            public long? EventId { get; set; }

            // Token: 0x17000047 RID: 71
            // (get) Token: 0x060000D4 RID: 212 RVA: 0x0000262E File Offset: 0x0000082E
            // (set) Token: 0x060000D5 RID: 213 RVA: 0x00002636 File Offset: 0x00000836
            public long? RecRoomId { get; set; }

            // Token: 0x17000048 RID: 72
            // (get) Token: 0x060000D6 RID: 214 RVA: 0x0000263F File Offset: 0x0000083F
            // (set) Token: 0x060000D7 RID: 215 RVA: 0x00002647 File Offset: 0x00000847
            public long? CreatorPlayerId { get; set; }

            // Token: 0x17000049 RID: 73
            // (get) Token: 0x060000D8 RID: 216 RVA: 0x00002650 File Offset: 0x00000850
            // (set) Token: 0x060000D9 RID: 217 RVA: 0x00002658 File Offset: 0x00000858
            public string Name { get; set; }

            // Token: 0x1700004A RID: 74
            // (get) Token: 0x060000DA RID: 218 RVA: 0x00002661 File Offset: 0x00000861
            // (set) Token: 0x060000DB RID: 219 RVA: 0x00002669 File Offset: 0x00000869
            public string ActivityLevelId { get; set; }

            // Token: 0x1700004B RID: 75
            // (get) Token: 0x060000DC RID: 220 RVA: 0x00002672 File Offset: 0x00000872
            // (set) Token: 0x060000DD RID: 221 RVA: 0x0000267A File Offset: 0x0000087A
            public bool Private { get; set; }

            // Token: 0x1700004C RID: 76
            // (get) Token: 0x060000DE RID: 222 RVA: 0x00002683 File Offset: 0x00000883
            // (set) Token: 0x060000DF RID: 223 RVA: 0x0000268B File Offset: 0x0000088B
            public bool Sandbox { get; set; }

            // Token: 0x1700004D RID: 77
            // (get) Token: 0x060000E0 RID: 224 RVA: 0x00002694 File Offset: 0x00000894
            // (set) Token: 0x060000E1 RID: 225 RVA: 0x0000269C File Offset: 0x0000089C
            public bool SupportsVR { get; set; }

            // Token: 0x1700004E RID: 78
            // (get) Token: 0x060000E2 RID: 226 RVA: 0x000026A5 File Offset: 0x000008A5
            // (set) Token: 0x060000E3 RID: 227 RVA: 0x000026AD File Offset: 0x000008AD
            public bool SupportsScreens { get; set; }

            // Token: 0x1700004F RID: 79
            // (get) Token: 0x060000E4 RID: 228 RVA: 0x000026B6 File Offset: 0x000008B6
            // (set) Token: 0x060000E5 RID: 229 RVA: 0x000026BE File Offset: 0x000008BE
            public bool GameInProgress { get; set; }

            // Token: 0x17000050 RID: 80
            // (get) Token: 0x060000E6 RID: 230 RVA: 0x000026C7 File Offset: 0x000008C7
            // (set) Token: 0x060000E7 RID: 231 RVA: 0x000026CF File Offset: 0x000008CF
            public int MaxCapacity { get; set; }

            // Token: 0x17000051 RID: 81
            // (get) Token: 0x060000E8 RID: 232 RVA: 0x000026D8 File Offset: 0x000008D8
            // (set) Token: 0x060000E9 RID: 233 RVA: 0x000026E0 File Offset: 0x000008E0
            public bool IsFull { get; set; }
        }

        // Token: 0x02000023 RID: 35
        public class SessionInstance
        {
            // Token: 0x17000043 RID: 67
            // (get) Token: 0x060000CC RID: 204 RVA: 0x000025EA File Offset: 0x000007EA
            // (set) Token: 0x060000CD RID: 205 RVA: 0x000025F2 File Offset: 0x000007F2
            public long GameSessionId { get; set; }

            // Token: 0x17000044 RID: 68
            // (get) Token: 0x060000CE RID: 206 RVA: 0x000025FB File Offset: 0x000007FB
            // (set) Token: 0x060000CF RID: 207 RVA: 0x00002603 File Offset: 0x00000803
            public string RegionId { get; set; }

            // Token: 0x17000045 RID: 69
            // (get) Token: 0x060000D0 RID: 208 RVA: 0x0000260C File Offset: 0x0000080C
            // (set) Token: 0x060000D1 RID: 209 RVA: 0x00002614 File Offset: 0x00000814
            public string RoomId { get; set; }

            // Token: 0x17000046 RID: 70
            // (get) Token: 0x060000D2 RID: 210 RVA: 0x0000261D File Offset: 0x0000081D
            // (set) Token: 0x060000D3 RID: 211 RVA: 0x00002625 File Offset: 0x00000825
            public long? EventId { get; set; }

            // Token: 0x17000047 RID: 71
            // (get) Token: 0x060000D4 RID: 212 RVA: 0x0000262E File Offset: 0x0000082E
            // (set) Token: 0x060000D5 RID: 213 RVA: 0x00002636 File Offset: 0x00000836
            public long? RecRoomId { get; set; }

            // Token: 0x17000048 RID: 72
            // (get) Token: 0x060000D6 RID: 214 RVA: 0x0000263F File Offset: 0x0000083F
            // (set) Token: 0x060000D7 RID: 215 RVA: 0x00002647 File Offset: 0x00000847
            public long? CreatorPlayerId { get; set; }

            // Token: 0x17000049 RID: 73
            // (get) Token: 0x060000D8 RID: 216 RVA: 0x00002650 File Offset: 0x00000850
            // (set) Token: 0x060000D9 RID: 217 RVA: 0x00002658 File Offset: 0x00000858
            public string Name { get; set; }

            // Token: 0x1700004A RID: 74
            // (get) Token: 0x060000DA RID: 218 RVA: 0x00002661 File Offset: 0x00000861
            // (set) Token: 0x060000DB RID: 219 RVA: 0x00002669 File Offset: 0x00000869
            public string ActivityLevelId { get; set; }

            // Token: 0x1700004B RID: 75
            // (get) Token: 0x060000DC RID: 220 RVA: 0x00002672 File Offset: 0x00000872
            // (set) Token: 0x060000DD RID: 221 RVA: 0x0000267A File Offset: 0x0000087A
            public bool Private { get; set; }

            // Token: 0x1700004C RID: 76
            // (get) Token: 0x060000DE RID: 222 RVA: 0x00002683 File Offset: 0x00000883
            // (set) Token: 0x060000DF RID: 223 RVA: 0x0000268B File Offset: 0x0000088B
            public bool Sandbox { get; set; }

            // Token: 0x1700004D RID: 77
            // (get) Token: 0x060000E0 RID: 224 RVA: 0x00002694 File Offset: 0x00000894
            // (set) Token: 0x060000E1 RID: 225 RVA: 0x0000269C File Offset: 0x0000089C
            public bool SupportsVR { get; set; }

            // Token: 0x1700004E RID: 78
            // (get) Token: 0x060000E2 RID: 226 RVA: 0x000026A5 File Offset: 0x000008A5
            // (set) Token: 0x060000E3 RID: 227 RVA: 0x000026AD File Offset: 0x000008AD
            public bool SupportsScreens { get; set; }

            // Token: 0x1700004F RID: 79
            // (get) Token: 0x060000E4 RID: 228 RVA: 0x000026B6 File Offset: 0x000008B6
            // (set) Token: 0x060000E5 RID: 229 RVA: 0x000026BE File Offset: 0x000008BE
            public bool GameInProgress { get; set; }

            // Token: 0x17000050 RID: 80
            // (get) Token: 0x060000E6 RID: 230 RVA: 0x000026C7 File Offset: 0x000008C7
            // (set) Token: 0x060000E7 RID: 231 RVA: 0x000026CF File Offset: 0x000008CF
            public int MaxCapacity { get; set; }

            // Token: 0x17000051 RID: 81
            // (get) Token: 0x060000E8 RID: 232 RVA: 0x000026D8 File Offset: 0x000008D8
            // (set) Token: 0x060000E9 RID: 233 RVA: 0x000026E0 File Offset: 0x000008E0
            public bool IsFull { get; set; }
        }


        public class SessionInstancev2
        {
            public bool EncryptVoiceChat { get; set; }
            public long? clubId { get; set; }
            public string dataBlob { get; set; }
            public long? EventId { get; set; }
            public bool isFull { get; set; }
            public bool isInProgress { get; set; }
            public bool isPrivate { get; set; }
            public string location { get; set; }
            public int MaxCapacity { get; set; }
            public string Name { get; set; }
            public string photonRegionId { get; set; }
            public string photonRoomId { get; set; }
            public string roomCode { get; set; }
            public long? roomId { get; set; }
            public long? roomInstanceId { get; set; }
            public int roomInstanceType { get; set; } //todo: roomInstanceType
            public long? subRoomId { get; set; }

        }

        public class SessionInstancev3
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
            public bool encryptVoiceChat { get; set; }
            public long? clubId { get; set; }
            public long? EventId { get; set; }
            public string roomCode { get; set; }

        }

        // Token: 0x02000024 RID: 36
        public class JoinRandomRequest
		{
			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060000EB RID: 235 RVA: 0x000026E9 File Offset: 0x000008E9
			// (set) Token: 0x060000EC RID: 236 RVA: 0x000026F1 File Offset: 0x000008F1
			public string[] ActivityLevelIds { get; set; }

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x060000ED RID: 237 RVA: 0x000026FA File Offset: 0x000008FA
			// (set) Token: 0x060000EE RID: 238 RVA: 0x00002702 File Offset: 0x00000902
			public ulong[] ExpectedPlayerIds { get; set; }

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060000EF RID: 239 RVA: 0x0000270B File Offset: 0x0000090B
			// (set) Token: 0x060000F0 RID: 240 RVA: 0x00002713 File Offset: 0x00000913
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
			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060000FB RID: 251 RVA: 0x00002760 File Offset: 0x00000960
			// (set) Token: 0x060000FC RID: 252 RVA: 0x00002768 File Offset: 0x00000968
			public string Region { get; set; }

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060000FD RID: 253 RVA: 0x00002771 File Offset: 0x00000971
			// (set) Token: 0x060000FE RID: 254 RVA: 0x00002779 File Offset: 0x00000979
			public int Ping { get; set; }
		}

		// Token: 0x02000027 RID: 39
		private class JoinResult
		{
			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000100 RID: 256 RVA: 0x00002782 File Offset: 0x00000982
			// (set) Token: 0x06000101 RID: 257 RVA: 0x0000278A File Offset: 0x0000098A
			public int Result { get; set; }

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000102 RID: 258 RVA: 0x00002793 File Offset: 0x00000993
			// (set) Token: 0x06000103 RID: 259 RVA: 0x0000279B File Offset: 0x0000099B
			public GameSessions.SessionInstance GameSession { get; set; }
		}

		public static string myuuidAsString { get; set; }

        public static string gameroomlocation { get; set; }
        public static long gameroomId { get; set; }

        public static long gamesessionid { get; set; }
        public static long gamesessionsubroomid {  get; set; }
        public class JoinResultv2
        {

            public int? errorCode { get; set; } //todo: a custom thing?


            // Token: 0x1700005C RID: 92
            // (get) Token: 0x06000102 RID: 258 RVA: 0x00002793 File Offset: 0x00000993
            // (set) Token: 0x06000103 RID: 259 RVA: 0x0000279B File Offset: 0x0000099B
            public GameSessions.SessionInstancev2 roomInstance { get; set; }


        }
        public class JoinResultv3
        {

            public int? errorCode { get; set; } //todo: a custom thing?

            // Token: 0x1700005C RID: 92
            // (get) Token: 0x06000102 RID: 258 RVA: 0x00002793 File Offset: 0x00000993
            // (set) Token: 0x06000103 RID: 259 RVA: 0x0000279B File Offset: 0x0000099B
            public GameSessions.SessionInstancev3 roomInstance { get; set; }

        }
        public class JoinResultv3old
        {
            // Token: 0x1700005B RID: 91
            // (get) Token: 0x06000100 RID: 256 RVA: 0x00002782 File Offset: 0x00000982
            // (set) Token: 0x06000101 RID: 257 RVA: 0x0000278A File Offset: 0x0000098A
            public string appVersion { get; set; }
            public int deviceClass { get; set; }
            public int? errorCode { get; set; } //todo: a custom thing?

            public bool isOnline { get; set; }

            public long? playerId { get; set; }

            // Token: 0x1700005C RID: 92
            // (get) Token: 0x06000102 RID: 258 RVA: 0x00002793 File Offset: 0x00000993
            // (set) Token: 0x06000103 RID: 259 RVA: 0x0000279B File Offset: 0x0000099B
            public GameSessions.SessionInstancev3 roomInstance { get; set; }

            public int statusVisibility { get; set; }

            public int vrMovementMode { get; set; }

        }
        public class JoinResultnone
        {

            public int? errorCode { get; set; } //todo: a custom thing?


            public GameSessions.SessionInstancev3 roomInstance { get; set; }



        }
    }
}
