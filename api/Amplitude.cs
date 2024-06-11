using System;
using Newtonsoft.Json;

namespace api
{
    internal class Amplitude
    {
        public static string amplitude()
        {
            return JsonConvert.SerializeObject(new Amplitude
            {
                AmplitudeKey = "NoKeyProvided"
            });
        }
        public string AmplitudeKey { get; set; }
    }
}
