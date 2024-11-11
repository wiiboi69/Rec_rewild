using server;
using System;
using System.IO;
using static start.Program;

internal static class ProgramHelpers
{

    public static Reponse createResponse()
    {
        return new Reponse
        {
            Id = WebSocketHTTP_new.ResponseResults.ModerationKick,
            Msg = new ModerationBlockDetails()
            {
                ReportCategory = 1,
                Duration = 0,
                IsHostKick = true,
                GameSessionId = 100L,
                Message = "test of websocket"
            }
        };
    }
    //MessageReceived
    public static Reponse createResponse_box()
    {
        return new Reponse
        {
            Id = WebSocketHTTP_new.ResponseResults.RelationshipChanged,
            Msg = new Relationship_Detail()
            {
                PlayerID = 1,
                Favorited = ReciprocalStatus.None,
                Ignored = ReciprocalStatus.None,
                Muted = ReciprocalStatus.None,
                Type = RelationshipType.FriendRequestReceived,
            }
        };
    }

    public static Reponse createResponse_FriendInvite()
    {
        return new Reponse
        {
            Id = WebSocketHTTP_new.ResponseResults.MessageReceived,
            Msg = new Message_Detail()
            {
                FromPlayerId = 1,
                Id = 1,
                SentTime = DateTime.Today,
                Type = MessageType.FriendInvite,
                Data = "ddddddddd",
                RoomId = 20,

            }
        };
    }

    public static Reponse createResponse_FriendRequestAccepted()
    {
        return new Reponse
        {
            Id = WebSocketHTTP_new.ResponseResults.MessageReceived,
            Msg = new Message_Detail()
            {
                FromPlayerId = 1,
                Id = 1,
                SentTime = DateTime.Today,
                Type = MessageType.FriendRequestAccepted,
                Data = "ddddddddd",
                RoomId = 20,

            }
        };
    }
    public static Reponse createResponse_FriendStatusOnline()
    {
        return new Reponse
        {
            Id = WebSocketHTTP_new.ResponseResults.MessageReceived,
            Msg = new Message_Detail()
            {
                FromPlayerId = 1,
                Id = 1,
                SentTime = DateTime.Today,
                Type = MessageType.FriendStatusOnline,
                Data = "ddddddddd",
                RoomId = 20,

            }
        };
    }

    public static Reponse createResponse_give()
    {
        return new Reponse
        {
            Id = WebSocketHTTP_new.ResponseResults.GiftPackageReceivedImmediate,
            Msg = new GiftPackage()
            {
                Id = 1,
                PlayerId = ulong.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt")),
                FromPlayerId = 1,
                ConsumableItemDesc = "frOMH6WxDEG1fBqC4_83vg",
                ConsumableCount = 3,
                AvatarItemDesc = "",
                AvatarItemType = null,
                CurrencyType = 0,
                Currency = 0,
                Xp = 0,
                PackageType = 0,
                Message = "A gift for you <3",
                EquipmentPrefabName = "",
                EquipmentModificationGuid = "",
                GiftContext = (GiftContext)500,
                GiftRarity = 0,
                Platform = PlatformType.All,
                platformsToSpawnOn = PlatformType.All,
                balanceType = null,
                createdAt = DateTime.Now
            }
        };
    }

    public static Reponse createResponse_msg()
    {
        return new Reponse
        {
            Id = WebSocketHTTP_new.ResponseResults.MessageReceived,
            Msg = new Message_Detail()
            {
                FromPlayerId = 1,
                Id = 1,
                SentTime = DateTime.Today,
                Type = MessageType.TextMessage,
                Data = "ddddddddd",
                RoomId = 20,

            }
        };
    }

    public static Reponse createResponse_time()
    {
        return new Reponse
        {
            Id = WebSocketHTTP_new.ResponseResults.ServerMaintenance,
            Msg = new StartsInMinutes_Detail()
            {
                StartsInMinutes = 10,

            }
        };
    }
}