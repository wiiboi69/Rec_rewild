using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace server
{
    // Token: 0x02000070 RID: 112
    internal class NotificationsServer
    {
        // Token: 0x06000396 RID: 918 RVA: 0x0000B0AC File Offset: 0x000092AC
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

        // Token: 0x06000397 RID: 919 RVA: 0x0000B114 File Offset: 0x00009314
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
                        url = new Uri("http://localhost:20199/")
                    });
                }
                if (rawUrl.StartsWith("versioncheck"))
                {
                    text = APIServer.VersionCheckResponse;
                }
                Console.WriteLine("Notifications Data: " + text2);
                Console.WriteLine("Notifications Response: ");
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                response.ContentLength64 = (long)bytes.Length;
                response.OutputStream.Write(bytes, 0, bytes.Length);
                Thread.Sleep(1);
                this.listener.Stop();
            }
        }

        // Token: 0x0400026A RID: 618
        public static string VersionCheckResponse = "{\"ValidVersion\":true}";

        // Token: 0x0400026B RID: 619
        public static string BlankResponse = "";

        // Token: 0x0400026C RID: 620
        private HttpListener listener = new HttpListener();
    }
}
