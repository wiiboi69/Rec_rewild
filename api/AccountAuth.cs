using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using Newtonsoft.Json;
using start;

namespace api
{
    internal class AccountAuth
    {
        public static string CachedLogins()
        {
            return JsonConvert.SerializeObject(new List<mCachedLogins>
            {
                new mCachedLogins
                {
                    platform = 0,
                    platformId = "1",
                    accountId = int.Parse(File.ReadAllText(Program.ProfilePath + "\\userid.txt")),
                    lastLoginTime = DateTime.Now
                }
            });
        }
        public static string GetAccountsBulk()
        {
            return JsonConvert.SerializeObject(new List<Account>
            {
                new Account
                {
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
        public static string GetCoachBulk()
        {
            return JsonConvert.SerializeObject(new List<Account>
            {
                new Account
                {
                    accountId = 1,
                    displayName = "Coach",
                    bannerImage = "Coach.png",
                    createdAt = DateTime.Now,
                    isJunior = false,
                    platforms = 1,
                    profileImage = "Coach.png",
                    username = "Coach",
                }
            });
        }
        //"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2Njk1NzUzOTksImV4cCI6MTY2OTU3ODk5OSwiaXNzIjoiaHR0cHM6Ly9hdXRoLnJlYy5uZXQiLCJjbGllbnRfaWQiOiJyZWNuZXQiLCJyb2xlIjoiZGV2ZWxvcGVyIiwic3ViIjoiNjIyNjgwNyIsImF1dGhfdGltZSI6MTY1Nzc3Mzk1NSwiaWRwIjoibG9jYWwiLCJqdGkiOiJEOUUwNTY2QjU2NTE4QkNEMjBBNjRDMkQ2MzUwQzRFMyIsInNpZCI6IjU2NEY5QUFGQzNBRjQxREQwQTQzOENDMTlFODk5NzYzIiwiaWF0IjoxNjY5NTc1Mzk5LCJzY29wZSI6WyJvcGVuaWQiLCJybi5hcGkiLCJybi5jb21tZXJjZSIsInJuLm5vdGlmeSIsInJuLm1hdGNoLnJlYWQiLCJybi5jaGF0Iiwicm4uYWNjb3VudHMiLCJybi5hdXRoIiwicm4ubGluayIsInJuLmNsdWJzIiwicm4ucm9vbXMiLCJybi5kaXNjb3ZlcnkiXSwiYW1yIjpbIm1mYSJdfQ.GdYHMcKKpDK8mQviYTFVUFTre3olRz8JGWPqNZ6Ke44",
        public static string ConnectToken()
        {
            var token = ClientSecurity.GenerateToken();
            Guid randomGuid = Guid.NewGuid();
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
            return JsonConvert.SerializeObject(new List<Progress>
            {
                new Progress
                {
                    PlayerId = int.Parse(File.ReadAllText(Program.ProfilePath + "\\userid.txt")),
                    Level = int.Parse(File.ReadAllText(Program.ProfilePath + "\\level.txt")),
                    XP = 0    
                }
            });
        }
        public static string GetLevel(string playerid)
        {
            return JsonConvert.SerializeObject(new List<Progress>
            {
                new Progress
                {
                    PlayerId = int.Parse(File.ReadAllText(Program.ProfilePath + "\\userid.txt")),
                    Level = int.Parse(File.ReadAllText(Program.ProfilePath + "\\level.txt")),
                    XP = 0
                }
            });
        }
        public static string GetRep()
        {
            return JsonConvert.SerializeObject(new Rep
            {
                AccountId = int.Parse(File.ReadAllText(Program.ProfilePath + "\\userid.txt")),
                IsCheerful = true,
                Noteriety = 0.0,
                SelectedCheer = 0,
                CheerCredit = 0,
                CheerGeneral = 0,
                CheerHelpful = 0,
                CheerCreative = 0,
                CheerGreatHost = 0,
                CheerSportsman = 0,
                SubscriberCount = 0,
                SubscribedCount = 0,
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
            public DateTime lastLoginTime { get; set; }
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
