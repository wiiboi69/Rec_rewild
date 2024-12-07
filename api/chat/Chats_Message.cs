using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rec_rewild.api.chat
{
    internal class Chats_Message
    {
        public class LatestMessage
        {
            public long ChatMessageId { get; set; }
            public long ChatThreadId { get; set; }
            public long SenderPlayerId { get; set; }
            public DateTime TimeSent { get; set; }
            public string Contents { get; set; }
            public long ModerationState { get; set; }
        }
        public class Thread
        {
            public LatestMessage LatestMessage { get; set; }
            public long ChatThreadId { get; set; }
            public long[] PlayerIds { get; set; }
            public long LastReadMessageId { get; set; }
            public string? ChatThreadName { get; set; } = null;
            public long ChatThreadType { get; set; }
            public DateTime? SnoozedUntil { get; set; } = null;
            public bool IsFavorited { get; set; }
        }
        public class Content
        {
            public chat_type Type { get; set; }
            public long Version { get; set; }
            public string Data { get; set; }
            public long moderationState { get; set; }
        }
        public enum chat_type
        {
            text,
            party_inv,
            image,
        }
    }
}
