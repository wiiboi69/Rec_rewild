using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Rec_rewild.api.route
{
    public class rewild_route_system
    {
        public static object ParseJsonBody(string body, Type targetType)
        {
            return JsonConvert.DeserializeObject(body, targetType);
        }

        public static string ParseRequestBody(HttpListenerRequest request)
        {
            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            return reader.ReadToEnd(); 
        }

        public static Dictionary<string, string> ParseFormData(string body)
        {
            var formData = new Dictionary<string, string>();
            var parsedQuery = HttpUtility.ParseQueryString(body);
            foreach (var key in parsedQuery.AllKeys)
            {
                formData[key] = parsedQuery[key];
            }
            return formData;
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
        public class RouteAttribute : Attribute
        {
            public string Path { get; }
            public RouteAttribute(string path)
            {
                Path = path;
            }
        }
    }
}