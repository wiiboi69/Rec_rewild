﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace start
{
	class Setup
	{
		public static bool firsttime = false;
        public static void setup()
        {
            //sets up all the important files so openrec doesnt crash like lame vaultserver xD
            Console.WriteLine("Setting up... (May take a minute to download everything.)");
            Directory.CreateDirectory("SaveData\\App\\");
            Directory.CreateDirectory("SaveData\\Profile\\");
            Directory.CreateDirectory("SaveData\\Images\\");
            Directory.CreateDirectory("SaveData\\Rooms\\");
            Directory.CreateDirectory("SaveData\\Rooms\\custom\\");
            Directory.CreateDirectory("SaveData\\Images\\");
            Directory.CreateDirectory("SaveData\\Rooms\\Downloaded\\");
            if (!(File.Exists("SaveData\\App\\firsttime.txt")))
            {
                File.WriteAllText("SaveData\\App\\firsttime.txt", "this text file has no use other than to tell the program whether to bring up the intro or not");
                firsttime = true;
            }
            if (!(File.Exists("SaveData\\avatar.txt")))
            {
                File.WriteAllText("SaveData\\avatar.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/avatar.txt"));
            }
            else if (File.ReadAllText("SaveData\\avatar.txt") == "")
            {
                File.WriteAllText("SaveData\\avatar.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/avatar.txt"));
            }
            if (!(File.Exists("SaveData\\avataritems.txt")))
            {
                File.WriteAllText("SaveData\\avataritems.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/avataritems.txt"));
            }
            if (!(File.Exists("SaveData\\avataritems2.txt")))
            {
                File.WriteAllText("SaveData\\avataritems2.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/avataritems2.txt"));
            }
            if (!(File.Exists("SaveData\\equipment.txt")))
            {
                File.WriteAllText("SaveData\\equipment.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/equipment.txt"));
            }
            if (!(File.Exists("SaveData\\consumables.txt")))
            {
                File.WriteAllText("SaveData\\consumables.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/consumables.txt"));
            }
            if (!(File.Exists("SaveData\\gameconfigs.txt")))
            {
                File.WriteAllText("SaveData\\gameconfigs.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/gameconfigs.txt"));
            }
            if (!(File.Exists("SaveData\\storefronts2.txt")))
            {
                File.WriteAllText("SaveData\\storefronts2.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/storefront2.txt"));
            }
            if (!(File.Exists("SaveData\\baserooms.txt")))
            {
                File.WriteAllText("SaveData\\baserooms.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/baserooms.txt"));
            }
            if (!(File.Exists("SaveData\\Profile\\username.txt")))
            {
                File.WriteAllText("SaveData\\Profile\\username.txt", "Rec_rewild User#" + new Random().Next(0, 1000000));
            }
            if (!(File.Exists("SaveData\\Profile\\displayName.txt")))
            {
                File.WriteAllText("SaveData\\Profile\\displayName.txt", File.ReadAllText("SaveData\\Profile\\username.txt"));
            }
            if (!(File.Exists("SaveData\\Profile\\level.txt")))
            {
                File.WriteAllText("SaveData\\Profile\\level.txt", "10");
            }
            if (!(File.Exists("SaveData\\Profile\\userid.txt")))
            {
                File.WriteAllText("SaveData\\Profile\\userid.txt", "10000000");
            }
            if (!(File.Exists("SaveData\\myrooms.txt")))
            {
                File.WriteAllText("SaveData\\myrooms.txt", "[]");
            }
            if (!(File.Exists("SaveData\\settings.txt")))
            {
                File.WriteAllText("SaveData\\settings.txt", Newtonsoft.Json.JsonConvert.SerializeObject(api.Settings.CreateDefaultSettings()));
            }
            if (!(File.Exists("SaveData\\profileimage.png")))
            {
                File.WriteAllBytes("SaveData\\profileimage.png", new WebClient().DownloadData("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/profileimage.png"));
            }
            if (!(File.Exists("SaveData\\App\\privaterooms.txt")))
            {
                File.WriteAllText("SaveData\\App\\privaterooms.txt", "Enabled");
            }
            if (!(File.Exists("SaveData\\App\\showopenrecinfo.txt")))
            {
                File.WriteAllText("SaveData\\App\\showopenrecinfo.txt", "Enabled");
            }
            if (!(File.Exists("SaveData\\App\\facefeaturesadd.txt")))
            {
                File.WriteAllText("SaveData\\App\\facefeaturesadd.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/facefeaturesadd.txt"));
            }
            goto tryagainroom;

        tryagainroom:
            File.WriteAllText("SaveData\\Rooms\\custom\\test_room.json", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/test_room.json"));

            /*
            if (!File.Exists("SaveData\\Rooms\\Downloaded\\roomname.txt"))
            {
                try
                {
                    api.CustomRooms.RoomGet("gogo9");
                }
                catch
                {
                    goto tryagainroom;
                }

            }*/
            Console.WriteLine("Done!");
            Console.Clear();
        }
        public static void quicksetup()
        {
            //check all the important files so rec_rewild doesnt crash
            //and for updates

            Directory.CreateDirectory("SaveData\\App\\");
            Directory.CreateDirectory("SaveData\\Profile\\");
            Directory.CreateDirectory("SaveData\\Images\\");
            Directory.CreateDirectory("SaveData\\Rooms\\");
            Directory.CreateDirectory("SaveData\\Rooms\\custom\\");
            Directory.CreateDirectory("SaveData\\Images\\");
            Directory.CreateDirectory("SaveData\\Rooms\\Downloaded\\");

            if (!(File.Exists("SaveData\\avatar.txt")))
            {
                File.WriteAllText("SaveData\\avatar.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/avatar.txt"));
            }
            else if (File.ReadAllText("SaveData\\avatar.txt") == "")
            {
                File.WriteAllText("SaveData\\avatar.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/avatar.txt"));
            }
            if (!(File.Exists("SaveData\\avataritems.txt")))
            {
                File.WriteAllText("SaveData\\avataritems.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/avataritems.txt"));
            }
            if (!(File.Exists("SaveData\\avataritems2.txt")))
            {
                File.WriteAllText("SaveData\\avataritems2.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/avataritems2.txt"));
            }
            if (!(File.Exists("SaveData\\equipment.txt")))
            {
                File.WriteAllText("SaveData\\equipment.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/equipment.txt"));
            }
            if (!(File.Exists("SaveData\\consumables.txt")))
            {
                File.WriteAllText("SaveData\\consumables.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/consumables.txt"));
            }
            if (!(File.Exists("SaveData\\gameconfigs.txt")))
            {
                File.WriteAllText("SaveData\\gameconfigs.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/gameconfigs.txt"));
            }
            if (!(File.Exists("SaveData\\storefronts2.txt")))
            {
                File.WriteAllText("SaveData\\storefronts2.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/storefront2.txt"));
            }
            if (!(File.Exists("SaveData\\baserooms.txt")))
            {
                File.WriteAllText("SaveData\\baserooms.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/baserooms.txt"));
            }
            if (!(File.Exists("SaveData\\Profile\\username.txt")))
            {
                File.WriteAllText("SaveData\\Profile\\username.txt", "Rec_rewild User#" + new Random().Next(0, 1000000));
            }
            if (!(File.Exists("SaveData\\Profile\\displayName.txt")))
            {
                File.WriteAllText("SaveData\\Profile\\displayName.txt", File.ReadAllText("SaveData\\Profile\\username.txt"));
            }
            if (!(File.Exists("SaveData\\Profile\\level.txt")))
            {
                File.WriteAllText("SaveData\\Profile\\level.txt", "10");
            }
            if (!(File.Exists("SaveData\\Profile\\bio.txt")))
            {
                File.WriteAllText("SaveData\\Profile\\bio.txt", "yeet");
            }
            if (!(File.Exists("SaveData\\Profile\\userid.txt")))
            {
                File.WriteAllText("SaveData\\Profile\\userid.txt", "10000000");
            }
            if (!(File.Exists("SaveData\\myrooms.txt")))
            {
                File.WriteAllText("SaveData\\myrooms.txt", "[]");
            }
            if (!(File.Exists("SaveData\\settings.txt")))
            {
                File.WriteAllText("SaveData\\settings.txt", Newtonsoft.Json.JsonConvert.SerializeObject(api.Settings.CreateDefaultSettings()));
            }
            if (!(File.Exists("SaveData\\profileimage.png")))
            {
                File.WriteAllBytes("SaveData\\profileimage.png", new WebClient().DownloadData("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/profileimage.png"));
            }
            if (!(File.Exists("SaveData\\App\\privaterooms.txt")))
            {
                File.WriteAllText("SaveData\\App\\privaterooms.txt", "Enabled");
            }
            if (!(File.Exists("SaveData\\App\\showopenrecinfo.txt")))
            {
                File.WriteAllText("SaveData\\App\\showopenrecinfo.txt", "Enabled");
            }
            if (!(File.Exists("SaveData\\App\\facefeaturesadd.txt")))
            {
                File.WriteAllText("SaveData\\App\\facefeaturesadd.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/facefeaturesadd.txt"));
            }

        }
    }
}
