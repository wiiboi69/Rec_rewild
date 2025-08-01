using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rec_rewild.api
{
    public class dummy_account_system
    {
        public class Accountstuff
        {
            public int accountId { get; set; }
            public string displayName { get; set; }
            public string bannerImage { get; set; }
            public DateTime createdAt { get; set; }
            public bool isJunior { get; set; }
            public int platforms { get; set; }
            public string profileImage { get; set; }
            public string username { get; set; }
        }

        private static readonly List<Accountstuff> _cache = new List<Accountstuff>(20);

        public static void AddAccount(Accountstuff account)
        {
            if (_cache.Count >= 20)
                _cache.RemoveAt(0);
            _cache.Add(account);
        }

        public static string GetAccountsJson()
        {
            return JsonConvert.SerializeObject(_cache);
        }

        public static void ClearCache()
        {
            _cache.Clear();
        }
    }
}