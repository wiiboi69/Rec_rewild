﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using server;
using System;
using System.Collections.Generic;
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

        public static string BlankResponse = "";

        public static string BracketResponse = "[]";

    }
}
