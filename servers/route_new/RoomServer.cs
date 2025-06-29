using api;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using Rec_rewild.api.route;
using server;
using start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rec_rewild.servers.route_new
{
    class RoomServer2021_new
    {
        public static RoomServer2021_new RoomServer;
        public RoomServer2021_new()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:" + start.Program.version + "8/");

            RegisterRoutes();

            new Thread(new ThreadStart(this.Start)).Start();

            Running = true;
        }
        public static bool Running = false;
        private readonly HttpListener _listener;
        public void RefreshApplicationState(object sender, FileSystemEventArgs e)
        {
            RegisterRoutes(true);
        }
        public static void reloadRegisterRoutes()
        {
            RoomServer.RegisterRoutes(true);
        }

        #region server_handling
        private readonly List<(Regex, MethodInfo, ParameterInfo[])> _routeHandlers = new();
        private void RegisterRoutes(bool reload = false)
        {
            if (reload)
            {
                Console.WriteLine("RoomServer2021 [DEBUG]: Reregistering all of the Route");
                _routeHandlers.Clear();
            }
            else
                Console.WriteLine("RoomServer2021: Registering Route");

            foreach (var method in Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)))
            {
                var routeAttribute = method.GetCustomAttribute<rewild_route_system.RouteAttribute>();
                if (routeAttribute != null)
                {

                    var pattern = "^" + Regex.Escape(routeAttribute.Path)
                        .Replace("\\*", ".*")
                        .Replace("\\{", "(?<")
                        .Replace("}", ">[^/]+)")
                        + "$";
                    var regex = new Regex(pattern, RegexOptions.Compiled);

                    var parameters = method.GetParameters();
                    _routeHandlers.Add((regex, method, parameters));
                }
            }
        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine("RoomServer2021: Listening...");
            while (true)
            {
                var context = _listener.GetContext();
                try
                {
                    ProcessRequest(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private void ProcessRequest(HttpListenerContext context)
        {
            var path = context.Request.Url.AbsolutePath;
            var query = context.Request.QueryString;
            string contentType = context.Request.ContentType;
            if (string.IsNullOrEmpty(contentType))
                contentType = "none";
            Console.WriteLine($"RoomServer2021: Start of request.");

            Console.WriteLine($"RoomServer2021: Room Requested: {path}");
            Console.WriteLine($"RoomServer2021: Content-Type: {contentType}");
            Console.WriteLine($"RoomServer2021: Request-Method-Type: {context.Request.HttpMethod}");

            var response = context.Response;
            object response_data = null;

            foreach (var (regex, method, parameters) in _routeHandlers)
            {
                var match = regex.Match(path);
                if (match.Success)
                {
                    var args = new object[parameters.Length];
                    string body = "";
                    if (context.Request.ContentType == "application/json" || context.Request.ContentType == "application/x-www-form-urlencoded")
                    {
                        body = rewild_route_system.ParseRequestBody(context.Request);
                        Console.WriteLine($"RoomServer2021: Room Data: {body}");
                    }

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var paramType = parameters[i].ParameterType;
                        var paramName = parameters[i].Name;

                        if (paramType == typeof(HttpListenerContext))
                        {
                            args[i] = context;
                        }
                        else if (paramType == typeof(Dictionary<string, string>) && context.Request.ContentType == "application/x-www-form-urlencoded")
                        {
                            body = rewild_route_system.ParseRequestBody(context.Request);
                            args[i] = rewild_route_system.ParseFormData(body);
                        }
                        else if (context.Request.ContentType == "application/json")
                        {
                            args[i] = rewild_route_system.ParseJsonBody(body, paramType);
                        }
                        else if (paramType == typeof(string) && query != null && query[paramName] != null)
                        {
                            args[i] = query[paramName];
                        }
                        else if (
                            (paramType == typeof(int) || paramType == typeof(uint) || paramType == typeof(long) ||
                             paramType == typeof(ulong) || paramType == typeof(string))
                            && match.Groups[paramName] != null && match.Groups[paramName].Success)
                        {
                            args[i] = Convert.ChangeType(match.Groups[paramName].Value, paramType);
                        }
                        else if (
                            (paramType == typeof(int) || paramType == typeof(uint) || paramType == typeof(long) ||
                             paramType == typeof(ulong) || paramType == typeof(string))
                            && context.Request.ContentType == "application/x-www-form-urlencoded")
                        {
                            var formData = rewild_route_system.ParseFormData(body);
                            if (formData.ContainsKey(paramName))
                                args[i] = Convert.ChangeType(formData[paramName], paramType);
                            else
                                args[i] = paramType.IsValueType ? Activator.CreateInstance(paramType) : null;
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unsupported parameter type or missing parameter: {paramType} / {paramName}");
                        }
                    }

                    var result = method.Invoke(null, args);

                    if (method.ReturnType == typeof(void))
                    {
                        response.StatusCode = 200;
                        response_data = "{\"success\":\"true\"}";
                    }
                    else
                    {
                        response_data = result;
                    }
                    break;
                }
            }

            if (response_data == null)
            {
                response_data = HandleNotFound(context);
                response.StatusCode = 404;
            }

            WriteResponse(response, response_data);
        }

        private static void WriteResponse(HttpListenerResponse response, object response_data)
        {

            if (response_data is string responseString)
            {
                if (responseString.Length <= 0x1ff)
                {
                    Console.WriteLine($"RoomServer2021: Room Json Response: " + responseString);
                }
                else
                {
                    Console.WriteLine($"RoomServer2021: Room Json Response Length: " + responseString.Length);
                }
                var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentType = "application/json";
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else if (response_data is byte[] responseBytes)
            {
                Console.WriteLine($"RoomServer2021: Room byte[] Response Length: " + responseBytes.Length);

                response.ContentType = "application/octet-stream";
                response.ContentLength64 = responseBytes.Length;
                response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
            }
            else
            {
                throw new InvalidOperationException("Unsupported response data type.");
            }
            Console.WriteLine($"RoomServer2021: End of request.\n");

            response.OutputStream.Close();
        }

        private string HandleNotFound(HttpListenerContext context)
        {
            return "{\"Success\": false, \"Error\": \"404 URL Not Found: " + context.Request.Url + "\"}";
        }
        #endregion

        public static bool gameinprogress;

        [rewild_route_system.Route("/player/login")]
        public static string PlayerLogin()
        {
            Console.WriteLine($"game requesting matchmaking login");
            return APIServer2021_new.BlankResponse;
        }

        [rewild_route_system.Route("/goto/none")]
        public static string GoToNone()
        {
            gameinprogress = false;
            return JsonConvert.SerializeObject(GameSessions.Createdorm());
        }

        [rewild_route_system.Route("/goto/room/{room}")]
        public static string GoToRoom(string room)
        {
            Console.WriteLine($"game requesting goto room: {room}");
            gameinprogress = false;

            var tmp = GameSessions.Createroom(room ?? "DormRoom", "");
            return tmp;
        }


        [rewild_route_system.Route("/player/heartbeat")]
        public static string Heartbeat()
        {
            Console.WriteLine($"game requesting heartbeat");
            return JsonConvert.SerializeObject(GameSessions.Presence());
        }

        [rewild_route_system.Route("/player/statusvisibility")]
        public static string StatusVisibility()
        {
            Console.WriteLine($"game requesting Status Visibility");
            return "{}";
        }

        [rewild_route_system.Route("/player/vrmovementmode")]
        public static string VRMovementMode()
        {
            Console.WriteLine($"game requesting VRMovementMode");
            return "{}";
        }

        [rewild_route_system.Route("/rooms/createdby/me")]
        public static string RoomsCreatedByMe()
        {
            Console.WriteLine($"game requesting RoomsCreatedByMe");
            return APIServer2021_new.BracketResponse;
        }

        [rewild_route_system.Route("/rooms/{id}")]
        public static string GetRoomById(string id)
        {
            string fullUrl = "/rooms/" + id;
            int offset = "/rooms/".Length;

            var raw = room_util.find_room_with_id(fullUrl, offset);
            var withCreator = room_util.room_change_CreatorAccount(raw);

            if (APIServer.CachedversionID > 20210899)
                return room_util.room_change_fix_room(withCreator);
            return withCreator;
        }

        [rewild_route_system.Route("/rooms")]
        public static string GetRoomByName(string name, string? include = null)
        {
            Console.WriteLine($"{name}.txt");

            try
            {
                return room_util.room_find_CustomRooms(name);
            }
            catch
            {
                using var httpClient = new HttpClient();
                string fallback = httpClient.GetStringAsync(
                    $"https://raw.githubusercontent.com/wiiboi69/Rec_rewild_server_data/main/rooms_name/{name.ToLower()}.txt"
                ).GetAwaiter().GetResult(); 

                if (APIServer.CachedversionID > 20210899)
                {
                    fallback = room_util.room_change_fix_room(fallback);
                }

                return fallback;
            }
        }
    }
}