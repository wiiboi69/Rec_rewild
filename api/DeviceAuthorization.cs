using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using Newtonsoft.Json;
using start;

namespace api
{
    internal class DeviceAuthorization
    {
        public class DeviceAuthorizationResponse
        {
            // Token: 0x1700013B RID: 315
            // (get) Token: 0x0600046A RID: 1130 RVA: 0x00004BE9 File Offset: 0x00002DE9
            // (set) Token: 0x0600046B RID: 1131 RVA: 0x00004BF1 File Offset: 0x00002DF1
            public string device_code { get; set; }

            // Token: 0x1700013C RID: 316
            // (get) Token: 0x0600046C RID: 1132 RVA: 0x00004BFA File Offset: 0x00002DFA
            // (set) Token: 0x0600046D RID: 1133 RVA: 0x00004C02 File Offset: 0x00002E02
            public string user_code { get; set; }

            // Token: 0x1700013D RID: 317
            // (get) Token: 0x0600046E RID: 1134 RVA: 0x00004C0B File Offset: 0x00002E0B
            // (set) Token: 0x0600046F RID: 1135 RVA: 0x00004C13 File Offset: 0x00002E13
            public string verification_uri { get; set; }

            // Token: 0x1700013E RID: 318
            // (get) Token: 0x06000470 RID: 1136 RVA: 0x00004C1C File Offset: 0x00002E1C
            // (set) Token: 0x06000471 RID: 1137 RVA: 0x00004C24 File Offset: 0x00002E24
            public string verification_uri_complete { get; set; }

            // Token: 0x1700013F RID: 319
            // (get) Token: 0x06000472 RID: 1138 RVA: 0x00004C2D File Offset: 0x00002E2D
            // (set) Token: 0x06000473 RID: 1139 RVA: 0x00004C35 File Offset: 0x00002E35
            public int expires_in { get; set; }

            // Token: 0x17000140 RID: 320
            // (get) Token: 0x06000474 RID: 1140 RVA: 0x00004C3E File Offset: 0x00002E3E
            // (set) Token: 0x06000475 RID: 1141 RVA: 0x00004C46 File Offset: 0x00002E46
            public int interval { get; set; }
        }
    }
}
