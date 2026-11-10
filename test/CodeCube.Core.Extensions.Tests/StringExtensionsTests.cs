using Xunit;

namespace CodeCube.Core.Extensions.Tests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void AsSha512_ReturnsKnownHash()
        {
            const string expected = "ddaf35a193617abacc417349ae20413112e6fa4e89a97ea20a9eeee64b55d39a2192992a274fc1a836ba3c23a3feebbd454d4423643ce80e2a9ac94fa54ca49f";

            Assert.Equal(expected, "abc".AsSha512());
        }

        [Theory]
        [InlineData("Hello World", "hello-world")]
        [InlineData("  Hello   --  World  ", "hello-world")]
        [InlineData("Wat is dit?!", "wat-is-dit")]
        [InlineData("", "")]
        public void GenerateSlug_ReturnsFriendlySlug(string input, string expected)
        {
            Assert.Equal(expected, input.GenerateSlug());
        }

        [Fact]
        public void GenerateSlug_CutsAtMaxLength()
        {
            Assert.Equal("hello", "hello world".GenerateSlug(5));
        }

        [Theory]
        [InlineData("Tom & Jerry", "tom-and-jerry")]
        [InlineData("It's a Test", "its-a-test")]
        [InlineData("--Hello__World--", "hello-world")]
        [InlineData(null, "")]
        public void AsFriendlyUrl_ReturnsExpected(string? input, string expected)
        {
            Assert.Equal(expected, input!.AsFriendlyUrl());
        }

        [Theory]
        [InlineData("<p>Hello</p>", "Hello")]
        [InlineData("Hello <b>big</b> world", "Hello big world")]
        [InlineData("no tags", "no tags")]
        public void StripTags_RemovesAllTags(string input, string expected)
        {
            Assert.Equal(expected, input.StripTags());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void StripTags_ReturnsNull_ForEmptyInput(string? input)
        {
            Assert.Null(input!.StripTags());
            Assert.Null(input!.StripTags(new[] { "b" }));
            Assert.Null(input!.StripHtml());
        }

        [Fact]
        public void StripTags_WithAllowedTags_KeepsAllowedTags()
        {
            var result = "<b>bold</b><i>italic</i>".StripTags(new[] { "b" });

            Assert.Equal("<b>bold</b>italic", result);
        }

        [Fact]
        public void StripHtml_RemovesHtml()
        {
            Assert.Equal("Hello world", "<div>Hello <span>world</span></div>".StripHtml());
        }

        [Fact]
        public void ReplaceFirst_ReplacesOnlyFirstOccurrence()
        {
            Assert.Equal("b-a-a", "a-a-a".ReplaceFirst("a", "b"));
        }

        [Fact]
        public void ReplaceFirst_EscapesRegexCharacters()
        {
            Assert.Equal("x+b", "a.b+b".ReplaceFirst("a.b", "x"));
        }

        [Fact]
        public void ShortenText_ReturnsNull_ForNull()
        {
            Assert.Null(((string?)null).ShortenText(10));
        }

        [Fact]
        public void ShortenText_ReturnsOriginal_WhenShortEnough()
        {
            Assert.Equal("short", "short".ShortenText(10));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(3)] // not larger than the suffix
        public void ShortenText_ReturnsOriginal_ForUnusableLength(int length)
        {
            Assert.Equal("some long text", "some long text".ShortenText(length));
        }

        [Fact]
        public void ShortenText_KeepFullWords_CutsAtLastSpace()
        {
            Assert.Equal("one two...", "one two three four".ShortenText(12));
        }

        [Fact]
        public void ShortenText_WithoutFullWords_CutsExactlyAndAddsSuffix()
        {
            Assert.Equal("abcdefg...", "abcdefghijklmnop".ShortenText(10, true, false));
        }

        [Fact]
        public void ShortenText_WithoutSuffix_CutsExactly()
        {
            Assert.Equal("abcdefghij", "abcdefghijklmnop".ShortenText(10, false, false));
        }

        [Theory]
        [InlineData("hello", "Hello")]
        [InlineData("Hello", "Hello")]
        [InlineData("h", "H")]
        [InlineData("", "")]
        public void UppercaseFirstChar_Works(string input, string expected)
        {
            Assert.Equal(expected, input.UppercaseFirstChar());
        }

        [Theory]
        [InlineData("d3b07384-d9a0-4c9e-a4b8-2f1e1c3a5c11", true)]
        [InlineData("not-a-guid", false)]
        [InlineData("", false)]
        public void IsValidGuid_Works(string input, bool expected)
        {
            Assert.Equal(expected, input.IsValidGuid());
        }

        [Fact]
        public void TryParseEnumOptional_ParsesCaseInsensitive()
        {
            Assert.Equal(TestEnum.Blue, "blue".TryParseEnumOptional<TestEnum>());
        }

        [Fact]
        public void TryParseEnum_WithDefault_ReturnsDefault_WhenUnparseable()
        {
            Assert.Equal(TestEnum.Blue, "purple".TryParseEnum(TestEnum.Blue));
        }

        [Fact]
        public void TryParseEnum_ReturnsFirstValue_WhenUnparseable()
        {
            Assert.Equal(TestEnum.Red, "purple".TryParseEnum<TestEnum>());
        }

        [Fact]
        public void TryParseEnumOptional_WithDefault_ReturnsDefault_WhenUnparseable()
        {
            Assert.Equal(TestEnum.Blue, "purple".TryParseEnumOptional(TestEnum.Blue));
        }
    }
}
