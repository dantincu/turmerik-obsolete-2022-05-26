using FsUtils.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FsUtils.Testing.Core.Tests
{
    public class UnitTestCoreBase
    {
        protected readonly IAppLogger logger;

        public UnitTestCoreBase()
        {
            logger = new AppLogger(this.GetType());
        }

        protected T AssertEqual<T>(Func<T> valueFactory, T expectedValue, IEqualityComparer<T> comparer = null)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;
            T actualValue = valueFactory();

            Assert.Equal(expectedValue, actualValue, comparer);
            return actualValue;
        }

        protected T AssertTrue<T>(Func<T> valueFactory, Func<T, bool> validator)
        {
            T actualValue = valueFactory();
            bool isValid = validator(actualValue);

            Assert.True(isValid);
            return actualValue;
        }
    }
}
