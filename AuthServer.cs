using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using api;
using api2017;
using api2018;
using api2019;
using Newtonsoft.Json;
using vaultgamesesh;
using System.Collections.Generic;
using System.Security.AccessControl;
using Client;
using static Client.ClientSecurity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace server
{
    internal class AuthServer
    {
        public AuthServer()
        {
            try
            {
                Console.WriteLine("[AuthServer.cs] has started.");
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

                this.listener.Prefixes.Add("http://localhost:" + start.Program.version + "4/");
                {
                    for (; ; )
                    {
                        this.listener.Start();
                        Console.WriteLine("[AuthServer.cs] is listening.");
                        HttpListenerContext context = this.listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;
                        List<byte> list = new List<byte>();
                        string rawUrl = request.RawUrl;
                        string Url = "";
                        byte[] bytes = null;
                        string signature = request.Headers.Get("X-RNSIG");
                        if (rawUrl.StartsWith("/Auth/"))
                        {
                            Url = rawUrl.Remove(0, 5);
                        }
                        if (!(Url == ""))
                        {
                            Console.WriteLine("Auth Requested: " + Url);
                        }
                        else
                        {
                            Console.WriteLine("Auth Requested (rawUrl): " + rawUrl);
                        }
                        string temp1 = "";
                        string temp2 = "";
                        string temp3 = "";
                        string temp4 = "";
                        string text;
                        string s = "";
                        using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
                        {
                            text = streamReader.ReadToEnd();
                        }
                        Console.WriteLine("Auth Data: " + text);
                        if (rawUrl == "/eac/challenge")
                        {
                            s = challengesResponse;
                            Thread.Sleep(100);

                        }

                        else if (rawUrl == "/connect/token")
                        {
                            if (APIServer.CachedversionID >= 20200000 - 1 && APIServer.CachedversionID <= 20200600 - 1)
                            {
                                s = tokenResponse;
                            }
                            else 
                            { 
                                temp1 = ClientSecurity.GenerateToken();
                                Guid myuuid = Guid.NewGuid();
                                temp2 = myuuid.ToString();
                                /*s = JsonConvert.SerializeObject(new ClientSecurity.auth_token_data
                                {
                                    access_token = temp1,
                                    error = "",
                                    error_description = "",
                                    key = "",
                                    refresh_token = temp2,
                                }.ToString());
                                */
                                s = "{\"access_token\":\"" + temp1 + "\",\"error_description\":\"\",\"error\":\"\",\"refresh_token\":\"" + temp2+"\",\"key\":\"\"}";
                            }
                            Thread.Sleep(100);

                        }
                        else if (rawUrl.StartsWith("/cachedlogin/forplatformids"))
                        {
                            s = AccountAuth.CachedLogins();
                        }
                        else if (rawUrl.StartsWith("/cachedlogin/forplatformid/"))
                        {
                            s = AccountAuth.CachedLogins();

                        }

                        Console.WriteLine("Auth Response: " + s);
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
                File.WriteAllText("crashdump-auth.txt", Convert.ToString(ex4));
                this.listener.Close();
                new AuthServer();
            }
        }
        public static ulong CachedPlayerID = ulong.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt"));
        public static ulong CachedPlatformID = 10000;
        public static int CachedVersionMonth = 01;

        public static string BlankResponse = "";
        public static string BracketResponse = "[]";

        public static string challengesResponse = "\"5a01a780-b011-4129-abbc-2474cfe2806e\"";

        public static string tokenResponse = "{\"access_token\":\"eyJhbGciOiJSUzI1NiIsImtpZCI6IkREU2F1d2dZdkE1S1NEcVFQWHJRbk1ZeXhMbyIsInR5cCI6ImF0K2p3dCIsIng1dCI6IkREU2F1d2dZdkE1S1NEcVFQWHJRbk1ZeXhMbyJ9.eyJuYmYiOjE2Njk1NzUzOTksImV4cCI6MTY2OTU3ODk5OSwiaXNzIjoiaHR0cHM6Ly9hdXRoLnJlYy5uZXQiLCJjbGllbnRfaWQiOiJyZWNuZXQiLCJyb2xlIjoid2ViQ2xpZW50Iiwic3ViIjoiNjIyNjgwNyIsImF1dGhfdGltZSI6MTY1Nzc3Mzk1NSwiaWRwIjoibG9jYWwiLCJqdGkiOiJEOUUwNTY2QjU2NTE4QkNEMjBBNjRDMkQ2MzUwQzRFMyIsInNpZCI6IjU2NEY5QUFGQzNBRjQxREQwQTQzOENDMTlFODk5NzYzIiwiaWF0IjoxNjY5NTc1Mzk5LCJzY29wZSI6WyJvcGVuaWQiLCJybi5hcGkiLCJybi5jb21tZXJjZSIsInJuLm5vdGlmeSIsInJuLm1hdGNoLnJlYWQiLCJybi5jaGF0Iiwicm4uYWNjb3VudHMiLCJybi5hdXRoIiwicm4ubGluayIsInJuLmNsdWJzIiwicm4ucm9vbXMiLCJybi5kaXNjb3ZlcnkiXSwiYW1yIjpbIm1mYSJdfQ.TVkpz8Nbmz_8fFdbf3xI0CHwjogaIR45TmhK4NXSgx__e85M0xNO8UDSbGJaUMeSN7rn_I1obrzvqqJhDjqOAyQs39rtKJ-lyMq_oFDf1DOjFhB_KWCQ3V_N1SIOpoTnzoD7kr3voixtB4VrTo1HkUQPK_6a2FvUfg3sNwBBAxVvSv7jRPF5_BLGLRACfT3vIHfM7baSOFYkgijnGu9Okd4XKCSolb0hBO14vRMSUZ_gzdm2YubWEF5PK4kiIKMLnnvqUIAXt37sn0m7SjFK_7CI5K7TcSGJcnO-r63PaKsH3UfPqkTq6QWJKUh9X59mQcUJ6iClkY6Pv8LZWjqpkg\",\"refresh_token\":\"eyJhbGciOiJSUzI1NiIsImtpZCI6IkREU2F1d2dZdkE1S1NEcVFQWHJRbk1ZeXhMbyIsInR5cCI6ImF0K2p3dCIsIng1dCI6IkREU2F1d2dZdkE1S1NEcVFQWHJRbk1ZeXhMbyJ9.eyJuYmYiOjE2Njk1NzUzOTksImV4cCI6MTY2OTU3ODk5OSwiaXNzIjoiaHR0cHM6Ly9hdXRoLnJlYy5uZXQiLCJjbGllbnRfaWQiOiJyZWNuZXQiLCJyb2xlIjoid2ViQ2xpZW50Iiwic3ViIjoiNjIyNjgwNyIsImF1dGhfdGltZSI6MTY1Nzc3Mzk1NSwiaWRwIjoibG9jYWwiLCJqdGkiOiJEOUUwNTY2QjU2NTE4QkNEMjBBNjRDMkQ2MzUwQzRFMyIsInNpZCI6IjU2NEY5QUFGQzNBRjQxREQwQTQzOENDMTlFODk5NzYzIiwiaWF0IjoxNjY5NTc1Mzk5LCJzY29wZSI6WyJvcGVuaWQiLCJybi5hcGkiLCJybi5jb21tZXJjZSIsInJuLm5vdGlmeSIsInJuLm1hdGNoLnJlYWQiLCJybi5jaGF0Iiwicm4uYWNjb3VudHMiLCJybi5hdXRoIiwicm4ubGluayIsInJuLmNsdWJzIiwicm4ucm9vbXMiLCJybi5kaXNjb3ZlcnkiXSwiYW1yIjpbIm1mYSJdfQ.TVkpz8Nbmz_8fFdbf3xI0CHwjogaIR45TmhK4NXSgx__e85M0xNO8UDSbGJaUMeSN7rn_I1obrzvqqJhDjqOAyQs39rtKJ-lyMq_oFDf1DOjFhB_KWCQ3V_N1SIOpoTnzoD7kr3voixtB4VrTo1HkUQPK_6a2FvUfg3sNwBBAxVvSv7jRPF5_BLGLRACfT3vIHfM7baSOFYkgijnGu9Okd4XKCSolb0hBO14vRMSUZ_gzdm2YubWEF5PK4kiIKMLnnvqUIAXt37sn0m7SjFK_7CI5K7TcSGJcnO-r63PaKsH3UfPqkTq6QWJKUh9X59mQcUJ6iClkY6Pv8LZWjqpkg\",\"key\":\"\"}";

        private HttpListener listener = new HttpListener();
    }
}
