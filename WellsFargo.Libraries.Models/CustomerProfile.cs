using System;
using System.Collections;
using System.Collections.Generic;

namespace WellsFargo.Libraries.Models
{
    public class CustomerProfile
    {
        public int CustomerProfileId { get; set; }
        public string FullName { get; set; }
        public ICollection<Address> CommunicationAddresses { get; set; }
        public ICollection<NotificationPreference> NotificationPreferences { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int LoyaltyPoint { get; set; }
        public string CustomerType { get; set; }
        public string Remarks { get; set; }

        public override string ToString()
        {
            return string.Format(@"{0}, {1}, {2}, {3}, {4}, {5}, {6}",
                this.CustomerProfileId, this.FullName, this.Email, this.PhoneNumber, this.LoyaltyPoint,
                this.CustomerType, this.Remarks);
        }
    }
}
