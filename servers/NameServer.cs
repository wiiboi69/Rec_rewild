using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using start;

namespace server
{
    internal class NameServer
    {
        public NameServer()
        {
            try
            {
                Console.WriteLine("[NameServer.cs] has started.");
                new Thread(new ThreadStart(this.StartListen)).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Exception Occurred while Listening :" + ex.ToString());
            }
        }
        private void StartListen()
        {
            this.listener.Prefixes.Add("http://localhost:20211/");
            for (; ; )
            {
                this.listener.Start();
                Console.WriteLine("[NameServer.cs] is listening.");
                HttpListenerContext context = this.listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                string rawUrl = request.RawUrl;
                string s = "";
                NSData data = new NSData()
                {
                    API = "http://localhost:20210",
                    Notifications = "http://localhost:20212",
                    Images = "http://localhost:20213",
                    Auth = "http://localhost:20214",
                    WWW = "http://localhost:2021",
                    Commerce = "http://localhost:2021",
                    Accounts = "http://localhost:2021",
                    PlayerSettings = "http://localhost:2021",
                    CDN = "http://localhost:2021/",
                    Data = "http://localhost:2021", 
                    DataCollection =  "http://localhost:2021",
                    Discovery =  "http://localhost:2021",
                    Matchmaking = "http://localhost:2021/",
                    Storage = "http://localhost:2021/",
                    Chat = "http://localhost:2021/",
                    Leaderboard = "http://localhost:2021/",
                    Link = "http://localhost:2021/",
                    RoomComments = "http://localhost:2021/",
                    Rooms = "http://localhost:2021/",
                    Clubs = "http://localhost:2021/"
                };
                s = JsonConvert.SerializeObject(data);
                Console.WriteLine("NameServer Response: " + s);
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                response.ContentLength64 = (long)bytes.Length;
                Stream outputStream = response.OutputStream;
                outputStream.Write(bytes, 0, bytes.Length);
                Thread.Sleep(400);
                outputStream.Close();
                this.listener.Stop();
            }
        }
        public class NSData
        {
            public string Accounts { get; set; }
            public string Auth { get; set; }
            public string API { get; set; }
            public string Notifications { get; set; }
            public string Images { get; set; }
            public string Data { get; set; }
            public string DataCollection { get; set; }
            public string Discovery { get; set; }
            public string Commerce { get; set; }
            public string CDN { get; set; }
            public string WWW { get; set; }
            public string Matchmaking { get; set; }
            public string Storage { get; set; }
            public string Leaderboard { get; set; }
            public string Chat { get; set; }
            public string Link { get; set; }
            public string RoomComments { get; set; }
            public string PlayerSettings { get; set; }
            public string Rooms { get; set; }
            public string Clubs { get; set; }
        }
        private HttpListener listener = new HttpListener();
    }
}