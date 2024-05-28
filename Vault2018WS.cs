using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using start;
using WebSocketSharp;
using WebSocketSharp.Server;
using ws;

namespace vaultgamesesh
{
	public class Late2018WebSock
	{
		public Late2018WebSock()
		{
			Late2018WebSock.instance = this;
			this.WebSock.AddWebSocketService<Late2018WebSock.NotificationWS>("/api/notification/v2");
			this.WebSock.AddWebSocketService<Late2018WebSock.HubWS>("/hub/v1");
			this.WebSock.Start();
			Console.WriteLine("[LateWebSocket.cs] has started.");
			Console.WriteLine("[LateWebSocket.cs] is listening.");
		}

		public void Broadcast(Notification.Reponse res)
		{
			Console.WriteLine(string.Concat(new string[]
			{
				"Broadcasting ",
				JsonConvert.SerializeObject(res),
				" to ",
				this.WebSock.WebSocketServices["/api/notification/v2"].Sessions.Count.ToString(),
				" clients."
			}));

			WebSock.WebSocketServices["/api/notification/v2"].Sessions.Broadcast(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res)));
		}

		public static Late2018WebSock instance;

		public WebSocketServer WebSock = new WebSocketServer("ws://localhost:20219/");

		public class HubWS : WebSocketBehavior
		{
			protected override void OnMessage(MessageEventArgs e)
			{
				Console.WriteLine("LateWebSocket.cs Hub Requested.");
				base.Send(JsonConvert.SerializeObject(new Late2018WebSock.Hub()));
			}

			public HubWS()
			{
			}
		}

		public class Hub : WebSocketBehavior
		{
			public Hub()
			{
				this.accessToken = "AccessDeezNuts";
				this.SupportedTransports = new List<string>();
				this.negotiateVersion = 0;
				this.url = new Uri(string.Format("http://localhost:{0}/", "20212"));
			}

			public Uri url { get; set; }

			public string accessToken { get; set; }

			public List<string> SupportedTransports { get; set; }

			public int negotiateVersion { get; set; }
		}

		public class NotificationWS : WebSocketBehavior
		{
			protected override void OnMessage(MessageEventArgs p0)
			{
				
				Console.WriteLine("LateWebSocket.cs Notif Requested.");
				bool flag2 = p0.Data == null;
				bool flag3 = flag2;
				bool flag4 = flag3;
				if (flag4)
				{
					base.Send(string.Empty);
				}
				else
				{
					base.Send(Notification2018.ProcessRequest(p0.Data));
				}
			}

			public NotificationWS()
			{
			}
		}
	}
}
