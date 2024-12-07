using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rec_rewild.api
{
    internal class RoomInstance_api
    {
        internal class SimpleRoomInstance
        {
            public long RoomInstanceId { get; set; }
            public long RoomId { get; set; }
            public long SubRoomId { get; set; }
            public bool IsFull { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool HasModPresent { get; set; }    
            public List<ulong> PlayerIds { get; set; }
            public long PlayerCount { get; set; }
            public string HashedInstanceId { get; set; }
        }
        /*
                     public string device_code { get; set; }
            public string user_code { get; set; }
            public string verification_uri { get; set; }
            public string verification_uri_complete { get; set; }
            public int expires_in { get; set; }
            public int interval { get; set; }
         
         */
    }
}
