using System;
using System.Reflection;

namespace LightBDD.Core.Extensibility
{
    /// <summary>
    /// Class describing scenario or step method parameter and offering a method to evaluate parameter value.
    /// </summary>
    public class ParameterDescriptor
    {
        private ParameterDescriptor(bool isConstant, ParameterInfo parameterInfo, Func<object, object> valueEvaluator)
        {
            if (parameterInfo == null)
                throw new ArgumentNullException(nameof(parameterInfo));
            if (valueEvaluator == null)
                throw new ArgumentNullException(nameof(valueEvaluator));
            RawName = parameterInfo.Name;
            IsConstant = isConstant;
            ParameterInfo = parameterInfo;
            ValueEvaluator = valueEvaluator;
        }
        /// <summary>
        /// Creates <see cref="ParameterDescriptor"/> object representing <paramref name="parameterInfo"/> with constant value <paramref name="value"/>.
        /// </summary>
        /// <param name="parameterInfo"><see cref="ParameterInfo"/> object describing parameter.</param>
        /// <param name="value">Constant value that would be bound to the parameter.</param>
        /// <returns></returns>
        public static ParameterDescriptor FromConstant(ParameterInfo parameterInfo, object value) => new ParameterDescriptor(true, parameterInfo, ctx => value);
        /// <summary>
        /// Creates <see cref="ParameterDescriptor"/> object representing <paramref name="parameterInfo"/> with not-constant value provided by <paramref name="valueEvaluator"/>.
        /// It is expected that <paramref name="valueEvaluator"/> would be used once to evaluate parameter value, just before execution of method requiring this parameter value.
        /// </summary>
        /// <param name="parameterInfo"><see cref="ParameterInfo"/> object describing parameter.</param>
        /// <param name="valueEvaluator">Value evaluator function that should be used to retrieve parameter value.</param>
        /// <returns></returns>
        public static ParameterDescriptor FromInvocation(ParameterInfo parameterInfo, Func<object, object> valueEvaluator) => new ParameterDescriptor(false, parameterInfo, valueEvaluator);
        /// <summary>
        /// Returns parameter raw name.
        /// </summary>
        public string RawName { get; private set; }
        /// <summary>
        /// Returns <c>true</c> if parameter is defined as constant with known value or <c>false</c> if parameter value has to be evaluated first in order to be known.
        /// </summary>
        public bool IsConstant { get; private set; }
        /// <summary>
        /// Returns <see cref="ParameterInfo"/> object describing this parameter.
        /// </summary>
        public ParameterInfo ParameterInfo { get; private set; }
        /// <summary>
        /// Returns parameter value evaluator that would be used to evaluate parameter value during execution.
        /// The value evaluator function parameter represents scenario context object defined by <see cref="IScenarioRunner.WithContext"/>() method.
        /// </summary>
        public Func<object, object> ValueEvaluator { get; private set; }
    }
}