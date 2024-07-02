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
    internal class Amplitude_2022
    {
        public static string amplitude_2022()
        {
            return JsonConvert.SerializeObject(new Amplitude_2022
            {
                AmplitudeKey = "NoKeyProvided",
                UseRudderStack = false,
                RudderStackKey = "NoKeyProvided",
                UseStatSig = false,
                StatSigKey = "NoKeyProvided",
                StatSigEnvironment = 0,
            });
        }
        public string AmplitudeKey { get; set; }
        public bool UseRudderStack { get; set; }
        public string RudderStackKey { get; set; }
        public bool UseStatSig { get; set; }
        public string StatSigKey { get; set; }
        public int StatSigEnvironment { get; set; }
    }
}
