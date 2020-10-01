using System;
using System.Collections.Generic;
using System.Text;

namespace WellsFargo.Libraries.Models
{
    public enum NotificationPreferenceType : byte
    {
        Email = 0,
        SMS = 1,
        Others = 2,
        Both = 3
    }
}
