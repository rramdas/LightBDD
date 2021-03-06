﻿using System;
using LightBDD.Core.Extensibility;
using Moq;
using NUnit.Framework;

namespace LightBDD.Framework.UnitTests.Scenarios.Contextual.Helpers
{
    public class ContextualScenarioTestsBase
    {
        protected Mock<ITestableBddRunner> Runner;
        protected Mock<IScenarioRunner> MockScenarioRunner;
        protected Func<object> CapturedContextProvider;

        public interface ITestableBddRunner : IBddRunner, IFeatureFixtureRunner { }

        protected class MyContext { }

        protected void VerifyAllExpectations()
        {
            Runner.Verify();
            MockScenarioRunner.Verify();
        }

        protected void ExpectContext()
        {
            MockScenarioRunner
                .Setup(s => s.WithContext(It.IsAny<Func<object>>()))
                .Returns((Func<object> obj) =>
                {
                    CapturedContextProvider = obj;
                    return MockScenarioRunner.Object;
                })
                .Verifiable();
        }

        protected void ExpectNewScenario()
        {
            Runner
                .Setup(r => r.NewScenario())
                .Returns(MockScenarioRunner.Object)
                .Verifiable();
        }

        [SetUp]
        public void SetUp()
        {
            Runner = new Mock<ITestableBddRunner>(MockBehavior.Strict);
            MockScenarioRunner = new Mock<IScenarioRunner>(MockBehavior.Strict);
            CapturedContextProvider = null;
        }
    }
}