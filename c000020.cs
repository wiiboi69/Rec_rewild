using api;
using gamesesh;
using server;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using static vaultgamesesh.c000020;

namespace vaultgamesesh
{
	// Token: 0x02000007 RID: 7
	internal sealed class c000020
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002B20 File Offset: 0x00000D20
		public static c000020.c000022 m000027()
		{
			bool flag = c000041.f000013 == null;
			bool flag2 = flag;
			bool flag3 = flag2;
			c000041.c000044 gameSession;
			if (flag3)
			{
				gameSession = c000041.m00002f();
			}
			else
			{
				gameSession = c000041.f000013;
			}
			return new c000020.c000022
			{
				PlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
				IsOnline = true,
				PlayerType = 2,
				GameSession = gameSession
			};
		}

        public static c000020.player_heartbeat_datav2 player_heartbeat()
        {
            bool flag = Config.localGameSessionv3 == null;

            GameSessions.SessionInstancev3 gameSession;
            /*
            if (flag)
            {
                gameSession = c000041.player_heartbeat_room();
                //gameSession = null;
            }
            else
            {
                gameSession = Config.localGameSessionv3;
            }
            */
            gameSession = Config.localGameSessionv3;
            Config.playerHeartbeatDatav2 = new c000020.player_heartbeat_datav2
            {
                PlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                statusVisibility = 0,
                deviceClass = 2,
                vrMovementMode = 1,
                roomInstance = gameSession,
                IsOnline = true,
                appVersion = APIServer.CachedversionID.ToString(),
                errorCode = null,

            };
			return Config.playerHeartbeatDatav2;
        }

        public static c000020.player_heartbeat_datav2 player_heartbeat_websocket()
        {

            return new c000020.player_heartbeat_datav2
            {
                PlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                statusVisibility = 0,
                deviceClass = 0,
                vrMovementMode = 1,
                roomInstance = Config.localGameSessionv3,
                IsOnline = true,
                appVersion = APIServer.CachedversionID.ToString(),
                errorCode = null,

            };
        }

        public static c000020.player_heartbeat_data player_heartbeatold()
        {
            bool flag = Config.localGameSessionv2 == null;


            GameSessions.SessionInstancev2 gameSessionold;
            
            if (flag)
            {
                gameSessionold = c000041.player_heartbeat_room_old();
            }
            else
            {
                gameSessionold = Config.localGameSessionv2;
            }

            Config.playerHeartbeatData = new c000020.player_heartbeat_data
            {
                errorCode = null,
                roomInstance = gameSessionold,
                GameSession = gameSessionold,

            };
			return Config.playerHeartbeatData;

        }

        // player_heartbeat
        // Token: 0x0200003C RID: 60
        public sealed class c000022
		{
			// Token: 0x17000088 RID: 136
			// (get) Token: 0x0600017E RID: 382 RVA: 0x0000A768 File Offset: 0x00008968
			// (set) Token: 0x0600017F RID: 383 RVA: 0x0000A780 File Offset: 0x00008980
			public ulong PlayerId {  get; set; }


			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000180 RID: 384 RVA: 0x0000A78C File Offset: 0x0000898C
			// (set) Token: 0x06000181 RID: 385 RVA: 0x0000A7A4 File Offset: 0x000089A4
			public bool IsOnline { get; set; }
			

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000182 RID: 386 RVA: 0x0000A7B0 File Offset: 0x000089B0
			// (set) Token: 0x06000183 RID: 387 RVA: 0x0000A7C8 File Offset: 0x000089C8
			public int PlayerType { get; set; }


			// Token: 0x1700008B RID: 139
			// (get) Token: 0x06000184 RID: 388 RVA: 0x0000A7D4 File Offset: 0x000089D4
			// (set) Token: 0x06000185 RID: 389 RVA: 0x0000A7EC File Offset: 0x000089EC
			public c000041.c000044 GameSession
			{
				[CompilerGenerated]
				get
				{
					return this.f000038;
				}
				[CompilerGenerated]
				set
				{
					this.f000038 = value;
				}
			}

			// Token: 0x040000C4 RID: 196
			private ulong f000001;

			// Token: 0x040000C5 RID: 197
			private bool f000037;

			// Token: 0x040000C6 RID: 198
			private int f000020;

			// Token: 0x040000C7 RID: 199
			private c000041.c000044 f000038;
		}

        public sealed class player_heartbeat_data
        {

            public int? errorCode { get; set; }


            public GameSessions.SessionInstancev2 roomInstance { get; set; }


            public GameSessions.SessionInstancev2 GameSession { get; set; }


        }
        public sealed class player_heartbeat_datav2
        {
            public string appVersion { get; set; }
            public int deviceClass { get; set; }
            public int? errorCode { get; set; }
            public bool IsOnline { get; set; }
            public ulong PlayerId { get; set; }

            public GameSessions.SessionInstancev3 roomInstance { get; set; }

            public int statusVisibility { get; set; }

            public int vrMovementMode { get; set; }

        }
    }
}
