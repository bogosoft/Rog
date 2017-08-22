using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Rog
{
    /// <summary>
    /// An implementation of the <see cref="IValueProvider"/> contract which can
    /// generate objects against a given complex type.
    /// </summary>
    public sealed class DefaultComplexTypeProvider : NullProviderBase
    {
        const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public;

        Dictionary<Type, ConstructorData> cache = new Dictionary<Type, ConstructorData>();
        ReaderWriterLockSlim @lock = new ReaderWriterLockSlim();

        ConstructorData GetConstructorData(Type type)
        {
            @lock.EnterUpgradeableReadLock();

            try
            {
                if (!cache.ContainsKey(type))
                {
                    @lock.EnterWriteLock();

                    try
                    {
                        cache.Add(type);
                    }
                    finally
                    {
                        @lock.ExitWriteLock();
                    }
                }

                return cache[type];
            }
            finally
            {
                @lock.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// When overridden in a derived class, returns a value that is not null.
        /// </summary>
        /// <param name="context">
        /// The context within which a value will be generated.
        /// </param>
        /// <returns>A generated non-null value.</returns>
        protected override object GetNonNullValue(GenerationContext context)
        {
            var data = GetConstructorData(context.CurrentType);

            var args = data.ArgTypes.Select(x => context.Generate(x)).ToArray();

            var @object = data.Constructor.Invoke(args);

            foreach (var fi in context.CurrentType.GetFields(Flags))
            {
                fi.SetValue(@object, context.Generate(fi.FieldType, fi.GetCustomAttributes()));
            }

            foreach (var pi in context.CurrentType.GetProperties(Flags).Where(x => x.CanRead && x.CanWrite))
            {
                pi.SetValue(@object, context.Generate(pi.PropertyType, pi.GetCustomAttributes()));
            }

            return @object;
        }

        /// <summary>
        /// Determine whether the curren value provider is capable of
        /// generating a value against a given type.
        /// </summary>
        /// <param name="type">A type to generate a value against.</param>
        /// <returns>
        /// True if the given type can be used to generate a value for; false otherwise.
        /// </returns>
        public override bool Matches(Type type) => !type.IsAbstract && !type.IsInterface;
    }
}