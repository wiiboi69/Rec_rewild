using System;
using server;
using System.IO;
using ws;
using api;
using System.Net;
using System.Diagnostics;
using vaultgamesesh;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace start
{
    class Program
    {
        static void Main()
        {

            //startup for Rec_rewild

            if ((File.Exists("SaveData\\App\\firsttime.txt")))
            {
                Setup.quicksetup();

                goto Start;
            }
            Setup.setup();

            goto Tutorial;

        Tutorial:
            if (Setup.firsttime == true)
            {
                Console.Title = "Rec_rewild Intro";
                Console.WriteLine("Welcome to Rec_rewild " + appversion + "!");
                Console.WriteLine("Is this your first time using Rec_rewild?");
                Console.WriteLine("Yes or No (Y, N)");
                string readline22 = Console.ReadLine();
                if (readline22 == "y" || readline22 == "Y")
                {
                    Console.Clear();
                    Console.Title = "Rec_rewild Tutorial";
                    Console.WriteLine("In that case, welcome to Rec_rewild!");
                    Console.WriteLine("Rec_rewild is server software that emulates the old servers of previous RecRoom versions.");
                    Console.WriteLine("it emulate server for rec room versions from 2020 to 2021");
                    Console.WriteLine("To use Rec_rewild, you'll need to have builds aswell!");
                    Console.WriteLine("To download builds, either go to the builds channel or use the links below: (these links are also available from the #builds channel)" + Environment.NewLine);
                    Console.WriteLine(new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Update/builds.txt"));
                    Console.WriteLine("Download a build and press any key to continue:");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Now that you have a build, what you're going to do is as follows:" + Environment.NewLine);
                    Console.WriteLine("1. Unzip the build");
                    Console.WriteLine("2. Start the server by pressing 5 on the main menu");
                    Console.WriteLine("3. Run Recroom_Release.exe from the folder of the build you downloaded." + Environment.NewLine);
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

            Console.WriteLine("Rec_rewild - a fork of openrec for rec room 2021. (Version: " + appversion + ")");
            Console.WriteLine("Download source code here: https://github.com/wiiboi69/Rec_rewild");

            if (!(new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/version.txt").Contains(appversion)))
            {
                Console.WriteLine("This version of Rec_rewild is outdated. We recommend you install the latest version, Rec_rewild " + new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild/master/Download/version.txt"));
            }
            
            Console.WriteLine("//Custom Room Downloader has been moved to the settings tab!" + Environment.NewLine);
            Console.WriteLine("(1) What's New" + Environment.NewLine +"(2) Change Settings" + Environment.NewLine + "(3) Modify Profile" + Environment.NewLine + "(4) Build Download Links" + Environment.NewLine + "(5) Start Server");
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
                goto Settings;

                Settings:
                Console.Title = "Rec_rewild Settings Menu";
                Console.WriteLine("(1) Private Rooms: " + File.ReadAllText("SaveData\\App\\privaterooms.txt") + Environment.NewLine + "(2) Custom Room Downloader " + Environment.NewLine + "(3) Reset SaveData" + Environment.NewLine + "(4) Go Back");
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
                    Console.Title = "Rec_rewild Custom Room Downloader";
                    Console.Clear();
                    Console.WriteLine("Custom Room Downloader: This tool takes the room data of any room you type in and imports it into ^CustomRoom in September 27th 2018.");
                    Console.WriteLine("Please type in the name of the room you would like to download: (Case sensitive)");
                    string roomname = Console.ReadLine();
                    string text = "";
                    try
                    {
                        text = new WebClient().DownloadString("https://rooms.rec.net/rooms?name=" + roomname + "&include=297");
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Failed to download room...");
                        goto Settings;
                    }
                    CustomRooms.RoomDecode(text);
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
                    File.Delete("SaveData\\Profile\\level.txt"); 
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
                    goto Start;
                }
            }
            if (readline == "3")
            {
                Console.Clear();
                goto Profile;

            Profile:
                Console.Title = "Rec_rewild Profile Menu";
                Console.WriteLine("(1) Change Username" + Environment.NewLine + "(2) Change Profile Image" + Environment.NewLine + "(3) Change Level" + Environment.NewLine + "(4) Change bio" + Environment.NewLine + "(5) Profile Downloader" + Environment.NewLine + "(6) Go Back");
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
                    Console.Clear();
                    Console.WriteLine("1) Upload Media Link" + Environment.NewLine + "2) Drag Image onto this window" + Environment.NewLine + "3) Download Rec.Net Profile Image" + Environment.NewLine + "4) Go Back");
                    string readline4 = Console.ReadLine();
                    if (readline4 == "1")
                    {
                        Console.WriteLine("Paste Media Link: ");
                        string medialink = Console.ReadLine();
                        try
                        {
                            File.WriteAllBytes("SaveData\\profileimage.png", new WebClient().DownloadData(medialink));
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
                                data = new WebClient().DownloadString("https://accounts.rec.net/account/search?name=" + username);
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
                else if (readline3 == "3")
                {
                    Console.WriteLine("Current Level: " + File.ReadAllText("SaveData\\Profile\\level.txt"));
                    Console.WriteLine("New Level: ");
                    string newlevel = Console.ReadLine();
                    File.WriteAllText("SaveData\\Profile\\level.txt", newlevel);
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Profile;
                }
                else if (readline3 == "4")
                {
                    Console.WriteLine("Current bio: " + File.ReadAllText("SaveData\\Profile\\bio.txt"));
                    Console.WriteLine("New bio: ");
                    string newlevel = Console.ReadLine();
                    File.WriteAllText("SaveData\\Profile\\bio.txt", newlevel);
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Profile;
                }
                else if (readline3 == "5")
                {
                    Console.Title = "Rec_rewild Profile Downloader";
                    Console.Clear();
                    Console.WriteLine("Profile Downloader: This tool takes the username and profile image of any username you type in and imports it to OpenRec.");
                    Console.WriteLine("Please type the @ username of the profile you would like:");
                    string readusername = Console.ReadLine();
                    if (readusername.StartsWith("@"))
                    {
                        readusername = readusername.Remove(0, 1);
                    }
                    string data2 = "";
                    try
                    {
                        data2 = new WebClient().DownloadString("https://accounts.rec.net/account/search?name=" + readusername);
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Failed to download profile...");
                        goto Start;
                    }
                    
                    ProfieStealer.ProfileSteal(data2);
                    
                    Console.Clear();
                    Console.WriteLine("Success!");
                    goto Start;
                }
                else if (readline3 == "6")
                {
                    Console.Clear();
                    goto Start;
                }
            }
            if (readline == "4")
            {
                Console.Title = "Rec_rewild Build Downloads";
                Console.Clear();
                Console.WriteLine("To download builds,  use the links below: " + Environment.NewLine);
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

                //note: nameserver is at the same port as before
                new NameServer();

                new ImageServer();
                new APIServer();
                new AuthServer();
                new NotificationsServer();
                new matchServer();
                new roomServer();
                new WebSocketHTTP();
                Console.Title = "Rec_rewild server started!";
                Console.WriteLine(msg);

            }
        }
        public static string msg = "//This is the server sending and recieving data from recroom." + Environment.NewLine + "//Ignore this if you don't know what this means." + Environment.NewLine + "//Please start up the build now.";
        public static string version = "";

        public static string appversion = "0.0.2";

        public static string DataPath = Environment.CurrentDirectory + "\\SaveData";

        // Token: 0x04000068 RID: 104   
        public static string ProfilePath = Program.DataPath + "\\Profile";

        // Token: 0x04000069 RID: 105
        public static string CustomImages = Program.DataPath + "\\Images";

        public static bool bannedflag = false;

    }

}
