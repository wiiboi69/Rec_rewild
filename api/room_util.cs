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
using static api.roomdata;

namespace api
{
    internal class room_util
    {
        public static string find_room_with_id(string rawUrl, int value)
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
                    return new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/" + temp2.ToLower() + ".txt").ToString();
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
            Console.WriteLine("finding custom room id: " + temp1);
            roomlist roomlistdata = JsonConvert.DeserializeObject<roomlist>(s);

            string[] roomlistdir = Directory.GetFiles("SaveData\\Rooms\\custom\\");
            foreach (string roomdir in roomlistdir)
            {
                roomdata.RoomRootv2 roomdata = JsonConvert.DeserializeObject<roomdata.RoomRootv2>(File.ReadAllText(roomdir));
                
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
            roomdata.RoomRootv2 root = JsonConvert.DeserializeObject<RoomRootv2>(value);
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

                RoomWarningMask = 0,
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

                LoadScreenLocked = false,
                //"LoadScreenLocked": false,

                LoadScreens = [],
                //"LoadScreens": [],

                MaxPlayerCalculationMode = 0,
                //"MaxPlayerCalculationMode": 0,

                MaxPlayers = root.MaxPlayers,
                //"MaxPlayers": 4,

                MinLevel = root.MinLevel,
                //"MinLevel": 0,

                Name = root.Name,
                //"Name": "DormRoom",

                PromoExternalContent = [],
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
                RoomId = 0,
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

            n = 0;
            do
            {
                rootv2.SubRooms[n].Accessibility = root.SubRooms[n].Accessibility;
                //"Accessibility": 1,

                rootv2.SubRooms[n].DataBlob = root.SubRooms[n].DataBlob;
                //"DataBlob": "",

                rootv2.SubRooms[n].DataBlobName = root.SubRooms[n].DataBlob;
                //"DataBlobName": "",

                rootv2.SubRooms[n].DataBlobHash = "";
                //"DataBlobHash": "",

                rootv2.SubRooms[n].DataSavedAt = DateTime.UtcNow;
                //"DataSavedAt": "2024-04-29T00:50:31.3734855",

                rootv2.SubRooms[n].IsSandbox = root.SubRooms[n].IsSandbox;
                //"IsSandbox": true,

                rootv2.SubRooms[n].MaxPlayers = root.SubRooms[n].MaxPlayers;
                //"MaxPlayers": 4,

                rootv2.SubRooms[n].Name = root.SubRooms[n].Name;
                //"Name": "Home",

                rootv2.SubRooms[n].RoomSceneId = root.SubRooms[n].RoomId;
                //"RoomSceneId": 1,

                rootv2.SubRooms[n].RoomId = root.SubRooms[n].RoomId;
                //"RoomId": 1,

                rootv2.SubRooms[n].SubRoomId = root.SubRooms[n].SubRoomId;
                //"SubRoomId": 1,

                rootv2.SubRooms[n].DataModifiedAt = DateTime.UtcNow;
                //"DataModifiedAt" : "2024-04-29T00:50:31.3734855",

                rootv2.SubRooms[n].UnitySceneId = root.SubRooms[n].UnitySceneId;
                //"UnitySceneId": "76d98498-60a1-430c-ab76-b54a29b7a163",

                rootv2.SubRooms[n].RoomSceneLocationId = root.SubRooms[n].UnitySceneId;
                //"RoomSceneLocationId": "76d98498-60a1-430c-ab76-b54a29b7a163",

                rootv2.SubRooms[n].Location = root.SubRooms[n].UnitySceneId;
                //"Location": "76d98498-60a1-430c-ab76-b54a29b7a163",

                n++;
            } while (n < root.SubRooms.Count);

            Console.WriteLine(value);
            Console.WriteLine(JsonConvert.SerializeObject(rootv2));
            Console.WriteLine(JsonConvert.SerializeObject(subroomsv2data));
            return JsonConvert.SerializeObject(rootv2);
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
            roomlist roomlistdata = JsonConvert.DeserializeObject<roomlist>(s);
            long rooms = roomlistdata.TotalResults;

            string[] roomlistdir = Directory.GetFiles("SaveData\\Rooms\\custom\\");
            foreach (string roomdir in roomlistdir)
            {
                roomdata.RoomRootv2 roomdata = JsonConvert.DeserializeObject<roomdata.RoomRootv2>(File.ReadAllText(roomdir));

                roomlistdata.Results.Add(roomdata);
                rooms ++;
            }
            roomlistdata.TotalResults = rooms;
            return JsonConvert.SerializeObject(roomlistdata);
        }

        public static string room_inject_MyRooms_list(string s)
        {
            roomlist roomlist = new roomlist();
            roomlist root = JsonConvert.DeserializeObject<roomlist>(s);

            return s;
        }

        public static string BlankResponse = "";

        public static string BracketResponse = "[]";

    }
}
