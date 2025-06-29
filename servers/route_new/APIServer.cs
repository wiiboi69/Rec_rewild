using api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using start;
using Rec_rewild.api.route;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using static api.AccountAuth;
using Rec_rewild.api;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using static server.APIServer;

namespace Rec_rewild.servers.route_new
{
    class APIServer2021_new
    {
        public static APIServer2021_new APIServer;

        public APIServer2021_new()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:" + Program.version + "0/");

            RegisterRoutes();

            new Thread(new ThreadStart(this.Start)).Start();

            Running = true;
        }
        public static bool Running = false;
        private readonly HttpListener _listener;
        public void RefreshApplicationState(object sender, FileSystemEventArgs e)
        {
            RegisterRoutes(true);
        }
        public static void reloadRegisterRoutes()
        {
            APIServer.RegisterRoutes(true);
        }

        #region server_handling
        private readonly List<(Regex, MethodInfo, ParameterInfo[])> _routeHandlers = new();
        private void RegisterRoutes(bool reload = false)
        {
            if (reload)
            {
                Console.WriteLine("APIServer2021 [DEBUG]: Reregistering all of the Route");
                _routeHandlers.Clear();
            }
            else
                Console.WriteLine("APIServer2021: Registering Route");

            foreach (var method in Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)))
            {
                var routeAttribute = method.GetCustomAttribute<rewild_route_system.RouteAttribute>();
                if (routeAttribute != null)
                {

                    var pattern = "^" + Regex.Escape(routeAttribute.Path)
                        .Replace("\\*", ".*")
                        .Replace("\\{", "(?<")
                        .Replace("}", ">[^/]+)")
                        + "$";
                    var regex = new Regex(pattern, RegexOptions.Compiled);

                    var parameters = method.GetParameters();
                    _routeHandlers.Add((regex, method, parameters));
                }
            }
        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine("APIServer2021: Listening...");
            while (true)
            {
                var context = _listener.GetContext();
                try
                {
                    ProcessRequest(context);
                }
                catch
                {
                }
            }
        }
        private void ProcessRequest(HttpListenerContext context)
        {
            var path = context.Request.Url.AbsolutePath;
            var query = context.Request.QueryString;
            string contentType = context.Request.ContentType;
            if (string.IsNullOrEmpty(contentType))
                contentType = "none";
            Console.WriteLine($"APIServer2021: Start of request.");

            Console.WriteLine($"APIServer2021: API Requested: {path}");
            Console.WriteLine($"APIServer2021: Content-Type: {contentType}");
            Console.WriteLine($"APIServer2021: Request-Method-Type: {context.Request.HttpMethod}");

            var response = context.Response;
            object response_data = null;

            foreach (var (regex, method, parameters) in _routeHandlers)
            {
                var match = regex.Match(path);
                if (match.Success)
                {
                    var args = new object[parameters.Length];
                    string body = "";
                    if (context.Request.ContentType == "application/json" || context.Request.ContentType == "application/x-www-form-urlencoded")
                    {
                        body = rewild_route_system.ParseRequestBody(context.Request);
                        Console.WriteLine($"APIServer2021: API Data: {body}");
                    }

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var paramType = parameters[i].ParameterType;

                        if (paramType == typeof(HttpListenerContext))
                        {
                            args[i] = context;
                        }
                        else if (paramType == typeof(Dictionary<string, string>) && context.Request.ContentType == "application/x-www-form-urlencoded")
                        {
                            body = rewild_route_system.ParseRequestBody(context.Request);
                            args[i] = rewild_route_system.ParseFormData(body);
                        }
                        else if (context.Request.ContentType == "application/json")
                        {
                            args[i] = rewild_route_system.ParseJsonBody(body, paramType);
                        }
                        else if (paramType == typeof(string) && query != null && query[parameters[i].Name] != null)
                        {
                            args[i] = query[parameters[i].Name];
                        }
                        else if ((
                                paramType == typeof(int) ||
                                paramType == typeof(uint) ||
                                paramType == typeof(long) ||
                                paramType == typeof(ulong) ||
                                paramType == typeof(string)) && context.Request.ContentType == "application/x-www-form-urlencoded")
                        {
                            var formData = rewild_route_system.ParseFormData(body);
                            var paramName = parameters[i].Name;
                            args[i] = Convert.ChangeType(formData[paramName], paramType);
                        }
                        else if (
                                paramType == typeof(int) ||
                                paramType == typeof(uint) ||
                                paramType == typeof(long) ||
                                paramType == typeof(ulong) ||
                                paramType == typeof(string))
                        {
                            var paramName = parameters[i].Name;
                            if (match.Groups[paramName]?.Success == true)
                            {
                                args[i] = Convert.ChangeType(match.Groups[paramName].Value, paramType);
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unsupported parameter type: {paramType}");
                        }
                    }
                    var result = method.Invoke(null, args);

                    if (method.ReturnType == typeof(void))
                    {
                        response.StatusCode = 200;
                        response_data = "{\"success\":\"true\"}";
                    }
                    else
                    {
                        response_data = result;
                    }
                    break;
                }
            }

            if (response_data == null)
            {
                response_data = HandleNotFound(context);
                response.StatusCode = 404;
            }

            WriteResponse(response, response_data);
        }

        private static void WriteResponse(HttpListenerResponse response, object response_data)
        {

            if (response_data is string responseString)
            {
                if (responseString.Length <= 0x1ff)
                {
                    Console.WriteLine($"APIServer2021: API json Response: " + responseString);
                }
                else
                {
                    Console.WriteLine($"APIServer2021: API json Response Length: " + responseString.Length);
                }
                var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentType = "application/json";
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else if (response_data is byte[] responseBytes)
            {
                Console.WriteLine($"APIServer2021: API byte[] Response Length: " + responseBytes.Length);

                response.ContentType = "application/octet-stream";
                response.ContentLength64 = responseBytes.Length;
                response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
            }
            else
            {
                throw new InvalidOperationException("Unsupported response data type.");
            }
            Console.WriteLine($"APIServer2021: End of request.\n");

            response.OutputStream.Close();
        }

        private string HandleNotFound(HttpListenerContext context)
        {
            return "{\"Success\": false, \"Error\": \"404 URL Not Found: " + context.Request.Url + "\"}";
        }
        #endregion

        public static string BlankResponse = "";
        public static string BracketResponse = "[]";
        public static string AmplitudeKey = "Rewild";
        public static string Version_build = "";

        [rewild_route_system.Route("/ns2021")]
        public static string GetNS()
        {
            Console.WriteLine($"game requested NS Server");
            return JsonConvert.SerializeObject(new
            {
                Accounts = "http://localhost:20210/",
                API = "http://localhost:20210/",
                Auth = "http://localhost:20214/",
                BugReporting = "http://localhost:20210/",
                Cards = "http://localhost:20210/",
                CDN = "http://localhost:20210/",
                Chat = "http://localhost:20210/",
                Clubs = "http://localhost:20210/",
                CMS = "http://localhost:20210/",
                Commerce = "http://localhost:20210/",
                Data = "http://localhost:20210/",
                DataCollection = "http://localhost:20210/",
                Discovery = "http://localhost:20210/",
                Econ = "http://localhost:20210/",
                GameLogs = "http://localhost:20210/",
                Geo = "http://localhost:20210/",
                Images = "http://localhost:20213/",
                Leaderboard = "http://localhost:20210/",
                Link = "http://localhost:20210/",
                Lists = "http://localhost:20210/",
                Matchmaking = "http://localhost:20218/",
                Moderation = "http://localhost:20210/",
                Notifications = "http://localhost:20210/",
                PlayerSettings = "http://localhost:20210/",
                RoomComments = "http://localhost:20210/",
                Rooms = "http://localhost:20218/",
                Storage = "http://localhost:20210/",
                Strings = "http://localhost:20210/",
                StringsCDN = "http://localhost:20210/",
                Thorn = "http://localhost:20210/",
                Videos = "http://localhost:20210/",
                WWW = "http://localhost:20210/"
            });
        }

        [rewild_route_system.Route("/api/versioncheck/v4")]
        public static string VersionCheck(string v, string p)
        {
            Console.WriteLine($"game requesting version check for version " + v);
            return JsonConvert.SerializeObject(new
            {
                VersionStatus = 0
            });
        }

        [rewild_route_system.Route("/api/gameconfigs/v1/all")]
        public static string GameConfigs()
        {
            Console.WriteLine($"game requesting gameconfigs");
            return File.ReadAllText("SaveData\\gameconfigs.txt");
        }

        [rewild_route_system.Route("/api/config/v1/amplitude")]
        public static string Amplitude()
        {
            Console.WriteLine($"game requested Amplitude, returning key " + AmplitudeKey);
            return JsonConvert.SerializeObject(new Amplitude
            {
                AmplitudeKey = AmplitudeKey
            });
        }

        [rewild_route_system.Route("/api/avatar/v1/defaultunlocked")]
        public static string DefaultUnlockedItems()
        {
            Console.WriteLine($"game requested default unlocked items");
            return BracketResponse;
        }

        [rewild_route_system.Route("/account/bulk")]
        public static string BulkAccounts(string id)
        {
            Console.WriteLine($"game requested Bulk accounts with id " + id);
            if (id == "1")
            {
                return AccountAuth.GetCoachyWoachy();
            }
            else
            {
                return AccountAuth.GetAccountsBulk();
            }
        }

        [rewild_route_system.Route("/account/me")]
        public static string AccountMe()
        {
            Console.WriteLine($"game requested account me");
            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<List<Account>>(AccountAuth.GetAccountsBulk())[0]);
        }

        [rewild_route_system.Route("/club/home/me")]
        public static string ClubHomeMe()
        {
            Console.WriteLine($"game requested your home club");
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/PlayerReporting/v1/moderationBlockDetails")]
        public static string ModerationBlockDetails()
        {
            Console.WriteLine($"game requested if you banned");
            return JsonConvert.SerializeObject(new
            {
                Duration = 0,
                GameSessionId = 0,
                IsBan = false,
                IsHostKick = false,
                Message = "",
                PlayerIdReporter = 0,
                ReportCategory = 0,
            });
        }

        [rewild_route_system.Route("/api/settings/v2/")]
        public static string SettingsV2()
        {
            Console.WriteLine($"game requested your settings");
            try
            {
                return File.ReadAllText("SaveData\\settings.txt");
            }
            catch
            {
                File.WriteAllText("SaveData\\settings.txt", JsonConvert.SerializeObject(Settings.CreateDefaultSettings()));
                return File.ReadAllText("SaveData\\settings.txt");
            }
        }

        [rewild_route_system.Route("/api/avatar/v2")]
        public static string AvatarV2()
        {
            Console.WriteLine($"game requested your avatar");
            try
            {
                return File.ReadAllText("SaveData\\avatar.txt");
            }
            catch
            {
                File.WriteAllText("SaveData\\avatar.txt", "{\"OutfitSelections\":\"\",\"FaceFeatures\":\"{\\\"ver\\\":3,\\\"eyeId\\\":\\\"AjGMoJhEcEehacRZjUMuDg\\\",\\\"eyePos\\\":{\\\"x\\\":0.0,\\\"y\\\":0.0},\\\"eyeScl\\\":0.0,\\\"mouthId\\\":\\\"FrZBRanXEEK29yKJ4jiMjg\\\",\\\"mouthPos\\\":{\\\"x\\\":0.0,\\\"y\\\":0.0},\\\"mouthScl\\\":0.0,\\\"beardColorId\\\":\\\"befcc00a-a2e6-48e4-864c-593d57bbbb5b\\\"}\",\"SkinColor\":\"85343b16-d58a-4091-96d8-083a81fb03ae\",\"HairColor\":\"befcc00a-a2e6-48e4-864c-593d57bbbb5b\"}");
                return "{\"OutfitSelections\":\"\",\"FaceFeatures\":\"{\\\"ver\\\":3,\\\"eyeId\\\":\\\"AjGMoJhEcEehacRZjUMuDg\\\",\\\"eyePos\\\":{\\\"x\\\":0.0,\\\"y\\\":0.0},\\\"eyeScl\\\":0.0,\\\"mouthId\\\":\\\"FrZBRanXEEK29yKJ4jiMjg\\\",\\\"mouthPos\\\":{\\\"x\\\":0.0,\\\"y\\\":0.0},\\\"mouthScl\\\":0.0,\\\"beardColorId\\\":\\\"befcc00a-a2e6-48e4-864c-593d57bbbb5b\\\"}\",\"SkinColor\":\"85343b16-d58a-4091-96d8-083a81fb03ae\",\"HairColor\":\"befcc00a-a2e6-48e4-864c-593d57bbbb5b\"}";
            }
        }

        [rewild_route_system.Route("/api/avatar/v4/items")]
        public static string AvatarV4Items()
        {
            Console.WriteLine($"game requested all avatar items");
            Console.WriteLine("Got avatar items");
            return File.ReadAllText("SaveData\\avataritems2.txt");
        }

        [rewild_route_system.Route("/api/players/v2/progression/bulk")]
        public static string ProgressionBulk(string id)
        {
            Console.WriteLine($"game requested ProgressionBulk");
            return GetLevel(id);
        }

        [rewild_route_system.Route("/api/playerReputation/v2/bulk")]
        public static string PlayerReputation(string id)
        {
            Console.WriteLine($"game requested playerReputation");
            return JsonConvert.SerializeObject(new List<mPlayerReputation>
            {
              new mPlayerReputation
                                {
                                    AccountId = ulong.Parse(id),
                                    PlayerId = ulong.Parse(id),
                                    IsInGoodStanding = true,
                                    IsCheerful = true,
                                    Noteriety = 0,
                                    CheerCreative = 10,
                                    CheerCredit = 9999,
                                    CheerGeneral = 10,
                                    CheerGreatHost = 10,
                                    CheerHelpful = 10,
                                    CheerSportsman = 10,
                                    SelectedCheer = null,
                                    SubscribedCount = 0,
                                    SubscriberCount = 0
                                }
                            });
        }


        [rewild_route_system.Route("/api/storefronts/v3/giftdropstore/{id}")]
        public static string StorefrontGiftDropStore(string id)
        {
            Console.WriteLine($"game requested storefronts");
            return BracketResponse;
        }

        [rewild_route_system.Route("/subscription/mine/member")]
        public static string SubscriptionMineMember(string id)
        {
            Console.WriteLine($"game requested subscription");
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/avatar/v2/set")]
        public static string AvatarV2Set(AvatarSelection avatarSelection)
        {
            Console.WriteLine($"game requested set avatar");
            Console.WriteLine(avatarSelection.SkinColor);
            return BracketResponse;
        }

        [rewild_route_system.Route("/hub/v1/negotiate")]
        public static string HubV1Negotiate()
        {
            Console.WriteLine($"game requested websockets");
            return JsonConvert.SerializeObject(new
            {
                ConnectionId = "skull",
                negotiateVersion = 0,
                SupportedTransports = new List<string>(),
                url = new Uri("ws://localhost:20199/")
            });
        }

        [rewild_route_system.Route("/api/relationships/v2/get")]
        public static string GetRelationships()
        {
            Console.WriteLine($"game requested relationships");
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/settings/v2/set")]
        public static string SettingsV2Set()
        {
            Console.WriteLine($"game requested set settings");
            return BracketResponse;
        }

        [rewild_route_system.Route("/subscription/details/{id}")]
        public static string SubDetails(string id)
        {
            Console.WriteLine($"game requested subscription details");
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/messages/v1/favoriteFriendOnlineStatus")]
        public static string FavoriteFriendOnlineStatus()
        {
            Console.WriteLine($"game requested favoriteFriendOnlineStatus");
            return BracketResponse;
        }

        [rewild_route_system.Route("/thread")]
        public static string Thread(string maxCount, string mode)
        {
            Console.WriteLine($"game requested chats with maxcount {maxCount} and mode {mode}");
            return BracketResponse;
        }

        [rewild_route_system.Route("/config/LoadingScreenTipData")]
        public static string LoadingScreenTipData()
        {
            var client = new HttpClient();
            Console.WriteLine($"game requested LoadingScreenTipData");
            return client.GetStringAsync("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/loadingscreen.json").GetAwaiter().GetResult();
        }

        [rewild_route_system.Route("/api/quickPlay/v1/getandclear")]
        public static string quickPlaygetandclear()
        {
            Console.WriteLine($"game requested quickPlaygetandclear");
            return JsonConvert.SerializeObject(new QuickPlayResponseDTO()
            {
                TargetPlayerId = null,
                RoomName = null,
                ActionCode = null
            });
        }

        [rewild_route_system.Route("/api/config/v2")]
        public static string ConfigV2()
        {
            Console.WriteLine($"game requested ConfigV2");
            return Config.GetDebugConfig();
        }

        [rewild_route_system.Route("/api/PlayerReporting/v1/hile")]
        public static string PlayerReportingHile()
        {
            Console.WriteLine($"game requested PlayerReportingHile");
            return JsonConvert.SerializeObject(new
            {
                Message = "",
                Type = 0
            });
        }

        [rewild_route_system.Route("/api/messages/v2/get")]
        public static string GetMessages()
        {
            Console.WriteLine($"game requested GetMessages");
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/challenge/v2/getCurrent")]
        public static string GetCurrentChallenges()
        {
            Console.WriteLine($"game requested challenges");
            return JsonConvert.SerializeObject(new
            {
                ChallengeMapId = 0,
                StartAt = DateTime.UtcNow,
                EndAt = DateTime.UtcNow.AddDays(7), 
                ServerTime = DateTime.UtcNow,
                Challenges = new List<object>
                {
                   
                },
                Gift = new
                {
                    GiftDropId = 0,
                    AvatarItemDesc = "",
                    Xp = 0,
                    Level = 1,
                    EquipmentPrefabName = "[PaintballGun]",
                    ChallengeThemeString = "idk"
                }
            });
        }

        [rewild_route_system.Route("/announcements/v2/subscription/mine/unread")]
        public static string UnreadSub()
        {
            Console.WriteLine($"game requested UnreadSub");
            return BracketResponse;
        }

        [rewild_route_system.Route("/announcements/v2/mine/unread")]
        public static string UnreadAnnoucements()
        {
            Console.WriteLine($"game requested UnreadAnnoucements");
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/roomcurrencies/v1/currencies")]
        public static string RoomCurrencies()
        {
            Console.WriteLine($"game requested RoomCurrencies");
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/annoucement/v1/get")]
        public static string GetAnnoucements()
        {
            Console.WriteLine($"game requested annoucement data");
            var client = new HttpClient();
            try
            {
                return client.GetStringAsync("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/master/CDN/announcements.json").GetAwaiter().GetResult();
            }
            catch
            {
                return BracketResponse;
            }
        }

        [rewild_route_system.Route("/api/PlayerReporting/v1/voteToKickReasons")]
        public static string VoteToKickReasons()
        {
            Console.WriteLine($"game requested VoteToKickReasons");
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/avatar/v3/saved")]
        public static string AvatarSaved()
        {
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/equipment/v2/getUnlocked")]
        public static string GetAllEquipments()
        {
            try
            {
                return File.ReadAllText("SaveData\\equipment.txt");
            }
            catch
            {
                return "[]";
            }
        }

        [rewild_route_system.Route("/api/consumables/v2/getUnlocked")]
        public static string GetAllConsumables()
        {
            try
            {
                return File.ReadAllText("SaveData\\consumables.txt");
            }
            catch
            {
                return "[]";
            }
        }

        [rewild_route_system.Route("/api/objectives/v1/myprogress")]
        public static string ObjectivesMyProgress()
        {
            return JsonConvert.SerializeObject(new
            {
                Objectives = new List<object>
                {

                },
                ObjectivesGroups = new List<object>
                {

                },
            });
        }

        [rewild_route_system.Route("/api/avatar/v2/gifts")]
        public static string AvatarV2Gifts()
        {
            return JsonConvert.SerializeObject(new
            {
                Id = 1,
                AvatarItemDesc = "",
                ConsumableItemDesc = "",
                EquipmentPrefabName = "[PaintballGun]",
                EquipmentModificationGuid = "",
                Currency = 0,
                CurrencyType = 2,
                Xp = 0,
                Level = 0,
                GiftRarity = 0,
                Message = "Welcome to Rec_rewild!",
            });
        }

        [rewild_route_system.Route("/api/images/v2/named")]
        public static string NamedImages()
        {
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/gamerewards/v1/pending")]
        public static string PendingGamerewards()
        {
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/communityboard/v2/current")]
        public static string CurrentCommunityBoard()
        {
            var client = new HttpClient();
            try
            {
                return client.GetStringAsync("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/communityboard.json").Result;
            }
            catch
            {
                return BracketResponse;
            }
        }

        [rewild_route_system.Route("/api/playerevents/v1/all")]
        public static string AllEvents()
        {
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/roomkeys/v1/mine")]
        public static string MyRoomkeys()
        {
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/CampusCard/v1/UpdateAndGetSubscription")]
        public static string RRPlus()
        {
            return JsonConvert.SerializeObject(new
            {
                Subscription = new
                {
                    SubscriptionId = 0,
                    RecNetPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                    PlatformType = 0,
                    PlatformId = 1,
                    PlatformPurchaseId = "0",
                    Level = SubscriptionLevel.Platinum,
                    Period = SubscriptionPeriod.Year,
                    ExpirationDate = DateTime.Parse("9999-12-30T23:37:28.553Z"),
                    IsAutoRenewing = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    IsActive = true
                },
                CanBuySubscription = true,
                PlatformAccountSubscribedPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt"))
            });
        }

        [rewild_route_system.Route("/api/storefronts/v4/balance/2")]
        public static string Tokens()
        {
            var balance = new[]
            {
                             new
                             {
                                 Balance = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\tokens.txt")),
                                 BalanceType = -2,
                                 CurrencyType = 2
                             }
                           };
            return JsonConvert.SerializeObject(balance);
        }

        [rewild_route_system.Route("/api/roomcurrencies/v1/betaEnabled")]
        public static string BetaEnabled()
        {
            return "true";
        }

        [rewild_route_system.Route("/api/roomcurrencies/v1/getAllBalances")]
        public static string GetAllBalances()
        {
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/roomkeys/v1/room")]
        public static string RoomKeys(string roomId)
        {
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/externalfriendinvite/v1/getplatformreferrers")]
        public static string Getplatformreferrers()
        {
            return BracketResponse;
        }

        [rewild_route_system.Route("/api/consumables/v1/updateActive")]
        public static string UpdateConsumables()
        {
            return JsonConvert.SerializeObject(new
            {
                Success = true, 
                Error = ""
            });
        }
    }
}
    
