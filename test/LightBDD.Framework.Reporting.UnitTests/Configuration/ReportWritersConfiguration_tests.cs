﻿using System;
using System.IO;
using System.Linq;
using LightBDD.Core.Configuration;
using LightBDD.Core.Reporting;
using LightBDD.Framework.Reporting.Configuration;
using LightBDD.Framework.Reporting.Formatters;
using Moq;
using NUnit.Framework;

namespace LightBDD.Framework.Reporting.UnitTests.Configuration
{
    [TestFixture]
    public class ReportWritersConfiguration_tests
    {
        [Test]
        public void It_should_return_default_configuration()
        {
            var configuration = new ReportWritersConfiguration();
            Assert.That(configuration.Count, Is.EqualTo(2));

            var featuresReportXml = @"~\Reports\FeaturesReport.xml";
            var featuresReportHtml = @"~\Reports\FeaturesReport.html";

            AssertWriter(configuration, featuresReportXml, typeof(XmlReportFormatter), featuresReportXml.Replace("~", AppDomainHelper.BaseDirectory));
            AssertWriter(configuration, featuresReportHtml, typeof(HtmlReportFormatter), featuresReportHtml.Replace("~", AppDomainHelper.BaseDirectory));
        }

        private void AssertWriter(ReportWritersConfiguration configuration, string expectedRelativePath, Type expectedFormatterType, string expectedFullPath)
        {
            var writer = configuration.OfType<ReportFileWriter>().FirstOrDefault(w => w.OutputPath == expectedRelativePath);
            Assert.That(writer, Is.Not.Null, $"Expected to find writer with path: {expectedRelativePath}");

            Assert.That(writer.FullOutputPath, Is.EqualTo(Path.GetFullPath(expectedFullPath)));
            Assert.That(writer.Formatter, Is.InstanceOf(expectedFormatterType));
        }

        [Test]
        public void It_should_allow_clear_add_and_remove_items()
        {
            var writer = Mock.Of<IReportWriter>();
            var writer2 = Mock.Of<IReportWriter>();
            var configuration = new ReportWritersConfiguration();
            Assert.That(configuration.Clear(), Is.Empty);
            Assert.That(configuration.Add(writer).Add(writer2).ToArray(), Is.EqualTo(new[] { writer, writer2 }));
            Assert.That(configuration.Remove(writer).ToArray(), Is.EqualTo(new[] { writer2 }));
        }

        [Test]
        public void It_should_not_allow_null_items()
        {
            var configuration = new ReportWritersConfiguration();
            Assert.Throws<ArgumentNullException>(() => configuration.Add(null));
        }

        [Test]
        public void It_should_allow_adding_file_writer_with_extension_method()
        {
            var writer = new ReportWritersConfiguration()
                .Clear()
                .AddFileWriter<PlainTextReportFormatter>("file.txt")
                .Cast<ReportFileWriter>()
                .SingleOrDefault();

            Assert.That(writer, Is.Not.Null);
            Assert.That(writer.Formatter, Is.TypeOf<PlainTextReportFormatter>());
            Assert.That(writer.OutputPath, Is.EqualTo("file.txt"));
        }

        [Test]
        public void Configuration_should_be_sealable()
        {
            var writer = Mock.Of<IReportWriter>();
            var lighbddConfig = new LightBddConfiguration();
            var cfg = lighbddConfig.Get<ReportWritersConfiguration>().Add(writer);
            lighbddConfig.Seal();

            Assert.Throws<InvalidOperationException>(() => cfg.Add(Mock.Of<IReportWriter>()));
            Assert.Throws<InvalidOperationException>(() => cfg.Clear());
            Assert.Throws<InvalidOperationException>(() => cfg.Remove(writer));
            Assert.That(cfg.ToArray(), Is.Not.Empty);
        }
    }
}
