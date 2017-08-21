using System;
using System.Collections.Generic;

namespace Rog
{
    /// <summary>
    /// Extended functionality for lists of <see cref="IValueProvider"/> objects.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Add a delegate to the current list of value providers.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the object that the delegate will generate.
        /// </typeparam>
        /// <param name="providers">The current list of providers.</param>
        /// <param name="provider">
        /// The delegate provider to add to the current list of providers.
        /// </param>
        public static void Add<T>(this List<IValueProvider> providers, Func<GenerationContext, T> provider)
        {
            providers.Add(new DelegatedRandomTypedValueProvider<T>(provider));
        }
    }
}