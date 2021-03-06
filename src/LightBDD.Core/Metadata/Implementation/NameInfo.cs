using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LightBDD.Core.Formatting.NameDecorators;

namespace LightBDD.Core.Metadata.Implementation
{
    [DebuggerStepThrough]
    internal class NameInfo : INameInfo
    {
        public string NameFormat { get; }

        public IEnumerable<INameParameterInfo> Parameters { get; }

        public NameInfo(string nameFormat, INameParameterInfo[] parameters)
        {
            NameFormat = nameFormat;
            Parameters = parameters;
        }

        public override string ToString()
        {
            return Format(StepNameDecorators.Default);
        }

        public string Format(INameDecorator decorator)
        {
            if (!Parameters.Any())
                return decorator.DecorateNameFormat(NameFormat);
            return string.Format(NameFormat, Parameters.Select(p => (object)decorator.DecorateParameterValue(p)).ToArray());
        }
    }
}