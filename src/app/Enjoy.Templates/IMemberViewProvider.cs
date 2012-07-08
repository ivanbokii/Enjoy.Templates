using System;
using System.Reflection;
using Wandering.Monads.Maybe;

namespace Enjoy
{
    /// <summary>
    /// MemberViewProviders take a single member of an Object and yield a View.
    /// </summary>
    public interface IMemberViewProvider
    {
        /// <summary>
        /// Builds a View from the selected Member of an Object.
        /// </summary>
        Maybe<object> ForMember(MemberInfo member, Func<object> instanceAccessor);
    }
}