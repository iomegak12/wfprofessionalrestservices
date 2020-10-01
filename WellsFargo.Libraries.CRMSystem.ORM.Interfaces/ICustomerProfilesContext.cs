using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.ORM.Interfaces
{
    public interface ICustomerProfilesContext : ISystemContext
    {
        DbSet<CustomerProfile> CustomerProfiles { get; set; }
        DbSet<Address> CommunicationAddresses { get; set; }
        DbSet<NotificationPreference> NotificationPreferences { get; set; }
    }
}
