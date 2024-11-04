using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api
{
    internal class Roomfilters
    {
        public List<string> PinnedFilters { get; set; }
        public List<string> PopularFilters { get; set; }
        public List<string> TrendingFilters { get; set; }
    }
}
