using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WellsFargo.Libraries.CRMSystem.Domain.Interfaces;
using WellsFargo.Libraries.Models;
using Xunit;

namespace WellsFargo.Libraries.CRMSystem.Services.Impl.Tests
{
    public class CustomerProfilesControllerTest
    {
        [Fact]
        public void ShouldGetCustomerProfilesBySearchStringReturnValidResults()
        {
            var searchString = "Balf";
            var mockCustomers = new List<CustomerProfile>
            {
                new CustomerProfile { CustomerProfileId = 1, FullName = "Balfton", CustomerType = "Silver", Email = "info@email.com", PhoneNumber = "080-98349384", LoyaltyPoint = 299, Remarks = "Simple Remarks"},
                new CustomerProfile { CustomerProfileId = 2, FullName = "Json Baltol", CustomerType = "Silver", Email = "info@email.com", PhoneNumber = "080-98349384", LoyaltyPoint = 299, Remarks = "Simple Remarks"},
                new CustomerProfile { CustomerProfileId = 3, FullName = "Mukhi Balf", CustomerType = "Silver", Email = "info@email.com", PhoneNumber = "080-98349384", LoyaltyPoint = 299, Remarks = "Simple Remarks"}
            };
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockCustomerProfileDomainService = mockRepository.Create<ICustomerProfileDomainService>();

            mockCustomerProfileDomainService
                .Setup(service => service.GetCustomerProfiles(searchString))
                .Returns(mockCustomers);

            var customerProfilesController = new CustomerProfilesController(mockCustomerProfileDomainService.Object);
            var result = customerProfilesController.GetCustomerProfiles(searchString);

            Assert.NotNull(result);

            var convertedResult = result as OkObjectResult;

            Assert.NotNull(convertedResult);

            var actualCustomers = convertedResult.Value as IEnumerable<CustomerProfile>;

            Assert.NotNull(actualCustomers);

            var expectedNoOfCustomers = 3;
            var actualNoOfCustomers = actualCustomers.Count();

            Assert.Equal(expectedNoOfCustomers, actualNoOfCustomers);

            var expectedCustomerName = "Balfton";
            var actualCustomerName = actualCustomers.First().FullName;

            Assert.Equal(expectedCustomerName, actualCustomerName);
        }

        [Fact]
        public void ShouldGetCustomerProfilesBySearchStringThrowsException()
        {
            var searchString = "Balf";
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockCustomerProfileDomainService = mockRepository.Create<ICustomerProfileDomainService>();

            mockCustomerProfileDomainService
                .Setup(service => service.GetCustomerProfiles(searchString))
                .Throws(new Exception("Unknown Error Occurred!"));

            var customerProfilesController = new CustomerProfilesController(mockCustomerProfileDomainService.Object);

            Assert.Throws(typeof(Exception), () =>
            {
                customerProfilesController.GetCustomerProfiles(searchString);
            });
        }
    }
}
