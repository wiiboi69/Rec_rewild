
using Microsoft.Win32;
using Newtonsoft.Json;
using server;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using static api2019.AccountAuth;

namespace Client
{
    public class ClientSecurity
    {
        public static string GenerateToken()
        {
            string temp1 = "";
            string temp2 = "{\"alg\":\"HS256\",\"typ\":\"JWT\"}";
            ClientSecurity.Auth auth = new ClientSecurity.Auth();

            DateTime dateTime = DateTime.UtcNow;
            dateTime = dateTime.AddDays(1.0);
            dateTime = dateTime.Date;
            temp1 = EncodeTo64(JsonConvert.SerializeObject( new
            {

                nbf = DateTime.UtcNow.Ticks,
                exp = dateTime.Ticks,
                iss = "http://localhost:2021",
                aud = new List<string>()
                {
                    "http://localhost:2021",
                    "http://localhost:20211",
                    "http://localhost:20212",
                    "http://localhost:20213",
                    "http://localhost:20214",
                    "http://localhost:20215",
                    "http://localhost:20216",
                    "http://localhost:20217",
                    "http://localhost:20218",
                    "http://localhost:20219",
                    "rr.api",
                    "rr.commerce",
                    "rr.rooms",
                    "rr.storage",
                    "rr.match",
                    "rr.leaderboard",
                    "rr.auth",
                    "rr.chat",
                    "rr.accounts"
                },
                client_id = "recroom",
                role = new List<String> 
                {
                    "gameClient", 
                    "screenshare",
                },
                RnPlat = "0",
                RnPlatid = "1",
                RnDeviceclass = "2",
                RnVer = APIServer.CachedversionID.ToString(),
                idp = "local",
                sub = GetMID().ToString(),
                RnAsid = DateTime.UtcNow.Ticks.ToString(),
                RnSk = "4289734123",
                iat = DateTime.UtcNow.Ticks,
                auth_time = DateTime.UtcNow.Ticks,
                RnPid = "3781123978",
                scope = new List<string>()
                {
                    "rn.accounts",
                    "rn.accounts.gc",
                    "rn.api",
                    "rn.auth",
                    "rn.auth.gc",
                    "rn.chat",
                    "rn.clubs",
                    "rn.commerce",
                    "rn.leaderboard",
                    "rn.link",
                    "rn.match.read",
                    "rn.match.write",
                    "rn.notify",
                    "rn.roomcomments",
                    "rn.rooms",
                    "rn.storage",
                    "offline_access"
                },
                amr = new List<string>() { "cached_login" },       
                }
            ));
            Console.WriteLine(temp1 + "\n");
            return EncodeTo64(temp2) + "" + temp1 + "" + "TVkpz8Nbmz_8fFdbf3xI0CHwjogaIR45TmhK4NXSgx__e85M0xNO8UDSbGJaUMeSN7rn_I1obrzvqqJhDjqOAyQs39rtKJ-lyMq_oFDf1DOjFhB_KWCQ3V_N1SIOpoTnzoD7kr3voixtB4VrTo1HkUQPK_6a2FvUfg3sNwBBAxVvSv7jRPF5_BLGLRACfT3vIHfM7baSOFYkgijnGu9Okd4XKCSolb0hBO14vRMSUZ_gzdm2YubWEF5PK4kiIKMLnnvqUIAXt37sn0m7SjFK_7CI5K7TcSGJcnO-r63PaKsH3UfPqkTq6QWJKUh9X59mQcUJ6iClkY6Pv8LZWjqpkg";
        }

        public class auth_token_data
        {
            public string? access_token { get; set; }
            public string error { get; set; }
            public string error_description { get; set; }
            public string key { get; set; }
            public string refresh_token { get; set; }
        }

        public static string EncodeTo64(string toEncode) => Convert.ToBase64String(Encoding.ASCII.GetBytes(toEncode));

        public static string DecodeFrom64(string encodedData) => Encoding.ASCII.GetString(Convert.FromBase64String(encodedData));


        public static ulong GetTokenSubject(string token) => ulong.Parse(new JwtSecurityTokenHandler().ReadJwtToken(token).Subject);

        public static uint GetMID()
        {
            string name1 = "SOFTWARE\\Microsoft\\Cryptography";
            string name2 = "MachineGuid";
            uint num = 0;
            using (RegistryKey registryKey1 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey registryKey2 = registryKey1.OpenSubKey(name1))
                    num = uint.Parse(((registryKey2 != null ? registryKey2.GetValue(name2) : throw new KeyNotFoundException(string.Format("Key Not Found: {0}", (object)name1))) ?? throw new IndexOutOfRangeException(string.Format("Index Not Found: {0}", (object)name2))).ToString().Split('-')[0], NumberStyles.HexNumber);
            }
            return num;
        }

        public class Auth
        {
            public long nbf { get; set; }

            public long exp { get; set; }

            public string iss { get; set; }

            public List<string> aud { get; set; }

            public string client_id { get; set; }

            public List<string> role { get; set; }

            public string RnPlat { get; set; }

            public string RnPlatid { get; set; }

            public string RnDeviceclass { get; set; }

            public string RnVer { get; set; }

            public string RnAsid { get; set; }

            public string RnSk { get; set; }

            public string sub { get; set; }

            public long auth_time { get; set; }

            public string idp { get; set; }

            public string RnPid { get; set; }

            public List<string> scope { get; set; }

            public List<string> amr { get; set; }
            public long iat { get; internal set; }
        }
    }
}