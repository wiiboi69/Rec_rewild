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
using util;

namespace start
{
    class Program
    {
        static void Main()
        {
            //check for Rec_rewild important files

            if (File.Exists("SaveData\\App\\firsttime.txt"))
            {
                Setup.quicksetup();
                goto Start;
            }

            //startup for Rec_rewild

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
                    Console.WriteLine("Rec_rewild is localhost server software that emulates the old servers of previous RecRoom versions.");
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
                            goto rec_net_profile_notimported;
                        }

                        if (!ProfieStealer.Profilefind(data2_setup, take_int: 12))
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

            appversion = appversion.Replace("\n", String.Empty);
            appversion = appversion.Replace("\r", String.Empty);
            appversion = appversion.Replace("\t", String.Empty);
            Console.WriteLine("Rec_rewild - a fork of OpenRec for Rec Room 2021. (Version: " + appversion + ")");
            Console.WriteLine("Download source code here: https://github.com/wiiboi69/Rec_rewild");
            Console.WriteLine("Discord server here: https://discord.gg/qZhThdFMjy");
            Console.WriteLine("This is a full server rewrite version" + Environment.NewLine);
            Console.WriteLine("(1) What's New" + Environment.NewLine +"(2) Change Settings" + Environment.NewLine + "(3) Modify Profile" + Environment.NewLine + "(4) Build Download Links" + Environment.NewLine + "(5) Start Server" + Environment.NewLine + "(6) Start beta 2022 Server");
            string readline = Console.ReadLine();
            if (readline == "1")
            {
                Console.Title = "Rec_rewild Changelog";
                Console.Clear();
                Console.WriteLine(new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/changelog.txt"));
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
                Console.WriteLine("(1) Private Rooms: " + File.ReadAllText("SaveData\\App\\privaterooms.txt") + Environment.NewLine + "(2) Custom Room Downloader" + Environment.NewLine + "(3) Reset SaveData" + Environment.NewLine + "(4) Update SaveData" +  Environment.NewLine + "(5) Go Back");
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
                    File.Delete("SaveData\\avatar.txt");
                    File.Delete("SaveData\\avataritems.txt");
                    File.Delete("SaveData\\equipment.txt");
                    File.Delete("SaveData\\consumables.txt");
                    File.Delete("SaveData\\gameconfigs.txt");
                    File.Delete("SaveData\\storefronts2.txt");
                    File.Delete("SaveData\\Profile\\username.txt");
                    File.Delete("SaveData\\Profile\\username.txt");
                    File.Delete("SaveData\\Profile\\level.txt");
                    File.Delete("SaveData\\Profile\\tokens.txt");
                    File.Delete("SaveData\\Profile\\userid.txt");
                    File.Delete("SaveData\\myrooms.txt"); 
                    File.Delete("SaveData\\settings.txt");
                    File.Delete("SaveData\\App\\privaterooms.txt");
                    File.Delete("SaveData\\App\\facefeaturesadd.txt");
                    File.Delete("SaveData\\profileimage.png");
                    File.Delete("SaveData\\App\\firsttime.txt");
                    File.Delete("SaveData\\avataritems2.txt");
                    File.Delete("SaveData\\Rooms\\Downloaded\\roomname.txt");
                    File.Delete("SaveData\\Rooms\\Downloaded\\roomid.txt");
                    File.Delete("SaveData\\Rooms\\Downloaded\\datablob.txt");
                    File.Delete("SaveData\\Rooms\\Downloaded\\roomsceneid.txt");
                    File.Delete("SaveData\\Rooms\\Downloaded\\imagename.txt");
                    File.Delete("SaveData\\Rooms\\Downloaded\\cheercount.txt");
                    File.Delete("SaveData\\Rooms\\Downloaded\\favcount.txt");
                    File.Delete("SaveData\\Rooms\\Downloaded\\visitcount.txt");
                    Console.WriteLine("Success!");
                    Setup.setup();
                    goto Settings;
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
                    Thread.Sleep(400); // to show the user that it success
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

                            List<ProfieStealer.Root> profile = JsonConvert.DeserializeObject<List<ProfieStealer.Root>>(data);
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
                    
                    if (!ProfieStealer.Profilefind(data2, take_int: 12))
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
                Console.WriteLine(new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/builds.txt"));
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
                        WebSocketHTTP_new.SendRequest(JsonConvert.SerializeObject(ProgramHelpers.createResponse_time()));
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
                    else if (input == "!exit")
                    {
                        Console.WriteLine();
                        Console.WriteLine(" closing the server");
                        Console.WriteLine();
                        Environment.Exit(0);
                        goto input_server;
                    }
                    input_server:
                    input = Console.ReadLine();
                }
            }
            if (readline == "6")
            {
                Console.Title = "starting beta server for 2022 build";
                Console.WriteLine("Please wait for server to start up");
                version = "2022";

                APIServer_2022.Cachedservertimestarted = (ulong)DateTime.Now.Ticks;

                beta = true;

                //note: nameserver is at the same port as before
                new NameServer();
                new APIServer_2022();
                new AuthServer();
                new ImageServer();
                new matchServer();
                new NotificationsServer();
                new WebSocketHTTP_new();
                new roomServer();

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
                        WebSocketHTTP_new.SendRequest(JsonConvert.SerializeObject(ProgramHelpers.createResponse_time()));
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
        public static string appversion = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/version.txt");
        public static string DataPath = Environment.CurrentDirectory + "\\SaveData";
        public static string ProfilePath = Program.DataPath + "\\Profile";
        public static string CustomImages = Program.DataPath + "\\Images";

        public class Reponse
        {
            public WebSocketHTTP_new.ResponseResults Id { get; set; }

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


            public string AvatarItemDescOrHairDyeDesc { get; set; }









            public string ErrorMessage { get; set; }
            public string SupportsCurrentPlatform { get; set; }
            public string HasAvatarItemOrHairDye { get; set; }
            public string HasEquipment { get; set; }
            public string Consumed { get; set; }
            public string IsValid { get; set; }
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
            None = -1, // 0xFFFFFFFF
            Default = 0,
            First_Activity = 1,
            Game_Drop = 2,
            All_Daily_Challenges_Complete = 3,
            All_Weekly_Challenge_Complete = 4,
            Daily_Challenge_Complete = 5,
            Weekly_Challenge_Complete = 6,
            Unassigned_Equipment = 10, // 0x0000000A
            Unassigned_Avatar = 11, // 0x0000000B
            Unassigned_Consumable = 12, // 0x0000000C
            Reacquisition = 20, // 0x00000014
            Membership = 21, // 0x00000015
            NUX_TokensAndDressUp = 30, // 0x0000001E
            NUX_Experiment1 = 31, // 0x0000001F
            NUX_Experiment2 = 32, // 0x00000020
            NUX_Experiment3 = 33, // 0x00000021
            NUX_Experiment4 = 34, // 0x00000022
            NUX_Experiment5 = 35, // 0x00000023
            LevelUp = 100, // 0x00000064
            Purchased_Gift_A = 500, // 0x000001F4
            Purchased_Gift_B = 501, // 0x000001F5
            Purchased_Gift_C = 502, // 0x000001F6
            Purchased_Gift_D = 503, // 0x000001F7
            Holiday = 1000, // 0x000003E8
            Contest = 1001, // 0x000003E9
            Promotion = 1002, // 0x000003EA
            SubscribersOnly = 1003, // 0x000003EB
            Deprecated = 1100, // 0x0000044C
            RecRoyale = 1200, // 0x000004B0
            Paintball_ClearCut = 2000, // 0x000007D0
            Paintball_Homestead = 2001, // 0x000007D1
            Paintball_Quarry = 2002, // 0x000007D2
            Paintball_River = 2003, // 0x000007D3
            Paintball_Dam = 2004, // 0x000007D4
            Paintball_DriveIn = 2005, // 0x000007D5
            Discgolf_Propulsion = 3000, // 0x00000BB8
            Discgolf_Lake = 3001, // 0x00000BB9
            Discgolf_Mode_CoopCatch = 3500, // 0x00000DAC
            Quest_Goblin_A = 4000, // 0x00000FA0
            Quest_Goblin_B = 4001, // 0x00000FA1
            Quest_Goblin_C = 4002, // 0x00000FA2
            Quest_Goblin_S = 4003, // 0x00000FA3
            Quest_Goblin_Consumable = 4004, // 0x00000FA4
            Quest_Cauldron_A = 4010, // 0x00000FAA
            Quest_Cauldron_B = 4011, // 0x00000FAB
            Quest_Cauldron_C = 4012, // 0x00000FAC
            Quest_Cauldron_S = 4013, // 0x00000FAD
            Quest_Cauldron_Consumable = 4014, // 0x00000FAE
            Quest_Pirate1_A = 4100, // 0x00001004
            Quest_Pirate1_B = 4101, // 0x00001005
            Quest_Pirate1_C = 4102, // 0x00001006
            Quest_Pirate1_S = 4103, // 0x00001007
            Quest_Pirate1_X = 4104, // 0x00001008
            Quest_Pirate1_Consumable = 4105, // 0x00001009
            Quest_Dracula1_A = 4200, // 0x00001068
            Quest_Dracula1_B = 4201, // 0x00001069
            Quest_Dracula1_C = 4202, // 0x0000106A
            Quest_Dracula1_S = 4203, // 0x0000106B
            Quest_Dracula1_X = 4204, // 0x0000106C
            Quest_Dracula1_Consumable = 4205, // 0x0000106D
            Quest_Dracula1_SS = 4206, // 0x0000106E
            Quest_SciFi_A = 4500, // 0x00001194
            Quest_SciFi_B = 4501, // 0x00001195
            Quest_SciFi_C = 4502, // 0x00001196
            Quest_SciFi_S = 4503, // 0x00001197
            Quest_Scifi_Consumable = 4504, // 0x00001198
            Charades = 5000, // 0x00001388
            Soccer = 6000, // 0x00001770
            Paddleball = 7000, // 0x00001B58
            Dodgeball = 8000, // 0x00001F40
            Lasertag = 9000, // 0x00002328
            Bowling = 10000, // 0x00002710
            StuntRunner_TheMainEvent_A = 11000, // 0x00002AF8
            StuntRunner_TheMainEvent_B = 11001, // 0x00002AF9
            StuntRunner_TheMainEvent_C = 11002, // 0x00002AFA
            StuntRunner_TheMainEvent_D = 11003, // 0x00002AFB
            StuntRunner_TheMainEvent_S = 11004, // 0x00002AFC
            StuntRunner_TheMainEvent_X = 11005, // 0x00002AFD
            StuntRunner_TheMainEvent_Consumable = 11006, // 0x00002AFE
            StuntRunner_TheMainEvent_SS = 11007, // 0x00002AFF
            Store_LaserTag = 100000, // 0x000186A0
            Store_RecCenter = 100010, // 0x000186AA
            Consumable = 110000, // 0x0001ADB0
            Token = 110100, // 0x0001AE14
            Punchcard_Challenge_Complete = 110200, // 0x0001AE78
            All_Punchcard_Challenges_Complete = 110201, // 0x0001AE79
            Commerce_Purchase = 200000, // 0x00030D40
        }

        public enum StorefrontTypes
        {
            // Token: 0x04009148 RID: 37192
            None,
            // Token: 0x04009149 RID: 37193
            LaserTag,
            // Token: 0x0400914A RID: 37194
            RecCenter,
            // Token: 0x0400914B RID: 37195
            Watch,
            // Token: 0x0400914C RID: 37196
            Quest_LostSkulls = 100,
            // Token: 0x0400914D RID: 37197
            Quest_Dracula,
            // Token: 0x0400914E RID: 37198
            Quest_GoldenTrophy,
            // Token: 0x0400914F RID: 37199
            Quest_CrimsonCauldron,
            // Token: 0x04009150 RID: 37200
            RecRoyale = 200,
            // Token: 0x04009151 RID: 37201
            Cafe = 300,
            // Token: 0x04009152 RID: 37202
            Paintball = 400,
            // Token: 0x04009153 RID: 37203
            Paintball_River,
            // Token: 0x04009154 RID: 37204
            Paintball_Homestead,
            // Token: 0x04009155 RID: 37205
            Paintball_Quarry,
            // Token: 0x04009156 RID: 37206
            Paintball_ClearCut,
            // Token: 0x04009157 RID: 37207
            Paintball_Spillway,
            // Token: 0x04009158 RID: 37208
            Paintball_SunsetDriveIn,
            // Token: 0x04009159 RID: 37209
            Bowling = 500,
            // Token: 0x0400915A RID: 37210
            StuntRunner = 600,
            // Token: 0x0400915B RID: 37211
            DormMirror = 700
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
