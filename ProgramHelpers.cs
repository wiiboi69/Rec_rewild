using Newtonsoft.Json;
using Rec_rewild.api;
using server;
using start;
using System;
using System.IO;
using static api.AccountAuth;
using static start.Program;

internal static class ProgramHelpers
{

    public static Reponse<WebSocketHTTP_new.ResponseResults> createResponse()
    {
        return new Reponse<WebSocketHTTP_new.ResponseResults>
        {
            Id = WebSocketHTTP_new.ResponseResults.ModerationKick,
            Msg = new ModerationBlockDetails()
            {
                ReportCategory = (int)VotekickTypes.Moderator,
                Duration = 2,
                IsHostKick = true,
                GameSessionId = 100L,
                Message = "test of websocket"
            }
        };
    }
    //MessageReceived
    public static Reponse<WebSocketHTTP_new.ResponseResults> createResponse_box()
    {
        return new Reponse<WebSocketHTTP_new.ResponseResults>
        {
            Id = WebSocketHTTP_new.ResponseResults.RelationshipChanged,
            Msg = new Relationship_Detail()
            {
                PlayerID = 1,
                Favorited = ReciprocalStatus.None,
                Ignored = ReciprocalStatus.None,
                Muted = ReciprocalStatus.None,
                Type = RelationshipType.Friend,
            }
        };
    }

    public static Reponse<WebSocketHTTP_new.ResponseResults> createResponse_FriendInvite()
    {
        return new Reponse<WebSocketHTTP_new.ResponseResults>
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

    public static Reponse<WebSocketHTTP_new.ResponseResults> createResponse_FriendRequestAccepted()
    {
        return new Reponse<WebSocketHTTP_new.ResponseResults>
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
    public static Reponse<WebSocketHTTP_new.ResponseResults> createResponse_FriendStatusOnline()
    {
        return new Reponse<WebSocketHTTP_new.ResponseResults>
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

    public static Reponse<WebSocketHTTP_new.ResponseResults> createResponse_give()
    {
        var setting = player_config.Setting;
        return new Reponse<WebSocketHTTP_new.ResponseResults>
        {
            Id = WebSocketHTTP_new.ResponseResults.GiftPackageReceivedImmediate,
            Msg = new GiftPackage()
            {
                Id = 1,
                PlayerId = (ulong)setting.AccountId,
                FromPlayerId = 1,
                ConsumableItemDesc = "frOMH6WxDEG1fBqC4_83vg",
                ConsumableCount = 3,
                AvatarItemDesc = "",
                AvatarItemType = null,
                CurrencyType = 0,
                Currency = 0,
                Xp = 0,
                PackageType = 0,
                Message = "First game of the day",
                EquipmentPrefabName = "",
                EquipmentModificationGuid = "",
                GiftContext = (GiftContext)500,
                GiftRarity = GiftRarity.Epic,
                Platform = PlatformType.All,
                platformsToSpawnOn = PlatformType.All,
                balanceType = null,
                createdAt = DateTime.Now
            }
        };
    }

    public static Reponse<WebSocketHTTP_new.ResponseResults> createResponse_msg()
    {
        return new Reponse<WebSocketHTTP_new.ResponseResults>
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
    public static void SelfAccountUpdate()
    {
        WebSocketHTTP_new.SendRequest(JsonConvert.SerializeObject(createResponse_AccountUpdate("SelfAccountUpdate")));
        WebSocketHTTP_new.SendRequest(JsonConvert.SerializeObject(createResponse_AccountUpdate("AccountUpdate")));
    }

    public static Reponse<string> createResponse_AccountUpdate(string tmp)
    {
        return new Reponse<string>
        {
            Id = tmp,//WebSocketHTTP_new.ResponseResults.SubscriptionUpdateProfile,
            Msg = JsonConvert.DeserializeObject(GetAccountsBulk())
        };
    }

    public static Reponse<WebSocketHTTP_new.ResponseResults> CreateServerMaintence()
    {
        return new Reponse<WebSocketHTTP_new.ResponseResults>
        {
            Id = WebSocketHTTP_new.ResponseResults.ServerMaintenance,
            Msg = new StartsInMinutes_Detail()
            {
                StartsInMinutes = 10,
            }
        };
    }
}