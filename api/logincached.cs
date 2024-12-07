using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using api;

namespace api
{
    // Token: 0x02000078 RID: 120
    public class Logincached
    {
        public string Error { get; set; }
        public Getcachedlogins.profile Player { get; set; }
        public string Token { get; set; }
        public bool FirstLoginOfTheDay { get; set; }
        public ulong AnalyticsSessionId { get; set; }
        public bool CanUseScreenMode { get; set; }
        public static string loginCache(ulong userid, ulong platformid)
        {
            int level = int.Parse(File.ReadAllText("SaveData\\Profile\\level.txt"));
            string name = File.ReadAllText("SaveData\\Profile\\username.txt");
            string displayName = File.ReadAllText("SaveData\\Profile\\displayName.txt");
            return JsonConvert.SerializeObject(new Logincached
            {
                Error = "",
                Player = new Getcachedlogins.profile
                {
                    Id = userid,
                    Username = name,
                    RawUsername = name,
                    DisplayName = displayName,
                    XP = 9999,
                    Level = level,
                    RegistrationStatus = Getcachedlogins.RegistrationStatus.Registered,
                    Developer = true,
                    CanReceiveInvites = false,
                    ProfileImageName = name,
                    JuniorProfile = false,
                    ForceJuniorImages = false,
                    PendingJunior = false,
                    HasBirthday = true,
                    AvoidJuniors = true,
                    PlayerReputation = new mPlayerReputation
                    {
                        Noteriety = 0,
                        CheerCredit = 20,
                        CheerGeneral = 10,
                        CheerHelpful = 10,
                        CheerGreatHost = 10,
                        CheerSportsman = 10,
                        CheerCreative = 10,
                        SubscriberCount = 0,
                        SubscribedCount = 0,
                        SelectedCheer = 0
                    },
                    PlatformIds = new List<mPlatformID>
                    {
                        new mPlatformID
                        {
                            Platform = 0,
                            PlatformId = platformid
                        }
                    }
                },
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjEsImV4cCI6MTYxOTI4NzQ2MSwidmVycyI6IjIwMTcxMTE3X0VBIn0.-GqtcqPwAzr9ZJioTiz5v0Kl4HMMjH8hFMtVzQtRN5c",
                FirstLoginOfTheDay = true,
                AnalyticsSessionId = 392394UL,
                CanUseScreenMode = true
            });
        }
    }
}
