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
                s = "{\r\n  \"Accounts\": \"http://localhost:20210\",\r\n  \"API\":\"http://localhost:20210\",\r\n  \"Auth\": \"http://localhost:20214\",\r\n  \"BugReporting\": \"http://localhost:20210\",\r\n  \"Cards\": \"http://localhost:20210\",\r\n  \"CDN\": \"http://localhost:20210\",\r\n  \"Chat\": \"http://localhost:20210\",\r\n  \"Clubs\": \"http://localhost:20210\",\r\n  \"CMS\": \"http://localhost:20210\",  \r\n  \"Commerce\": \"http://localhost:20210\",\r\n  \"Data\": \"http://localhost:20210\", \r\n  \"DataCollection\": \"http://localhost:20210\",\r\n  \"Discovery\": \"http://localhost:20210\",\r\n  \"Econ\": \"http://localhost:20210\",\r\n  \"GameLogs\": \"http://localhost:20210\",\r\n  \"Geo\": \"http://localhost:20210\",\r\n  \"Images\": \"http://localhost:20213\",\r\n  \"Leaderboard\": \"http://localhost:20210\",\r\n  \"Link\": \"http://localhost:20210\",\r\n  \"Lists\": \"http://localhost:20210\",\r\n  \"Matchmaking\": \"http://localhost:20215\",\r\n  \"Moderation\": \"http://localhost:20210\",\r\n  \"Notifications\": \"http://localhost:20212\",\r\n    \r\n  \"PlayerSettings\": \"http://localhost:20210\",\r\n  \"RoomComments\": \"http://localhost:20210\",\r\n  \"Rooms\": \"http://localhost:20218\",\r\n  \"Storage\": \"http://localhost:20210\", \r\n  \"Strings\": \"http://localhost:20210\",\r\n  \"StringsCDN\": \"http://localhost:20210\",\r\n  \r\n  \"Thorn\": \"http://localhost:20210\",\r\n  \"Videos\": \"http://localhost:20210\",\r\n  \"WWW\": \"http://localhost:20210\"\r\n} \r\n\r\n";
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