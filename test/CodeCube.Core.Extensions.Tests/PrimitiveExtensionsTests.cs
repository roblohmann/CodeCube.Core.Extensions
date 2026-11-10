using System;
using System.Collections.Generic;
using Xunit;

namespace CodeCube.Core.Extensions.Tests
{
    public class PrimitiveExtensionsTests
    {
        [Fact]
        public void ToNullSafeString_ReturnsEmpty_ForNull()
        {
            object? value = null;

            Assert.Equal(string.Empty, value!.ToNullSafeString());
        }

        [Fact]
        public void ToNullSafeString_ReturnsToString_ForValue()
        {
            Assert.Equal("42", 42.ToNullSafeString());
        }

        [Theory]
        [InlineData(typeof(int?), true)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(string), false)]
        public void IsNullableType_Works(Type type, bool expected)
        {
            Assert.Equal(expected, type.IsNullableType());
        }

        [Theory]
        [InlineData(typeof(List<int>), true)]
        [InlineData(typeof(string), true)] // string implements IEnumerable<char>
        [InlineData(typeof(int), false)]
        public void IsEnumerableType_Works(Type type, bool expected)
        {
            Assert.Equal(expected, type.IsEnumerableType());
        }

        [Theory]
        [InlineData(typeof(Dictionary<string, int>), true)]
        [InlineData(typeof(IDictionary<string, int>), true)]
        [InlineData(typeof(List<int>), false)]
        public void IsDictionaryType_Works(Type type, bool expected)
        {
            Assert.Equal(expected, type.IsDictionaryType());
        }

        [Fact]
        public void GetDictionaryType_ReturnsDictionaryInterface()
        {
            Assert.Equal(typeof(IDictionary<string, int>), typeof(Dictionary<string, int>).GetDictionaryType());
        }

        [Fact]
        public void GetDictionaryType_ReturnsNull_ForNonDictionary()
        {
            Assert.Null(typeof(List<int>).GetDictionaryType());
        }
    }
}
