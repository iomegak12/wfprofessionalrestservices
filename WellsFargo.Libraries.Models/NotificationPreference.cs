using System;
using System.Collections.Generic;
using System.Text;

namespace WellsFargo.Libraries.Models
{
    public class NotificationPreference
    {
        public int NotificationPreferenceId { get; set; }
        public int CustomerProfileId { get; set; }
        public NotificationPreferenceType NotificationType { get; set; }

        public CustomerProfile CustomerProfile { get; set; }

        public override string ToString()
        {
            return string.Format(@"{0}, {1}, {2}", this.NotificationPreferenceId, this.CustomerProfileId, this.NotificationType.ToString());
        }
    }
}
