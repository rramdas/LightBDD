using System;
using System.Diagnostics;
using System.Linq;
using LightBDD.Core.Formatting.NameDecorators;

namespace LightBDD.Core.Metadata.Implementation
{
    [DebuggerStepThrough]
    internal class StepNameInfo : NameInfo, IStepNameInfo
    {
        public StepNameInfo(IStepTypeNameInfo stepTypeName, string nameFormat, INameParameterInfo[] parameters)
            : base(nameFormat, parameters)
        {
            StepTypeName = stepTypeName;
        }

        public static StepNameInfo WithUpdatedParameters(IStepNameInfo nameInfo, INameParameterInfo[] parameters)
        {
            if (nameInfo.Parameters.Count() != parameters.Length)
                throw new ArgumentException("StepNameInfo cannot be updated with different number of parameters");
            return new StepNameInfo(nameInfo.StepTypeName, nameInfo.NameFormat, parameters);
        }

        public IStepTypeNameInfo StepTypeName { get; }

        public override string ToString()
        {
            return Format(StepNameDecorators.Default);
        }

        public string Format(IStepNameDecorator decorator)
        {
            return (StepTypeName != null)
                ? string.Format("{0} {1}", decorator.DecorateStepTypeName(StepTypeName), base.Format(decorator))
                : base.ToString();
        }
    }
}