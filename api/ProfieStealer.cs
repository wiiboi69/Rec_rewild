using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;


namespace api
{
    class ProfieStealer
    {
        public static bool Profilefind(string data, int skip_int = 0, int take_int = 0)
        {
            List<Root> profile = JsonConvert.DeserializeObject<List<Root>>(data);
            if (take_int == 0)
            {
                take_int = profile.Count;
            }
            take_int = profile.Count;

            string input = "";
            int skip = 0;
            int take = 9;
            string data_1 = "";
            string data_2 = "";
            int skippage = 0;
            int maxpage = (int)Math.Floor((float)take_int / 10f);
        Refreshpage:
            Console.Clear();
        Refreshpage_2:
            Console.WriteLine($"┌────────────────────────────┤ player ├────────────────────────────┐");

            skippage = (int)Math.Floor((float)(take + 1) / 10f);
            if (take > take_int)
                take = take_int;
            string temp = "";
            string temp2 = "";
            for (int i = skip; i < take; i++)
            {
                temp = "";
                temp2 = "";

                Root root = profile[i];
                temp = $"│ [{i}]  {root.displayName}  @{root.username}";
                for (int j = $"│ [{i}]  {root.displayName}  @{root.username}".Length; j < 67; j++)
                {
                    temp2 = temp2 + " ";
                }
                temp = temp + temp2 + "│";

                Console.WriteLine(temp);

            }
            if (skippage > 1)
            {
                data_1 = "│ Prev ├";
            }
            else
            {
                data_1 = "├───────";
            }

            if (skippage < maxpage)
            {
                data_2 = "┤ Next │";
            }
            else
            {
                data_2 = "───────┤";
            }
            Console.WriteLine($"{data_1}──────────────────────┤ Exit ├──────────────────────{data_2}");
            Console.WriteLine($"└─────────────────────────────┘ Page {skippage} / {maxpage}");
            while (true)
            {
                input = Console.ReadLine();
                if (input == "E" )
                {
                    return false;
                }
                else if ( input == "P")
                {
                    if (skippage > 1)
                    {
                        skip -= 10;
                        take -= 10;
                        goto Refreshpage;
                    }

                }
                else if (input == "N")
                {
                    if (skippage < maxpage)
                    {
                        skip += 10;
                        take += 10;
                        goto Refreshpage;
                    }
                }
                else
                {
                    if (int.TryParse(input, out int value))
                    {
                        if (value >= 0 || value < take_int)
                        {
                            ProfileSteal(data,value);
                            return true;
                        }
                        else
                        {
                            goto error;
                        }
                    }
                    else
                    {
                        goto error;
                    }
                }
            }
            return true;
        error:
            Console.Clear();
            Console.WriteLine($"{input} was not a valued int or out of range");
            goto Refreshpage_2;
        }
        /// <summary>
        /// this is used to get a rec net account username, displayName, profileimage, and the bio
        /// </summary>
        public static void ProfileSteal(string data, int value)
        {
            List<Root> profile = JsonConvert.DeserializeObject<List<Root>>(data);

            File.WriteAllText("SaveData\\Profile\\username.txt", profile[value].username);
            string temp = new WebClient().DownloadString($"https://apim.rec.net/accounts/account/{profile[value].accountId}/bio");
            Root_bio bio = JsonConvert.DeserializeObject<Root_bio>(temp);

            File.WriteAllText("SaveData\\Profile\\bio.txt", bio.bio);
            File.WriteAllText("SaveData\\Profile\\displayName.txt", profile[value].displayName);
            byte[] profileimage = new WebClient().DownloadData("https://img.rec.net/" + profile[value].profileImage + "?cropSquare=true");
            File.WriteAllBytes("SaveData\\profileimage.png", profileimage);
        }

        /// <summary>
        /// this is deplcated, use ProfileSteal(string data, int value);
        /// </summary>
        public static void ProfileSteal(string data)
        {
            List<Root> profile = JsonConvert.DeserializeObject<List<Root>>(data);

            File.WriteAllText("SaveData\\Profile\\username.txt", profile[0].username);
            File.WriteAllText("SaveData\\Profile\\displayName.txt", profile[0].displayName);
            byte[] profileimage = new WebClient().DownloadData("https://img.rec.net/" + profile[0].profileImage + "?cropSquare=true&width=192&height=192");
            File.WriteAllBytes("SaveData\\profileimage.png", profileimage);
        }
        public class Root_bio
        {
            public int accountId { get; set; }
            public string? bio { get; set; }
        }



        public class Root
        {
            public int accountId { get; set; }
            public string username { get; set; }
            public string displayName { get; set; }
            public string profileImage { get; set; }
            public bool? isJunior { get; set; }
            public int platforms { get; set; }
            public int personalPronouns { get; set; }
            public int identityFlags { get; set; }
            public DateTime createdAt { get; set; }
            public string bannerImage { get; set; }
        }
    }
}