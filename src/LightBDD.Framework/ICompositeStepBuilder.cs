﻿using System.Diagnostics;
using System.Threading.Tasks;

namespace LightBDD.Framework
{
    /// <summary>
    /// Generic allowing to compose <see cref="CompositeStep"/> instance.
    /// </summary>
    /// <typeparam name="TContext">Type of context that would be shared between steps.</typeparam>
    public interface ICompositeStepBuilder<TContext>
    {
        /// <summary>
        /// Builds <see cref="CompositeStep"/> based on specified steps and step context.
        /// </summary>
        /// <returns><see cref="CompositeStep"/> instance.</returns>
        CompositeStep Build();
    }

    /// <summary>
    /// Interface allowing to compose <see cref="CompositeStep"/> instance.
    /// </summary>
    public interface ICompositeStepBuilder : ICompositeStepBuilder<NoContext>
    {
    }
}