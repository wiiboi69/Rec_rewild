using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.Generic;
using start;
using api;
using static api.AccountAuth;
using Rec_rewild.api;
using System.Collections.Specialized;
using static Rec_rewild.api.file_util;
using util;
using System.Diagnostics.Eventing.Reader;
using WebSocketSharp;

namespace server
{
    internal class APIServer
    {
     
        public static string SanitizeChatMessageRequest(string postData) => "\"" + JsonConvert.DeserializeObject<SanitizePostDTO>(postData).Value + "\"";
        //playerReputation
        public class SanitizePostDTO
        {
            public string Value { get; set; }

            public int ReplacementChar { get; set; }
        }
        
        public enum SubscriptionLevel
        {
            Gold,
            Platinum,
        }
        public enum SubscriptionPeriod
        {
            Month,
            Year,
        }
        public enum PlatformType
        {
            All = -1, // 0xFFFFFFFF
            Steam = 0,
            Oculus = 1,
            PlayStation = 2,
            Microsoft = 3,
            HeadlessBot = 4,
            IOS = 5,
        }
        public class QuickPlayResponseDTO
        {
            public long? TargetPlayerId { get; set; }
            public string RoomName { get; set; }
            public string ActionCode { get; set; }
        }
        public static string auth = "";
        public static ulong CachedPlayerID = (ulong)player_config.Setting.AccountId;
        public static ulong CachedPlatformID = 10000;
        public static ulong CachedversionID = 20210804;
        public static ulong Cachedservertimestarted = 20206000;
        public static int CachedVersionMonth = 01;
        public static string PlayerEventsResponse = "{\"Created\":[],\"Responses\":[]}";
        public static string ModerationBlockDetails = "{\r\n   \"Duration\" : 0,\r\n   \"GameSessionId\" : 0,\r\n   \"IsBan\" : false,\r\n   \"IsHostKick\" : false,\r\n   \"Message\" : \"\",\r\n   \"PlayerIdReporter\" : 0,\r\n   \"ReportCategory\" : 0\r\n}";
        public static string ImagesV2Named = "[{\"FriendlyImageName\":\"DormRoomBucket\",\"ImageName\":\"DormRoomBucket\",\"StartTime\":\"2021-12-27T21:27:38.1880175-08:00\",\"EndTime\":\"2025-12-27T21:27:38.1880399-08:00\"}";
        public static string ChallengesV1GetCurrent = "{\"Success\":true,\"Message\":\"Rec_rewild\"}";
        public static string ChecklistV1Current = "[{\"Order\":0,\"Objective\":3000,\"Count\":3,\"CreditAmount\":100},{\"Order\":1,\"Objective\":3001,\"Count\":3,\"CreditAmount\":100},{\"Order\":2,\"Objective\":3002,\"Count\":3,\"CreditAmount\":100}]";
        public static string BlankResponse = "";
        public static string BracketResponse = "[]";
        public static string VersionCheckResponse = "{\"VersionStatus\":0}";

        private HttpListener listener = new HttpListener();
    }
}
