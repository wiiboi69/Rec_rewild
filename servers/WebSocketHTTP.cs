/*
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

namespace server
{
    internal class WebSocketHTTP
    {
        public WebSocketHTTP()
        {
            try
            {
                Console.WriteLine("{ws} server started!");
                WebSocketHTTP.listen.Start();
                WebSocketHTTP.server.Start();
            }
            catch (Exception)
            {
            }
        }
        public static void ADListen()
        {
            WebSocketHTTP.server.Prefixes.Add("http://localhost:20199/");
            for (; ; )
            {
                Console.WriteLine("{ws} listening");
                HttpListenerContext context = server.GetContext();
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
                            //Msg = ""//vaultgamesesh.c000020.player_heartbeat_websocket()
                            Msg = GameSessions.Presence()
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
                                //Msg = ""//vaultgamesesh.c000020.player_heartbeat_websocket()
                                Msg = GameSessions.Presence()
                            }) },
                            error = null,
                            invocationId = null,
                            item = null
                        });
                    }
                    Console.WriteLine(temp3 + "\u001e");

                    array = Encoding.ASCII.GetBytes(temp3 + "\u001e");

                    await ws.SendAsync(new ArraySegment<byte>(array, 0, array.Length), WebSocketMessageType.Text, true, src.Token);
                    received = null;
                }
                received = null;
            }
        }



        public static async void SendRequest(string json)
        {
            HttpListenerContext context = WebSocketHTTP.server.GetContext();
            HttpListenerWebSocketContext httpListenerWebSocketContext = await context.AcceptWebSocketAsync(null);
            CancellationTokenSource src = new CancellationTokenSource();
            byte[] array = Encoding.ASCII.GetBytes(json + "\u001e");
            await httpListenerWebSocketContext.WebSocket.SendAsync(new ArraySegment<byte>(array, 0, array.Length), WebSocketMessageType.Text, true, src.Token);

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
            //int tempval2 = 0;
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

        public enum ResponseResults
        {
            // Token: 0x0400C7F3 RID: 51187
            RelationshipChanged = 1,
            // Token: 0x0400C7F4 RID: 51188
            MessageReceived,
            // Token: 0x0400C7F5 RID: 51189
            MessageDeleted,
            // Token: 0x0400C7F6 RID: 51190
            PresenceHeartbeatResponse,
            // Token: 0x0400C7F7 RID: 51191
            RefreshLogin,
            // Token: 0x0400C7F8 RID: 51192
            Logout,
            // Token: 0x0400C7F9 RID: 51193
            SubscriptionUpdateProfile = 11,
            // Token: 0x0400C7FA RID: 51194
            SubscriptionUpdatePresence,
            // Token: 0x0400C7FB RID: 51195
            SubscriptionUpdateGameSession,
            // Token: 0x0400C7FC RID: 51196
            SubscriptionUpdateRoom = 15,
            // Token: 0x0400C7FD RID: 51197
            SubscriptionUpdateRoomPlaylist,
            // Token: 0x0400C7FE RID: 51198
            ModerationQuitGame = 20,
            // Token: 0x0400C7FF RID: 51199
            ModerationUpdateRequired,
            // Token: 0x0400C800 RID: 51200
            ModerationKick,
            // Token: 0x0400C801 RID: 51201
            ModerationKickAttemptFailed,
            // Token: 0x0400C802 RID: 51202
            ModerationRoomBan,
            // Token: 0x0400C803 RID: 51203
            ServerMaintenance,
            // Token: 0x0400C804 RID: 51204
            GiftPackageReceived = 30,
            // Token: 0x0400C805 RID: 51205
            GiftPackageReceivedImmediate,
            // Token: 0x0400C806 RID: 51206
            GiftPackageRewardSelectionReceived,
            // Token: 0x0400C807 RID: 51207
            ProfileJuniorStatusUpdate = 40,
            // Token: 0x0400C808 RID: 51208
            RelationshipsInvalid = 50,
            // Token: 0x0400C809 RID: 51209
            StorefrontBalanceAdd = 60,
            // Token: 0x0400C80A RID: 51210
            StorefrontBalanceUpdate,
            // Token: 0x0400C80B RID: 51211
            StorefrontBalancePurchase,
            // Token: 0x0400C80C RID: 51212
            ConsumableMappingAdded = 70,
            // Token: 0x0400C80D RID: 51213
            ConsumableMappingRemoved,
            // Token: 0x0400C80E RID: 51214
            PlayerEventCreated = 80,
            // Token: 0x0400C80F RID: 51215
            PlayerEventUpdated,
            // Token: 0x0400C810 RID: 51216
            PlayerEventDeleted,
            // Token: 0x0400C811 RID: 51217
            PlayerEventResponseChanged,
            // Token: 0x0400C812 RID: 51218
            PlayerEventResponseDeleted,
            // Token: 0x0400C813 RID: 51219
            PlayerEventStateChanged,
            // Token: 0x0400C814 RID: 51220
            ChatMessageReceived = 90,
            // Token: 0x0400C815 RID: 51221
            CommunityBoardUpdate = 95,
            // Token: 0x0400C816 RID: 51222
            CommunityBoardAnnouncementUpdate,
            // Token: 0x0400C817 RID: 51223
            InventionModerationStateChanged = 100,
            // Token: 0x0400C818 RID: 51224
            FreeGiftButtonItemsAdded = 110,
            // Token: 0x0400C819 RID: 51225
            LocalRoomKeyCreated = 120,
            // Token: 0x0400C81A RID: 51226
            LocalRoomKeyDeleted
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
*/