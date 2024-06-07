using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using api;
using api2017;
using api2018;
using api2019;
using Newtonsoft.Json;
using vaultgamesesh;
using System.Collections.Generic;
using System.Security.AccessControl;
using gamesesh;

namespace server
{
    internal class roomServer
    {
        public roomServer()
        {
            try
            {
                Console.WriteLine("[roomServer.cs] has started.");
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

                this.listener.Prefixes.Add("http://localhost:" + start.Program.version + "8/");
                {
                    for (; ; )
                    {
                        this.listener.Start();
                        Console.WriteLine("[roomServer.cs] is listening.");
                        HttpListenerContext context = this.listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;
                        List<byte> list = new List<byte>();
                        string rawUrl = request.RawUrl;
                        string Url = "";
                        byte[] bytes = null;
                        string signature = request.Headers.Get("X-RNSIG");
                        string temp1 = "";
                        string temp2 = "";
                        Console.WriteLine("room Requested: " + rawUrl);
                        string text;
                        string s = "";
                        using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
                        {
                            text = streamReader.ReadToEnd();
                        }
                        Console.WriteLine("room Data: " + text);
                        if (rawUrl == "/rooms/createdby/me")
                        {
                            s = BracketResponse;
                        }
                        else if (rawUrl == "/rooms/ownedby/me")
                        {
                            s = BracketResponse;
                        }
                        else if (rawUrl == "/rooms/favoritedby/me")
                        {
                            s = BracketResponse;
                        }
                        else if (rawUrl.StartsWith("/rooms/visitedby/me"))
                        {
                            s = BracketResponse;
                        }
                        else if (rawUrl.StartsWith("/rooms/") && rawUrl.EndsWith("/playerdata/me"))
                        {
                            s = BlankResponse;
                        }
                        else if (rawUrl.StartsWith("/rooms/bulk?name="))
                        {
                            Console.WriteLine(rawUrl.Remove(0, 17) + ".txt");
                            s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms/" + rawUrl.Remove(0, 17).ToLower() + ".txt");
                            //s = File.ReadAllText("SaveData\\Rooms\\1-bulk.txt");
                        }
                        else if (rawUrl.StartsWith("/rooms?name="))
                        {
                            Url = rawUrl.Remove(0, 12);
                            string[] stringSeparators = new string[] { "?include=1325" };
                            string[] subs = Url.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries); 
                            stringSeparators = new string[] { "&" };
                            subs = Url.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            stringSeparators = new string[] { "?include=301" };
                            subs = subs[0].Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            Console.WriteLine(subs[0] + ".txt");
                            s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/" + subs[0].ToLower() + ".txt");
                            if (subs[0].ToLower() == "orientation") 
                            {
                                GameSessions.gamesessionid = 20161L;
                                GameSessions.gamesessionsubroomid = 20161L;

                                if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Enabled")
                                {
                                    GameSessions.gamesessionid = new Random().Next(0, 99);
                                    GameSessions.gamesessionsubroomid = new Random().Next(0, 0xffff);
                                }
                                Guid myuuid = Guid.NewGuid();
                                GameSessions.myuuidAsString = myuuid.ToString();


                                Config.localGameSessionv3 = new GameSessions.SessionInstancev3
                                {
                                    EncryptVoiceChat = false,
                                    clubId = null,
                                    dataBlob = "",
                                    EventId = null,
                                    isFull = false,
                                    isInProgress = false,
                                    isPrivate = true,
                                    location = "c79709d8-a31b-48aa-9eb8-cc31ba9505e8",
                                    MaxCapacity = 1,
                                    Name = "orientation",
                                    photonRegionId = "us",
                                    photonRoomId = "orientation" + "-" + GameSessions.myuuidAsString + "-room",
                                    roomCode = null,
                                    roomId = 3,
                                    roomInstanceId = GameSessions.gamesessionid,
                                    roomInstanceType = 0,
                                    subRoomId = GameSessions.gamesessionsubroomid,

                                };
                            }
                            //s = File.ReadAllText("SaveData\\Rooms\\1-bulk.txt");
                        }
                        else if (rawUrl.StartsWith("/rooms/"))
                        {
                            Url = rawUrl.Remove(0, 7);
                            string[] stringSeparators = new string[] { "?include=1325" };
                            string[] subs = Url.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            stringSeparators = new string[] { "?include=301" };
                            subs = subs[0].Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            temp1 = subs[0];
                            temp2 = GameSessions.FindRoomid(ulong.Parse(temp1));
                            if (temp2 != "")
                            {

                                Console.WriteLine("found room name: " + temp2 + " using room id: " + temp1);


                                s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/" + temp2.ToLower() + ".txt");

                            }
                            else 
                            {
                                s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/dormroom.txt");

                                Console.WriteLine("can't find room id : " + temp1);
                            }


                            //s = File.ReadAllText("SaveData\\Rooms\\" + subs[0]+".txt");
                            //s = JsonConvert.SerializeObject(c00005d.rooms_find_list(Convert.ToInt32(subs[0])));
                        }
                        

                        Console.WriteLine("room Response: " + s);
                        bytes = Encoding.UTF8.GetBytes(s);
                        response.ContentLength64 = (long)bytes.Length;
                        Stream outputStream = response.OutputStream;
                        outputStream.Write(bytes, 0, bytes.Length);
                        Thread.Sleep(100);
                        outputStream.Close();
                        this.listener.Stop();

                    }
                }
            }
            catch (Exception ex4)
            {
                Console.WriteLine(ex4);
                File.WriteAllText("crashdump-room.txt", Convert.ToString(ex4));
                this.listener.Close();
                new roomServer();
            }
        }
        public static ulong CachedPlayerID = ulong.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt"));
        public static ulong CachedPlatformID = 10000;
        public static int CachedVersionMonth = 01;

        public static string BlankResponse = "";
        public static string BracketResponse = "[]";

        private HttpListener listener = new HttpListener();
    }
}
