using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using server;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ws
{
    // Token: 0x0200007B RID: 123
    internal class WebSocketHTTP
    {
        // Token: 0x060003E9 RID: 1001 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
        public WebSocketHTTP()
        {
            try
            {
                Console.WriteLine("{ws} server started!");
                WebSocketHTTP.listen.Start();
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x060003EA RID: 1002 RVA: 0x0000C034 File Offset: 0x0000A234
        public static void ADListen()
        {
            WebSocketHTTP.server.Prefixes.Add("http://localhost:20199/");
            for (; ; )
            {
                WebSocketHTTP.server.Start();
                Console.WriteLine("{ws} listening");
                HttpListenerContext context = WebSocketHTTP.server.GetContext();
                string rawUrl = context.Request.RawUrl;
                string text = "[]";
                Console.WriteLine("{ws} requested! " + rawUrl + ".");
                string text2;
                using (StreamReader streamReader = new StreamReader(context.Request.InputStream))
                {
                    text2 = streamReader.ReadToEnd();
                }
                if (rawUrl.StartsWith("/negotiate"))
                {
                    text = "{\"negotiateVersion\":0,\"connectionId\":\"notif\",\"availableTransports\":[{\"transport\":\"WebSockets\",\"transferFormats\":[\"Text\", \"Binary\"]}]}";
                    goto IL_C9;
                }
                if (!context.Request.IsWebSocketRequest)
                {
                    goto IL_C9;
                }
                Console.WriteLine("{ws} requested!");
                Console.WriteLine(text2);
                WebSocketHTTP.ProcessRequest(context);
            IL_BA:
                Console.WriteLine("{ws} connected!");
                continue;
            IL_C9:
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                context.Response.ContentLength64 = (long)bytes.Length;
                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                goto IL_BA;
            }
        }

        // Token: 0x060003EB RID: 1003 RVA: 0x0000C150 File Offset: 0x0000A350
        private static async void ProcessRequest(HttpListenerContext ctx)
        {
            HttpListenerWebSocketContext httpListenerWebSocketContext = await ctx.AcceptWebSocketAsync(null);
            CancellationTokenSource src = new CancellationTokenSource();
            System.Net.WebSockets.WebSocket ws = httpListenerWebSocketContext.WebSocket;
            while (ws.State == WebSocketState.Open)
            {
                string temp1 = "";
                string temp2 = "";
                string temp3 = "";

                temp1 = EncodeNonAsciiCharacters(temp1, '"');
                if (temp1 == null)
                {
                    temp1 = "{}";
                }
                //temp1.Replace("\"", "\\" + "u0022");
                Console.WriteLine(temp1);
                byte[] received = new byte[2048];
                int offset = 0;
                for (; ; )
                {
                    try
                    {
                        ArraySegment<byte> arraySegment = new ArraySegment<byte>(received, offset, received.Length);
                        WebSocketReceiveResult webSocketReceiveResult = await ws.ReceiveAsync(arraySegment, src.Token);
                        offset += webSocketReceiveResult.Count;
                        if (!webSocketReceiveResult.EndOfMessage)
                        {
                            continue;
                        }
                    }
                    catch
                    {
                    }
                    break;
                }
                if (offset != 0)
                {
                    string @string = Encoding.ASCII.GetString(received);
                    //Console.WriteLine(@string);
                    WebSocketHTTP.id++;
                    temp2 = JsonConvert.SerializeObject(new
                    {
                        Id = "PresenceUpdate",
                        Msg = temp1
                    });
                    byte[] array;
                    temp3 = JsonConvert.SerializeObject(new WebSocketHTTP.SockSignalR
                    {
                        type = WebSocketHTTP.MessageTypes.Invocation,
                        result = "200 OK",
                        nonblocking = true,
                        target = "Notification",
                        arguments = new object[] { JsonConvert.SerializeObject(new Respond
                        {
                            Id = "PresenceUpdate",
                            Msg = vaultgamesesh.c000020.player_heartbeat_websocket()
                        }) },
                        error = "",
                        invocationId = "1",
                        item = ""
                    });
                    if (@string.Contains("version"))
                    {
                        Console.WriteLine("{ws} game request json handshake!");
                        array = Encoding.ASCII.GetBytes("{}\u001e");
                    }
                    else if (@string.Contains("SubscribeToPlayers"))    
                    {
                        Console.WriteLine("{ws} game request presence!");
                        temp3 = JsonConvert.SerializeObject(new WebSocketHTTP.SockSignalR
                        {
                            type = WebSocketHTTP.MessageTypes.Invocation,
                            result = "200 OK",
                            nonblocking = true,
                            target = "Notification",
                            arguments = new object[] { JsonConvert.SerializeObject(new Respond
                            {
                                Id = "PresenceUpdate",
                                Msg = vaultgamesesh.c000020.player_heartbeat_websocket()
                            }) },
                            error = null,
                            invocationId = null,
                            item = null
                        });
                    }
                    Console.WriteLine(temp3 + "\u001e");

                    /*temp3 = fixNonAsciiString(temp3, '\\', 5, 1);
                    //Console.WriteLine(temp3 + "\u001e");

                    temp3 = fixNonAsciiStringset(temp3,'\\', "\\u0022");
                    //Console.WriteLine(temp3 + "\u001e");

                    temp3 = temp3.Replace("\"{\\u0022Success\\u0022:true}\"", "\\\"{\\\"success\\\":true}\\\"");
                    //Console.WriteLine(temp3 + "\u001e");

                    temp3 = temp3.Replace("{\\u0022Id\\u0022:\\u0022PresenceUpdate\\u0022,\\u0022Msg\\u0022:", "{\\\"Id\\\":\\\"PresenceUpdate\\\",\\\"Msg\\\":");*/
                    //Console.WriteLine(temp3 + "\u001e");

                    array = Encoding.ASCII.GetBytes(temp3 + "\u001e");

                    await ws.SendAsync(new ArraySegment<byte>(array, 0, array.Length), WebSocketMessageType.Text, true, src.Token);
                    received = null;
                }
                received = null;
            }
        }

        static string EncodeNonAsciiCharacters(string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
                    string encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        static string fixNonAsciiString(string value, char value1, int value2, int value3)
        {
            StringBuilder sb = new StringBuilder();
            int tempval = 0;
            foreach (char c in value)
            {
                if (value1 == c)
                {
                    tempval++;
                    // This character is too big for ASCII
                    if (tempval == value2)
                    {
                        tempval = 0;
                        sb.Append(c);
                    }
                    else if (tempval <= value3)
                    {
                        sb.Append(c);
                    }

                }
                else
                {
                    tempval = 0;
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        static string fixNonAsciiStringdata(string value, char value2, string value1, string value3)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            int tempval = 0;
            int tempval1 = 0;
            int tempval2 = 0;
            int skipval = 0;
            bool skip = false;
            char t = 'a';
            foreach (char c in value)
            {
                if (skipval > 0)
                {
                    skipval = -1;
                    tempval++;
                    continue;
                }
                skiptoadd:
                if (value2 == c && !skip)
                {
                    t = value[tempval + 1];
                    if (t != value1[1])
                    {
                        skip = true;
                        goto skiptoadd;
                    }
                    foreach (char r in value1)
                    {
                        sb2.Append(r);
                        if (r == value[tempval + 1])
                        tempval1 ++;

                    }

                    skipval = 2;
                    tempval++;
                    continue;
                }
                else
                {
                    sb.Append(c);
                }
                skip = false;
                tempval++;
            }
            return sb.ToString();
        }
        static string fixNonAsciiStringset(string value, char value2, string value3)
        {
            StringBuilder sb = new StringBuilder();
            int tempval = 0;
            int skipval = 0;
            char t = 'a';
            foreach (char c in value)
            {
                if (skipval > 0)
                {
                    skipval =- 1;
                    tempval++;
                    continue;
                }
                if (value2 == c)
                {
                    t = value[tempval + 1];
                    if (t == '"')
                    {
                        foreach (char r in value3)
                        {
                            sb.Append(r);  
                        }
                        skipval = 2;
                        tempval++;  
                        continue;
                    }
                    if (t == 'u' || t == 'U')
                    { 
                        sb.Append(c);
                        sb.Append(t);
                        skipval = 2;
                        tempval++;
                        continue;
                    }
                }
                else
                {
                    sb.Append(c);
                }
                tempval++;
            }
            return sb.ToString();
        }
        static string EncodeNonAsciiCharacters(string value, char value1)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c > 127 || value1 == c)
                {
                    // This character is too big for ASCII
                    string encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        static string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-fA-F0-9]{4})",
                m => {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }

        public class Respond
        {
            public object Id { get; set; }
            public object Msg { get; set; }
        }   

        // Token: 0x04000294 RID: 660
        public static HttpListener server = new HttpListener();

        // Token: 0x04000295 RID: 661
        public static Thread listen = new Thread(new ThreadStart(WebSocketHTTP.ADListen));

        // Token: 0x04000296 RID: 662
        public static Dictionary<string, string> missingApis;

        // Token: 0x04000297 RID: 663
        public static int id = 1;

        // Token: 0x0200007C RID: 124
        public class SockSignalR
        {
            // Token: 0x04000298 RID: 664
            public WebSocketHTTP.MessageTypes type;

            // Token: 0x04000299 RID: 665
            public string invocationId;

            // Token: 0x0400029A RID: 666
            public bool nonblocking;

            // Token: 0x0400029B RID: 667
            public string target;

            // Token: 0x0400029C RID: 668
            public object[] arguments;

            // Token: 0x0400029D RID: 669
            public object item;

            // Token: 0x0400029E RID: 670
            public object result;

            // Token: 0x0400029F RID: 671
            public string error;
        }

        // Token: 0x0200007D RID: 125
        public enum MessageTypes
        {
            // Token: 0x040002A1 RID: 673
            Handshake,
            // Token: 0x040002A2 RID: 674
            Invocation,
            // Token: 0x040002A3 RID: 675
            StreamItem,
            // Token: 0x040002A4 RID: 676
            Completion,
            // Token: 0x040002A5 RID: 677
            StreamInvocation,
            // Token: 0x040002A6 RID: 678
            CancelInvocation,
            // Token: 0x040002A7 RID: 679
            Ping,
            // Token: 0x040002A8 RID: 680
            Close
        }
    }
}
