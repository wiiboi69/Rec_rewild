using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace server
{
    internal class NotificationsServer
    {
        public NotificationsServer()
        {
            try
            {
                Console.WriteLine("[NotificationsServer.cs] has started.");
                new Thread(new ThreadStart(this.StartListen)).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Exception Occurred while Listening :" + ex.ToString());
            }
        }
        private void StartListen()
        {
            this.listener.Prefixes.Add("http://localhost:20212/");
            for (; ; )
            {
                this.listener.Start();
                Console.WriteLine("{NotificationsServer.cs] is listening.");
                HttpListenerContext context = this.listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                string rawUrl = request.RawUrl;
                string text = "";
                string text2;
                using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    text2 = streamReader.ReadToEnd();
                }
                if (!(text2 == ""))
                {
                    Console.WriteLine("Notifications Requested: " + text2);
                }
                else
                {
                    Console.WriteLine("Notifications Requested (rawUrl): " + rawUrl);
                }
                if (rawUrl.StartsWith("/hub/v1/negotiate"))
                {
                    text = JsonConvert.SerializeObject(new
                    {
                        ConnectionId = "skull",
                        negotiateVersion = 0,
                        SupportedTransports = new List<string>(),
                        url = new Uri("ws://localhost:20199/")
                    });
                }
                if (rawUrl.StartsWith("versioncheck"))
                {
                    text = APIServer.VersionCheckResponse;
                }
                Console.WriteLine("Notifications Data: " + text2);
                Console.WriteLine("Notifications Response: " + text);
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                response.ContentLength64 = (long)bytes.Length;
                response.OutputStream.Write(bytes, 0, bytes.Length);
                Thread.Sleep(1);
                this.listener.Stop();
            }
        }
        public static string VersionCheckResponse = "{\"ValidVersion\":true}";
        public static string BlankResponse = "";

        private HttpListener listener = new HttpListener();
    }
}
