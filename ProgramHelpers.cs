using server;
using System;
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
            Id = WebSocketHTTP_new.ResponseResults.GiftPackageReceived,
            Msg = new ChallengeGift()
            {
                /*
                Id = 1,
                CurrencyType = CurrencyType.RecCenterTokens,
                Currency = 100000,
                GiftRarity = GiftRarity.Legendary,
                GiftContext = GiftContext.Default,
                Message = "fnaf",
                FromPlayerId = 1,
                Platform = PlatformType.All,
                ConsumableItemDesc = "",
                AvatarItemDescOrHairDyeDesc = "",
                EquipmentPrefabName = "",
                EquipmentModificationGuid = "",
                AvatarItemType = AvatarItemType.Outfit,
                Xp = 0,
                Level = 0,
                */
                GiftDropId = 2,
                GiftRarity = GiftRarity.Legendary,
                GiftContext = GiftContext.None,
                ConsumableItemDesc = "",
                StorefrontType = StorefrontTypes.None,
                AvatarItemDesc = "",
                EquipmentPrefabName = "",
                EquipmentModificationGuid = "",
                Xp = 100000,
                Level = 0,
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