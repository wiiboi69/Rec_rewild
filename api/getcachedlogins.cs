using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using api;
using System.IO;
using static api.AccountAuth;

namespace api
{
    // Token: 0x02000076 RID: 118
    public class Getcachedlogins
    {
        // Token: 0x0600033C RID: 828 RVA: 0x00008F30 File Offset: 0x00007130
        public static string GetDebugLogin(ulong userid, ulong platformid)
        {
            int level = int.Parse(File.ReadAllText("SaveData\\Profile\\level.txt"));
            //displayName
            string name = File.ReadAllText("SaveData\\Profile\\username.txt");
            string displayName = File.ReadAllText("SaveData\\Profile\\displayName.txt");
            return JsonConvert.SerializeObject(new List<Getcachedlogins>
            {
                new Getcachedlogins
                {
                    Id = userid,
                    Username = name,
                    DisplayName = displayName,
                    XP = 9999,
                    Level = level,
                    RegistrationStatus = 2,
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
                }

            });
        }
        public ulong Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public int XP { get; set; }
        public int Level { get; set; }
        public int RegistrationStatus { get; set; }
        public bool Developer { get; set; }
        public bool CanReceiveInvites { get; set; }
        public string ProfileImageName { get; set; }
        public bool JuniorProfile { get; set; }
        public bool ForceJuniorImages { get; set; }
        public bool PendingJunior { get; set; }
        public bool HasBirthday { get; set; }
        public bool AvoidJuniors { get; set; }
        public mPlayerReputation PlayerReputation { get; set; }
        public List<mPlatformID> PlatformIds { get; set; }
    }
}
