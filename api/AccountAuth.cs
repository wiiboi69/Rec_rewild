using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using Newtonsoft.Json;
using Rec_rewild.api;
using start;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace api
{
    public class AccountAuth
    {
        public static string CachedLogins()
        {
            var setting = player_config.Setting;
            return JsonConvert.SerializeObject(new List<mCachedLogins>
            {
                new mCachedLogins
                {
                    platform = 0,
                    platformId = "1",
                    accountId = setting.AccountId,
                    lastLoginTime = setting.LastLogin ?? DateTime.UtcNow,
                }
            });
        }
        public static string GetAccountsBulk()
        {
            var setting = player_config.Setting;

            return JsonConvert.SerializeObject(new List<Account>
            {
                new Account
                {
                    accountId = setting.AccountId,
                    displayName = setting.DisplayName,
                    bannerImage = setting.BannerImage,
                    createdAt = setting.CreatedAt,
                    isJunior = setting.IsJunior,
                    platforms = 1,
                    profileImage = setting.ProfileImage,
                    username = setting.Username,
                }
            });
        }
        /*  public int availableUsernameChanges { get; set; } = 9999;
            public string email { get; set; } = "";
            public DateTime birthday { get; set; } = DateTime.Now;
            public bool? isFakeJuniorBirthday { get; set; } = null;
            public int accountId { get; set; }
            public string username { get; set; }
            public string displayName { get; set; }
            public string profileImage { get; set; }
            public string bannerImage { get; set; }
            public bool isJunior { get; set; }
            public int platforms { get; set; }
            public int personalPronouns { get; set; }
            public int identityFlags { get; set; }
            public DateTime createdAt { get; set; }
            public bool isMetaPlatformBlocked { get; set; } = false;
        */
        public static string GetAccountMe()
        {
            return JsonConvert.SerializeObject(new List<AccountMe>
            {
                new AccountMe
                {
                    availableUsernameChanges = 9999,
                    email = "zesty@zestyrecrewild.com",
                    birthday = DateTime.Parse("2000-01-01T00:00:00Z"),
                    isFakeJuniorBirthday = false,
                    accountId = int.Parse(File.ReadAllText(Program.ProfilePath + "\\userid.txt")),
                    displayName = File.ReadAllText(Program.ProfilePath + "\\displayName.txt"),
                    bannerImage = File.ReadAllText(Program.ProfilePath + "\\username.txt"),
                    createdAt = DateTime.Now,
                    isJunior = false,
                    platforms = 1,
                    profileImage = "Profile.png",
                    username = File.ReadAllText(Program.ProfilePath + "\\username.txt"),
                }
            });
        }


        //"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2Njk1NzUzOTksImV4cCI6MTY2OTU3ODk5OSwiaXNzIjoiaHR0cHM6Ly9hdXRoLnJlYy5uZXQiLCJjbGllbnRfaWQiOiJyZWNuZXQiLCJyb2xlIjoiZGV2ZWxvcGVyIiwic3ViIjoiNjIyNjgwNyIsImF1dGhfdGltZSI6MTY1Nzc3Mzk1NSwiaWRwIjoibG9jYWwiLCJqdGkiOiJEOUUwNTY2QjU2NTE4QkNEMjBBNjRDMkQ2MzUwQzRFMyIsInNpZCI6IjU2NEY5QUFGQzNBRjQxREQwQTQzOENDMTlFODk5NzYzIiwiaWF0IjoxNjY5NTc1Mzk5LCJzY29wZSI6WyJvcGVuaWQiLCJybi5hcGkiLCJybi5jb21tZXJjZSIsInJuLm5vdGlmeSIsInJuLm1hdGNoLnJlYWQiLCJybi5jaGF0Iiwicm4uYWNjb3VudHMiLCJybi5hdXRoIiwicm4ubGluayIsInJuLmNsdWJzIiwicm4ucm9vbXMiLCJybi5kaXNjb3ZlcnkiXSwiYW1yIjpbIm1mYSJdfQ.GdYHMcKKpDK8mQviYTFVUFTre3olRz8JGWPqNZ6Ke44",
        public static string ConnectToken(bool newversion)
        {
            var token = ClientSecurity.GenerateToken();
            Guid randomGuid = Guid.NewGuid();
            if (newversion)
            {
                JsonConvert.SerializeObject(new
                {
                    access_token = token,
                    expires_in = 999999999,
                    token_type = "Bearer",
                    refresh_token = randomGuid,
                    scope = "offline_access rn.accounts rn.accounts.gc rn.api rn.auth rn.auth.gc rn.bugreporting rn.cards rn.chat rn.clubs rn.cms rn.commerce rn.data rn.data.gc rn.datacollection rn.datacollection.gc rn.discovery rn.gamelogs.gc rn.leaderboard rn.link rn.lists rn.match.read rn.match.write rn.moderation rn.notify rn.platformnotifications rn.playersettings rn.roomcomments rn.rooms rn.storage rn.strings rn.studio.gc",
                    key = ""
                });
            }
            return JsonConvert.SerializeObject(new TokenCached
            {
                access_token = token, 
                error = "",
                error_description = "",
                key = "",
                refresh_token = randomGuid.ToString(),
            });
        }

        public static string GetLevel()
        {
            var setting = player_config.Setting;
            return JsonConvert.SerializeObject(new List<Progress>
            {
                new Progress
                {
                    PlayerId = setting.AccountId,
                    Level = setting.Level,
                    XP = setting.XP
                }
            });
        }

        public static string GetRep()
        {
            var setting = player_config.Setting;
            return JsonConvert.SerializeObject(new Rep
            {
                AccountId = setting.AccountId,
                IsCheerful = setting.Reputation.IsCheerful,
                Noteriety = setting.Reputation.Noteriety,
                SelectedCheer = setting.Reputation.SelectedCheer,
                CheerCredit = setting.Reputation.CheerCredit,
                CheerGeneral = setting.Reputation.CheerGeneral,
                CheerHelpful = setting.Reputation.CheerHelpful,
                CheerCreative = setting.Reputation.CheerCreative,
                CheerGreatHost = setting.Reputation.CheerGreatHost,
                CheerSportsman = setting.Reputation.CheerSportsman,
                SubscriberCount = setting.Reputation.SubscriberCount,
                SubscribedCount = setting.Reputation.SubscribedCount,
            });
        }
        public class Rep
        {
            public int AccountId { get; set; }
            public bool IsCheerful { get; set; }
            public double Noteriety { get; set; }
            public int SelectedCheer { get; set; }
            public int CheerCredit { get; set; }
            public int CheerGeneral { get; set; }
            public int CheerHelpful { get; set; }
            public int CheerCreative { get; set; }
            public int CheerGreatHost { get; set; }
            public int CheerSportsman { get; set; }
            public int SubscriberCount { get; set; }
            public int SubscribedCount { get; set; }
        }
        public class Progress
        {
            public int PlayerId { get; set; }
            public int Level { get; set; }
            public int XP { get; set; }
        }
        public class TokenCached
        {
            public string access_token { get; set; }
            public string error { get; set; }
            public string error_description { get; set; }
            public string refresh_token { get; set; }
            public string key { get; set; }
        }
        public class mCachedLogins
        {
            public int platform { get; set; }
            public string platformId { get; set; }
            public int accountId { get; set; }
            public DateTime? lastLoginTime { get; set; }
        }
        public class Account
        {
            public int accountId { get; set; }
            public string username { get; set; }
            public string displayName { get; set; }
            public string profileImage { get; set; }
            public string bannerImage { get; set; }
            public bool isJunior { get; set; }
            public int platforms { get; set; }
            public DateTime createdAt { get; set; }
        }

        public class AccountMe
        {
            public int availableUsernameChanges { get; set; } = 9999;
            public string email { get; set; } = "";
            public DateTime birthday { get; set; } = DateTime.Now;
            public bool? isFakeJuniorBirthday { get; set; } = null;
            public int accountId { get; set; }
            public string username { get; set; }
            public string displayName { get; set; }
            public string profileImage { get; set; }
            public string bannerImage { get; set; }
            public bool isJunior { get; set; }
            public int platforms { get; set; }
            public int personalPronouns { get; set; }
            public int identityFlags { get; set; }
            public DateTime createdAt { get; set; }
            public bool isMetaPlatformBlocked { get; set; } = false;
        }


        public class Account_update
        {
            public int availableUsernameChanges { get; set; } = 9999;
            public string email { get; set; } = "";
            public DateTime birthday { get; set; } = DateTime.Now;
            public bool? isFakeJuniorBirthday { get; set; } = null;
            public int accountId { get; set; }
            public string username { get; set; }
            public string displayName { get; set; }
            public string profileImage { get; set; }
            public string bannerImage { get; set; }
            public bool isJunior { get; set; }
            public int platforms { get; set; }
            public int personalPronouns { get; set; }
            public int identityFlags { get; set; }
            public DateTime createdAt { get; set; }
            public bool isMetaPlatformBlocked { get; set; } = false;
        }
    }
}
