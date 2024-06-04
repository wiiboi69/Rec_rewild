using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

using Newtonsoft.Json;
using start;

namespace api2019
{
    // Token: 0x02000005 RID: 5
    internal class AccountAuth
    {
        // Token: 0x06000005 RID: 5 RVA: 0x00002090 File Offset: 0x00000290
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

        // Token: 0x06000006 RID: 6 RVA: 0x000020F0 File Offset: 0x000002F0
        public static string GetAccountsBulk()
        {
            return JsonConvert.SerializeObject(new List<Account>
            {
                new Account
                {
                    accountId = int.Parse(File.ReadAllText(Program.ProfilePath + "\\userid.txt")),
                    username = File.ReadAllText(Program.ProfilePath + "\\username.txt"),
                    displayName = File.ReadAllText(Program.ProfilePath + "\\username.txt"),
                    bannerImage = File.ReadAllText(Program.ProfilePath + "\\username.txt"),
                    createdAt = DateTime.Now,
                    isJunior = false,
                    platforms = 1,
                    profileImage = "Profile.png",
                }
            });
        }//[{"accountId":7383744,"displayName":"wordcarboy84","bannerImage":"wordcarboy84","createdAt":"0001-01-01T00:00:00","isJunior":false,"platforms":0,"profileImage":"wordcarboy84","username":"wordcarboy84"}]

        // Token: 0x06000007 RID: 7 RVA: 0x0000219B File Offset: 0x0000039B 
        public static string ConnectToken()
        {
            return JsonConvert.SerializeObject(new TokenCached
            {
                access_token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkREU2F1d2dZdkE1S1NEcVFQWHJRbk1ZeXhMbyIsInR5cCI6ImF0K2p3dCIsIng1dCI6IkREU2F1d2dZdkE1S1NEcVFQWHJRbk1ZeXhMbyJ9.eyJuYmYiOjE2Njk1NzUzOTksImV4cCI6MTY2OTU3ODk5OSwiaXNzIjoiaHR0cHM6Ly9hdXRoLnJlYy5uZXQiLCJjbGllbnRfaWQiOiJyZWNuZXQiLCJyb2xlIjoid2ViQ2xpZW50Iiwic3ViIjoiNjIyNjgwNyIsImF1dGhfdGltZSI6MTY1Nzc3Mzk1NSwiaWRwIjoibG9jYWwiLCJqdGkiOiJEOUUwNTY2QjU2NTE4QkNEMjBBNjRDMkQ2MzUwQzRFMyIsInNpZCI6IjU2NEY5QUFGQzNBRjQxREQwQTQzOENDMTlFODk5NzYzIiwiaWF0IjoxNjY5NTc1Mzk5LCJzY29wZSI6WyJvcGVuaWQiLCJybi5hcGkiLCJybi5jb21tZXJjZSIsInJuLm5vdGlmeSIsInJuLm1hdGNoLnJlYWQiLCJybi5jaGF0Iiwicm4uYWNjb3VudHMiLCJybi5hdXRoIiwicm4ubGluayIsInJuLmNsdWJzIiwicm4ucm9vbXMiLCJybi5kaXNjb3ZlcnkiXSwiYW1yIjpbIm1mYSJdfQ.TVkpz8Nbmz_8fFdbf3xI0CHwjogaIR45TmhK4NXSgx__e85M0xNO8UDSbGJaUMeSN7rn_I1obrzvqqJhDjqOAyQs39rtKJ-lyMq_oFDf1DOjFhB_KWCQ3V_N1SIOpoTnzoD7kr3voixtB4VrTo1HkUQPK_6a2FvUfg3sNwBBAxVvSv7jRPF5_BLGLRACfT3vIHfM7baSOFYkgijnGu9Okd4XKCSolb0hBO14vRMSUZ_gzdm2YubWEF5PK4kiIKMLnnvqUIAXt37sn0m7SjFK_7CI5K7TcSGJcnO-r63PaKsH3UfPqkTq6QWJKUh9X59mQcUJ6iClkY6Pv8LZWjqpkg",
                error = "",
                error_description = "",
                refresh_token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkREU2F1d2dZdkE1S1NEcVFQWHJRbk1ZeXhMbyIsInR5cCI6ImF0K2p3dCIsIng1dCI6IkREU2F1d2dZdkE1S1NEcVFQWHJRbk1ZeXhMbyJ9.eyJuYmYiOjE2Njk1NzUzOTksImV4cCI6MTY2OTU3ODk5OSwiaXNzIjoiaHR0cHM6Ly9hdXRoLnJlYy5uZXQiLCJjbGllbnRfaWQiOiJyZWNuZXQiLCJyb2xlIjoid2ViQ2xpZW50Iiwic3ViIjoiNjIyNjgwNyIsImF1dGhfdGltZSI6MTY1Nzc3Mzk1NSwiaWRwIjoibG9jYWwiLCJqdGkiOiJEOUUwNTY2QjU2NTE4QkNEMjBBNjRDMkQ2MzUwQzRFMyIsInNpZCI6IjU2NEY5QUFGQzNBRjQxREQwQTQzOENDMTlFODk5NzYzIiwiaWF0IjoxNjY5NTc1Mzk5LCJzY29wZSI6WyJvcGVuaWQiLCJybi5hcGkiLCJybi5jb21tZXJjZSIsInJuLm5vdGlmeSIsInJuLm1hdGNoLnJlYWQiLCJybi5jaGF0Iiwicm4uYWNjb3VudHMiLCJybi5hdXRoIiwicm4ubGluayIsInJuLmNsdWJzIiwicm4ucm9vbXMiLCJybi5kaXNjb3ZlcnkiXSwiYW1yIjpbIm1mYSJdfQ.TVkpz8Nbmz_8fFdbf3xI0CHwjogaIR45TmhK4NXSgx__e85M0xNO8UDSbGJaUMeSN7rn_I1obrzvqqJhDjqOAyQs39rtKJ-lyMq_oFDf1DOjFhB_KWCQ3V_N1SIOpoTnzoD7kr3voixtB4VrTo1HkUQPK_6a2FvUfg3sNwBBAxVvSv7jRPF5_BLGLRACfT3vIHfM7baSOFYkgijnGu9Okd4XKCSolb0hBO14vRMSUZ_gzdm2YubWEF5PK4kiIKMLnnvqUIAXt37sn0m7SjFK_7CI5K7TcSGJcnO-r63PaKsH3UfPqkTq6QWJKUh9X59mQcUJ6iClkY6Pv8LZWjqpkg"
            });
        }

        // Token: 0x06000008 RID: 8 RVA: 0x000021D4 File Offset: 0x000003D4
        public static string GetLevel()
        {
            return JsonConvert.SerializeObject(new Progress
            {
                PlayerId = int.Parse(File.ReadAllText(Program.ProfilePath + "\\userid.txt")),
                Level = int.Parse(File.ReadAllText(Program.ProfilePath + "\\level.txt")),
                XP = 0
            });
        }

        // Token: 0x06000009 RID: 9 RVA: 0x00002230 File Offset: 0x00000430
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
            });
        }

        public class Rep
        {
            // Token: 0x17000015 RID: 21
            // (get) Token: 0x06000037 RID: 55 RVA: 0x00002429 File Offset: 0x00000629
            // (set) Token: 0x06000038 RID: 56 RVA: 0x00002431 File Offset: 0x00000631
            public int AccountId { get; set; }

            // Token: 0x17000016 RID: 22
            // (get) Token: 0x06000039 RID: 57 RVA: 0x0000243A File Offset: 0x0000063A
            // (set) Token: 0x0600003A RID: 58 RVA: 0x00002442 File Offset: 0x00000642
            public bool IsCheerful { get; set; }

            // Token: 0x17000017 RID: 23
            // (get) Token: 0x0600003B RID: 59 RVA: 0x0000244B File Offset: 0x0000064B
            // (set) Token: 0x0600003C RID: 60 RVA: 0x00002453 File Offset: 0x00000653
            public double Noteriety { get; set; }

            // Token: 0x17000018 RID: 24
            // (get) Token: 0x0600003D RID: 61 RVA: 0x0000245C File Offset: 0x0000065C
            // (set) Token: 0x0600003E RID: 62 RVA: 0x00002464 File Offset: 0x00000664
            public int SelectedCheer { get; set; }

            // Token: 0x17000019 RID: 25
            // (get) Token: 0x0600003F RID: 63 RVA: 0x0000246D File Offset: 0x0000066D
            // (set) Token: 0x06000040 RID: 64 RVA: 0x00002475 File Offset: 0x00000675
            public int CheerCredit { get; set; }

            // Token: 0x1700001A RID: 26
            // (get) Token: 0x06000041 RID: 65 RVA: 0x0000247E File Offset: 0x0000067E
            // (set) Token: 0x06000042 RID: 66 RVA: 0x00002486 File Offset: 0x00000686
            public int CheerGeneral { get; set; }

            // Token: 0x1700001B RID: 27
            // (get) Token: 0x06000043 RID: 67 RVA: 0x0000248F File Offset: 0x0000068F
            // (set) Token: 0x06000044 RID: 68 RVA: 0x00002497 File Offset: 0x00000697
            public int CheerHelpful { get; set; }

            // Token: 0x1700001C RID: 28
            // (get) Token: 0x06000045 RID: 69 RVA: 0x000024A0 File Offset: 0x000006A0
            // (set) Token: 0x06000046 RID: 70 RVA: 0x000024A8 File Offset: 0x000006A8
            public int CheerCreative { get; set; }

            // Token: 0x1700001D RID: 29
            // (get) Token: 0x06000047 RID: 71 RVA: 0x000024B1 File Offset: 0x000006B1
            // (set) Token: 0x06000048 RID: 72 RVA: 0x000024B9 File Offset: 0x000006B9
            public int CheerGreatHost { get; set; }

            // Token: 0x1700001E RID: 30
            // (get) Token: 0x06000049 RID: 73 RVA: 0x000024C2 File Offset: 0x000006C2
            // (set) Token: 0x0600004A RID: 74 RVA: 0x000024CA File Offset: 0x000006CA
            public int CheerSportsman { get; set; }

            // Token: 0x1700001F RID: 31
            // (get) Token: 0x0600004B RID: 75 RVA: 0x000024D3 File Offset: 0x000006D3
            // (set) Token: 0x0600004C RID: 76 RVA: 0x000024DB File Offset: 0x000006DB
            public int SubscriberCount { get; set; }

            // Token: 0x17000020 RID: 32
            // (get) Token: 0x0600004D RID: 77 RVA: 0x000024E4 File Offset: 0x000006E4
            // (set) Token: 0x0600004E RID: 78 RVA: 0x000024EC File Offset: 0x000006EC
            public int SubscribedCount { get; set; }
        }

        public class Progress
        {
            // Token: 0x17000012 RID: 18
            // (get) Token: 0x06000030 RID: 48 RVA: 0x000023EE File Offset: 0x000005EE
            // (set) Token: 0x06000031 RID: 49 RVA: 0x000023F6 File Offset: 0x000005F6
            public int PlayerId { get; set; }

            // Token: 0x17000013 RID: 19
            // (get) Token: 0x06000032 RID: 50 RVA: 0x000023FF File Offset: 0x000005FF
            // (set) Token: 0x06000033 RID: 51 RVA: 0x00002407 File Offset: 0x00000607
            public int Level { get; set; }

            // Token: 0x17000014 RID: 20
            // (get) Token: 0x06000034 RID: 52 RVA: 0x00002410 File Offset: 0x00000610
            // (set) Token: 0x06000035 RID: 53 RVA: 0x00002418 File Offset: 0x00000618
            public int XP { get; set; }
        }

        public class TokenCached
        {
            // Token: 0x1700000E RID: 14
            // (get) Token: 0x06000027 RID: 39 RVA: 0x000023A2 File Offset: 0x000005A2
            // (set) Token: 0x06000028 RID: 40 RVA: 0x000023AA File Offset: 0x000005AA
            public string access_token { get; set; }

            // Token: 0x1700000F RID: 15
            // (get) Token: 0x06000029 RID: 41 RVA: 0x000023B3 File Offset: 0x000005B3
            // (set) Token: 0x0600002A RID: 42 RVA: 0x000023BB File Offset: 0x000005BB
            public string error { get; set; }

            // Token: 0x17000010 RID: 16
            // (get) Token: 0x0600002B RID: 43 RVA: 0x000023C4 File Offset: 0x000005C4
            // (set) Token: 0x0600002C RID: 44 RVA: 0x000023CC File Offset: 0x000005CC
            public string error_description { get; set; }

            // Token: 0x17000011 RID: 17
            // (get) Token: 0x0600002D RID: 45 RVA: 0x000023D5 File Offset: 0x000005D5
            // (set) Token: 0x0600002E RID: 46 RVA: 0x000023DD File Offset: 0x000005DD
            public string refresh_token { get; set; }
        }

        public class mCachedLogins
        {
            // Token: 0x1700000A RID: 10
            // (get) Token: 0x0600001E RID: 30 RVA: 0x00002356 File Offset: 0x00000556
            // (set) Token: 0x0600001F RID: 31 RVA: 0x0000235E File Offset: 0x0000055E
            public int platform { get; set; }

            // Token: 0x1700000B RID: 11
            // (get) Token: 0x06000020 RID: 32 RVA: 0x00002367 File Offset: 0x00000567
            // (set) Token: 0x06000021 RID: 33 RVA: 0x0000236F File Offset: 0x0000056F
            public string platformId { get; set; }

            // Token: 0x1700000C RID: 12
            // (get) Token: 0x06000022 RID: 34 RVA: 0x00002378 File Offset: 0x00000578
            // (set) Token: 0x06000023 RID: 35 RVA: 0x00002380 File Offset: 0x00000580
            public int accountId { get; set; }

            // Token: 0x1700000D RID: 13
            // (get) Token: 0x06000024 RID: 36 RVA: 0x00002389 File Offset: 0x00000589
            // (set) Token: 0x06000025 RID: 37 RVA: 0x00002391 File Offset: 0x00000591
            public DateTime lastLoginTime { get; set; }
        }

        public class Account
        {
            // Token: 0x17000001 RID: 1
            // (get) Token: 0x0600000B RID: 11 RVA: 0x000022B5 File Offset: 0x000004B5
            // (set) Token: 0x0600000C RID: 12 RVA: 0x000022BD File Offset: 0x000004BD
            public int accountId { get; set; }

            // Token: 0x17000003 RID: 3
            // (get) Token: 0x0600000F RID: 15 RVA: 0x000022D7 File Offset: 0x000004D7
            // (set) Token: 0x06000010 RID: 16 RVA: 0x000022DF File Offset: 0x000004DF
            public string username { get; set; }

            // Token: 0x17000004 RID: 4
            // (get) Token: 0x06000011 RID: 17 RVA: 0x000022E8 File Offset: 0x000004E8
            // (set) Token: 0x06000012 RID: 18 RVA: 0x000022F0 File Offset: 0x000004F0
            public string displayName { get; set; }

            // Token: 0x17000005 RID: 5
            // (get) Token: 0x06000013 RID: 19 RVA: 0x000022F9 File Offset: 0x000004F9
            // (set) Token: 0x06000014 RID: 20 RVA: 0x00002301 File Offset: 0x00000501
            public string profileImage { get; set; }

            // Token: 0x17000006 RID: 6
            // (get) Token: 0x06000015 RID: 21 RVA: 0x0000230A File Offset: 0x0000050A
            // (set) Token: 0x06000016 RID: 22 RVA: 0x00002312 File Offset: 0x00000512
            public string bannerImage { get; set; }

            // Token: 0x17000007 RID: 7
            // (get) Token: 0x06000017 RID: 23 RVA: 0x0000231B File Offset: 0x0000051B
            // (set) Token: 0x06000018 RID: 24 RVA: 0x00002323 File Offset: 0x00000523
            public bool isJunior { get; set; }

            // Token: 0x17000008 RID: 8
            // (get) Token: 0x06000019 RID: 25 RVA: 0x0000232C File Offset: 0x0000052C
            // (set) Token: 0x0600001A RID: 26 RVA: 0x00002334 File Offset: 0x00000534
            public int platforms { get; set; }

            // Token: 0x17000009 RID: 9
            // (get) Token: 0x0600001B RID: 27 RVA: 0x0000233D File Offset: 0x0000053D
            // (set) Token: 0x0600001C RID: 28 RVA: 0x00002345 File Offset: 0x00000545
            public DateTime createdAt { get; set; }
        }


    }
}
