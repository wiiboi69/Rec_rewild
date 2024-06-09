using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.AccessControl;
using start;
using System.Security.Principal;
using api;

namespace server
{
    internal class APIServer
    {
        public APIServer()
        {
            try
            {
                Console.WriteLine("[APIServer.cs] has started.");
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
                this.listener.Prefixes.Add("http://localhost:" + Program.version + "0/");
                {
                    for (; ; )
                    {
                        this.listener.Start();
                        Console.WriteLine("[APIServer.cs] is listening.");
                        HttpListenerContext context = this.listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;
                        List<byte> list = new List<byte>();
                        string rawUrl = request.RawUrl;
                        string Url = "";
                        byte[] bytes = null;
                        string signature = request.Headers.Get("X-RNSIG");
                        if (rawUrl.StartsWith("/api/"))
                        {
                            Url = rawUrl.Remove(0, 5);
                        }
                        if (!(Url == ""))
                        {
                            Console.WriteLine("API Requested: " + Url);
                        }
                        else
                        {
                            Console.WriteLine("API Requested (rawUrl): " + rawUrl);
                        }
                        string text;
                        string s = "";
                        using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
                        {
                            text = streamReader.ReadToEnd();
                        }
                        if (text.Length > 0xfff)
                        {
                            Console.WriteLine("API Data: unviewable");
                        }
                        else
                        {
                            Console.WriteLine("API Data: " + text);
                        }
                        if (Url.StartsWith("versioncheck"))
                        {
                            CachedversionID = ulong.Parse(Url.Substring(18, 8));
                            Console.WriteLine(CachedversionID);
                            s = VersionCheckResponse;
                        }
                        if (Url == "settings/v2/")
                        {
                            s = File.ReadAllText("SaveData\\settings.txt");
                        }
                        if (Url == "settings/v2/set")
                        {
                            Settings.SetPlayerSettings(text);
                        }
                        if (s.Length > 400)
                        {
                            Console.WriteLine("api Response: " + s.Length);
                        }
                        else
                        {
                            Console.WriteLine("api Response: " + s);
                        }

                        bytes = Encoding.UTF8.GetBytes(s);
                        response.ContentLength64 = (long)bytes.Length;
                        Stream outputStream = response.OutputStream;
                        outputStream.Write(bytes, 0, bytes.Length);
                        Thread.Sleep(40);
                        outputStream.Close();
                        this.listener.Stop();

                    }
                }
            }
            catch (Exception ex4)
            {
                Console.WriteLine(ex4);
                File.WriteAllText("crashdump.txt", Convert.ToString(ex4));
                this.listener.Close();
                new APIServer();
            }
        }
        public static ulong CachedPlayerID = ulong.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt"));

        public static ulong CachedPlatformID = 10000;
        public static ulong CachedversionID = 20206000;
        public static int CachedVersionMonth = 01;

        public static string BlankResponse = "";
        public static string BracketResponse = "[]";
        public static string VersionCheckResponse = "{\"VersionStatus\":0}";

        private HttpListener listener = new HttpListener();
    }
}