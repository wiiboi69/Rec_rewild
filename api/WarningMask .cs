using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api
{
    internal class WarningMask
    {
        [Flags]
        public enum WarningMaskType
        {
            None = 0,
            Scary = 1,
            Mature = 2,
            FlashingLights = 4,
            IntenseMotion = 8,
            Violence = 16,
            Custom = 32,
            Reports = 64
        }
    }
}
