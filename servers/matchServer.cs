using System;
using System.IO;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using api;
using Newtonsoft.Json;

namespace server
{
	internal class matchServer
	{
		public matchServer()
		{
			try
			{
				Console.WriteLine("[matchServer.cs] has started.");
				new Thread(new ThreadStart(this.StartListen)).Start();
			}
			catch (Exception ex)
			{
				Console.WriteLine("An Exception Occurred while Listening :" + ex.ToString());
			}
		}
		private void StartListen()
		{
			this.listener.Prefixes.Add("http://localhost:20215/");
			for (; ; )
			{
				this.listener.Start();
				Console.WriteLine("{matchServer.cs] is listening.");
				HttpListenerContext context = this.listener.GetContext();
				HttpListenerRequest request = context.Request;
				HttpListenerResponse response = context.Response;
				string rawUrl = request.RawUrl;
				string text;
				string s = "[]";
				byte[] bytes;
				using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
				{
					text = streamReader.ReadToEnd();
				}
				Console.WriteLine("match Requested: " + rawUrl);
				Console.WriteLine("match Data: " + text);
				if (rawUrl.Contains("player/heartbeat"))
				{
					s = JsonConvert.SerializeObject(GameSessions.Presence());
				}
				if (rawUrl.StartsWith("/player/login"))
				{
					s = "0";
				}
				if (rawUrl == "/goto/none")
				{
                    gameinprogress = false;
                    s = JsonConvert.SerializeObject(GameSessions.Createdorm());
				}
				else if (rawUrl.StartsWith("/goto/room/"))
				{
                    gameinprogress = false;
                    s = GameSessions.CreateRoom(rawUrl.Remove(0, 11));	
                    if (Config.GameSession.roomInstance is not null)
                    {
                        Config.GameSession.roomInstance.isInProgress = gameinprogress;
                    }
				}
				else if (rawUrl.StartsWith("/goto/player/"))
				{
                    gameinprogress = false;
                    Console.WriteLine("\"/goto/player/\" api not ready. \nGoing to dormroom!");
					s = GameSessions.Createdorm();
                    if (Config.GameSession.roomInstance is not null)
                    {
                        Config.GameSession.roomInstance.isInProgress = gameinprogress;
                    }
				}
                if (rawUrl.StartsWith("/roominstance/") && rawUrl.EndsWith("/inprogress"))
                {
					gameinprogress = bool.Parse(text.Substring("inProgress=".Length));
                    if (Config.GameSession.roomInstance is not null)
					{
						Config.GameSession.roomInstance.isInProgress = gameinprogress;
                    }
                    s = "{\"Success\":true,\"Message\":\"\"}";
                }
				else if (rawUrl.StartsWith("/roominstance/"))
				{
					s = "{\"Success\":true,\"Message\":\"\"}";
				}
                if (rawUrl.StartsWith("/player/statusvisibility"))
				{
					s = "{}";
				}
				if (rawUrl.StartsWith("/player/vrmovementmode"))
				{
					s = "{}";
				}
				Console.WriteLine("match Response: " + s);
				bytes = Encoding.UTF8.GetBytes(s);
				response.ContentLength64 = (long)bytes.Length;
				Stream outputStream = response.OutputStream;
				outputStream.Write(bytes, 0, bytes.Length);
				Thread.Sleep(1);
				outputStream.Close();
				this.listener.Stop();
			}
		}
		public static string VersionCheckResponse = "{\"ValidVersion\":true}";
		public static string BlankResponse = "";

		public static bool gameinprogress;

        private HttpListener listener = new HttpListener();
	}
}
