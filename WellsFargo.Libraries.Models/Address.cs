using System;
using System.Collections.Generic;
using System.Text;

namespace WellsFargo.Libraries.Models
{
    public class Address
    {
        public CustomerProfile CustomerProfile { get; set; }
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public int CustomerProfileId { get; set; }

        public override string ToString()
        {
            return string.Format(@"{0}, {1}, {2}, {3}, {4}, {5}",
                this.AddressLine1, this.AddressLine2, this.City, this.State, this.Country, this.Zip);
        }
    }
}
