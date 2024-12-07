using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using api;
using System.IO;
using static api.AccountAuth;
using server;

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
            //if (APIServer.CachedversionID <= 20199999)
            if (false)
            {
                return JsonConvert.SerializeObject(  new List <Getcachedlogins>
                { 
                    new Getcachedlogins
                    {
                        Profile = new profile
                        {
                            Id = userid,
                            Username = name,
                            RawUsername = name,
                            DisplayName = displayName,
                            XP = 9999,
                            Level = level,
                            RegistrationStatus = RegistrationStatus.Registered,
                            Developer = true,
                            Verified = true,
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
                    }  
                });
            }
            return JsonConvert.SerializeObject(new List<profile>
            {
                new profile
                {
                    Id = userid,
                    Username = name,
                    RawUsername = name,
                    DisplayName = displayName,
                    
                    XP = 9999,
                    Level = level,
                    RegistrationStatus = RegistrationStatus.Registered,
                    Developer = true,
                    Verified = true,
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
                    },
                    PlatformId = new mPlatformID
                    {
                        Platform = 0,
                        PlatformId = platformid
                    }
                    
                }

            });
        }
        //
        public bool RequirePassword { get; set; } = false;
        public profile Profile { get; set; }
        public DateTime LastLoginTime { get; set; } = DateTime.UtcNow;
        public class profile
        {
            public ulong Id { get; set; }
            public string Username { get; set; }
            public string RawUsername { get; set; }
            public string Bio { get; set; } = "";
            public DateTime LastLoginTime { get; set; } = DateTime.UtcNow;
            public string DisplayName { get; set; }
            public int XP { get; set; }
            public int Level { get; set; }
            public RegistrationStatus RegistrationStatus { get; set; }
            public bool Verified { get; set; }
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
            public mPlatformID PlatformId { get; set; }
            public AdministrativeData AdministrativeData { get; set; } = new AdministrativeData()
            {
                LastLoginTime = 638041373704901902L,
                JoinData  = 638041373704883267L,
                PermissionFactors  = 0,
                TrustFactor = 5,
            };
        }
        public class AdministrativeData
        {
            public long LastLoginTime { get; set; } = 638041373704901902L;
            public long JoinData { get; set; } = 638041373704883267L;
            public int PermissionFactors { get; set; } = 0;
            public int TrustFactor { get; set; } = 5;
        }
        public enum RegistrationStatus
        {
            // Token: 0x04009811 RID: 38929
            Unregistered,
            // Token: 0x04009812 RID: 38930
            PendingEmailVerification,
            // Token: 0x04009813 RID: 38931
            Registered
        }
    }
}
