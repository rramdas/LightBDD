﻿using LightBDD.Framework;
using Xunit;
using Xunit.Abstractions;

namespace LightBDD.XUnit2.UnitTests
{
    public class FeatureFixtureTests
    {
        class TestableFeatureFixture : FeatureFixture
        {
            public TestableFeatureFixture(ITestOutputHelper output) : base(output)
            {
            }

            public IBddRunner GetRunner() => Runner;
        }

        [Fact]
        public void Runner_should_be_initialized()
        {
            Assert.NotNull(new TestableFeatureFixture(null).GetRunner());
        }
    }
}