using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodeCube.Core.Extensions.Tests
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void WithNextAndPrevious_ReturnsTriples()
        {
            var result = new[] { 1, 2, 3, 4 }.WithNextAndPrevious().ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal((1, 2, 3), (result[0].Item1, result[0].Item2, result[0].Item3));
            Assert.Equal((2, 3, 4), (result[1].Item1, result[1].Item2, result[1].Item3));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void WithNextAndPrevious_ReturnsEmpty_ForFewerThanThreeItems(int count)
        {
            Assert.Empty(Enumerable.Range(0, count).WithNextAndPrevious());
        }

        [Fact]
        public void ForEach_ExecutesActionForEachItem()
        {
            var seen = new List<int>();

            new[] { 1, 2, 3 }.ForEach(seen.Add);

            Assert.Equal(new[] { 1, 2, 3 }, seen);
        }

        [Fact]
        public void Split_SplitsInChunks_LastChunkMayBeSmaller()
        {
            var result = new List<int> { 1, 2, 3, 4, 5 }.Split(2);

            Assert.Equal(3, result.Count);
            Assert.Equal(new[] { 1, 2 }, result[0]);
            Assert.Equal(new[] { 3, 4 }, result[1]);
            Assert.Equal(new[] { 5 }, result[2]);
        }

        [Fact]
        public void Split_EmptyList_ReturnsEmpty()
        {
            Assert.Empty(new List<int>().Split(3));
        }
    }
}
