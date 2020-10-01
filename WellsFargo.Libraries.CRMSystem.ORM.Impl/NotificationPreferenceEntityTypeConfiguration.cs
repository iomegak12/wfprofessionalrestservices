using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.ORM.Impl
{
    public class NotificationPreferenceEntityTypeConfiguration : IEntityTypeConfiguration<NotificationPreference>
    {
        public void Configure(EntityTypeBuilder<NotificationPreference> builder)
        {
            builder.HasKey(
                model =>
                new
                {
                    model.NotificationPreferenceId,
                    model.CustomerProfileId
                });

            builder.Property(model => model.NotificationType)
                   .HasConversion(
                        enumValue => enumValue.ToString(),
                        enumStringValue => (NotificationPreferenceType)Enum.Parse(typeof(NotificationPreferenceType), enumStringValue));

            builder.HasOne(model => model.CustomerProfile)
                   .WithMany(model => model.NotificationPreferences)
                   .IsRequired();

            builder.ToTable("NotificationPreferences");
        }
    }
}
