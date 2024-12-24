using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api;
using static api.WarningMask;

namespace api
{
    internal class Roomdata
    {
        public static Dictionary<string, RoomRoot> RROS = new Dictionary<string, RoomRoot>
        {
            {
                "DormRoom",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 1,
                        Name = "DormRoom",
                        Description = "Your private dorm.",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "DormRoom.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        DisableMicAutoMute = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        AllowsJuniors = false,
                        IsDormRoom = true,
                        SupportsVRLow = true,
                        CustomRoomWarning = "",
                        RoomWarningMask = 0,
                        EncryptVoiceChat = false,
                        DisableRoomComments = true,
                        SupportsMobile = true,
                        SetType = 0,
                        Type = 0,
                        creatorPlayerId = 0UL
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 1,
                            RoomId = 1,
                            Location = "76d98498-60a1-430c-ab76-b54a29b7a163",
                            Name = "Home",
                            IsSandbox = true,
                            DataBlobName = null,
                            MaxPlayers = 4,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now,
                            RoomSceneLocationId = "76d98498-60a1-430c-ab76-b54a29b7a163"
                        }
                    },
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    CoOwners = new List<ulong>(),
                    Hosts = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    InvitedHosts = new List<int>(),
                    InvitedModerators = new List<int>(),
                    Moderators = new List<int>(),
                    Tags = new List<Tags>(),
                    LocalPlayerRole = 3
                }
            },
            {
                "RecCenter",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 2,
                        Name = "RecCenter",
                        Description = "Rec Center.",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "RecCenter.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        DisableMicAutoMute = true,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        AllowsJuniors = true,
                        IsDormRoom = false,
                        SupportsVRLow = true,
                        CustomRoomWarning = "",
                        RoomWarningMask = 0
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 1,
                            RoomId = 2,
                            Location = "cbad71af-0831-44d8-b8ef-69edafa841f6",
                            Name = "Home",
                            IsSandbox = false,
                            DataBlobName = "",
                            MaxPlayers = 4,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now,
                            RoomSceneLocationId = "cbad71af-0831-44d8-b8ef-69edafa841f6"
                        }
                    },
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    CoOwners = new List<ulong>(),
                    Hosts = new List<int>(),
                    InvitedCoOwners = new List<int>(),
                    InvitedHosts = new List<int>(),
                    InvitedModerators = new List<int>(),
                    Moderators = new List<int>(),
                    LocalPlayerRole = 3,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "Rec Center",
                            Type = 1
                        }
                    }
                }
            },
            {
                "Paddleball",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 3,
                        Name = "Paddleball",
                        Description = "A simple rally game between two players in a plexiglass tube with a zero-g ball.",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "PaddleBall.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 3,
                            RoomId = 3,
                            RoomSceneLocationId = "d89f74fa-d51e-477a-a425-025a891dd499",
                            Name = "paddleball",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 2,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        },
                        new Tags
                        {
                            Tag = "sport",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Dodgeball",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 4,
                        Name = "Dodgeball",
                        Description = "Throw dodgeballs to knock out your friends in this gym classic!",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "Dodgeball.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 4,
                            RoomId = 4,
                            RoomSceneLocationId = "3d474b26-26f7-45e9-9a36-9b02847d5e6f",
                            Name = "dodgeball",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        },
                        new Tags
                        {
                            Tag = "sport",
                            Type = 2
                        }
                    }
                }
            },
            {
                "DiscGolfPropultion",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 5,
                        Name = "DiscGolfPropultion",
                        Description = "Disk Golf with a twist!",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "DiscGolfPropultion.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 2,
                            RoomId = 1,
                            RoomSceneLocationId = "d9378c9f-80bc-46fb-ad1e-1bed8a674f55",
                            Name = "Propultion",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 4,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Paintball",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 6,
                        Name = "Paintball",
                        Description = "there are paints everyware.",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "Paintball.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = true,
                        IsAGRoom = true,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 0,
                            RoomId = 6,
                            RoomSceneLocationId = "e122fe98-e7db-49e8-a1b1-105424b6e1f0",
                            Name = "River",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new Scene
                        {
                            RoomSceneId = 1,
                            RoomId = 7,
                            RoomSceneLocationId = "a785267d-c579-42ea-be43-fec1992d1ca7",
                            Name = "Homestead",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new Scene
                        {
                            RoomSceneId = 2,
                            RoomId = 8,
                            RoomSceneLocationId = "ff4c6427-7079-4f59-b22a-69b089420827",
                            Name = "Quarry",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new Scene
                        {
                            RoomSceneId = 3,
                            RoomId = 9,
                            RoomSceneLocationId = "380d18b5-de9c-49f3-80f7-f4a95c1de161",
                            Name = "Clearcut",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new Scene
                        {
                            RoomSceneId = 4,
                            RoomId = 10,
                            RoomSceneLocationId = "58763055-2dfb-4814-80b8-16fac5c85709",
                            Name = "Spillway",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new Scene
                        {
                            RoomSceneId = 5,
                            RoomId = 11,
                            RoomSceneLocationId = "65ddbb48-5a01-4e3e-972d-e5c7419e2bc3",
                            Name = "DriveIn",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "LaserTag",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 12,
                        Name = "LaserTag",
                        Description = "(bots included)",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "LaserTag.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = true,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 0,
                            RoomId = 12,
                            RoomSceneLocationId = "239e676c-f12f-489f-bf3a-d4c383d692c3",
                            Name = "Hanger",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        },
                        new Scene
                        {
                            RoomSceneId = 1,
                            RoomId = 13,
                            RoomSceneLocationId = "9d6456ce-6264-48b4-808d-2d96b3d91038",
                            Name = "CyberJunkCity",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Bowling",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 14,
                        Name = "Bowling",
                        Description = "Wii Sports Bowling",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "bowling.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 20,
                            RoomId = 14,
                            RoomSceneLocationId = "ae929543-9a07-41d5-8ee9-dbbee8c36800",
                            Name = "wii",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "RecRoyaleSquads",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 15,
                        Name = "RecRoyaleSquads",
                        Description = "Squad up with 3 other players in this battle royale!",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "RecRoyale.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = true,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 22,
                            RoomId = 15,
                            RoomSceneLocationId = "253fa009-6e65-4c90-91a1-7137a56a267f",
                            Name = "home",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "RecRoyaleSolos",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 16,
                        Name = "RecRoyaleSolos",
                        Description = "fortnite",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "RecRoyale.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = true,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 23,
                            RoomId = 16,
                            RoomSceneLocationId = "b010171f-4875-4e89-baba-61e878cd41e1",
                            Name = "home",
                            IsSandbox = false,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "RecRoyaleSandbox",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 17,
                        Name = "RecRoyaleSandbox",
                        Description = "Use the maker pen on a very big map",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "RecRoyale.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 23,
                            RoomId = 17,
                            RoomSceneLocationId = "b010171f-4875-4e89-baba-61e878cd41e1",
                            Name = "home",
                            IsSandbox = true,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    LocalPlayerRole = 3,
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Lounge",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 18,
                        Name = "Lounge",
                        Description = "A small, cozy lounge to hang out with your friends",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "Lounge.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = false,
                        IsAGRoom = false,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 24,
                            RoomId = 18,
                            RoomSceneLocationId = "a067557f-ca32-43e6-b6e5-daaec60b4f5a",
                            Name = "Lounge",
                            IsSandbox = true,
                            DataBlobName = string.Empty,
                            MaxPlayers = 8,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Registration",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 19,
                        Name = "Registration",
                        Description = "Register your account and make your game die",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "Regestration.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = true,
                        IsAGRoom = true,
                        CloningAllowed = false,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 27,
                            RoomId = 19,
                            RoomSceneLocationId = "cf61556d-68fd-4288-9ae5-7a512621e569",
                            Name = "Regestration",
                            IsSandbox = true,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Orientation",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 20,
                        Name = "Orientation",
                        Description = "welcome to orientation",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "Orientation.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = true,
                        IsAGRoom = false,
                        CloningAllowed = true,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 27,
                            RoomId = 20,
                            RoomSceneLocationId = "c79709d8-a31b-48aa-9eb8-cc31ba9505e8",
                            Name = "intro",
                            IsSandbox = true,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        },
                        new Tags
                        {
                            Tag = "Orientation",
                            Type = 2
                        },
                        new Tags
                        {
                            Tag = "welcome center",
                            Type = 2
                        },
                        new Tags
                        {
                            Tag = "hell",
                            Type = 2
                        }
                    }
                }
            },
            {
                "Crescendo",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 21,
                        Name = "Crescendo",
                        Description = "Gather your vampire hunting crew, conquer a legendary castle, and restore peace to Rec Room!",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "Crescendo.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = true,
                        IsAGRoom = false,
                        CloningAllowed = true,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 27,
                            RoomId = 21,
                            RoomSceneLocationId = "49cb8993-a956-43e2-86f4-1318f279b22a",
                            Name = "home",
                            IsSandbox = true,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        },
                        new Tags
                        {
                            Tag = "quest",
                            Type = 2
                        }
                    }
                }
            },
            {
                "GoldenTrophy",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 22,
                        Name = "GoldenTrophy",
                        Description = "The goblin king stole Coach's Golden Trophy. Team up and embark on an epic quest to recover it!",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "GoldenTrophy.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = true,
                        IsAGRoom = false,
                        CloningAllowed = true,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 27,
                            RoomId = 22,
                            RoomSceneLocationId = "91e16e35-f48f-4700-ab8a-a1b79e50e51b",
                            Name = "home",
                            IsSandbox = true,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        },
                        new Tags
                        {
                            Tag = "quest",
                            Type = 2
                        }
                    }
                }
            },
            {
                "TheRiseofJumbotron",
                new RoomRoot
                {
                    Room = new Room
                    {
                        RoomId = 23,
                        Name = "TheRiseofJumbotron",
                        Description = "Robot invaders threaten the galaxy! Team up with your friends and bring the laser heat!",
                        CreatorPlayerId = Convert.ToUInt64(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                        ImageName = "TheRiseofJumbotron.png",
                        State = 0,
                        Accessibility = 1,
                        SupportsLevelVoting = true,
                        IsAGRoom = false,
                        CloningAllowed = true,
                        SupportsScreens = true,
                        SupportsTeleportVR = true,
                        SupportsWalkVR = true,
                        SupportsVRLow = true
                    },
                    Scenes = new List<Scene>
                    {
                        new Scene
                        {
                            RoomSceneId = 27,
                            RoomId = 23,
                            RoomSceneLocationId = "acc06e66-c2d0-4361-b0cd-46246a4c455c",
                            Name = "home",
                            IsSandbox = true,
                            DataBlobName = string.Empty,
                            MaxPlayers = 20,
                            CanMatchmakeInto = true,
                            DataModifiedAt = DateTime.Now
                        }
                    },
                    CoOwners = new List<ulong>(),
                    InvitedCoOwners = new List<int>(),
                    Hosts = new List<int>(),
                    InvitedHosts = new List<int>(),
                    CheerCount = 1,
                    FavoriteCount = 1,
                    VisitCount = 1,
                    Tags = new List<Tags>
                    {
                        new Tags
                        {
                            Tag = "rro",
                            Type = 2
                        },
                        new Tags
                        {
                            Tag = "quest",
                            Type = 2
                        }
                    }
                }
            }
        };
        public class RoomRoot
        {
            public Room Room { get; set; }
            public List<Scene> Scenes { get; set; }
            public List<ulong> CoOwners { get; set; }
            public List<int> InvitedCoOwners { get; set; }
            public List<int> InvitedModerators { get; set; }
            public List<int> Moderators { get; set; }
            public List<int> Hosts { get; set; }
            public List<int> PlayerIdsWithModPower { get; set; }
            public List<int> InvitedHosts { get; set; }
            public int CheerCount { get; set; }
            public int LocalPlayerRole { get; set; }
            public int localPlayerRole { get; set; }
            public int FavoriteCount { get; set; }
            public int VisitCount { get; set; }
            public List<Tags> Tags { get; set; }
            public bool beta { get; set; }
        }

        public class RoomRootv2
        {
            public ulong RoomId { get; set; }
            public bool IsDorm { get; set; }
            public int MaxPlayerCalculationMode { get; set; }
            public int MaxPlayers { get; set; }
            public bool CloningAllowed { get; set; }
            public bool DisableMicAutoMute { get; set; }
            public bool DisableRoomComments { get; set; }
            public bool EncryptVoiceChat { get; set; }
            public bool ToxmodEnabled { get; set; }
            public bool LoadScreenLocked { get; set; }
            public int PersistenceVersion { get; set; }
            public bool AutoLocalizeRoom { get; set; }
            public bool IsDeveloperOwned { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImageName { get; set; }
            public WarningMaskType WarningMask { get; set; }
            public string CustomWarning { get; set; }
            public ulong CreatorAccountId { get; set; }
            public int State { get; set; }
            public int Accessibility { get; set; }
            public bool SupportsLevelVoting { get; set; }
            public bool IsRRO { get; set; }
            public bool SupportsScreens { get; set; }
            public bool SupportsWalkVR { get; set; }
            public bool SupportsTeleportVR { get; set; }
            public bool SupportsVRLow { get; set; }
            public bool SupportsQuest2 { get; set; }
            public bool SupportsMobile { get; set; }
            public bool SupportsJuniors { get; set; }
            public int MinLevel { get; set; }
            public string CreatedAt { get; set; }
            public Stats Stats { get; set; }
            public string? RankedEntityId { get; set; }
            public string? RankingContext { get; set; }
            public List<SubRooms> SubRooms { get; set; }
            public List<Roles> Roles { get; set; }
            public string? DataBlob { get; set; }
            public int UgcVersion { get; set; }
            public List<Tags> Tags { get; set; }
            public List<Dummy> PromoImages { get; set; }
            public List<Dummy> PromoExternalContent { get; set; }
            public List<LoadScreens> LoadScreens { get; set; }
            /*
            public bool beta { get; set; }
            public List<ulong> CoOwners { get; set; }
            public List<int> InvitedCoOwners { get; set; }
            public List<int> InvitedModerators { get; set; }
            public List<int> Moderators { get; set; }
            public List<int> Hosts { get; set; }
            public List<int> PlayerIdsWithModPower { get; set; }
            public List<int> InvitedHosts { get; set; }
            */

        }

        public class RoomRootv3
        {
            public ulong RoomId { get; set; }
            public bool IsDorm { get; set; }
            public int MaxPlayerCalculationMode { get; set; }
            public int MaxPlayers { get; set; }
            public bool CloningAllowed { get; set; }
            public bool DisableMicAutoMute { get; set; }
            public bool DisableRoomComments { get; set; }
            public bool EncryptVoiceChat { get; set; }
            public bool ToxmodEnabled { get; set; }
            public bool LoadScreenLocked { get; set; }
            public int PersistenceVersion { get; set; }
            public bool AutoLocalizeRoom { get; set; }
            public bool IsDeveloperOwned { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImageName { get; set; }
            public WarningMaskType WarningMask { get; set; }
            public string CustomWarning { get; set; }
            public ulong CreatorAccountId { get; set; }
            public int State { get; set; }
            public int Accessibility { get; set; }
            public bool SupportsLevelVoting { get; set; }
            public bool IsRRO { get; set; }
            public bool SupportsScreens { get; set; }
            public bool SupportsWalkVR { get; set; }
            public bool SupportsTeleportVR { get; set; }
            public bool SupportsVRLow { get; set; }
            public bool SupportsQuest2 { get; set; }
            public bool SupportsMobile { get; set; }
            public bool SupportsJuniors { get; set; }
            public int MinLevel { get; set; }
            public int SetType { get; set; }
            public int Type { get; set; }
            public string CreatedAt { get; set; }
            public Statsv2 Stats { get; set; }
            public string? RankedEntityId { get; set; }
            public string? RankingContext { get; set; }
            public List<SubRoomsv2> SubRooms { get; set; }
            public List<Roles> Roles { get; set; }
            public string? DataBlob { get; set; }
            public int UgcVersion { get; set; }
            public List<Tags> Tags { get; set; }
            public List<Dummy> PromoImages { get; set; }
            public List<Dummy> PromoExternalContent { get; set; }
            public List<LoadScreens> LoadScreens { get; set; }
            public List<ulong> CoOwners { get; set; }
            public List<int> InvitedCoOwners { get; set; }
            public List<int> InvitedModerators { get; set; }
            public List<int> Moderators { get; set; }
            public List<int> Hosts { get; set; }
            public List<int> PlayerIdsWithModPower { get; set; }
            public List<int> InvitedHosts { get; set; }
            public int Version { get; internal set; }
            public int CheerCount { get; internal set; }
            public int FavoriteCount { get; internal set; }
            public int VisitCount { get; internal set; }
            public int VisitorCount { get; internal set; }
            public string CustomRoomWarning { get; internal set; }
            public WarningMask.WarningMaskType RoomWarningMask { get; internal set; }
            public ulong CreatorPlayerId { get; internal set; }
            public bool AllowsJuniors { get; internal set; }
            /*
public bool beta { get; set; }
*/

        }

        public class RoomRootv3_1
        {
            public ulong RoomId { get; set; }
            public bool IsDorm { get; set; }
            public int MaxPlayerCalculationMode { get; set; }
            public int MaxPlayers { get; set; }
            public bool CloningAllowed { get; set; }
            public bool DisableMicAutoMute { get; set; }
            public bool DisableRoomComments { get; set; }
            public bool EncryptVoiceChat { get; set; }
            public bool ToxmodEnabled { get; set; }
            public bool LoadScreenLocked { get; set; }
            public int PersistenceVersion { get; set; }
            public bool AutoLocalizeRoom { get; set; }
            public bool IsDeveloperOwned { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImageName { get; set; }
            public WarningMaskType WarningMask { get; set; }
            public string CustomWarning { get; set; }
            public ulong CreatorAccountId { get; set; }
            public int State { get; set; }
            public int Accessibility { get; set; }
            public bool SupportsLevelVoting { get; set; }
            public bool IsRRO { get; set; }
            public bool SupportsScreens { get; set; }
            public bool SupportsWalkVR { get; set; }
            public bool SupportsTeleportVR { get; set; }
            public bool SupportsVRLow { get; set; }
            public bool SupportsQuest2 { get; set; }
            public bool SupportsMobile { get; set; }
            public bool SupportsJuniors { get; set; }
            public int MinLevel { get; set; }
            public string CreatedAt { get; set; }
            public Stats Stats { get; set; }
            public string? RankedEntityId { get; set; }
            public string? RankingContext { get; set; }
            public List<SubRooms> SubRooms { get; set; }
            public List<Roles> Roles { get; set; }
            public string? DataBlob { get; set; }
            public int UgcVersion { get; set; }
            public int Version { get; set; }
            public List<Tags> Tags { get; set; }
            public List<Dummy> PromoImages { get; set; }
            public List<Dummy> PromoExternalContent { get; set; }
            public List<LoadScreens> LoadScreens { get; set; }
            /*
            public bool beta { get; set; }
            public List<ulong> CoOwners { get; set; }
            public List<int> InvitedCoOwners { get; set; }
            public List<int> InvitedModerators { get; set; }
            public List<int> Moderators { get; set; }
            public List<int> Hosts { get; set; }
            public List<int> PlayerIdsWithModPower { get; set; }
            public List<int> InvitedHosts { get; set; }
            */

        }
        public class Room
        {
            public ulong RoomId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImageName { get; set; }
            public ulong CreatorPlayerId { get; set; }
            public ulong creatorPlayerId { get; set; }
            public int State { get; set; }
            public int Accessibility { get; set; }
            public bool SupportsLevelVoting { get; set; }
            public bool IsAGRoom { get; set; }
            public bool IsDormRoom { get; set; }
            public bool CloningAllowed { get; set; }
            public bool SupportsVRLow { get; set; }
            public bool SupportsScreens { get; set; }
            public bool SupportsWalkVR { get; set; }
            public bool SupportsTeleportVR { get; set; }
            public bool AllowsJuniors { get; set; }
            public WarningMaskType RoomWarningMask { get; set; }
            public string CustomRoomWarning { get; set; }
            public bool DisableMicAutoMute { get; set; }
            public int Type { get; set; }
            public int ListOrder { get; set; }
            public ulong RoomOrPlaylistId { get; set; }
            public int RoomPlaylistId { get; set; }
            public bool DisableRoomComments { get; set; }
            public bool EncryptVoiceChat { get; set; }
            public bool SupportsMobile { get; set; }
            public int SetType { get; set; }
        }
        public class Scene
        {
            public int RoomSceneId { get; set; }
            public int SubRoomId { get; set; }
            public int RoomId { get; set; }
            public string RoomSceneLocationId { get; set; }
            public string Location { get; set; }
            public string Name { get; set; }
            public bool IsSandbox { get; set; }
            public string? DataBlobName { get; set; }
            public int MaxPlayers { get; set; }
            public bool CanMatchmakeInto { get; set; }
            public DateTime DataModifiedAt { get; set; }
        }
        public class SubRooms
        {
            public int SubRoomId { get; set; }
            public int RoomId { get; set; }
            public string Name { get; set; }
            public string? DataBlob { get; set; }
            public bool IsSandbox { get; set; }
            public int MaxPlayers { get; set; }
            public int Accessibility { get; set; }
            public string UnitySceneId { get; set; }
            public int SavedByAccountId { get; set; }
        }
        public class Tags
        {
            public string Tag { get; set; }
            public int Type { get; set; }
        }
        public class LoadScreens
        {
            public string ImageName { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }
        }
        public class Stats
        {
            public int CheerCount { get; set; }
            public int FavoriteCount { get; set; }
            public int VisitorCount { get; set; }
            public int VisitCount { get; set; }
        }
        public class Roles
        {
            public int AccountId { get; set; }
            public int Role { get; set; }
            public int InvitedRole { get; set; }
        }
        public class Roomlist
        {
            public List<RoomRootv2> Results { get; set; }
            public long TotalResults { get; set; }
        }
        public class roomlistv2
        {
            public List<RoomRootv3> Results { get; set; }
            public long TotalResults { get; set; }
        }
        public class roomlistv2_1
        {
            public List<RoomRootv3_1> Results { get; set; }
            public long TotalResults { get; set; }
        }
        public class Dummy
        { 

        }
        public class SubRoomsv2
        {
            public int Accessibility { get; set; }
            public string? DataBlob { get; set; }
            public string? DataBlobName { get; set; }
            public string? DataBlobHash { get; set; }
            public bool IsSandbox { get; set; }
            public int MaxPlayers { get; set; }
            public string Name { get; set; }
            public int SubRoomId { get; set; }
            public int RoomId { get; set; }
            public string UnitySceneId { get; set; }
            public string Location { get; set; }
            public int SavedByAccountId { get; set; }
            public DateTime DataModifiedAt { get; internal set; }
            public int RoomSceneId { get; internal set; }
            public DateTime DataSavedAt { get; internal set; }
            public string RoomSceneLocationId { get; internal set; }
        }
        public class Statsv2
        {
            public int CheerCount { get; set; }
            public int FavoriteCount { get; set; }
            public ulong RoomId { get; set; }
            public int VisitorCount { get; set; }
            public int VisitCount { get; set; }
        }
    }
}
