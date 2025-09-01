using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Rec_rewild.api;
using System.Reflection.Emit;
using Rec_rewild.console;

namespace start
{
	class Setup
	{
		public static bool firsttime = false;
        public static void setup()
        {
            //sets up all the important files so Rec_rewild doesnt crash
            //Console.WriteLine("Setting up... (May take a minute to download everything.)");
            Loading.ShowLoading("Setting up... (May take a minute to download everything.)");
            Directory.CreateDirectory("SaveData\\App\\");
            Directory.CreateDirectory("SaveData\\Profile\\");
            Directory.CreateDirectory("SaveData\\Images\\");
            Directory.CreateDirectory("SaveData\\Rooms\\");
            Directory.CreateDirectory("SaveData\\Rooms\\custom\\");
            Directory.CreateDirectory("SaveData\\Rooms\\cdn\\");
            Directory.CreateDirectory("SaveData\\Rooms\\custom\\Downloaded\\");
            Directory.CreateDirectory("SaveData\\Images\\");
            Directory.CreateDirectory("SaveData\\video\\");
            Directory.CreateDirectory("SaveData\\custom\\");
            Directory.CreateDirectory("SaveData\\custom\\items\\");
            Directory.CreateDirectory("SaveData\\custom\\avatar items\\");
            Directory.CreateDirectory("SaveData\\custom\\skins\\");


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
            if (!(File.Exists("SaveData\\all_avatar_items.txt")))
            {
                File.WriteAllText("SaveData\\all_avatar_items.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/refs/heads/main_v2/setup/avataritemsfull.json"));
            }
            if (!(File.Exists("SaveData\\equipment.txt")))
            {
                File.WriteAllText("SaveData\\equipment.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/equipment.txt"));
            }
            if (!(File.Exists("SaveData\\consumables.txt")))
            {
                File.WriteAllText("SaveData\\consumables.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/consumables.txt"));
            }
            if (!(File.Exists("SaveData\\myrooms.txt")))
            {
                File.WriteAllText("SaveData\\myrooms.txt", "[]");
            }
            setup_profile();
            if (!(File.Exists("SaveData\\settings.txt")))
            {
                File.WriteAllText("SaveData\\settings.txt", JsonConvert.SerializeObject(api.Settings.CreateDefaultSettings()));
            }
            if (!(File.Exists("SaveData\\profileimage.png")))
            {
                File.WriteAllBytes("SaveData\\profileimage.png", new WebClient().DownloadData("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/profileimage.png"));
            }
            if (!(File.Exists("SaveData\\App\\privaterooms.txt")))
            {
                File.WriteAllText("SaveData\\App\\privaterooms.txt", "Disabled");
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
            Loading.StopLoading();
            Console.WriteLine("Done!");
            Console.Clear();
        }

        public static void setup_profile()
        {
            if (!(File.Exists("SaveData\\Profile\\player.json")))
            {
                string Username = "Rec_rewild User#" + new Random().Next(0, 1000000);
                player_config._setting = new player_config.playerSetting
                {
                    AccountId = new Random().Next(100000, 999999999),
                    Username = Username,
                    DisplayName = Username,
                    Bio = "Welcome to Rec_rewild! This is a custom server for Rec Room. Enjoy your stay!",
                    Level = 30,
                    XP = 0,
                    Email = Username + "@recrewild.server",
                    Balances = new player_config.Balances
                    {
                        Tokens = 1500,
                        Tickets = 0,
                        Gold = 0,
                        Silver = 0,
                        RecRoyale_Season1 = 0,
                        RoomCurrency = new List<player_config.RoomCurrency>
                        {
                            new player_config.RoomCurrency
                            {
                                room_id = 0,
                                Currency = new List<player_config.CurrencyItem>
                                {
                                    new player_config.CurrencyItem { id = "", Balance = 0 }
                                }
                            }
                        }
                    },
                    Roomkeys = new List<player_config.RoomKey>
                    {
                        new player_config.RoomKey { id = "", have_key = false }
                    },
                    Reputation = new api.AccountAuth.Rep
                    {
                        IsCheerful = false,
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
                    },
                    CreatedAt = DateTime.UtcNow,
                    Birthday = DateTime.UtcNow,
                    ProfileImage = "Default.png",
                    BannerImage = "Default.png",
                    IsJunior = false,
                    IsDeveloper = true,
                    LastLogin = null,
                    recent_rooms = new List<int>(),
                    cheered_rooms = new List<int>(),
                    favorited_rooms = new List<int>()
                };
                player_config.save_setting();
            }
        }

        public static void setup_server()
        {
            if (!(File.Exists("SaveData\\App\\server_setting.json")))
            {
                server_config._setting = new server_config.App_Setting
                {
                    ConsoleSound = false
                };
                server_config.save_setting();
            }
        }
    }
}
