using System;
using Xunit;

namespace CodeCube.Core.Extensions.Tests.DateTimeExtensionsTests
{
    public sealed class ToIsoDateTests
    {
        [Fact]
        public void ToISODate_Succeeds()
        {
            //Setup
            var dateTime = new DateTime(2022, 2, 18);
            const string expected = "2022-02-18";

            //Act
            var ISODate = dateTime.ToISODate();

            //Assert
            Assert.Equal(expected, ISODate);
        }

        [Fact]
        public void ToISODateTime_Succeeds()
        {
            //Setup
            var dateTime = new DateTime(2022, 2, 18, 16, 09, 10);
            const string expected = "2022-02-18T16:09:10";

            //Act
            var ISODate = dateTime.ToISODateTime();

            //Assert
            Assert.Equal(expected, ISODate);
        }
    }
}
