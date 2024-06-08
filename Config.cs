﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace api
{
    class Config
    {
        public static void setup()
        {
            Console.WriteLine("Setting up...");
            Directory.CreateDirectory("SaveData\\Profile\\");
            if (!(File.Exists("SaveData\\avatar.txt")))
            {
                File.Create("SaveData\\avatar.txt");
            }
            if (!(File.Exists("SaveData\\Profile\\username.txt")))
            {
                File.WriteAllText("SaveData\\Profile\\username.txt", "DefaultUsername");
            }
            if (!(File.Exists("SaveData\\profileimage.png")))
            {
                File.WriteAllBytes("SaveData\\profileimage.png", new WebClient().DownloadData("https://github.com/OpenRecRoom/OpenRec/raw/main/profileimage.png"));
            }
            Console.WriteLine("Done!");
            Console.Clear();
        }
        public static gamesesh.GameSessions.SessionInstance localGameSession;
        public static gamesesh.GameSessions.SessionInstancev2 localGameSessionv2;
        public static gamesesh.GameSessions.SessionInstancev3 localGameSessionv3;
        public static gamesesh.GameSessions.JoinResultv3 GameSession;

        public static vaultgamesesh.c000020.player_heartbeat_data playerHeartbeatData;
        public static vaultgamesesh.c000020.player_heartbeat_datav2 playerHeartbeatDatav2;
        public static vaultgamesesh.c000020.player_heartbeat_datav2old player_heartbeat_datav2old;
        

        public static Objective[][] dailyObjectives = new Objective[][]
        {
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            },
            new Objective[]
            {
                new Objective
                {
                    type = 20,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 21,
                    score = 1,
                    xp = 200
                },
                new Objective
                {
                    type = 22,
                    score = 1,
                    xp = 200
                }
            }
        };
    }
}
