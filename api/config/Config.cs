using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using api;
using System.Security.AccessControl;

namespace api
{
	internal class Config
	{

        public static GameSessions.SessionInstance localGameSession;
        public static GameSessions.JoinResult GameSession;

        public string MessageOfTheDay { get; set; }
		public string CdnBaseUri { get; set; }
        public string ShareBaseUrl { get; set; }
        public List<LevelProgressionEntry> LevelProgressionMaps { get; set; }
		public MatchmakingConfigParams MatchmakingParams { get; set; }
		public Objective[][] DailyObjectives { get; set; }
        public ServerMaintainence serverMaintainence { get; set; }
        public AutoMicMutingConfig autoMicMutingConfig { get; set; }
        public StorefrontConfig storefrontConfig { get; set; }
        public RoomKeyConfig roomKeyConfig { get; set; }
        public RoomCurrencyConfig roomCurrencyConfig { get; set; }
        public List<ConfigTableEntry> ConfigTable { get; set; }
		public photonConfig PhotonConfig { get; set; }
		public static string GetDebugConfig()
		{
			return JsonConvert.SerializeObject(new Config
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
				DailyObjectives = dailyObjectives,
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
        public class photonConfig
        {
            public string CloudRegion { get; set; }
            public bool CrcCheckEnabled { get; set; }
            public bool EnableServerTracingAfterDisconnect { get; set; }
        }
        internal class ConfigTableEntry
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
        public class LevelProgressionEntry
        {
            public int Level { get; set; }
            public int RequiredXp { get; set; }
            public int? GiftDropId { get; set; }
        }
        public class Objective
        {
            public int type { get; set; }
            public int score { get; set; }
            public int? xp { get; set; }
        }
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
            public float PreferFullRoomsFrequency { get; set; }
            public float PreferEmptyRoomsFrequency { get; set; }
        }
        public class ServerMaintainence
        {
            public int StartsInMinutes { get; set; }
        }
        public class AutoMicMutingConfig
        {
            public double MicSpamVolumeThreshold { get; set; }
            public double MicVolumeSampleInterval { get; set; }
            public double MicVolumeSampleRollingWindowLength { get; set; }
            public double MicSpamSamplePercentageForWarning { get; set; }
            public double MicSpamSamplePercentageForWarningToEnd { get; set; }
            public double MicSpamSamplePercentageForForceMute { get; set; }
            public double MicSpamSamplePercentageForForceMuteToEnd { get; set; }
            public double MicSpamWarningStateVolumeMultiplier { get; set; }
        }
        internal class MatchmakingConfigParams
        {
            public float PreferFullRoomsFrequency { get; set; }
            public float PreferEmptyRoomsFrequency { get; set; }
        }

        public static Objective[][] dailyObjectives = new Objective[][]
        {
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            }
        };
    }
}
