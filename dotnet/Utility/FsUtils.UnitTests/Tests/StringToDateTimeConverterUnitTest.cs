using FsUtils.Testing.Core.Tests;
using Turmerik.Core.Data;
using Xunit;

namespace FsUtils.UnitTests.Tests
{
    public class StringToDateTimeConverterUnitTest : UnitTestCoreBase
    {
        private readonly StringToDateTimeConverter converter;

        public StringToDateTimeConverterUnitTest()
        {
            converter = new StringToDateTimeConverter();
        }

        [Fact]
        public void MainTest()
        {

        }
    }
}