using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;

namespace Domain.Tests.ExternalTeamTests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void ShouldNotBeValidWithoutPhoneNumber()
        {
            var sut = new ExternalTeam
                          {
                              Club = new Club(),
                              Name = "TeamName",
                              ContactEmailAddress = "test@test.com",
                              ContactName = "Contact Name",
                              CityState = "City, ST"
                          };

            //sut.ContactPhoneNumber = "1234567890";

            Assert.IsFalse(Validator.TryValidateObject(sut, new ValidationContext(sut, null, null), new List<ValidationResult>()));
        }

        [Test]
        public void ShouldNotBeValidWithoutClub()
        {
            var sut = new ExternalTeam
                          {
                              Name = "TeamName",
                              ContactEmailAddress = "test@test.com",
                              ContactName = "Contact Name",
                              CityState = "City, ST",
                              ContactPhoneNumber = "1234567890"
                          };


            Assert.IsFalse(Validator.TryValidateObject(sut, new ValidationContext(sut, null, null), new List<ValidationResult>()));
        }
    }
}