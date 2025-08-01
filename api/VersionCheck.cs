using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rec_rewild.api
{
    public class VersionCheck
    {
        public VersionStatus VersionStatus { get; set; }

    }
    public class NewVersionCheck
    {
        public VersionStatus ValidVersion { get; set; }
        public VersionStatus VersionStatus { get; set; }
        public UpdateNoti UpdateNotificationStage { get; set; }
        public bool IsVersionIslanded { get; set; }
        public bool IsCrossPlayDisabled { get; set; }

    }

    public enum UpdateNoti
    {
        None,
        Silent,
        Warn,
        Prompt,
        Require
    }

    public enum VersionStatus
    {
        ValidForPlay,
        UpdateRequired
    }
}
