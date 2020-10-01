using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.ORM.Impl
{
    public class CustomerProfileEntityTypeConfiguration : IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            builder.HasKey(model => model.CustomerProfileId);

            builder.HasMany(model => model.CommunicationAddresses)
                   .WithOne(address => address.CustomerProfile)
                   .IsRequired();

            builder.HasMany(model => model.NotificationPreferences)
                   .WithOne(notificationPreference => notificationPreference.CustomerProfile)
                   .IsRequired();

            builder.ToTable("CustomerProfiles");
        }
    }
}
