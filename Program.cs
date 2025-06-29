using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;
using server;
using api;
using System.Threading;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.AccessControl;
using System.Net.Http;
using util;
using Rec_rewild.servers.route_new;


namespace start
{
    class Program
    {
        static void Main()
        {
            if (File.Exists("SaveData\\App\\firsttime.txt"))
            {
                Setup.quicksetup();
                goto Start;
            }
            Setup.setup();
            goto Tutorial;

        Tutorial:
            if (Setup.firsttime == true)
              
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Title = "Rec_rewild Intro";
                Console.WriteLine("Welcome to Rec_rewild!");
                Console.WriteLine("Is this your first time using Rec_rewild?");
                Console.WriteLine("Yes or No (Y, N)");
                string readline22 = Console.ReadLine();
                if (readline22 == "y" || readline22 == "Y")
                {
                    Console.Clear();
                    Console.Title = "Rec_rewild Tutorial";
                    Console.WriteLine("Welcome to Rec_rewild!");
                    Console.WriteLine("Rec_rewild is localhost server software that emulates RecNet for previous RecRoom versions.");
                    Console.WriteLine("It emulates servers for Rec Room versions from 2020 to 2021.");
                    Console.WriteLine("To use Rec_rewild, you'll need to have builds aswell!");
                    Console.WriteLine("Do you want to import your Rec Net profile?\nYes or No (Y, N)");
                    string mode = Console.ReadLine();
                    if (mode.ToLower() == "y")
                    {
                        download_profile_setup:
                        Console.Title = "Rec_rewild Profile Downloader";
                        Console.Clear();
                        Console.WriteLine("Profile Downloader: This tool takes the username and profile image of any username you type in and imports it to Rec_rewild.");
                        Console.WriteLine("Please type the username of the profile you would like: ");
                        string readusername_setup = Console.ReadLine();
                        string data2_setup = "";
                        try
                        {
                            data2_setup = new WebClient().DownloadString("https://apim.rec.net/accounts/account/search?name=" + readusername_setup + "&take=5");
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("Failed to download profile...");
                            api.DebugLog.Log("FAILED TO DOWNLOAD PROFILE!", api.DebugLog.LogLevel.Error);
                            goto rec_net_profile_notimported;
                        }

                        if (!ProfileDownloader.FindProfile(data2_setup, take_int: 12))
                        {
                            goto download_profile_setup;
                        }
                        goto rec_net_profile_imported;
                    }
                rec_net_profile_notimported:
                    Console.WriteLine("Please enter the username you would like to use:");
                    string newusername = Console.ReadLine();
                    File.WriteAllText("SaveData\\Profile\\username.txt", newusername);
                    File.WriteAllText("SaveData\\Profile\\displayName.txt", newusername);
                rec_net_profile_imported:
                    Console.WriteLine("To download builds, either go to the #rec-room-builds channel or use the links below: (these links are also available from the #rec-room-builds channel)" + Environment.NewLine);
                    Console.WriteLine(new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/builds.txt"));
                    Console.WriteLine("Download a build and press any key to continue:");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Now that you have a build, what you're going to do is as follows:" + Environment.NewLine);
                    Console.WriteLine("1. Unzip the build");
                    Console.WriteLine("2. Start the server by pressing 5 on the main menu");
                    Console.WriteLine("3. Run Recroom.exe from the folder of the build you downloaded." + Environment.NewLine);
                    Console.WriteLine("And that's it! Press any key to go to the main menu, where you will be able to start the server:");
                    Console.ReadKey();
                    Console.Clear();
                    goto Start;
                }
                else
                {
                    Console.Clear();
                    goto Start;
                }
            }
            else
            {
                goto Start;
            }
        Start:
            Console.Title = "Rec_rewild Startup Menu";
            var client = new HttpClient();
            appversion = appversion.Replace("\n", String.Empty);
            appversion = appversion.Replace("\r", String.Empty);
            appversion = appversion.Replace("\t", String.Empty);
            Console.WriteLine("Rec_rewild - a fork of OpenRec for Rec Room 2021. (Version: " + appversion + ")");
            Console.WriteLine("Branch: server-rewrite-v2");
            Console.WriteLine("Download source code here: https://github.com/wiiboi69/Rec_rewild");
            Console.WriteLine("Discord server here: https://discord.gg/recrewild");
            Console.WriteLine("This is a full server rewrite version: v2" + Environment.NewLine);
            Console.WriteLine("(1) What's New"
                + Environment.NewLine
                + "(2) Change Settings"
                + Environment.NewLine
                + "(3) Modify Profile"
                + Environment.NewLine
                + "(4) Build Download Links"
                + Environment.NewLine
                + "(5) Start 2021 Server");

            string readline = Console.ReadLine();
            if (!int.TryParse(readline, out int choice))
            {
                Console.WriteLine("only enter numbers");
                Console.Clear();
                goto Start;
            }
            if (!int.TryParse(readline, out int choice1) || choice < 1 || choice > 5)
            {
                Console.WriteLine("invalid");
                Console.Clear();
                goto Start; 
            }
            if (readline == "1")
            {
                Console.Title = "Rec_rewild Changelog";
                Console.Clear();
                Console.WriteLine(client.GetStringAsync("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/changelog.txt").Result);
                Console.WriteLine("Press any key to continue:");
                Console.ReadKey();
                Console.Clear();
                goto Start;
            }
            if (readline == "2")
            {
                Console.Clear();
                Settings:
                Console.Title = "Rec_rewild Settings Menu";
                Console.WriteLine("(1) Private Rooms: " + File.ReadAllText("SaveData\\App\\privaterooms.txt") + Environment.NewLine + "(2) Custom Room Downloader" + Environment.NewLine + "(3) Delete All SaveData" + Environment.NewLine + "(4) Update SaveData" +  Environment.NewLine + "(5) Go Back");
                string readline4 = Console.ReadLine();
                if (readline4 == "1")
                {
                    if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Disabled")
                    {
                        File.WriteAllText("SaveData\\App\\privaterooms.txt", "Enabled");
                    }
                    else
                    {
                        File.WriteAllText("SaveData\\App\\privaterooms.txt", "Disabled");
                    }
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Settings;
                }
                else if (readline4 == "2")
                {
                    Console.Clear();
                download_Room:
                    Console.Title = "Rec_rewild room Downloader";
                    Console.Clear();
                    Console.WriteLine("room Downloader: This tool takes the room and the data of any name you type in and download and import it to Rec_rewild.");
                    Console.WriteLine("Please type the roomname of the room you would like to download: ");
                    string readusername_setup = Console.ReadLine();
                    string data2_setup = "";
                    try
                    {
                        data2_setup = new WebClient().DownloadString("https://rooms.rec.net/rooms/search?query=" + readusername_setup);
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Failed to download room...");
                        goto Settings;
                    }
                    if (!roomdownloader.room_find(data2_setup, take_int: 12))
                    {
                        goto download_Room;
                    }
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Settings;
                }
                else if (readline4 == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Are you sure you want to delete all your SaveData? (Y, N)");
                    string readlinee = Console.ReadLine();
                    if (readlinee == "y" || readlinee == "Y")
                    {
                        Directory.Delete("SaveData", true);
                        Console.Clear();
                        Console.WriteLine("Success!");
                        Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                        Environment.Exit(0);
                        // goto Tutorial; // not work
                    }
                    else
                    {
                        Console.Clear();
                        goto Settings;
                    }
                }
                else if (readline4 == "4")
                {
                    Console.Clear();
                    File.WriteAllText("SaveData\\avataritems.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/avataritems.txt"));
                    File.WriteAllText("SaveData\\avataritems2.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/avataritems2.txt"));
                    Console.WriteLine("Downloaded avatar items");
                    File.WriteAllText("SaveData\\equipment.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/equipment.txt"));
                    Console.WriteLine("Downloaded equipment");
                    File.WriteAllText("SaveData\\consumables.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/consumables.txt"));
                    Console.WriteLine("Downloaded fresh consumables");
                    File.WriteAllText("SaveData\\gameconfigs.txt", new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/gameconfigs.txt"));
                    Console.WriteLine("Downloaded game configs");
                    Console.WriteLine("Updated successfully");
                    Thread.Sleep(400); 
                    goto Settings;
                }
                else if (readline4 == "5")
                {
                    Console.Clear();
                    goto Start;
                }
            }
            if (readline == "3")
            {
                Console.Clear();
            Profile:
                Console.WriteLine("Quick Tip: You can change them in-game." + Environment.NewLine);
                Console.Title = "Rec_rewild Profile Menu";
                Console.WriteLine(
                      "(1) Change Username      " + File.ReadAllText("SaveData\\Profile\\username.txt")
                    + Environment.NewLine 
                    + "(2) Change Display Name   " + File.ReadAllText("SaveData\\Profile\\displayName.txt")
                    + Environment.NewLine 
                    + "(3) Change Profile Image " 
                    + Environment.NewLine 
                    + "(4) Change Level         " + File.ReadAllText("SaveData\\Profile\\level.txt")
                    + Environment.NewLine
                    + "(5) Change Tokens        " + File.ReadAllText("SaveData\\Profile\\tokens.txt")
                    + Environment.NewLine 
                    + "(6) Change Bio           " + File.ReadAllText("SaveData\\Profile\\bio.txt")
                    + Environment.NewLine 
                    + "(7) Profile Downloader" 
                    + Environment.NewLine 
                    + "(8) Go Back");
                string readline3 = Console.ReadLine();
                if (readline3 == "1")
                {
                    Console.WriteLine("Current Username: " + File.ReadAllText("SaveData\\Profile\\username.txt"));
                    Console.WriteLine("New Username: ");
                    string newusername = Console.ReadLine();
                    File.WriteAllText("SaveData\\Profile\\username.txt", newusername);
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Profile;
                }
                else if (readline3 == "2")
                {
                    Console.WriteLine("Current displayName: " + File.ReadAllText("SaveData\\Profile\\displayName.txt"));
                    Console.WriteLine("New displayName: ");
                    string newdisplayName = Console.ReadLine();
                    File.WriteAllText("SaveData\\Profile\\displayName.txt", newdisplayName);
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Profile;
                }
                else if (readline3 == "3")
                {
                    Console.Clear();
                    Console.WriteLine("1) Upload Media Link" + Environment.NewLine + "2) Drag Image onto this window" + Environment.NewLine + "3) Download Rec.Net Profile Image" + Environment.NewLine + "4) Go Back");
                    string readline4 = Console.ReadLine();
                    if (readline4 == "1")
                    {
                        Console.WriteLine("Paste Media Link: ");
                        string medialink = Console.ReadLine();
                        try
                        {
                            File.WriteAllBytes("SaveData\\profileimage.png", new WebClient().DownloadData(medialink)); // web client is obsolete, well guess what i dont care!
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid Media Link");
                            goto Profile;
                        }
                        Console.Clear();
                        Console.WriteLine("Success!");
                        goto Profile;
                    }
                    else if (readline4 == "2")
                    {
                        Console.WriteLine("Drag any image onto this window and press enter: ");
                        string imagedir = Console.ReadLine();
                        try
                        {
                            byte[] imagefile = File.ReadAllBytes(imagedir);
                            File.Replace(imagedir, "SaveData\\profileimage.png", "backupfilename.png");
                            File.WriteAllBytes(imagedir, imagefile);
                            File.Delete("backupfilename.png");
                        }
                        catch (Exception ex4)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid Image (Make sure its on the same drive as Rec_rewild)");
                            goto Profile;
                        }
                        Console.Clear();
                        Console.WriteLine("Success!");
                        goto Profile;
                    }
                    else if (readline4 == "3")
                    {
                        
                        Console.WriteLine("Type a RecRoom @ username and press enter: ");
                        string username = Console.ReadLine();
                        if (username.StartsWith("@"))
                        {
                            username = username.Remove(0, 1);
                        }
                        try
                        {
                            string data = "";
                            try
                            {
                                data = new WebClient().DownloadString("https://apim.rec.net/accounts/account/search?name=" + username);
                            }
                            catch
                            {
                                Console.Clear();
                                Console.WriteLine("Failed to download profile...");
                                goto Start;
                            }

                            List<ProfileDownloader.Root> profile = JsonConvert.DeserializeObject<List<ProfileDownloader.Root>>(data);
                            byte[] profileimage = new WebClient().DownloadData("https://img.rec.net/" + profile[0].profileImage + "?cropSquare=true&width=192&height=192");
                            File.WriteAllBytes("SaveData\\profileimage.png", profileimage);

                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("Unable to download image...");
                            goto Profile;
                        }
                        Console.Clear();
                        Console.WriteLine("Success!");

                        goto Profile;
                    }
                    else if (readline4 == "4")
                    {
                        Console.Clear();
                        goto Start;
                    }
                }
                else if (readline3 == "4")
                {
                    Console.WriteLine("Current Level: " + File.ReadAllText("SaveData\\Profile\\level.txt"));
                    Console.WriteLine("New Level: ");
                    string newlevel = Console.ReadLine();
                    File.WriteAllText("SaveData\\Profile\\level.txt", newlevel);
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Profile;
                }
                else if (readline3 == "5")
                {
                    Console.WriteLine("Current tokens: " + File.ReadAllText("SaveData\\Profile\\tokens.txt"));
                    Console.WriteLine("New Tokens: ");
                    string newtokens = Console.ReadLine();
                    File.WriteAllText("SaveData\\Profile\\tokens.txt", newtokens);
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Profile;
                }
                else if (readline3 == "6")
                {
                    Console.WriteLine("Current bio: " + File.ReadAllText("SaveData\\Profile\\bio.txt"));
                    Console.WriteLine("New bio: ");
                    string newlevel = Console.ReadLine();
                    File.WriteAllText("SaveData\\Profile\\bio.txt", newlevel);
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Profile;
                }
                else if (readline3 == "7")
                {
                    download_profile:
                    Console.Title = "Rec_rewild Profile Downloader";
                    Console.Clear();
                    Console.WriteLine("Profile Downloader: This tool takes the username and profile image of any username you type in and imports it to Rec_rewild.");
                    Console.WriteLine("Please type the username of the profile you would like: ");
                    string readusername = Console.ReadLine();
                    string data2 = "";
                    try
                    {
                        data2 = new WebClient().DownloadString("https://apim.rec.net/accounts/account/search?name=" + readusername + "&take=5");
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Failed to download profile...");
                        goto Profile;
                    }
                    
                    if (!ProfileDownloader.FindProfile(data2, take_int: 12))
                    {
                        goto download_profile;
                    }
                    
                    Console.Clear();
                    goto Profile;
                }
                else if (readline3 == "8")
                {
                    Console.Clear();
                    goto Start;
                }
            }
            if (readline == "4")
            {
                Console.Title = "Rec_rewild Build Downloads";
                Console.Clear();
                Console.WriteLine("To download builds, use the links below: " + Environment.NewLine);
                Console.WriteLine(client.GetStringAsync("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/builds.txt").Result);
                Console.WriteLine("Download a build and press any key to continue:");
                Console.ReadKey();
                Console.Clear();
                goto Start;
            }
            if (readline == "5")
            {
                Console.Title = "starting server";
                Console.WriteLine("Please wait for server to start up");
                version = "2021";

                APIServer.Cachedservertimestarted = (ulong)DateTime.Now.Ticks;

                beta = false;

                //ConsoleEMU.OpenNewConsole();

                //note: nameserver is at the same port as before

                /*
                new NameServer();
                new APIServer();
                new AuthServer();
                new ImageServer();
                new matchServer();
                new NotificationsServer();
                //new WebSocketHTTP();
                //new WebSocketHTTP_New_test();
                new WebSocketHTTP_new();
                new roomServer();   
                */
                new APIServer2021_new();
                new AuthServer2021_new();
                new WebSocketHTTP_new();
                new RoomServer2021_new();

                Console.Title = "Rec_rewild server started!";
                Console.WriteLine(msg);

                var input = Console.ReadLine();
                for (; ; )
                {
                    if (input == "!mod")
                    {
                        Console.WriteLine();
                        Console.WriteLine("sending test websocket data");
                        WebSocketHTTP_new.SendRequest(JsonConvert.SerializeObject(ProgramHelpers.createResponse()));
                        Console.WriteLine();
                        goto input_server;
                    }
                    else if (input == "!mod_box")
                    {
                        Console.WriteLine();
                        Console.WriteLine("sending test websocket data");
                        WebSocketHTTP_new.SendRequest(JsonConvert.SerializeObject(ProgramHelpers.createResponse_box()));
                        Console.WriteLine();
                        goto input_server;
                    }
                    else if (input == "!mod_msg")
                    {
                        Console.WriteLine();
                        Console.WriteLine("sending test websocket data");
                        WebSocketHTTP_new.SendRequest(JsonConvert.SerializeObject(ProgramHelpers.createResponse_msg()));
                        Console.WriteLine();
                        goto input_server;
                    }
                    else if (input == "!mod_time")
                    {
                        Console.WriteLine();
                        Console.WriteLine("sending test websocket data");
                        WebSocketHTTP_new.SendRequest(JsonConvert.SerializeObject(ProgramHelpers.CreateServerMaintence()));
                        Console.WriteLine();
                        goto input_server;
                    }
                    else if (input == "!mod_give")
                    {
                        Console.WriteLine();
                        Console.WriteLine("sending test websocket data");
                        WebSocketHTTP_new.SendRequest(JsonConvert.SerializeObject(ProgramHelpers.createResponse_give()));
                        Console.WriteLine();
                        goto input_server;
                    }
                    else if (input == "!mod_online")
                    {
                        Console.WriteLine();
                        Console.WriteLine("sending test websocket data");
                        WebSocketHTTP_new.SendRequest(JsonConvert.SerializeObject(ProgramHelpers.createResponse_FriendStatusOnline()));
                        Console.WriteLine();
                        goto input_server;
                    }
                    else if (input == "!mod_accept")
                    {
                        Console.WriteLine();
                        Console.WriteLine("sending test websocket data");
                        WebSocketHTTP_new.SendRequest(JsonConvert.SerializeObject(ProgramHelpers.createResponse_FriendRequestAccepted()));
                        Console.WriteLine();
                        goto input_server;
                    }
                    else if (input == "!exit")
                    {
                        Console.WriteLine();
                        Console.WriteLine("closing the server");
                        Console.WriteLine();
                        Environment.Exit(0);
                        goto input_server;
                    }
                    input_server:
                    input = Console.ReadLine();
                }
            }

        }
        public static string msg = "//This is the server sending and recieving data from recroom." + Environment.NewLine + "//Ignore this if you don't know what this means." + Environment.NewLine + "//Please start up the build now.";
        public static string version = "";
        public static bool beta = false;
        public static bool dev_log = false;
        public static string appversion = "0.0.18"; //new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/version.txt");
        public static string DataPath = Environment.CurrentDirectory + "\\SaveData";
        public static string ProfilePath = Program.DataPath + "\\Profile";
        public static string CustomImages = Program.DataPath + "\\Images";

        public class Reponse<T>
        {
            public T Id { get; set; }

            public object Msg { get; set; }
        }

        public enum AvatarItemType
        {
            Outfit,
            HairDye
        }

        public enum CurrencyType
        {
            Invalid,
            LaserTagTickets,
            RecCenterTokens,
            LostSkullsGold = 100,
            DraculaSilver,
            RecRoyale_Season1 = 200
        }




        public class GiftPackage
        {
            public ulong Id { get; set; }
            public ulong PlayerId { get; set; }
            public ulong FromPlayerId { get; set; }
            public string ConsumableItemDesc { get; set; }
            public ulong ConsumableCount { get; set; }
            public string AvatarItemDesc { get; set; } = "";
            public AvatarItemType? AvatarItemType { get; set; }
            public CurrencyType CurrencyType { get; set; }
            public int Currency { get; set; }
            public int Xp { get; set; }
            public int Level { get; set; }
            public int PackageType { get; set; } //TODO: get enum for it
            public string Message { get; set; }
            public string EquipmentPrefabName { get; set; } 
            public string EquipmentModificationGuid { get; set; }
            public GiftContext GiftContext { get; set; } = GiftContext.Weekly_Challenge_Complete;
            public GiftRarity GiftRarity { get; set; }
            public PlatformType Platform { get; set; } = PlatformType.All;
            public PlatformType platformsToSpawnOn { get; set; } = PlatformType.All;
            public int? balanceType { get; set; }
            public DateTime? createdAt { get; set; }
        }

        public class ChallengeGift
        {
            public long Id { get; set; }
            public string ConsumableItemDesc { get; set; }
            public string AvatarItemDesc { get; set; }
            public AvatarItemType? AvatarItemType { get; set; }
            public string EquipmentPrefabName { get; set; }
            public string EquipmentModificationGuid { get; set; }
            public StorefrontTypes StorefrontType { get; set; }
            public int Xp { get; set; }
            public int Level { get; set; }
            public GiftContext GiftContext { get; set; }
            public GiftRarity GiftRarity { get; set; }
        }


        public enum PlatformType
        {
            All = -1,
            Steam,
            Oculus,
            PlayStation,
            Microsoft,
            HeadlessBot,
            IOS
        }

        public enum GiftBoxContents
        {
            Unspecified = -1,
            XP,
            OutfitItem,
            Equipment,
            Currency,
            Consumable,
            Query,
            HairDye
        }

        public enum GiftRarity
        {
            None = -1,
            Common,
            Uncommon = 10,
            Rare = 20,
            Epic = 30,
            Legendary = 50
        }

        public enum GiftContext
        {
            None = -1,
            // Token: 0x0400CF97 RID: 53143
            Default,
            // Token: 0x0400CF98 RID: 53144
            First_Activity,
            // Token: 0x0400CF99 RID: 53145
            Game_Drop,
            // Token: 0x0400CF9A RID: 53146
            All_Daily_Challenges_Complete,
            // Token: 0x0400CF9B RID: 53147
            All_Weekly_Challenge_Complete,
            // Token: 0x0400CF9C RID: 53148
            Daily_Challenge_Complete,
            // Token: 0x0400CF9D RID: 53149
            Weekly_Challenge_Complete,
            // Token: 0x0400CF9E RID: 53150
            Unassigned_Equipment = 10,
            // Token: 0x0400CF9F RID: 53151
            Unassigned_Avatar,
            // Token: 0x0400CFA0 RID: 53152
            Unassigned_Consumable,
            // Token: 0x0400CFA1 RID: 53153
            Reacquisition = 20,
            // Token: 0x0400CFA2 RID: 53154
            Membership,
            // Token: 0x0400CFA3 RID: 53155
            NUX_TokensAndDressUp = 30,
            // Token: 0x0400CFA4 RID: 53156
            NUX_Experiment1,
            // Token: 0x0400CFA5 RID: 53157
            NUX_Experiment2,
            // Token: 0x0400CFA6 RID: 53158
            NUX_Experiment3,
            // Token: 0x0400CFA7 RID: 53159
            NUX_Experiment4,
            // Token: 0x0400CFA8 RID: 53160
            NUX_Experiment5,
            // Token: 0x0400CFA9 RID: 53161
            GameRewards = 50,
            // Token: 0x0400CFAA RID: 53162
            GameRewards_Tokens,
            // Token: 0x0400CFAB RID: 53163
            LevelUp = 100,
            // Token: 0x0400CFAC RID: 53164
            Purchased_Gift_A = 500,
            // Token: 0x0400CFAD RID: 53165
            Purchased_Gift_B,
            // Token: 0x0400CFAE RID: 53166
            Purchased_Gift_C,
            // Token: 0x0400CFAF RID: 53167
            Purchased_Gift_D,
            // Token: 0x0400CFB0 RID: 53168
            Holiday = 1000,
            // Token: 0x0400CFB1 RID: 53169
            Contest,
            // Token: 0x0400CFB2 RID: 53170
            Promotion,
            // Token: 0x0400CFB3 RID: 53171
            SubscribersOnly,
            // Token: 0x0400CFB4 RID: 53172
            Deprecated = 1100,
            // Token: 0x0400CFB5 RID: 53173
            RecRoyale = 1200,
            // Token: 0x0400CFB6 RID: 53174
            DEPRECATED_Paintball_ClearCut = 2000,
            // Token: 0x0400CFB7 RID: 53175
            DEPRECATED_Paintball_Homestead,
            // Token: 0x0400CFB8 RID: 53176
            DEPRECATED_Paintball_Quarry,
            // Token: 0x0400CFB9 RID: 53177
            DEPRECATED_Paintball_River,
            // Token: 0x0400CFBA RID: 53178
            DEPRECATED_Paintball_Dam,
            // Token: 0x0400CFBB RID: 53179
            DEPRECATED_Paintball_DriveIn,
            // Token: 0x0400CFBC RID: 53180
            Paintball_ClearCut = 2010,
            // Token: 0x0400CFBD RID: 53181
            Paintball_Homestead,
            // Token: 0x0400CFBE RID: 53182
            Paintball_Quarry,
            // Token: 0x0400CFBF RID: 53183
            Paintball_River,
            // Token: 0x0400CFC0 RID: 53184
            Paintball_Dam,
            // Token: 0x0400CFC1 RID: 53185
            Paintball_DriveIn,
            // Token: 0x0400CFC2 RID: 53186
            DEPRECATED_Discgolf_Propulsion = 3000,
            // Token: 0x0400CFC3 RID: 53187
            DEPRECATED_Discgolf_Lake,
            // Token: 0x0400CFC4 RID: 53188
            Discgolf_Propulsion = 3010,
            // Token: 0x0400CFC5 RID: 53189
            Discgolf_Lake,
            // Token: 0x0400CFC6 RID: 53190
            Discgolf_Mode_CoopCatch = 3500,
            // Token: 0x0400CFC7 RID: 53191
            Quest_Goblin_A = 4000,
            // Token: 0x0400CFC8 RID: 53192
            Quest_Goblin_B,
            // Token: 0x0400CFC9 RID: 53193
            Quest_Goblin_C,
            // Token: 0x0400CFCA RID: 53194
            Quest_Goblin_S,
            // Token: 0x0400CFCB RID: 53195
            Quest_Goblin_Consumable,
            // Token: 0x0400CFCC RID: 53196
            Quest_Cauldron_A = 4010,
            // Token: 0x0400CFCD RID: 53197
            Quest_Cauldron_B,
            // Token: 0x0400CFCE RID: 53198
            Quest_Cauldron_C,
            // Token: 0x0400CFCF RID: 53199
            Quest_Cauldron_S,
            // Token: 0x0400CFD0 RID: 53200
            Quest_Cauldron_Consumable,
            // Token: 0x0400CFD1 RID: 53201
            Quest_Pirate1_A = 4100,
            // Token: 0x0400CFD2 RID: 53202
            Quest_Pirate1_B,
            // Token: 0x0400CFD3 RID: 53203
            Quest_Pirate1_C,
            // Token: 0x0400CFD4 RID: 53204
            Quest_Pirate1_S,
            // Token: 0x0400CFD5 RID: 53205
            Quest_Pirate1_X,
            // Token: 0x0400CFD6 RID: 53206
            Quest_Pirate1_Consumable,
            // Token: 0x0400CFD7 RID: 53207
            Quest_Dracula1_A = 4200,
            // Token: 0x0400CFD8 RID: 53208
            Quest_Dracula1_B,
            // Token: 0x0400CFD9 RID: 53209
            Quest_Dracula1_C,
            // Token: 0x0400CFDA RID: 53210
            Quest_Dracula1_S,
            // Token: 0x0400CFDB RID: 53211
            Quest_Dracula1_X,
            // Token: 0x0400CFDC RID: 53212
            Quest_Dracula1_Consumable,
            // Token: 0x0400CFDD RID: 53213
            Quest_Dracula1_SS,
            // Token: 0x0400CFDE RID: 53214
            Quest_SciFi_A = 4500,
            // Token: 0x0400CFDF RID: 53215
            Quest_SciFi_B,
            // Token: 0x0400CFE0 RID: 53216
            Quest_SciFi_C,
            // Token: 0x0400CFE1 RID: 53217
            Quest_SciFi_S,
            // Token: 0x0400CFE2 RID: 53218
            Quest_Scifi_Consumable,
            // Token: 0x0400CFE3 RID: 53219
            DEPRECATED_Charades = 5000,
            // Token: 0x0400CFE4 RID: 53220
            Charades,
            // Token: 0x0400CFE5 RID: 53221
            DEPRECATED_Soccer = 6000,
            // Token: 0x0400CFE6 RID: 53222
            Soccer,
            // Token: 0x0400CFE7 RID: 53223
            DEPRECATED_Paddleball = 7000,
            // Token: 0x0400CFE8 RID: 53224
            Paddleball,
            // Token: 0x0400CFE9 RID: 53225
            DEPRECATED_Dodgeball = 8000,
            // Token: 0x0400CFEA RID: 53226
            Dodgeball,
            // Token: 0x0400CFEB RID: 53227
            DEPRECATED_Lasertag = 9000,
            // Token: 0x0400CFEC RID: 53228
            Lasertag,
            // Token: 0x0400CFED RID: 53229
            DEPRECATED_Bowling = 10000,
            // Token: 0x0400CFEE RID: 53230
            Bowling,
            // Token: 0x0400CFEF RID: 53231
            StuntRunner_TheMainEvent_A = 11000,
            // Token: 0x0400CFF0 RID: 53232
            StuntRunner_TheMainEvent_B,
            // Token: 0x0400CFF1 RID: 53233
            StuntRunner_TheMainEvent_C,
            // Token: 0x0400CFF2 RID: 53234
            StuntRunner_TheMainEvent_D,
            // Token: 0x0400CFF3 RID: 53235
            StuntRunner_TheMainEvent_S,
            // Token: 0x0400CFF4 RID: 53236
            StuntRunner_TheMainEvent_X,
            // Token: 0x0400CFF5 RID: 53237
            StuntRunner_TheMainEvent_Consumable,
            // Token: 0x0400CFF6 RID: 53238
            StuntRunner_TheMainEvent_SS,
            // Token: 0x0400CFF7 RID: 53239
            Store_LaserTag = 100000,
            // Token: 0x0400CFF8 RID: 53240
            Store_RecCenter = 100010,
            // Token: 0x0400CFF9 RID: 53241
            Consumable = 110000,
            // Token: 0x0400CFFA RID: 53242
            Token = 110100,
            // Token: 0x0400CFFB RID: 53243
            Punchcard_Challenge_Complete = 110200,
            // Token: 0x0400CFFC RID: 53244
            All_Punchcard_Challenges_Complete,
            // Token: 0x0400CFFD RID: 53245
            Commerce_Purchase = 200000
        }

        public enum StorefrontTypes
        {
            // Token: 0x0400CF39 RID: 53049
            None,
            // Token: 0x0400CF3A RID: 53050
            LaserTag,
            // Token: 0x0400CF3B RID: 53051
            RecCenter,
            // Token: 0x0400CF3C RID: 53052
            Watch,
            // Token: 0x0400CF3D RID: 53053
            Quest_LostSkulls = 100,
            // Token: 0x0400CF3E RID: 53054
            Quest_Dracula,
            // Token: 0x0400CF3F RID: 53055
            Quest_GoldenTrophy,
            // Token: 0x0400CF40 RID: 53056
            Quest_CrimsonCauldron,
            // Token: 0x0400CF41 RID: 53057
            RecRoyale = 200,
            // Token: 0x0400CF42 RID: 53058
            Cafe = 300,
            // Token: 0x0400CF43 RID: 53059
            Paintball = 400,
            // Token: 0x0400CF44 RID: 53060
            Paintball_River,
            // Token: 0x0400CF45 RID: 53061
            Paintball_Homestead,
            // Token: 0x0400CF46 RID: 53062
            Paintball_Quarry,
            // Token: 0x0400CF47 RID: 53063
            Paintball_ClearCut,
            // Token: 0x0400CF48 RID: 53064
            Paintball_Spillway,
            // Token: 0x0400CF49 RID: 53065
            Paintball_SunsetDriveIn,
            // Token: 0x0400CF4A RID: 53066
            Bowling = 500,
            // Token: 0x0400CF4B RID: 53067
            StuntRunner = 600,
            // Token: 0x0400CF4C RID: 53068
            DormMirror = 700,
            // Token: 0x0400CF4D RID: 53069
            InventionStore = 800,
            // Token: 0x0400CF4E RID: 53070
            RoomKeys = 900,
            // Token: 0x0400CF4F RID: 53071
            Player_Profile = 1000,
            // Token: 0x0400CF50 RID: 53072
            Room_Save = 1100,
            // Token: 0x0400CF51 RID: 53073
            RoomCurrency = 1200,
            // Token: 0x0400CF52 RID: 53074
            Wishlist = 1300
        }
        public class ModerationBlockDetails
        {
            public int ReportCategory { get; set; }
            public int Duration { get; set; }
            public bool IsHostKick { get; set; }
            public long GameSessionId { get; set; }
            public string Message { get; set; }
        }
        public class baseDetail
        {
            public string Message { get; set; }
        }

        public class StartsInMinutes_Detail
        {
            public int StartsInMinutes { get; set; }
        }

        public class Message_Detail
        {
            public int Id { get; set; }
            public int FromPlayerId { get; set; }
            public DateTime SentTime { get; set; }

            public MessageType Type { get; set; }
            public string Data { get; set; }
            public int RoomId { get; set; }
            public int PlayerEventId { get; set; }

            public object Details { get; set; }
            public DateTime DeserializedAt { get; set; }
        }

        public enum MessageType
        {
            // Token: 0x040089EA RID: 35306
            GameInvite,
            // Token: 0x040089EB RID: 35307
            GameInviteDeclined,
            // Token: 0x040089EC RID: 35308
            GameJoinFailed,
            // Token: 0x040089ED RID: 35309
            PartyActivitySwitch,
            // Token: 0x040089EE RID: 35310
            FriendInvite,
            // Token: 0x040089EF RID: 35311
            VoteToKick,
            // Token: 0x040089F0 RID: 35312
            GameInviteV2,
            // Token: 0x040089F1 RID: 35313
            PartyActivitySwitchV2,
            // Token: 0x040089F2 RID: 35314
            RequestGameInvite = 10,
            // Token: 0x040089F3 RID: 35315
            RequestGameInviteDeclined,
            // Token: 0x040089F4 RID: 35316
            FriendStatusOnline = 20,
            // Token: 0x040089F5 RID: 35317
            TextMessage = 30,
            // Token: 0x040089F6 RID: 35318
            FriendRequestAccepted = 40,
            // Token: 0x040089F7 RID: 35319
            PlayerCheer = 50,
            // Token: 0x040089F8 RID: 35320
            PlayerCheerAnonymous,
            // Token: 0x040089F9 RID: 35321
            RoomCoOwnerAdded = 60,
            // Token: 0x040089FA RID: 35322
            RoomCoOwnerRemoved,
            // Token: 0x040089FB RID: 35323
            RoomCoOwnerInvited,
            // Token: 0x040089FC RID: 35324
            CreatorPublishedNewRoom = 70,
            // Token: 0x040089FD RID: 35325
            PlayerAttendingEvent = 80,
            // Token: 0x040089FE RID: 35326
            PlayerEventInvitation,
            // Token: 0x040089FF RID: 35327
            GroupInvitation = 90,
            // Token: 0x04008A00 RID: 35328
            PlayerJoinedGroup,
            // Token: 0x04008A01 RID: 35329
            CoachMessage = 100
        }

        public class Relationship_Detail
        {
            public int PlayerID { get; set; }
            public RelationshipType Type { get; set; }
            public ReciprocalStatus Muted { get; set; }
            public ReciprocalStatus Ignored { get; set; }
            public ReciprocalStatus Favorited { get; set; }
        }

        public enum VotekickTypes
        {
            // Token: 0x0400CC67 RID: 52327
            Moderator = -1,
            // Token: 0x0400CC68 RID: 52328
            Unknown,
            // Token: 0x0400CC69 RID: 52329
            DEPRECATED_MicrophoneAbuse,
            // Token: 0x0400CC6A RID: 52330
            Harassment,
            // Token: 0x0400CC6B RID: 52331
            Cheating,
            // Token: 0x0400CC6C RID: 52332
            DEPRECATED_ImmatureBehavior,
            // Token: 0x0400CC6D RID: 52333
            AFK,
            // Token: 0x0400CC6E RID: 52334
            Misc,
            // Token: 0x0400CC6F RID: 52335
            Underage,
            // Token: 0x0400CC70 RID: 52336
            VoteKick = 10,
            // Token: 0x0400CC71 RID: 52337
            MisleadingPurchases,
            // Token: 0x0400CC72 RID: 52338
            CoC_Underage = 100,
            // Token: 0x0400CC73 RID: 52339
            CoC_Sexual,
            // Token: 0x0400CC74 RID: 52340
            CoC_Discrimination,
            // Token: 0x0400CC75 RID: 52341
            CoC_Trolling,
            // Token: 0x0400CC76 RID: 52342
            CoC_NameOrProfile,
            // Token: 0x0400CC77 RID: 52343
            IssuingInaccurateReports = 1000
        }

        public enum RelationshipType
        {
            // Token: 0x04008D54 RID: 36180
            None,
            // Token: 0x04008D55 RID: 36181
            FriendRequestSent,
            // Token: 0x04008D56 RID: 36182
            FriendRequestReceived,
            // Token: 0x04008D57 RID: 36183
            Friend
        }

        // Token: 0x02000CE6 RID: 3302
        public enum ReciprocalStatus
        {
            // Token: 0x04008D59 RID: 36185
            None,
            // Token: 0x04008D5A RID: 36186
            Local,
            // Token: 0x04008D5B RID: 36187
            Remote,
            // Token: 0x04008D5C RID: 36188
            Mutual
        }
    }
}
