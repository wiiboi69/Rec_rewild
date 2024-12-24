using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using server;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static api.Roomdata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace api
{
    internal class room_util
    {
        public static string find_room_with_id(string rawUrl, int value)
        {
            Console.WriteLine(rawUrl + " | " + value);
            string s = BlankResponse;
            //string Url = rawUrl.Remove(0, value);
            string Url = rawUrl.Substring(value);
            string[] stringSeparators = new string[] { "?include=1325" };
            string[] subs = Url.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            stringSeparators = new string[] { "?include=301" };
            subs = subs[0].Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            string temp1 = subs[0];
            string temp2 = GameSessions.FindRoomid(ulong.Parse(temp1));
            
            roomdownload:
            if (temp2 != "")
            {
                
                try
                {
                    Console.WriteLine("found room name: " + temp2 + " using room id: " + temp1);
                    string replaceWith = "";
                    temp2 = temp2.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
                    return new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/" + temp2 + ".txt").ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    goto roomdownload_ToLower;
                }
            }
        roomdownload_ToLower:
            if (temp2 != "")
            {

                try
                {
                    Console.WriteLine("found room name: " + temp2 + " using room id: " + temp1);
                    string replaceWith = "";
                    temp2 = temp2.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
                    return new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/" + temp2.ToLower() + ".txt").ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    goto roomfaileddownload;
                }
            }
            else
            {
                try
                {
                    Console.WriteLine("finding room id: " + temp1);
                    temp2 = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_id/" + temp1 + ".txt").ToString();
                    goto roomdownload;
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    goto roomfaileddownload;
                }
            }
            roomfaileddownload:
            Console.WriteLine("finding custom room id: " + temp1);
            Roomlist roomlistdata = JsonConvert.DeserializeObject<Roomlist>(s);

            string[] roomlistdir = Directory.GetFiles("SaveData\\Rooms\\custom\\");
            foreach (string roomdir in roomlistdir)
            {
                Roomdata.RoomRootv2 roomdata = JsonConvert.DeserializeObject<Roomdata.RoomRootv2>(File.ReadAllText(roomdir));
                
                if (roomdata.RoomId == ulong.Parse(temp1))
                {
                    Console.WriteLine("found room name: " + roomdir + " using room id: " + temp1);
                    return File.ReadAllText(roomdir);
                }
                else
                {
                    int n = 0;
                    List<SubRooms> subroomdata = roomdata.SubRooms;
                    foreach (SubRooms sunroomdir in subroomdata)
                    {
                        if (sunroomdir.RoomId == int.Parse(temp1))
                        {
                            Console.WriteLine("found room name: " + roomdir + " using room id: " + temp1);
                            return File.ReadAllText(roomdir);
                        }
                    }
                }
            }
            Console.WriteLine("can't find room id: " + temp1);
            return new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/dormroom.txt").ToString();
        }

        public static string room_change_CreatorAccount(string value)
        {
            string temp1;
            //roomdata.RoomRootv2 root = JsonConvert.DeserializeObject(value);
            Roomdata.RoomRootv2 root = JsonConvert.DeserializeObject<RoomRootv2>(value);
            root.CreatorAccountId = APIServer.CachedPlayerID;
            temp1 = JsonConvert.SerializeObject(root);
            root = JsonConvert.DeserializeObject<RoomRootv2>(temp1);
            root.Roles.Add(new Roles
            {
                Role = 255,
                InvitedRole = 0,
                AccountId = (int)APIServer.CachedPlayerID,
            });
            Console.WriteLine(value);
            Console.WriteLine(JsonConvert.SerializeObject(root));
            return JsonConvert.SerializeObject(root);
        }

        public static string room_change_fix_room(string value)
        {
            string temp1;
            //roomdata.RoomRootv2 root = JsonConvert.DeserializeObject(value);
            RoomRootv2 root = JsonConvert.DeserializeObject<RoomRootv2>(value);
            root.CreatorAccountId = APIServer.CachedPlayerID;
            temp1 = JsonConvert.SerializeObject(root);
            root = JsonConvert.DeserializeObject<RoomRootv2>(temp1);
            List<SubRoomsv2> subroomsv2data = new List<SubRoomsv2>
            {

            };

            SubRoomsv2 subroomdata = new SubRoomsv2
            {
                Accessibility = 0,
                DataBlob = "",
                DataBlobName = "",
                DataBlobHash = "",
                IsSandbox = true,
                MaxPlayers = 0,
                Name = "",
                SubRoomId = 0,
                RoomId = 0,
                UnitySceneId = "",
                RoomSceneId = 0,
                RoomSceneLocationId = "",
                DataModifiedAt = DateTime.UtcNow,
                DataSavedAt = DateTime.UtcNow,
                Location = "",
                SavedByAccountId = -1,
            };

            int n = 0;
            do
            {
                subroomdata.Accessibility = root.SubRooms[n].Accessibility;
                //"Accessibility": 1,

                subroomdata.DataBlob = root.SubRooms[n].DataBlob;
                //"DataBlob": "",

                subroomdata.DataBlobName = root.SubRooms[n].DataBlob;
                //"DataBlobName": "",

                subroomdata.DataBlobHash = "";
                //"DataBlobHash": "",

                subroomdata.DataSavedAt = DateTime.UtcNow;
                //"DataSavedAt": "2024-04-29T00:50:31.3734855",

                subroomdata.IsSandbox = root.SubRooms[n].IsSandbox;
                //"IsSandbox": true,

                subroomdata.MaxPlayers = root.SubRooms[n].MaxPlayers;
                //"MaxPlayers": 4,

                subroomdata.Name = root.SubRooms[n].Name;
                //"Name": "Home",

                subroomdata.RoomSceneId = root.SubRooms[n].RoomId;
                //"RoomSceneId": 1,

                subroomdata.RoomId = root.SubRooms[n].RoomId;
                //"RoomId": 1,

                subroomdata.SubRoomId = root.SubRooms[n].SubRoomId;
                //"SubRoomId": 1,

                subroomdata.DataModifiedAt = DateTime.UtcNow;
                //"DataModifiedAt" : "2024-04-29T00:50:31.3734855",

                subroomdata.UnitySceneId = root.SubRooms[n].UnitySceneId;
                //"UnitySceneId": "76d98498-60a1-430c-ab76-b54a29b7a163",

                subroomdata.RoomSceneLocationId = root.SubRooms[n].UnitySceneId;
                //"RoomSceneLocationId": "76d98498-60a1-430c-ab76-b54a29b7a163",

                subroomdata.Location = root.SubRooms[n].UnitySceneId;
                //"Location": "76d98498-60a1-430c-ab76-b54a29b7a163",


                subroomsv2data.Add(subroomdata);

                n++;
            } while (n < root.SubRooms.Count);

            Statsv2 Stats = new Statsv2
            {
                CheerCount = root.Stats.CheerCount,
                FavoriteCount = root.Stats.FavoriteCount,
                RoomId = root.RoomId,
                VisitCount = root.Stats.VisitCount,
                VisitorCount = root.Stats.VisitorCount,
            };

            RoomRootv3 rootv2 = new RoomRootv3
            {
                Accessibility = root.Accessibility,
                //"Accessibility": 2,

                AllowsJuniors = root.SupportsJuniors,
                //"AllowsJuniors": true,    

                CustomRoomWarning = root.CustomWarning,
                //"CustomRoomWarning": "",

                RoomWarningMask = root.WarningMask,
                //"RoomWarningMask": 0,

                CloningAllowed = root.CloningAllowed,
                //"CloningAllowed": true,

                CreatedAt = root.CreatedAt,
                //"CreatedAt": "2024-04-29T00:50:31.3734855",

                CreatorAccountId = APIServer.CachedPlayerID,
                //"CreatorAccountId": 1,

                CreatorPlayerId = APIServer.CachedPlayerID,
                //"CreatorPlayerId": 1,

                CustomWarning = root.CustomWarning,
                //"CustomWarning": "",

                DataBlob = root.DataBlob,
                //"DataBlob": null,

                Description = root.Description,
                //"Description": "",

                DisableMicAutoMute = root.DisableMicAutoMute,
                //"DisableMicAutoMute": false,

                DisableRoomComments = root.DisableRoomComments,
                //"DisableRoomComments": false,

                EncryptVoiceChat = root.EncryptVoiceChat,
                //"EncryptVoiceChat": false,

                ImageName = root.ImageName,
                //"ImageName": "Dorm_Room",

                IsDorm = root.IsDorm,
                //"IsDorm": true,

                IsRRO = root.IsRRO,
                //"IsRRO": true,

                LoadScreenLocked = root.LoadScreenLocked,
                //"LoadScreenLocked": false,

                LoadScreens = root.LoadScreens,
                //"LoadScreens": [],

                MaxPlayerCalculationMode = 0,
                //"MaxPlayerCalculationMode": 0,

                MaxPlayers = root.MaxPlayers,
                //"MaxPlayers": 4,

                MinLevel = root.MinLevel,
                //"MinLevel": 0,

                Name = root.Name,
                //"Name": "DormRoom",

                PromoExternalContent = root.PromoExternalContent,
                //"PromoExternalContent": [],

                PromoImages = root.PromoImages,
                //"PromoImages": [],

                AutoLocalizeRoom = true,
                CoOwners = 
                [
                    APIServer.CachedPlayerID,
                ],
                InvitedCoOwners = [],
                RankingContext = null,
                IsDeveloperOwned = root.IsDeveloperOwned,
                Hosts = [],
                InvitedHosts = [],
                InvitedModerators = [],
                Moderators = [],
                PersistenceVersion = 0,
                PlayerIdsWithModPower = [],
                RankedEntityId = null,
                Roles = root.Roles,
                //RoomId = root.RoomId,
                RoomId = root.RoomId,
                SetType = 0,
                State = 0,
                CheerCount = root.Stats.CheerCount,
                FavoriteCount = root.Stats.FavoriteCount,
                VisitCount = root.Stats.VisitCount,
                VisitorCount = root.Stats.VisitorCount,
                Stats = Stats,
                SubRooms = subroomsv2data,
                SupportsJuniors = true,
                //"SupportsJuniors": true,

                SupportsLevelVoting = true,
                //"SupportsLevelVoting": true,

                SupportsMobile = true,
                //"SupportsMobile": true,

                SupportsQuest2 = true,
                //"SupportsQuest2": true,

                SupportsScreens = true,
                //"SupportsScreens": true,
                
                SupportsTeleportVR = true,
                //"SupportsTeleportVR": true,
                
                SupportsVRLow = true,
                //"SupportsVRLow": true,
                
                SupportsWalkVR = true,
                //"SupportsWalkVR": true,
                
                Tags = root.Tags,
                /*"Tags": [
                    {
                        "Tag": "DormRoom",
                        "Type": 0
                    }
                 ],
                 */
                ToxmodEnabled = false,

                Type = 0,
                
                UgcVersion = 0,
                
                Version = 0,
                //"Version": 0,
                
                WarningMask = 0,
                //"WarningMask": 0
            };

            rootv2.SubRooms = subroomsv2data;

            Console.WriteLine(value);
            Console.WriteLine(JsonConvert.SerializeObject(rootv2));
            Console.WriteLine(JsonConvert.SerializeObject(subroomsv2data));
            return JsonConvert.SerializeObject(rootv2);
        }

        public static string room_change_fix_room_2020(string value)
        {
            string temp1;
            //roomdata.RoomRootv2 root = JsonConvert.DeserializeObject(value);
            RoomRootv2 root = JsonConvert.DeserializeObject<RoomRootv2>(value);
            root.CreatorAccountId = APIServer.CachedPlayerID;
            temp1 = JsonConvert.SerializeObject(root);
            root = JsonConvert.DeserializeObject<RoomRootv2>(temp1);
            List<Scene> subroomsv2data = new List<Scene>
            {

            };

            Scene subroomdata = new Scene
            {
                DataBlobName = "",
                IsSandbox = true,
                MaxPlayers = 0,
                Name = "",
                SubRoomId = 0,
                RoomId = 0,
                RoomSceneId = 0,
                RoomSceneLocationId = "",
                DataModifiedAt = DateTime.UtcNow,
                Location = "",
                CanMatchmakeInto = true,
            };

            int n = 0;
            do
            {

                subroomdata.DataBlobName = root.SubRooms[n].DataBlob;
                //"DataBlobName": "",

                subroomdata.IsSandbox = root.SubRooms[n].IsSandbox;
                //"IsSandbox": true,

                subroomdata.MaxPlayers = root.SubRooms[n].MaxPlayers;
                //"MaxPlayers": 4,

                subroomdata.Name = root.SubRooms[n].Name;
                //"Name": "Home",

                subroomdata.RoomSceneId = root.SubRooms[n].RoomId;
                //"RoomSceneId": 1,

                subroomdata.RoomId = root.SubRooms[n].RoomId;
                //"RoomId": 1,

                subroomdata.SubRoomId = root.SubRooms[n].SubRoomId;
                //"SubRoomId": 1,

                subroomdata.DataModifiedAt = DateTime.UtcNow;
                //"DataModifiedAt" : "2024-04-29T00:50:31.3734855",

                subroomdata.RoomSceneLocationId = root.SubRooms[n].UnitySceneId;
                //"RoomSceneLocationId": "76d98498-60a1-430c-ab76-b54a29b7a163",

                subroomdata.Location = root.SubRooms[n].UnitySceneId;
                //"Location": "76d98498-60a1-430c-ab76-b54a29b7a163",

                subroomdata.CanMatchmakeInto = true;

                subroomsv2data.Add(subroomdata);

                n++;
            } while (n < root.SubRooms.Count);

            Room rootv2 = new Room
            {
                Accessibility = root.Accessibility,
                //"Accessibility": 2,

                AllowsJuniors = root.SupportsJuniors,
                //"AllowsJuniors": true,    

                CustomRoomWarning = root.CustomWarning,
                //"CustomRoomWarning": "",

                RoomWarningMask = root.WarningMask,
                //"RoomWarningMask": 0,

                CloningAllowed = root.CloningAllowed,
                //"CloningAllowed": true,
                
                creatorPlayerId = APIServer.CachedPlayerID,
                //"CreatorAccountId": 1,

                CreatorPlayerId = APIServer.CachedPlayerID,
                //"CreatorPlayerId": 1,

                Description = root.Description,
                //"Description": "",

                DisableMicAutoMute = root.DisableMicAutoMute,
                //"DisableMicAutoMute": false,

                DisableRoomComments = root.DisableRoomComments,
                //"DisableRoomComments": false,

                EncryptVoiceChat = root.EncryptVoiceChat,
                //"EncryptVoiceChat": false,

                ImageName = root.ImageName,
                //"ImageName": "Dorm_Room",

                IsDormRoom = root.IsDorm,
                //"IsDorm": true,

                IsAGRoom = root.IsRRO,
                //"IsRRO": true,

                Name = root.Name,
                //"Name": "DormRoom",

                //RoomId = root.RoomId,
                RoomId = root.RoomId,
                SetType = 0,
                State = 0,

                SupportsLevelVoting = true,
                //"SupportsLevelVoting": true,

                SupportsMobile = true,
                //"SupportsMobile": true,

                SupportsScreens = true,
                //"SupportsScreens": true,

                SupportsTeleportVR = true,
                //"SupportsTeleportVR": true,

                SupportsVRLow = true,
                //"SupportsVRLow": true,

                SupportsWalkVR = true,
                //"SupportsWalkVR": true,

                Type = 0,
            };

            RoomRoot main_root = new RoomRoot
            {
                CheerCount = root.Stats.CheerCount,
                FavoriteCount = root.Stats.FavoriteCount,
                VisitCount = root.Stats.VisitCount,
                Tags = root.Tags,
                beta = false,
                CoOwners =
                [
                    APIServer.CachedPlayerID
                ],
                Moderators = [],
                InvitedModerators = [],
                Hosts = [],
                InvitedCoOwners = [],
                InvitedHosts = [],
                PlayerIdsWithModPower = [],
                
            };

            main_root.Scenes = subroomsv2data;


            main_root.Room = rootv2;

            Console.WriteLine(value);
            Console.WriteLine(JsonConvert.SerializeObject(main_root));
            Console.WriteLine(JsonConvert.SerializeObject(subroomsv2data));
            return JsonConvert.SerializeObject(main_root);
        }

        public static string find_room_with_id_lowercase(string rawUrl, int value)
        {
            Console.WriteLine(rawUrl + " | " + value);
            string s = BlankResponse;
            string Url = rawUrl.Remove(0, value);
            string[] stringSeparators = new string[] { "?include=1325" };
            string[] subs = Url.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            stringSeparators = new string[] { "?include=301" };
            subs = subs[0].Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            string temp1 = subs[0];
            string temp2 = GameSessions.FindRoomid(ulong.Parse(temp1));
            if (temp2 != "")
            {
                try
                {
                    Console.WriteLine("found room name: " + temp2 + " using room id: " + temp1);
                    return new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/" + temp2.ToLower() + ".txt").ToString().ToLower();
                }
                catch
                {
                    goto roomfaileddownload;
                }
            }
            else
            {
                goto roomfaileddownload;
            }
        roomfaileddownload:
            Console.WriteLine("can't find room id : " + temp1);
            return new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/dormroom.txt").ToString().ToLower();
        }

        public static string room_find_CustomRooms(string s)
        {
            throw new NotImplementedException();
        }

        public static string room_inject_CustomRooms_list(string s)
        {
            Roomlist roomlistdata = JsonConvert.DeserializeObject<Roomlist>(s);
            long rooms = roomlistdata.TotalResults;

            string[] roomlistdir = Directory.GetFiles("SaveData\\Rooms\\custom\\");
            foreach (string roomdir in roomlistdir)
            {
                Roomdata.RoomRootv2 roomdata = JsonConvert.DeserializeObject<Roomdata.RoomRootv2>(File.ReadAllText(roomdir));

                roomlistdata.Results.Add(roomdata);
                rooms ++;
            }
            roomlistdata.TotalResults = rooms;
            return JsonConvert.SerializeObject(roomlistdata);
        }

        public static string room_inject_MyRooms_list(string s)
        {
            Roomlist roomlist = new Roomlist();
            Roomlist root = JsonConvert.DeserializeObject<Roomlist>(s);

            return s;
        }

        internal static string room_fix_Rooms_list(string s)
        {
            Roomlist roomlistdata = JsonConvert.DeserializeObject<Roomlist>(s);
            roomlistv2_1 roomlistdatav2 = new roomlistv2_1
            {
                Results = [],
                TotalResults = roomlistdata.TotalResults,
            };

            foreach (Roomdata.RoomRootv2 roomrootdata in roomlistdata.Results)
            {
                /*
                string temp = room_util.room_change_fix_room(JsonConvert.SerializeObject(roomrootdata));
                roomdata.RoomRootv3 roomrootdatav2 = JsonConvert.DeserializeObject<roomdata.RoomRootv3>(temp);
                */
                string temp1;
                //roomdata.RoomRootv2 root = JsonConvert.DeserializeObject(value);
                RoomRootv2 root = roomrootdata;
                root.CreatorAccountId = APIServer.CachedPlayerID;
                List<SubRooms> subroomsv2data = new List<SubRooms>
                {

                };

                SubRooms subroomdata = new SubRooms
                {
                    Accessibility = 0,
                    DataBlob = "",
                    IsSandbox = true,
                    MaxPlayers = 0,
                    Name = "",
                    SubRoomId = 0,
                    RoomId = 0,
                    UnitySceneId = "",
                    SavedByAccountId = -1,
                };

                int n = 0;
                do
                {
                    subroomdata.Accessibility = root.SubRooms[n].Accessibility;
                    //"Accessibility": 1,

                    subroomdata.DataBlob = root.SubRooms[n].DataBlob;
                    //"DataBlob": "",

                    subroomdata.IsSandbox = root.SubRooms[n].IsSandbox;
                    //"IsSandbox": true,

                    subroomdata.MaxPlayers = root.SubRooms[n].MaxPlayers;
                    //"MaxPlayers": 4,

                    subroomdata.Name = root.SubRooms[n].Name;
                    //"Name": "Home",

                    subroomdata.RoomId = root.SubRooms[n].RoomId;
                    //"RoomId": 1,

                    subroomdata.SubRoomId = root.SubRooms[n].SubRoomId;
                    //"SubRoomId": 1,

                    subroomdata.UnitySceneId = root.SubRooms[n].UnitySceneId;
                    //"UnitySceneId": "76d98498-60a1-430c-ab76-b54a29b7a163",

                    subroomsv2data.Add(subroomdata);

                    n++;
                } while (n < root.SubRooms.Count);

                Stats Stats = new Stats
                {
                    CheerCount = root.Stats.CheerCount,
                    FavoriteCount = root.Stats.FavoriteCount,
                    VisitCount = root.Stats.VisitCount,
                    VisitorCount = root.Stats.VisitorCount,
                };

                RoomRootv3_1 rootv2 = new RoomRootv3_1
                {
                    Accessibility = root.Accessibility,
                    //"Accessibility": 2,

                    CloningAllowed = root.CloningAllowed,
                    //"CloningAllowed": true,

                    CreatedAt = root.CreatedAt,
                    //"CreatedAt": "2024-04-29T00:50:31.3734855",

                    CreatorAccountId = APIServer.CachedPlayerID,
                    //"CreatorAccountId": 1,

                    CustomWarning = root.CustomWarning,
                    //"CustomWarning": "",

                    DataBlob = root.DataBlob,
                    //"DataBlob": null,

                    Description = root.Description,
                    //"Description": "",

                    DisableMicAutoMute = root.DisableMicAutoMute,
                    //"DisableMicAutoMute": false,

                    DisableRoomComments = root.DisableRoomComments,
                    //"DisableRoomComments": false,

                    EncryptVoiceChat = root.EncryptVoiceChat,
                    //"EncryptVoiceChat": false,

                    ImageName = root.ImageName,
                    //"ImageName": "Dorm_Room",

                    IsDorm = root.IsDorm,
                    //"IsDorm": true,

                    IsRRO = root.IsRRO,
                    //"IsRRO": true,

                    LoadScreenLocked = root.LoadScreenLocked,
                    //"LoadScreenLocked": false,

                    LoadScreens = root.LoadScreens,
                    //"LoadScreens": [],

                    MaxPlayerCalculationMode = root.MaxPlayerCalculationMode,
                    //"MaxPlayerCalculationMode": 0,

                    MaxPlayers = root.MaxPlayers,
                    //"MaxPlayers": 4,

                    MinLevel = root.MinLevel,
                    //"MinLevel": 0,

                    Name = root.Name,
                    //"Name": "DormRoom",

                    PromoExternalContent = root.PromoExternalContent,
                    //"PromoExternalContent": [],

                    PromoImages = root.PromoImages,
                    //"PromoImages": [],

                    AutoLocalizeRoom = root.AutoLocalizeRoom,
                    RankingContext = root.RankingContext,
                    IsDeveloperOwned = root.IsDeveloperOwned,
                    PersistenceVersion = root.PersistenceVersion,
                    RankedEntityId = root.RankedEntityId,
                    Roles = root.Roles,
                    RoomId = root.RoomId,
                    State = root.State,
                    Stats = Stats,
                    SubRooms = subroomsv2data,
                    SupportsJuniors = true,
                    //"SupportsJuniors": true,

                    SupportsLevelVoting = true,
                    //"SupportsLevelVoting": true,

                    SupportsMobile = true,
                    //"SupportsMobile": true,

                    SupportsQuest2 = true,
                    //"SupportsQuest2": true,

                    SupportsScreens = true,
                    //"SupportsScreens": true,

                    SupportsTeleportVR = true,
                    //"SupportsTeleportVR": true,

                    SupportsVRLow = true,
                    //"SupportsVRLow": true,

                    SupportsWalkVR = true,
                    //"SupportsWalkVR": true,

                    Tags = root.Tags,
                    /*"Tags": [
                        {
                            "Tag": "DormRoom",
                            "Type": 0
                        }
                     ],
                     */
                    ToxmodEnabled = false,

                    UgcVersion = root.UgcVersion,

                    Version = root.UgcVersion,
                    //"Version": 0,

                    WarningMask = root.WarningMask,
                    //"WarningMask": 0
                };

                rootv2.SubRooms = subroomsv2data;

                roomlistdatav2.Results.Add(rootv2);
            }
            File.WriteAllText("test.txt", s);
            File.WriteAllText("test2.txt", JsonConvert.SerializeObject(roomlistdatav2));
            return JsonConvert.SerializeObject(roomlistdatav2);
        }

        public static string Saveroom(string text, string roomid, string subroom)
        {




            return JsonConvert.SerializeObject(new
            {
                error = "failed: [error_code:not_implemented]",
                value = "",
                success = false
            });// "{\"error\":\"failed: [error_code:not_implemented]\",\"success\":false,\"value\":\"\"}";
        }

        public static string BlankResponse = "";

        public static string BracketResponse = "[]";

    }
}
