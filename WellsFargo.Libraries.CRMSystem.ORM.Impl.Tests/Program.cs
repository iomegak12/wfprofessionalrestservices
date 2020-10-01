using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WellsFargo.Libraries.Models;

namespace WellsFargo.Libraries.CRMSystem.ORM.Impl.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("WFCRMSystemDB");
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<CustomerProfilesContext>();

            dbContextOptionsBuilder.UseSqlServer(connectionString);

            using (var customerProfilesContext = new CustomerProfilesContext(dbContextOptionsBuilder.Options))
            {
                var customerProfiles =
                    customerProfilesContext
                        .CustomerProfiles
                        .Where(customerProfile => customerProfile.FullName.Contains("Balf"))
                        .ToList();

                foreach (var customerProfile in customerProfiles)
                {
                    Console.WriteLine(customerProfile.ToString());

                    Console.WriteLine();

                    customerProfilesContext.Entry(customerProfile).Collection(profile => profile.CommunicationAddresses).Load();

                    if (customerProfile.CommunicationAddresses != default(ICollection<Address>))
                    {
                        foreach (var address in customerProfile.CommunicationAddresses)
                        {
                            Console.WriteLine(address.ToString());
                        }
                    }

                    Console.WriteLine();

                    customerProfilesContext.Entry(customerProfile).Collection(profile => profile.NotificationPreferences).Load();

                    if (customerProfile.NotificationPreferences != default(ICollection<NotificationPreference>))
                    {
                        foreach (var notificationPreference in customerProfile.NotificationPreferences)
                        {
                            Console.WriteLine(notificationPreference.ToString());
                        }
                    }
                }
            }

            Console.WriteLine("End of Application!");
            Console.ReadLine();
        }
    }
}
