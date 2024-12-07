using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rec_rewild.api
{
    internal class leaderboard_api
    {
        //WorldLeaderBoard
        public class SingleLeaderboard
        {
            public List<Entry> Rows {  get; set; }
        }
        public class WorldLeaderBoard
        {
            public string title { get; set; }
            public List<Entry> Rows { get; set; }
        }

        public enum StuntRunnerStatType
        {
            CURRENT_COURSE_FINISHED = 1,
            COURSE_TIME,
            TOTAL_TIME = 9,
            COMPLETED_COURSE_TIME1,
            COMPLETED_COURSE_TIME2,
            COMPLETED_COURSE_TIME3,
            COMPLETED_COURSE_TIME4,
            COMPLETED_COURSE_TIME5,
            COMPLETED_COURSE_TIME6
        }


        public class Entry
        {
            public int PlayerId { get; set; }
            public int Score { get; set; }
            public int Rank { get; set; }
        }
        public enum LeaderboardSortMode
        {
            NearbyScoresGlobal,
            NearbyScoresFriends,
            Champions,
            NUM_VALUES
        }
        public enum RequestResult
        {
            Success,
            InvalidStat,
            RedisConnectionError
        }

        public enum FilterType
        {
            Global,
            Friends
        }

        public enum Timeframe
        {
            AllTime,
            TwoWeeks
        }
        public enum SetMode
        {
            Always,
            IfGreater,
            IfLess
        }
        public enum GameUIDataModelStatFormat
        {
            INVALID = -1,
            INT,
            TIME_MILLISECONDS
        }
        public enum SetModeType
        {
            SetAlways,
            Increment,
            SetIfGreater,
            SetIfLess
        }
    }
}
