using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using start;

namespace server
{
	// Token: 0x02000050 RID: 80
	internal class NameServer
	{
		
		// Token: 0x06000227 RID: 551 RVA: 0x00006D1C File Offset: 0x00004F1C
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

		// Token: 0x06000228 RID: 552 RVA: 0x00006D84 File Offset: 0x00004F84
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
                /*NSData data = new NSData()
                {
                    API = "http://localhost:2021",
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
                s = JsonConvert.SerializeObject(data);*/
                s = "{\r\n  \"Accounts\": \"http://localhost:20210\",\r\n  \"API\":\"http://localhost:20210\",\r\n  \"Auth\": \"http://localhost:20214\",\r\n  \"BugReporting\": \"http://localhost:20210\",\r\n  \"Cards\": \"http://localhost:20210\",\r\n  \"CDN\": \"http://localhost:20210\",\r\n  \"Chat\": \"http://localhost:20210\",\r\n  \"Clubs\": \"http://localhost:20210\",\r\n  \"CMS\": \"http://localhost:20210\",  \r\n  \"Commerce\": \"http://localhost:20210\",\r\n  \"Data\": \"http://localhost:20210\", \r\n  \"DataCollection\": \"http://localhost:20210\",\r\n  \"Discovery\": \"http://localhost:20210\",\r\n  \"Econ\": \"http://localhost:20210\",\r\n  \"GameLogs\": \"http://localhost:20210\",\r\n  \"Geo\": \"http://localhost:20210\",\r\n  \"Images\": \"http://localhost:20213\",\r\n  \"Leaderboard\": \"http://localhost:20210\",\r\n  \"Link\": \"http://localhost:20210\",\r\n  \"Lists\": \"http://localhost:20210\",\r\n  \"Matchmaking\": \"http://localhost:20215\",\r\n  \"Moderation\": \"http://localhost:20210\",\r\n  \"Notifications\": \"http://localhost:20212\",\r\n    \r\n  \"PlayerSettings\": \"http://localhost:20210\",\r\n  \"RoomComments\": \"http://localhost:20210\",\r\n  \"Rooms\": \"http://localhost:20218\",\r\n  \"Storage\": \"http://localhost:20210\", \r\n  \"Strings\": \"http://localhost:20210\",\r\n  \"StringsCDN\": \"http://localhost:20210\",\r\n  \r\n  \"Thorn\": \"http://localhost:20210\",\r\n  \"Videos\": \"http://localhost:20210\",\r\n  \"WWW\": \"http://localhost:20210\"\r\n} \r\n\r\n";
                Console.WriteLine("API Response: " + s);
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                response.ContentLength64 = (long)bytes.Length;
                Stream outputStream = response.OutputStream;
                outputStream.Write(bytes, 0, bytes.Length);
                Thread.Sleep(400);
                outputStream.Close();
                this.listener.Stop();
            }
        }

        
        public static string VersionCheckResponse = "{\"ValidVersion\":true}";
		public static string BlankResponse = "";
        public class NSData
        {
            // Token: 0x17000045 RID: 69
            // (get) Token: 0x060000C9 RID: 201 RVA: 0x00007DC2 File Offset: 0x00005FC2
            // (set) Token: 0x060000CA RID: 202 RVA: 0x00007DCA File Offset: 0x00005FCA
            public string Accounts { get; set; }

            // Token: 0x17000046 RID: 70
            // (get) Token: 0x060000CB RID: 203 RVA: 0x00007DD3 File Offset: 0x00005FD3
            // (set) Token: 0x060000CC RID: 204 RVA: 0x00007DDB File Offset: 0x00005FDB
            public string Auth { get; set; }

            // Token: 0x17000047 RID: 71
            // (get) Token: 0x060000CD RID: 205 RVA: 0x00007DE4 File Offset: 0x00005FE4
            // (set) Token: 0x060000CE RID: 206 RVA: 0x00007DEC File Offset: 0x00005FEC
            public string API { get; set; }

            // Token: 0x17000048 RID: 72
            // (get) Token: 0x060000CF RID: 207 RVA: 0x00007DF5 File Offset: 0x00005FF5
            // (set) Token: 0x060000D0 RID: 208 RVA: 0x00007DFD File Offset: 0x00005FFD
            public string Notifications { get; set; }

            // Token: 0x17000049 RID: 73
            // (get) Token: 0x060000D1 RID: 209 RVA: 0x00007E06 File Offset: 0x00006006
            // (set) Token: 0x060000D2 RID: 210 RVA: 0x00007E0E File Offset: 0x0000600E
            public string Images { get; set; }

        public string Data { get; set; }
        public string DataCollection { get; set; }
        public string Discovery {  get; set; }

            // Token: 0x1700004A RID: 74
            // (get) Token: 0x060000D3 RID: 211 RVA: 0x00007E17 File Offset: 0x00006017
            // (set) Token: 0x060000D4 RID: 212 RVA: 0x00007E1F File Offset: 0x0000601F
            public string Commerce { get; set; }

            // Token: 0x1700004B RID: 75
            // (get) Token: 0x060000D5 RID: 213 RVA: 0x00007E28 File Offset: 0x00006028
            // (set) Token: 0x060000D6 RID: 214 RVA: 0x00007E30 File Offset: 0x00006030
            public string CDN { get; set; }

            // Token: 0x1700004C RID: 76
            // (get) Token: 0x060000D7 RID: 215 RVA: 0x00007E39 File Offset: 0x00006039
            // (set) Token: 0x060000D8 RID: 216 RVA: 0x00007E41 File Offset: 0x00006041
            public string WWW { get; set; }

            // Token: 0x1700004D RID: 77
            // (get) Token: 0x060000D9 RID: 217 RVA: 0x00007E4A File Offset: 0x0000604A
            // (set) Token: 0x060000DA RID: 218 RVA: 0x00007E52 File Offset: 0x00006052
            public string Matchmaking { get; set; }

            // Token: 0x1700004E RID: 78
            // (get) Token: 0x060000DB RID: 219 RVA: 0x00007E5B File Offset: 0x0000605B
            // (set) Token: 0x060000DC RID: 220 RVA: 0x00007E63 File Offset: 0x00006063
            public string Storage { get; set; }

            // Token: 0x1700004F RID: 79
            // (get) Token: 0x060000DD RID: 221 RVA: 0x00007E6C File Offset: 0x0000606C
            // (set) Token: 0x060000DE RID: 222 RVA: 0x00007E74 File Offset: 0x00006074
            public string Leaderboard { get; set; }

            // Token: 0x17000050 RID: 80
            // (get) Token: 0x060000DF RID: 223 RVA: 0x00007E7D File Offset: 0x0000607D
            // (set) Token: 0x060000E0 RID: 224 RVA: 0x00007E85 File Offset: 0x00006085
            public string Chat { get; set; }

            // Token: 0x17000051 RID: 81
            // (get) Token: 0x060000E1 RID: 225 RVA: 0x00007E8E File Offset: 0x0000608E
            // (set) Token: 0x060000E2 RID: 226 RVA: 0x00007E96 File Offset: 0x00006096
            public string Link { get; set; }

            // Token: 0x17000052 RID: 82
            // (get) Token: 0x060000E3 RID: 227 RVA: 0x00007E9F File Offset: 0x0000609F
            // (set) Token: 0x060000E4 RID: 228 RVA: 0x00007EA7 File Offset: 0x000060A7
            public string RoomComments { get; set; }

            public string PlayerSettings { get; set; }
            public string Rooms { get; set; }

            // Token: 0x17000053 RID: 83
            // (get) Token: 0x060000E5 RID: 229 RVA: 0x00007EB0 File Offset: 0x000060B0
            // (set) Token: 0x060000E6 RID: 230 RVA: 0x00007EB8 File Offset: 0x000060B8
            public string Clubs { get; set; }
        }
    


        // Token: 0x04000192 RID: 402
        private HttpListener listener = new HttpListener();

	}
}
