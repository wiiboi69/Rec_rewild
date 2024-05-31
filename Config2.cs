using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using api;
using static storefront2018.StoreFronts;

namespace api
{
	// Token: 0x02000013 RID: 19
	internal class Config2
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002215 File Offset: 0x00000415
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000221D File Offset: 0x0000041D
		public string MessageOfTheDay { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002226 File Offset: 0x00000426
		// (set) Token: 0x06000053 RID: 83 RVA: 0x0000222E File Offset: 0x0000042E
		public string CdnBaseUri { get; set; }

        // Token: 0x17000064 RID: 100
        // (get) Token: 0x06000125 RID: 293 RVA: 0x00009175 File Offset: 0x00007375
        // (set) Token: 0x06000126 RID: 294 RVA: 0x0000917D File Offset: 0x0000737D
        public string ShareBaseUrl { get; set; }

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000054 RID: 84 RVA: 0x00002237 File Offset: 0x00000437
        // (set) Token: 0x06000055 RID: 85 RVA: 0x0000223F File Offset: 0x0000043F
        public List<LevelProgressionEntry> LevelProgressionMaps { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002248 File Offset: 0x00000448
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002250 File Offset: 0x00000450
		public MatchmakingConfigParams MatchmakingParams { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002259 File Offset: 0x00000459
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002261 File Offset: 0x00000461
		public Objective[][] DailyObjectives { get; set; }

        // Token: 0x17000067 RID: 103
        // (get) Token: 0x0600012B RID: 299 RVA: 0x000091A8 File Offset: 0x000073A8
        // (set) Token: 0x0600012C RID: 300 RVA: 0x000091B0 File Offset: 0x000073B0
        public ServerMaintainence serverMaintainence { get; set; }

        // Token: 0x17000016 RID: 22
        // (get) Token: 0x0600005A RID: 90 RVA: 0x0000226A File Offset: 0x0000046A
        // (set) Token: 0x0600005B RID: 91 RVA: 0x00002272 File Offset: 0x00000472

        public AutoMicMutingConfig autoMicMutingConfig { get; set; }
        public StorefrontConfig storefrontConfig { get; set; }
        public RoomKeyConfig roomKeyConfig { get; set; }
        public RoomCurrencyConfig roomCurrencyConfig { get; set; }

        public List<ConfigTableEntry> ConfigTable { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000227B File Offset: 0x0000047B
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002283 File Offset: 0x00000483
		public photonConfig PhotonConfig { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00005450 File Offset: 0x00003650
		public static string GetDebugConfig()
		{
			
			return JsonConvert.SerializeObject(new Config2
			{
				MessageOfTheDay = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/motd.txt"),
				CdnBaseUri = "http://localhost:20212/",
                ShareBaseUrl = "http://localhost:20212/",
                LevelProgressionMaps = new List<LevelProgressionEntry>
				{
					new LevelProgressionEntry
					{
                        Level = 0,
						RequiredXp = 1,
                        GiftDropId = null
                    },
					new LevelProgressionEntry
					{
						Level = 1,
						RequiredXp = 2,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 2,
						RequiredXp = 3,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 3,
						RequiredXp = 4,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 4,
						RequiredXp = 5,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 5,
						RequiredXp = 6,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 6,
						RequiredXp = 7,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 7,
						RequiredXp = 8,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 8,
						RequiredXp = 9,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 9,
						RequiredXp = 10,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 10,
						RequiredXp = 11,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 11,
						RequiredXp = 12,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 12,
						RequiredXp = 13,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 13,
						RequiredXp = 14,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 14,
						RequiredXp = 15,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 15,
						RequiredXp = 16,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 16,
						RequiredXp = 17,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 17,
						RequiredXp = 18,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 18,
						RequiredXp = 19,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 19,
						RequiredXp = 20,
                        GiftDropId = 0
                    },
					new LevelProgressionEntry
					{
						Level = 20,
						RequiredXp = 21,
                        GiftDropId = 0
                    }
				},
				MatchmakingParams = new MatchmakingConfigParams
				{
					PreferEmptyRoomsFrequency = 0f,
					PreferFullRoomsFrequency = 1f
				},
				DailyObjectives = Config.dailyObjectives,
                serverMaintainence = new ServerMaintainence
                {
                    StartsInMinutes = 0
                },
                ConfigTable = new List<ConfigTableEntry>
				{
					new ConfigTableEntry
					{
						Key = "Gift.DropChance",
						Value = 0.5f.ToString()
					},
					new ConfigTableEntry
					{
						Key = "Gift.XP",
						Value = 0.5f.ToString()
					}
				},
                PhotonConfig = new photonConfig
                {
                    CloudRegion = "us",
                    CrcCheckEnabled = false,
                    EnableServerTracingAfterDisconnect = false
                },
                autoMicMutingConfig = new AutoMicMutingConfig
                {
                    MicSpamVolumeThreshold = 10.0,
                    MicSpamSamplePercentageForForceMute = 500.0,
                    MicSpamSamplePercentageForForceMuteToEnd = 0.0,
                    MicSpamSamplePercentageForWarning = 100.0,
                    MicSpamSamplePercentageForWarningToEnd = 0.0,
                    MicSpamWarningStateVolumeMultiplier = 10.0,
                    MicVolumeSampleInterval = 1.0,
                    MicVolumeSampleRollingWindowLength = 15.0
                },
                storefrontConfig = new StorefrontConfig
                {
                    MinPlayerLevelForGifting = 0,
                },
                roomKeyConfig = new RoomKeyConfig
                {
                    MaxKeysPerRoom = 100,
                },
                roomCurrencyConfig = new RoomCurrencyConfig
                {
                    AwardCurrencyCooldownSeconds = 10.0f,
                }
            });
		}
        //RoomCurrencyConfig

        public class RoomCurrencyConfig
        {
            public float AwardCurrencyCooldownSeconds {  get; set; }
        }
        public class StorefrontConfig
        {
            public int MinPlayerLevelForGifting { get; set; }
        }
        public class RoomKeyConfig
        {
            public int MaxKeysPerRoom { get; set; }
        }
        public class MatchPrams
        {
            // Token: 0x1700006E RID: 110
            // (get) Token: 0x0600013B RID: 315 RVA: 0x0000922F File Offset: 0x0000742F
            // (set) Token: 0x0600013C RID: 316 RVA: 0x00009237 File Offset: 0x00007437
            public float PreferFullRoomsFrequency { get; set; }

            // Token: 0x1700006F RID: 111
            // (get) Token: 0x0600013D RID: 317 RVA: 0x00009240 File Offset: 0x00007440
            // (set) Token: 0x0600013E RID: 318 RVA: 0x00009248 File Offset: 0x00007448
            public float PreferEmptyRoomsFrequency { get; set; }
        }
        public class ServerMaintainence
        {
            // Token: 0x17000077 RID: 119
            // (get) Token: 0x06000151 RID: 337 RVA: 0x000092E8 File Offset: 0x000074E8
            // (set) Token: 0x06000152 RID: 338 RVA: 0x000092F0 File Offset: 0x000074F0
            public int StartsInMinutes { get; set; }
        }
        public class AutoMicMutingConfig
        {
            // Token: 0x17000078 RID: 120
            // (get) Token: 0x06000154 RID: 340 RVA: 0x00009301 File Offset: 0x00007501
            // (set) Token: 0x06000155 RID: 341 RVA: 0x00009309 File Offset: 0x00007509
            public double MicSpamVolumeThreshold { get; set; }

            // Token: 0x17000079 RID: 121
            // (get) Token: 0x06000156 RID: 342 RVA: 0x00009312 File Offset: 0x00007512
            // (set) Token: 0x06000157 RID: 343 RVA: 0x0000931A File Offset: 0x0000751A
            public double MicVolumeSampleInterval { get; set; }

            // Token: 0x1700007A RID: 122
            // (get) Token: 0x06000158 RID: 344 RVA: 0x00009323 File Offset: 0x00007523
            // (set) Token: 0x06000159 RID: 345 RVA: 0x0000932B File Offset: 0x0000752B
            public double MicVolumeSampleRollingWindowLength { get; set; }

            // Token: 0x1700007B RID: 123
            // (get) Token: 0x0600015A RID: 346 RVA: 0x00009334 File Offset: 0x00007534
            // (set) Token: 0x0600015B RID: 347 RVA: 0x0000933C File Offset: 0x0000753C
            public double MicSpamSamplePercentageForWarning { get; set; }

            // Token: 0x1700007C RID: 124
            // (get) Token: 0x0600015C RID: 348 RVA: 0x00009345 File Offset: 0x00007545
            // (set) Token: 0x0600015D RID: 349 RVA: 0x0000934D File Offset: 0x0000754D
            public double MicSpamSamplePercentageForWarningToEnd { get; set; }

            // Token: 0x1700007D RID: 125
            // (get) Token: 0x0600015E RID: 350 RVA: 0x00009356 File Offset: 0x00007556
            // (set) Token: 0x0600015F RID: 351 RVA: 0x0000935E File Offset: 0x0000755E
            public double MicSpamSamplePercentageForForceMute { get; set; }

            // Token: 0x1700007E RID: 126
            // (get) Token: 0x06000160 RID: 352 RVA: 0x00009367 File Offset: 0x00007567
            // (set) Token: 0x06000161 RID: 353 RVA: 0x0000936F File Offset: 0x0000756F
            public double MicSpamSamplePercentageForForceMuteToEnd { get; set; }

            // Token: 0x1700007F RID: 127
            // (get) Token: 0x06000162 RID: 354 RVA: 0x00009378 File Offset: 0x00007578
            // (set) Token: 0x06000163 RID: 355 RVA: 0x00009380 File Offset: 0x00007580
            public double MicSpamWarningStateVolumeMultiplier { get; set; }
        }
    }
}
