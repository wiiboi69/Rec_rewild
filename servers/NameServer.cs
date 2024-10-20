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
            //this.listener.Prefixes.Add("http://localhost:2059/");
            for (; ; )
            {
                this.listener.Start();
                Console.WriteLine("[NameServer.cs] is listening.");
                HttpListenerContext context = this.listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                string rawUrl = request.RawUrl;
                string s = "";  
                
                //if (Program.beta)
                if (false)
                {
                    /*
                    s = "{"+
                      "\"Accounts\": \"https://accounts.rec.net\","+
                      "\"API\": \"https://api.rec.net\","+
                      "\"Auth\": \"https://auth.rec.net\","+
                      "\"BugReporting\": \"https://bugreporting.rec.net\","+
                      "\"Cards\": \"https://cards.rec.net\","+
                      "\"CDN\": \"https://cdn.rec.net\","+
                      "\"Chat\": \"https://chat.rec.net\","+
                      "\"Clubs\": \"https://clubs.rec.net\","+
                      "\"CMS\": \"https://cms.rec.net\","+
                      "\"Commerce\": \"https://commerce.rec.net\","+
                      "\"Data\": \"https://data.rec.net\","+
                      "\"DataCollection\": \"https://datacollection.rec.net\","+
                      "\"Discovery\": \"https://discovery.rec.net\","+
                      "\"Econ\": \"https://econ.rec.net\"," +
                      "\"GameLogs\": \"https://gamelogs.rec.net\"," +
                      "\"Geo\": \"https://geo.rec.net\"," +
                      "\"Images\": \"https://img.rec.net\"," +
                      "\"Leaderboard\": \"https://leaderboard.rec.net\"," +
                      "\"Link\": \"https://link.rec.net\"," +
                      "\"Lists\": \"https://lists.rec.net\"," +
                      "\"Matchmaking\": \"https://match.rec.net\"," +
                      "\"Moderation\": \"https://moderation.rec.net\"," +
                      "\"Notifications\": \"https://notify.rec.net\"," +
                      "\"PlatformNotifications\": \"https://platformnotifications.rec.net\"," +
                      "\"PlayerSettings\": \"https://playersettings.rec.net\"," +
                      "\"RoomComments\": \"https://roomcomments.rec.net\"," +
                      "\"Rooms\": \"https://rooms.rec.net\"," +
                      "\"Storage\": \"https://storage.rec.net\"," +
                      "\"Strings\": \"https://strings.rec.net\"," +
                      "\"StringsCDN\": \"https://strings-cdn.rec.net\"," +
                      "\"Studio\": \"https://studio.rec.net\"," +
                      "\"Thorn\": \"https://thorn.rec.net\"," +
                      "\"Videos\": \"https://videos.rec.net\"," +
                      "\"WWW\": \"https://rec.net\""+
                    "}";*/
                    /*
                    s = "{" +
                        "  \"Accounts\": \"https://example.com/\"," +
                        "  \"API\":\"https://example.com/\"," +
                        "  \"Auth\": \"https://example.com/auth/\"," +
                        "  \"BugReporting\": \"https://example.com/\"," +
                        "  \"Cards\": \"https://example.com/\"," +
                        "  \"CDN\": \"https://example.com/\"," +
                        "  \"Chat\": \"https://example.com/\"," +
                        "  \"Clubs\": \"https://example.com/\"," +
                        "  \"CMS\": \"https://example.com/\"," +
                        "  \"Commerce\": \"https://example.com/\"," +
                        "  \"Data\": \"https://example.com/\"," +
                        "  \"DataCollection\": \"https://example.com/\"," +
                        "  \"Discovery\": \"https://example.com/\"," +
                        "  \"Econ\": \"https://example.com/\"," +
                        "  \"GameLogs\": \"https://example.com/\"," +
                        "  \"Geo\": \"https://example.com/\"," +
                        "  \"Images\": \"https://example.com/img/\"," +
                        "  \"Leaderboard\": \"https://example.com/\"," +
                        "  \"Link\": \"https://example.com/\"," +
                        "  \"Lists\": \"https://example.com/\"," +
                        "  \"Matchmaking\": \"https://example.com/Matchmaking/\"," +
                        "  \"Moderation\": \"https://example.com/\"," +
                        "  \"Notifications\": \"https://example.com/Notifications/\"," +
                        //"  \"PlatformNotifications\": \"https://example.com/platformnotifications/\"," +
                        "  \"PlayerSettings\": \"https://example.com/\"," +
                        "  \"RoomComments\": \"https://example.com/\"," +
                        "  \"Rooms\": \"https://example.com/roomserver/\"," +
                        "  \"Storage\": \"https://example.com/Storage/\"," +
                        "  \"Strings\": \"https://example.com/\"," +
                        "  \"StringsCDN\": \"https://example.com/\"," +
                        "  \"Studio\": \"https://example.com/Studio/\"," +
                        "  \"Thorn\": \"https://example.com/\"," +
                        "  \"Videos\": \"https://example.com/\"," +
                        "  \"WWW\": \"https://example.com/\"" +
                        "}";*/
                }
                else
                {
                    s = "{" +
                        "  \"Accounts\": \"http://localhost:20210/\"," +
                        "  \"API\":\"http://localhost:20210/\"," +
                        "  \"Auth\": \"http://localhost:20214/\"," +
                        "  \"BugReporting\": \"http://localhost:20210/\"," +
                        "  \"Cards\": \"http://localhost:20210/\"," +
                        "  \"CDN\": \"http://localhost:20210/\"," +
                        "  \"Chat\": \"http://localhost:20210/\"," +
                        "  \"Clubs\": \"http://localhost:20210/\"," +
                        "  \"CMS\": \"http://localhost:20210/\"," +
                        "  \"Commerce\": \"http://localhost:20210/\"," +
                        "  \"Data\": \"http://localhost:20210/\"," +
                        "  \"DataCollection\": \"http://localhost:20210/\"," +
                        "  \"Discovery\": \"http://localhost:20210/\"," +
                        "  \"Econ\": \"http://localhost:20210/\"," +
                        "  \"GameLogs\": \"http://localhost:20210/\"," +
                        "  \"Geo\": \"http://localhost:20210/\"," +
                        "  \"Images\": \"http://localhost:20213/\"," +
                        "  \"Leaderboard\": \"http://localhost:20210/\"," +
                        "  \"Link\": \"http://localhost:20210/\"," +
                        "  \"Lists\": \"http://localhost:20210/\"," +
                        "  \"Matchmaking\": \"http://localhost:20215/\"," +
                        "  \"Moderation\": \"http://localhost:20210/\"," +
                        "  \"Notifications\": \"http://localhost:20212/\"," +
                        "  \"PlayerSettings\": \"http://localhost:20210/\"," +
                        "  \"RoomComments\": \"http://localhost:20210/\"," +
                        "  \"Rooms\": \"http://localhost:20218/\"," +
                        "  \"Storage\": \"http://localhost:20210/\"," +
                        "  \"Strings\": \"http://localhost:20210/\"," +
                        "  \"StringsCDN\": \"http://localhost:20210/\"," +
                        "  \"Thorn\": \"http://localhost:20210/\"," +
                        "  \"Videos\": \"http://localhost:20210/\"," +
                        "  \"WWW\": \"http://localhost:20210/\"" +
                        "}";
                }
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