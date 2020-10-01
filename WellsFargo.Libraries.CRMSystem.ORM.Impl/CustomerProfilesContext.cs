using Microsoft.EntityFrameworkCore;
using System;
using WellsFargo.Libraries.CRMSystem.ORM.Interfaces;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.ORM.Impl
{
    public class CustomerProfilesContext : DbContext, ICustomerProfilesContext
    {
        private const int MIN_ROWS_AFFECTED = 1;

        public CustomerProfilesContext(DbContextOptions<CustomerProfilesContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<Address> CommunicationAddresses { get; set; }
        public DbSet<NotificationPreference> NotificationPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<CustomerProfile>(new CustomerProfileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<Address>(new AddressEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<NotificationPreference>(new NotificationPreferenceEntityTypeConfiguration());
        }

        public bool CommitChanges()
        {
            var noOfRowsAffected = this.SaveChanges();

            return noOfRowsAffected >= MIN_ROWS_AFFECTED;
        }
    }
}
