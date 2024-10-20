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
using static Rec_rewild.api.image_util;


namespace server
{
    internal class APIServer
    {
        public APIServer()
        {
            try
            {
                Console.WriteLine("[APIServer.cs] has started.");
                new Thread(new ThreadStart(this.StartListen)).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Exception Occurred while Listening :" + ex.ToString());
            }
        }
        private void StartListen()
        {
            try
            {
                this.listener.Prefixes.Add("http://localhost:" + Program.version + "0/");
                {
                    for (; ; )
                    {
                        this.listener.Start();
                        Console.WriteLine("[APIServer.cs] is listening.");
                        HttpListenerContext context = this.listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;
                        List<byte> list = new List<byte>();
                        string rawUrl = request.RawUrl;
                        string Url = "";
                        byte[] bytes = null;
                        byte[] roomdatabytes = null;
                        bool roomdata = false;
                        string signature = request.Headers.Get("X-RNSIG");
                        if (rawUrl.StartsWith("/api/"))
                        {
                            Url = rawUrl.Remove(0, 5);
                        }
                        if (!(Url == ""))
                        {
                            Console.WriteLine("API Requested: " + Url);
                        }
                        else
                        {
                            Console.WriteLine("API Requested (rawUrl): " + rawUrl);
                        }
                        string text;
                        string s = "";
                        byte[] array;
                        
                        
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            context.Request.InputStream.CopyTo(memoryStream);
                            array = memoryStream.ToArray();
                            text = Encoding.ASCII.GetString(array);
                        }
                        

                        string str2 = "";
                        NameValueCollection headers1 = request.Headers;
                        for (int index = 0; index < request.Headers.Count; ++index)
                        {
                            string key = headers1.GetKey(index);
                            if (key == "Authorization")
                                auth = headers1.GetValues("Authorization")[0];
                            if (key == "X-RNSIG")
                                str2 = headers1.GetValues(key)[0];
                        }
                        if (text.Length > 0xfff)
                        {
                            Console.WriteLine("API Data: unviewable");
                        }
                        else
                        {
                            Console.WriteLine("API Data: " + text);
                        }
                        if (Url.StartsWith("versioncheck"))
                        {
                            CachedversionID = ulong.Parse(Url.Substring(18, 8));
                            Console.WriteLine(CachedversionID);
                            s = VersionCheckResponse;
                        }
                        if (Url.StartsWith("platformlogin/v2/getcachedlogins"))
                        {
                            s = Getcachedlogins.GetDebugLogin(ulong.Parse(text.Remove(0, 32)), ulong.Parse(text.Remove(0, 22)));
                            CachedPlayerID = ulong.Parse(text.Remove(0, 32));
                            CachedPlatformID = ulong.Parse(text.Remove(0, 22));
                            File.WriteAllText("SaveData\\Profile\\userid.txt", Convert.ToString(CachedPlayerID));
                        }
                        if (Url == "equipment/v1/getUnlocked")
                        {
                            s = File.ReadAllText("SaveData\\equipment.txt");
                        }
                        if (Url == "equipment/v2/getUnlocked")
                        {
                            s = File.ReadAllText("SaveData\\equipment.txt");
                        }
                        if (Url == ("config/v2"))
                        {
                            s = Config.GetDebugConfig();
                        }
                        if (Url == "platformlogin/v1/getcachedlogins")
                        {
                            s = Getcachedlogins.GetDebugLogin(ulong.Parse(text.Remove(0, 32)), ulong.Parse(text.Remove(0, 22)));
                            CachedPlayerID = ulong.Parse(text.Remove(0, 32));
                            CachedPlatformID = ulong.Parse(text.Remove(0, 22));
                            File.WriteAllText("SaveData\\Profile\\userid.txt", Convert.ToString(CachedPlayerID));
                        }
                        if (Url == "platformlogin/v1/loginaccount")
                        {
                            s = Logincached.loginCache(CachedPlayerID, CachedPlatformID);
                        }
                        if (Url == "platformlogin/v1/createaccount")
                        {
                            s = Logincached.loginCache(CachedPlayerID, CachedPlatformID);
                        }
                        if (Url == "platformlogin/v1/logincached")
                        {
                            s = Logincached.loginCache(CachedPlayerID, CachedPlatformID);
                        }
                        if (Url == "relationships/v1/bulkignoreplatformusers")
                        {
                            s = BlankResponse;
                        }
                        if (Url.StartsWith("roomkeys/"))
                        {
                            s = BracketResponse;
                        }
                        if (Url.StartsWith("players/v1/progression/"))
                        {
                            s = AccountAuth.GetLevel();
                        }
                        if (Url.StartsWith("playerReputation/v1/"))
                        {
                            s = AccountAuth.GetRep();
                        }
                        if (Url == "players/v1/list")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "config/v1/amplitude")
                        {
                            s = Amplitude.amplitude();
                        }
                        if (Url == "images/v2/named")
                        {
                            s = ImagesV2Named;
                        }
                        if (Url == "PlayerReporting/v1/moderationBlockDetails")
                        {
                            s = ModerationBlockDetails;
                        }
                        if (Url == "/api/chat/v2/myChats?mode=0&count=50")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "messages/v2/get")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "gameconfigs/v1/all")
                        {
                            s = File.ReadAllText("SaveData\\gameconfigs.txt");
                        }
                        if (Url.StartsWith("relationships/v2/get") || Url.StartsWith("relationships/v1/get"))
                        {
                            //s = Relationships.Friends();
                            s = "[]";
                        }
                        /*
                        if (Url.StartsWith("relationships/v2/sendfriendrequest"))
                        {
                            s = Relationships.SendFriendRequest(int.Parse(Url.Split('=')[1]));
                        }
                        if (Url.StartsWith("relationships/v2/acceptfriendrequest"))
                        {
                            s = Relationships.AcceptFriendRequest(int.Parse(Url.Split('=')[1]));
                        }
                        if (Url.StartsWith("relationships/v1/bulkignoreplatformuser"))
                        {
                            s = "[]";
                        }*/

                        if (Url.StartsWith("storefronts/v3/giftdropstore"))
                        {
                            s = BracketResponse;
                        }
                        if (Url.StartsWith("storefronts/v3/balance/"))
                        {
                            s = BracketResponse;
                        }
                        if (Url == "avatar/v2")
                        {
                            s = File.ReadAllText("SaveData\\avatar.txt");
                        }
                        if (Url == "avatar/v2/saved")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "avatar/v2/set")
                        {
                            //for later 2018 builds compatibility
                            if (!(text.Contains("FaceFeatures")))
                            {
                                string postdatacache = text;
                                text = postdatacache.Remove(postdatacache.Length - 1, 1) + File.ReadAllText("SaveData\\App\\facefeaturesadd.txt");
                            }
                            File.WriteAllText("SaveData\\avatar.txt", text);
                        }
                        if (rawUrl.Contains("/club/"))
                        {
                            s = BracketResponse;
                        }
                        if (Url.Contains("storefronts/v3"))
                        {
                            s = BlankResponse;
                        }
                        if (rawUrl.Contains("/thread"))
                        {
                            s = BracketResponse;
                        }
                        if (Url == "challenge/v2/getCurrent")
                        {
                            s = "{\"ChallengeMapId\":0,\"StartAt\":\"2021-12-27T21:27:38.188Z\",\"EndAt\":\"2025-12-27T21:27:38.188Z\",\"ServerTime\":\"2023-12-27T21:27:38.188Z\",\"Challenges\":[],\"Gift\":{\"GiftDropId\":1,\"AvatarItemDesc\":\"\",\"Xp\":2,\"Level\":0,\"EquipmentPrefabName\":\"[WaterBottle]\"},\"ChallengeThemeString\":\"RebornRec Water\"}";
                        }
                        if (rawUrl == "/api/chat/v2/myChats?mode=0&count=50")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "playersubscriptions/v1/my")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "avatar/v3/items")
                        {
                            s = File.ReadAllText("SaveData\\avataritems2.txt");
                            s = Avatar_Item_Util.inject_AvatarItem_list(s);
                        }
                        if (Url == "avatar/v4/items")
                        {
                            s = File.ReadAllText("SaveData\\avataritems2.txt");
                            Console.WriteLine("Got avatar items");
                            s = Avatar_Item_Util.inject_AvatarItem_list(s);
                        }
                        if (rawUrl.Contains("quickPlay/v1/getandclear"))
                        {
                            s = JsonConvert.SerializeObject(new QuickPlayResponseDTO()
                            {
                                TargetPlayerId = null,
                                RoomName = null,
                                ActionCode = null
                            });
                        }
                        if (Url == "equipment/v1/getUnlocked")
                        {
                            s = File.ReadAllText("SaveData\\equipment.txt");
                        }
                        if (Url == "avatar/v1/saved")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "consumables/v1/getUnlocked")
                        {
                            s = File.ReadAllText("SaveData\\consumables.txt");
                        }
                        if (Url == "consumables/v2/getUnlocked")
                        {
                            s = File.ReadAllText("SaveData\\consumables.txt");
                        }
                        if (Url == "avatar/v2/gifts")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "storefronts/v2/2")
                        {
                            s = BlankResponse;
                        }
                        if (Url == "storefronts/v1/allGiftDrops/2")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "objectives/v1/myprogress")
                        {
                            s = "{\"Objectives\": [], \"ObjectiveGroups\": []}";
                        }
                        if (Url == "rooms/v1/myrooms")
                        {
                            s = File.ReadAllText("SaveData\\myrooms.txt");
                        }
                        if (Url == "rooms/v2/myrooms")
                        {
                            s = BracketResponse;
                        }
                        if (Url.StartsWith("rooms/v4/details/"))
                        {
                            //Url = Url.Substring(("rooms/v4/details/").Length);
                            //s = GameSessions.GetDetails(Url);
                            s = room_util.find_room_with_id(Url, "rooms/v4/details/".Length);

                            s = room_util.room_change_CreatorAccount(s);
                            if (CachedversionID < 20209906)
                            {
                                s = room_util.room_change_fix_room_2020(s);
                            }

                        }
                        if (Url.StartsWith("sanitize/v1/isPure"))
                        {
                            s = "{\"IsPure\":true}";
                        }
                        if (Url == "rooms/v2/baserooms")
                        {
                            s = File.ReadAllText("SaveData\\baserooms.txt");
                        }
                        if (Url == "rooms/v1/mybookmarkedrooms")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "rooms/v1/myRecent?skip=0&take=10")
                        {
                            s = BracketResponse;
                        }
                        /*
                        if (Url == "events/v3/list")
                        {
                            s = Events.list();
                        }
                        if (Url == "playerevents/v1/all")
                        {
                            s = PlayerEventsResponse;
                        }
                        if (Url == "activities/charades/v1/words")
                        {
                            s = Activities.Charades.words();
                        }
                        if (Url == "gamesessions/v2/joinrandom")
                        {
                            s = gamesesh.GameSessions.JoinRandom(text);
                        }
                        if (Url == "gamesessions/v2/create")
                        {
                            s = gamesesh.GameSessions.Create(text);
                        }
                        if (Url == "gamesessions/v3/joinroom")
                        {
                            s = JsonConvert.SerializeObject(c000041.m000030(text));
                        }
                        */
                        if (Url.StartsWith("images/v4/uploadsaved"))
                        {
        
                            bool flag1;
                            string rnfn;
                            //File.WriteAllBytes("SaveData\\image.dat", array);
                            string temp1 = SaveImageFile(array, out flag1, out rnfn);
                            
                            if (flag1)
                            {
                                s = "{\"success\":false,\"error\":\"failed to uploaded image\",\"ImageName\":\"\"}";
                            }
                            else
                            {
                                s = "{\"success\":true,\"error\":\"\",\"ImageName\":\"" + rnfn + "\",\"value\":\"File saved: " + rnfn + "\"}";
                            }

                        }
                        if (Url == "sanitize/v1/isPure")
                        {
                            s = "{\"IsPure\":true}";
                        }
                        if (Url == "sanitize/v1")
                        {
                            s = SanitizeChatMessageRequest(text);
                        }
                        /*{
                             "FileName": ""
                        }*/
                        if (rawUrl == "/upload")
                        {
                            byte[] temp_data;
                            //FileType data_type = GetFileType(array);
                            FileType data_type = image_util.GetType(array,out temp_data);
                            File.WriteAllBytes("SaveData\\data.dat", array);
                            bool flag1 = false;
                            string rnfn = string.Empty;
                            string temp1 = string.Empty;
                            if (data_type == FileType.RoomSave)
                            {
                                temp1 = SaveRoomFile(temp_data, out flag1, out rnfn);
                            }
                            else if (data_type == FileType.RoomMetadata)
                            {
                                temp1 = SavedummyFile(temp_data, out flag1, out rnfn);
                            }
                            else if (data_type == FileType.Holotar)
                            {
                                temp1 = SavedummyFile(temp_data, out flag1, out rnfn);
                            }
                            else if (data_type == FileType.Image)
                            {
                                temp1 = SaveImageFile(temp_data, out flag1, out rnfn);
                            }
                            else
                            {
                                goto data_type_unknowed;
                            }
                            if (flag1)
                            {
                                s = "{\"success\":false,\"error\":\"failed to uploaded\"}";
                            }
                            else
                            {
                                if (data_type == FileType.Image)
                                {
                                    s = "{\"success\":true,\"error\":\"\",\"Filename\":\"" + rnfn + "\",\"value\":\"File saved: " + rnfn + "\"}";
                                    goto send_data;
                                }
                                

                                s = "{\"success\":true,\"error\":\"\",\"Filename\":\"" + temp1 + "\" ,\"value\":\"File saved: " + rnfn + "\"}";
                                
                            }
                            goto send_data;
                            data_type_unknowed:
                            s = "{\"success\":false,\"error\":\"data type unknowed or not yet inpermeted: " + data_type + " \"}";

                        }
                        if (Url == "avatar/v1/defaultunlocked")
                        {
                            s = BracketResponse;
                        }
                        //playerevents/v1/room
                        if (Url.StartsWith("images/v4/room"))
                        {
                            s = BracketResponse;
                        }
                        if (Url.StartsWith("playerevents/v1/room"))
                        {
                            s = BracketResponse;
                        }
                        if (Url == "avatar/v3/saved")
                            {
                            s = BracketResponse;
                        }
                        if (Url == "checklist/v1/current")
                        {
                            s = ChecklistV1Current;
                        }
                        if (Url == "presence/v1/setplayertype")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "challenge/v1/getCurrent")
                        {
                            s = ChallengesV1GetCurrent;
                        }
                        if (Url == "rooms/v1/verifyRole")
                        {
                            s = "true";
                        }
                        ///data/
                        if (rawUrl.StartsWith("/data/"))
                        {
                            string temp = rawUrl.Substring("/data/".Length);
                            try
                            {
                                roomdatabytes = File.ReadAllBytes("SaveData\\Rooms\\cdn\\htr\\" + temp);

                            }
                            catch
                            {
                                File.WriteAllBytes("SaveData\\Rooms\\cdn\\htr\\" + temp, new WebClient().DownloadData("https://cdn.rec.net/data/" + temp));
                                roomdatabytes = File.ReadAllBytes("SaveData\\Rooms\\cdn\\htr\\" + temp);
                            }
                            roomdata = true;
                        }
                        if (rawUrl.StartsWith("/room/"))
                        {
                            string temp = rawUrl.Substring("/room/".Length);
                            roomdatabytes = File.ReadAllBytes("SaveData\\Rooms\\cdn\\" + temp + ".room");
                            roomdata = true;
                        }
                        if (Url == "rooms/v1/featuredRoomGroup")
                        {
                            s = BracketResponse;
                        }/*
                        if (Url == "rooms/v1/clone")
                        {
                            s = JsonConvert.SerializeObject(c000099.m00000a(text));
                        }
                        if (Url.StartsWith("rooms/v2/saveData"))
                        {
                            string text26 = "5GDNL91ZY43PXN2YJENTBL";
                            string path = c000004.m000007() + c000041.f000043.Room.Name;
                            File.WriteAllBytes(string.Concat(new string[]
                            {
                                c000004.m000007(),
                                c000041.f000043.Room.Name,
                                "\\room\\",
                                text26,
                                ".room"
                            }), c0000a5.m00005d(list.ToArray(), "data.dat"));
                            c000041.f000043.Scenes[0].DataBlobName = text26 + ".room";
                            c000041.f000043.Scenes[0].DataModifiedAt = DateTime.Now;
                            File.WriteAllText(c000004.m000007() + c000041.f000043.Room.Name + "\\RoomDetails.json", JsonConvert.SerializeObject(c000041.f000043));
                            s = JsonConvert.SerializeObject(c00005d.m000035());
                        }*/
                        ///leaderboard/GetRanks
                        ///
                        if (rawUrl.StartsWith("/leaderboard/GetRanks"))
                        {
                            //s = "{\"success\":false,\"error\":\"oops!\nyou cant create new account yet,\n[code: create]\"}";
                            s = "[{\"PlayerId\":" + CachedPlayerID + ",\"Score\":69,\"Rank\":0}]";
                        }
                        if (rawUrl.StartsWith("/leaderboard/GetNearbyScores"))
                        {
                            //s = "{\"success\":false,\"error\":\"oops!\nyou cant create new account yet,\n[code: create]\"}";
                            s = "[{\"PlayerId\":" + CachedPlayerID + ",\"Score\":69,\"Rank\":0}]";
                        }
                        if (rawUrl.StartsWith("/account/create"))
                        {
                            //s = "{\"success\":false,\"error\":\"oops!\nyou cant create new account yet,\n[code: create]\"}";
                            s = "{\"success\":true,\"error\":\"\"}";
                        }
                        if (Url == "consumables/v1/updateActive")
                        {
                            s = "{\"success\":true,\"error\":\"\"}";
                        }
                        if (Url == "CampusCard/v1/UpdateAndGetSubscription")
                        {
                            s = JsonConvert.SerializeObject(new
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
                        if (Url == "PlayerReporting/v1/hile")
                        {
                            s = "{\"Message\":\"\",\"Type\":0}";
                        }
                        if (rawUrl == "/player/statusvisibility")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "PlayerReporting/v1/voteToKickReasons")
                        {
                            s = BracketResponse;
                        }
                        if (Url.StartsWith("roomconsumables/v1/roomConsumable/"))
                        {
                            s = BracketResponse;
                        }
                        if (Url.StartsWith("roomcurrencies/v1/currencies"))
                        {
                            s = BracketResponse;
                        }
                        if (Url.StartsWith("storefronts/v4/balance/"))
                        {
                            s = BracketResponse;
                        }
                        if (Url == "gamerewards/v1/pending")
                        {
                            s = BracketResponse;
                        }
                        if (Url.StartsWith("playerReputation/v2/bulk"))
                        {
                            string temp = Url.Substring("playerReputation/v2/bulk?id=".Length);
                            s = "{\"AccountId\":" + temp + ",\"Noteriety\":0,\"CheerGeneral\":1,\"CheerHelpful\":1,\"CheerGreatHost\":1,\"CheerSportsman\":1,\"CheerCreative\":1,\"CheerCredit\":77,\"SubscriberCount\":2,\"SubscribedCount\":0,\"SelectedCheer\":40}";
                        }
                        /*
                        else if (Url.StartsWith("playerReputation/v2/"))
                        {
                            s = "[{\"AccountId\":" + Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")) + ",\"Noteriety\":0,\"CheerGeneral\":1,\"CheerHelpful\":1,\"CheerGreatHost\":1,\"CheerSportsman\":1,\"CheerCreative\":1,\"CheerCredit\":77,\"SubscriberCount\":2,\"SubscribedCount\":0,\"SelectedCheer\":40}]";
                        }*/
                        if (rawUrl == "/config/LoadingScreenTipData")
                        {
                            s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/loadingscreen.json"); ;
                        }
                        if (rawUrl.Contains("/roomcurrencies/v1/getAllBalances"))
                        {
                            s = BracketResponse;
                        }
                        if (Url == "rooms/v1/featuredRoomGroup")
                        {
                            s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/dormslideshow.txt");
                        }
                        if (Url.StartsWith("rooms/v1/hot"))
                        {
                            s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/hotrooms.txt");
                        }
                        if (Url.StartsWith("rooms/v2/instancedetails"))
                        {
                            s = BracketResponse;
                        }
                        if (rawUrl.StartsWith("/subscription"))
                        {
                            s = BracketResponse;
                        }
                        if (rawUrl.StartsWith("/account/bulk?id="))
                        {
                            string temp = rawUrl.Substring("/account/bulk?id=".Length);
                            if (temp == "1")
                                s = AccountAuth.GetCoachyWoachy();
                            else
                                s = AccountAuth.GetAccountsBulk();
                        }
                        else if (rawUrl.Contains("/account/me/email"))
                        {
                            s = "{\"error\":\"failed: error code: not implemented\",\"success\":false,\"value\":\"\"}";
                        }
                        else if(rawUrl.StartsWith("/account/me/bio"))
                        {
                            string temp = text.Substring("bio=".Length);
                            File.WriteAllText(Program.ProfilePath + "\\bio.txt", temp);
                            s = "{\"success\":true,\"error\":\"\"}";
                        }
                        else if (rawUrl.StartsWith("/account/") && rawUrl.EndsWith("/bio"))
                        {
                            s = JsonConvert.SerializeObject(new
                            {
                                accountId = int.Parse(File.ReadAllText(Program.ProfilePath + "\\userid.txt")),
                                bio = File.ReadAllText(Program.ProfilePath + "\\bio.txt")
                            });
                        }
                        else if (rawUrl.StartsWith("/account/me/displayName"))
                        {
                            string temp = text.Substring("displayName=".Length);
                            File.WriteAllText(Program.ProfilePath + "\\displayName.txt", temp);
                            s = "{\"success\":true,\"error\":\"\",\"value\":\"" + temp + "\"}";
                            goto send_data;
                        }
                        else if (rawUrl.StartsWith("/account/") && rawUrl.EndsWith("/displayName"))
                        {
                            s = JsonConvert.SerializeObject(new
                            {
                                accountId = int.Parse(File.ReadAllText(Program.ProfilePath + "\\userid.txt")),
                                Name = File.ReadAllText(Program.ProfilePath + "\\displayName.txt")
                            }   );
                        }
                        else if (rawUrl.StartsWith("/account/me"))
                        {
                            s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<List<Account>>(AccountAuth.GetAccountsBulk())[0]);
                            Console.WriteLine("checking: " + File.ReadAllText("SaveData\\Profile\\username.txt"));
                        }
                        else if (rawUrl.StartsWith("/account/"))
                        {
                            s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<List<Account>>(AccountAuth.GetAccountsBulk())[0]);
                        }
                        if (Url == "announcement/v1/get")
                        {
                            s = BracketResponse;
                        }
                        if (rawUrl == "/rooms/createdby/me")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "announcement/v1/get")
                        {
                            s = BracketResponse;
                        }
                        if (Url == "communityboard/v2/current")
                        {
                            s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/communityboard.json");
                        }
                        if (Url == "communityboard/v1/current")
                        {
                            s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/communityboard.json");
                        }
                        if (Url.StartsWith("rooms/v2/search?value="))
                        {
                            //CustomRooms.RoomGet(Url.Remove(0, 22));
                        }
                        if (Url.StartsWith("players/v2/progression/bulk?"))
                        {
                            string temp = Url.Substring("players/v2/progression/bulk?id=".Length);
                            s = GetLevel(temp);
                        }
                        if (Url.StartsWith("messages/v1/favoriteFriendOnlineStatus"))
                        {
                            s = BracketResponse;
                        }
                        if (rawUrl.StartsWith("/announcements/v2/"))
                        {
                            s = BracketResponse;
                        }
                        if (Url == "images/v1/slideshow")
                        {
                            s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/rcslideshow.txt");
                        }
                        if (Url == "settings/v2/")
                        {
                            try
                            {
                                s = File.ReadAllText("SaveData\\settings.txt");
                            }
                            catch
                            {
                                File.WriteAllText("SaveData\\settings.txt", JsonConvert.SerializeObject(Settings.CreateDefaultSettings()));
                                s = File.ReadAllText("SaveData\\settings.txt");
                            }
                        }
                        if (Url == "settings/v2/set")
                        {
                            Settings.SetPlayerSettings(text);
                        }
                        if (Url == "mod_settings/v2/")
                        {
                            s = Settings.LoadmodSettings_file();
                        }
                        if (Url == "mod_settings/v2/set")
                        {
                            Settings.SetmodSettings(text);
                        }
                    send_data:
                        if (s.Length > 400)
                        {
                            Console.WriteLine("api Response: " + s.Length);
                        }
                        else
                        {
                            Console.WriteLine("api Response: " + s);
                        }
                        if (roomdata == true)
                        {
                            bytes = roomdatabytes;
                        }
                        else
                        {
                            bytes = Encoding.UTF8.GetBytes(s);
                        }
                        response.ContentLength64 = (long)bytes.Length;
                        Stream outputStream = response.OutputStream;
                        outputStream.Write(bytes, 0, bytes.Length);
                        Thread.Sleep(40);
                        outputStream.Close();
                        this.listener.Stop();
                    }
                }
            }
            catch (Exception ex4)
            {
                Console.WriteLine(ex4);
                File.WriteAllText("crashdump.txt", Convert.ToString(ex4));
                this.listener.Close();
                new APIServer();
            }
        }
        public static string SanitizeChatMessageRequest(string postData) => "\"" + JsonConvert.DeserializeObject<SanitizePostDTO>(postData).Value + "\"";

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
            public string? RoomName { get; set; }
            public string? ActionCode { get; set; }
        }

        public static string auth = "";

        public static ulong CachedPlayerID = ulong.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt"));

        public static ulong CachedPlatformID = 10000;
        public static ulong CachedversionID = 20200600;
        public static ulong Cachedservertimestarted = 20206000;
        public static int CachedVersionMonth = 01;

        public static string PlayerEventsResponse = "{\"Created\":[],\"Responses\":[]}";
        public static string ModerationBlockDetails = "{\"ReportCategory\":0,\"Duration\":0,\"GameSessionId\":0,\"Message\":\"\"}";
        public static string ImagesV2Named = "[{\"FriendlyImageName\":\"DormRoomBucket\",\"ImageName\":\"DormRoomBucket\",\"StartTime\":\"2021-12-27T21:27:38.1880175-08:00\",\"EndTime\":\"2025-12-27T21:27:38.1880399-08:00\"}";
        public static string ChallengesV1GetCurrent = "{\"Success\":true,\"Message\":\"OpenRec\"}";
        public static string ChecklistV1Current = "[{\"Order\":0,\"Objective\":3000,\"Count\":3,\"CreditAmount\":100},{\"Order\":1,\"Objective\":3001,\"Count\":3,\"CreditAmount\":100},{\"Order\":2,\"Objective\":3002,\"Count\":3,\"CreditAmount\":100}]";

        public static string BlankResponse = "";
        public static string BracketResponse = "[]";
        public static string VersionCheckResponse = "{\"VersionStatus\":0}";

        private HttpListener listener = new HttpListener();
    }
}
