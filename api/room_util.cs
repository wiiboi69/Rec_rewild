using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using server;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net;
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
            Console.WriteLine("can't find room id : " + temp1);
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
                Location = "",
                SavedByAccountId = -1,
            };

            int n = 0;
            do
            {
                subroomdata.Accessibility = root.SubRooms[n].Accessibility;
                subroomdata.DataBlob = root.SubRooms[n].DataBlob;
                subroomdata.DataBlobName = root.SubRooms[n].DataBlob;

                subroomdata.IsSandbox = root.SubRooms[n].IsSandbox;
                subroomdata.MaxPlayers = root.SubRooms[n].MaxPlayers;
                subroomdata.Name = root.SubRooms[n].Name;
                subroomdata.SubRoomId = root.SubRooms[n].SubRoomId;
                subroomdata.RoomId = root.SubRooms[n].RoomId;
                subroomdata.UnitySceneId = root.SubRooms[n].UnitySceneId;
                subroomdata.Location = root.SubRooms[n].UnitySceneId;
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
                AutoLocalizeRoom = true,
                CloningAllowed = root.CloningAllowed,
                CreatedAt = "",
                CoOwners = 
                [
                    APIServer.CachedPlayerID,
                ],
                CreatorAccountId = APIServer.CachedPlayerID,
                CustomWarning = root.CustomWarning,
                DisableRoomComments = root.DisableRoomComments,
                EncryptVoiceChat = root.EncryptVoiceChat,
                InvitedCoOwners = [],
                MaxPlayerCalculationMode = 0,
                PromoExternalContent = [],
                RankingContext = null,
                DataBlob = root.DataBlob,
                Description = root.Description,
                DisableMicAutoMute = root.DisableMicAutoMute,
                IsDeveloperOwned = root.IsDeveloperOwned,
                IsDorm = root.IsDorm,
                Hosts = [],
                ImageName = root.ImageName,
                InvitedHosts = [],
                InvitedModerators = [],
                IsRRO = root.IsRRO,
                LoadScreenLocked = false,
                LoadScreens = [],
                MaxPlayers = root.MaxPlayers,
                MinLevel = root.MinLevel,
                Moderators = [],
                Name = root.Name,
                PersistenceVersion = 0,
                PlayerIdsWithModPower = [],
                PromoImages = root.PromoImages,
                RankedEntityId = null,
                Roles = root.Roles,
                RoomId = 0,
                SetType = 0,
                State = 0,
                Stats = Stats,
                SubRooms = subroomsv2data,
                SupportsJuniors = true,
                SupportsLevelVoting = true,
                SupportsMobile = true,
                SupportsQuest2 = true,
                SupportsScreens = true,
                SupportsTeleportVR = true,
                SupportsVRLow = true,
                SupportsWalkVR = true,
                Tags = root.Tags,
                ToxmodEnabled = false,
                Type = 0,
                UgcVersion = 0,
                WarningMask = 0,
            };


            Console.WriteLine(value);
            Console.WriteLine(JsonConvert.SerializeObject(root));
            return JsonConvert.SerializeObject(root);
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
            roomlist roomlist = new roomlist();
            roomlist root = JsonConvert.DeserializeObject<roomlist>(s);

            return s;
        }

        public static string BlankResponse = "";

        public static string BracketResponse = "[]";

    }
}
