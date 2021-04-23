using Xunit;

namespace CodeCube.Core.Extensions.Tests
{
    public enum TestEnum
    {
        Red = 0,
        Blue = 1,
    }

    public class EnumExtensionsTests
    {
        [Fact]
        public void ToEnumShouldParseEqualString()
        {
            const string test = "Red";
            TestEnum? result = test.TryParseEnum<TestEnum>();
            Assert.Equal(TestEnum.Red, result);
        }

        [Fact]
        public void ToEnumShouldParseWrongCaseString()
        {
            const string test = "blue";
            TestEnum? result = test.TryParseEnum<TestEnum>();
            Assert.Equal(TestEnum.Blue, result);
        }

        [Fact]
        public void ToEnumShouldBeNullForInvalidString()
        {
            const string test = "orange";
            TestEnum? result = test.TryParseEnumOptional<TestEnum>();
            Assert.False(result.HasValue);
        }
    }
}