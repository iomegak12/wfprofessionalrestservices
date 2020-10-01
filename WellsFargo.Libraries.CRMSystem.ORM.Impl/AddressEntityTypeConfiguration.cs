using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.ORM.Impl
{
    public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(model => model.Zip)
                   .HasColumnName("ZipCode");

            builder.HasKey(model =>
                new
                {
                    model.AddressId,
                    model.CustomerProfileId
                });

            builder.HasOne(model => model.CustomerProfile)
                   .WithMany(customerProfile => customerProfile.CommunicationAddresses)
                   .IsRequired();

            builder.ToTable("CommunicationAddresses");
        }
    }
}
