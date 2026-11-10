using Xunit;

namespace CodeCube.Core.Extensions.Tests
{
    public class IntegerExtensionsTests
    {
        [Fact]
        public void TryParseEnum_ParsesKnownValue()
        {
            Assert.Equal(TestEnum.Blue, 1.TryParseEnum<TestEnum>());
        }

        [Fact]
        public void TryParseEnum_WithDefault_ReturnsParsedValue_WhenKnown()
        {
            Assert.Equal(TestEnum.Red, 0.TryParseEnum(TestEnum.Blue));
        }

        [Fact]
        public void TryParseEnumOptional_ParsesKnownValue()
        {
            Assert.Equal(TestEnum.Blue, 1.TryParseEnumOptional<TestEnum>());
        }

        [Fact]
        public void TryParseEnumOptional_WithDefault_ReturnsParsedValue()
        {
            Assert.Equal(TestEnum.Blue, 1.TryParseEnumOptional(TestEnum.Red));
        }

        [Fact]
        public void Nullable_TryParseEnum_WithNull_ReturnsDefault()
        {
            int? value = null;

            Assert.Equal(TestEnum.Blue, value.TryParseEnum(TestEnum.Blue));
            Assert.Equal(TestEnum.Red, value.TryParseEnum<TestEnum>());
        }

        [Fact]
        public void Nullable_TryParseEnum_WithValue_ParsesValue()
        {
            int? value = 1;

            Assert.Equal(TestEnum.Blue, value.TryParseEnum<TestEnum>());
            Assert.Equal(TestEnum.Blue, value.TryParseEnum(TestEnum.Red));
        }

        [Fact]
        public void Nullable_TryParseEnumOptional_WithNull_ReturnsDefaultValue()
        {
            int? value = null;

            Assert.Equal(TestEnum.Blue, value.TryParseEnumOptional(TestEnum.Blue));
            Assert.Null(value.TryParseEnumOptional<TestEnum>());
        }

        [Fact]
        public void Nullable_TryParseEnumOptional_WithValue_ParsesValue()
        {
            int? value = 1;

            Assert.Equal(TestEnum.Blue, value.TryParseEnumOptional<TestEnum>());
            Assert.Equal(TestEnum.Blue, value.TryParseEnumOptional(TestEnum.Red));
        }
    }
}
