using System;
using System.Collections.Generic;
using Xunit;

namespace CodeCube.Core.Extensions.Tests
{
    public class DictionaryExtensionsTests
    {
        [Fact]
        public void ToNameValueCollection_SkipsNullValues()
        {
            var dictionary = new Dictionary<string, object?> { ["a"] = 1, ["b"] = null, ["c"] = "x" };

            var result = dictionary.ToNameValueCollection();

            Assert.Equal(2, result.Count);
            Assert.Equal("1", result["a"]);
            Assert.Null(result["b"]);
            Assert.Equal("x", result["c"]);
        }

        [Fact]
        public void AddRange_AddsOnlyMissingKeys()
        {
            var target = new Dictionary<string, int> { ["a"] = 1 };
            var source = new Dictionary<string, int> { ["a"] = 99, ["b"] = 2 };

            target.AddRange(source);

            Assert.Equal(1, target["a"]);
            Assert.Equal(2, target["b"]);
        }

        [Fact]
        public void AddRange_Throws_ForNullSource()
        {
            var target = new Dictionary<string, int>();

            Assert.Throws<ArgumentNullException>(() => target.AddRange(null!));
        }

        [Fact]
        public void Filter_ReturnsMatchingEntriesOnly()
        {
            var dictionary = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 };

            var result = dictionary.Filter(kvp => kvp.Value > 1);

            Assert.Equal(2, result.Count);
            Assert.False(result.ContainsKey("a"));
        }
    }
}
