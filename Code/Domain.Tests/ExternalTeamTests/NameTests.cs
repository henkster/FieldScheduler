using System;
using NUnit.Framework;

namespace Domain.Tests.ExternalTeamTests
{
    [TestFixture]
    public class FullNameTests
    {
        const string ClubName = "ClubName";
        const string TeamName = "TeamName";

        [Test]
        public void ShouldBeClubNameDashTeamName()
        {
            var club = new Club
                           {
                               Name = ClubName
                           };
            var sut = new ExternalTeam
                          {
                              Club = club,
                              Name = TeamName
                          };
            Assert.AreEqual("ClubName - TeamName", sut.FullName);
        }

        //[Test]
        //public void ShouldThroughExceptionIfClubNotDefined()
        //{
        //    var sut = new ExternalTeam
        //    {
        //        Name = TeamName
        //    };

        //    Type t = typeof (ExternalTeam);

        //    var td = (TestDelegate)Delegate.CreateDelegate(t, t.GetMethod("FullName"));

        //    Assert.Throws<NullReferenceException>();
        //}

        [Test]
        [ExpectedException]
        public void ShouldThroughExceptionIfClubNotDefined()
        {
            var sut = new ExternalTeam
            {
                Name = TeamName
            };

            var x = sut.FullName;
        }

        [Test]
        public void ShouldIncludeD1IfTeamIsD1()
        {
            var sut = new ExternalTeam
                          {
                              Club = new Club {Name = ClubName},
                              Name = TeamName,
                              Level = Level.D1
                          };

            Assert.AreEqual("ClubName - TeamName (D1)", sut.FullName);
        }

        [Test]
        public void ShouldNotIncludeLevelIfTeamIsAcademy()
        {
            var sut = new ExternalTeam
            {
                Club = new Club { Name = ClubName },
                Name = TeamName,
                Level = Level.Academy
            };

            Assert.AreEqual("ClubName - TeamName", sut.FullName);
        }
    }
}