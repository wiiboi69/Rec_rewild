using System;
using System.IO;
using System.Net;
using System.Web;
using System.Text;
using System.Threading;
using api;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.AccessControl;
using static server.WebSocketHTTP;


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
                            try
                            {
                                s = room_util.room_find_CustomRooms(s);
                            }
                            catch
                            {
                                s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/" + subs[0].ToLower() + ".txt");
                            }
                        }
                        else if (rawUrl.StartsWith("/rooms/search?query=") || rawUrl.StartsWith("/rooms/hot"))
                        {
                            s = new WebClient().DownloadString("https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/RRORooms.json");
                            s = room_util.room_inject_CustomRooms_list(s);

                        }
                        else if (rawUrl.StartsWith("/rooms/"))
                        {
                            s = room_util.find_room_with_id(rawUrl, 7);
                            if (APIServer.CachedversionID > 20210899)
                            {
                                s = room_util.room_change_fix_room(s);
                            } 
                            s = room_util.room_change_CreatorAccount(s);
                            //s = "[" + room_util.find_room_with_id(rawUrl, 7) + "]";
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
