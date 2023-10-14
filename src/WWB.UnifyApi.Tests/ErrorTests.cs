using Xunit;

namespace WWB.UnifyApi.Tests
{
    public class ErrorTests
    {
        [Fact]
        public void TestNone()
        {
            throw FriendlyException.Of(Errors.None);
        }

        [Fact]
        public void TestNotDesc()
        {
            throw FriendlyException.Of(Errors.TestNotDesc);
        }

        [Fact]
        public void TestWithFormat()
        {
            try
            {
                throw FriendlyException.Of(Errors.TestWithFormat, "aaaa", "dddd");
            }
            catch
            {
                throw FriendlyException.Of(Errors.TestWithFormat, "aaaa", "dddd");
            }
        }
    }
}