using System;

namespace api
{
    public class mPlayerReputation
    {
        //IsInGoodStanding
        public ulong AccountId { get; set; }
        public ulong PlayerId { get; set; }
        public bool IsInGoodStanding { get; set; }
        public bool IsCheerful { get; set; }
        public float Noteriety { get; set; }
        public CheerCategory? SelectedCheer { get; set; }
        public int CheerCredit { get; set; }
        public int CheerGeneral { get; set; }
        public int CheerHelpful { get; set; }
        public int CheerCreative { get; set; }
        public int CheerGreatHost { get; set; }
        public int CheerSportsman { get; set; }
        public int SubscriberCount { get; set; }
        public int SubscribedCount { get; set; }
        public enum CheerCategory
        {
            // Token: 0x04008DB9 RID: 36281
            General,
            // Token: 0x04008DBA RID: 36282
            Helpful = 10,
            // Token: 0x04008DBB RID: 36283
            Sportmanship = 20,
            // Token: 0x04008DBC RID: 36284
            GreatHost = 30,
            // Token: 0x04008DBD RID: 36285
            Creative = 40
        }
    }
}
