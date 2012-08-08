using System;
using System.Collections.Generic;
using Own.Failure;

namespace Enjoy.Mvc.Util
{
    internal static class InternalExtensions
    {
        /// <summary>
        /// Returns the type, then its base class, then its base class, all the way to Object.
        /// </summary>
        // TODO Find the correct name for this?
        public static IEnumerable<Type> GetTypeAncestry(this Type type)
        {
            Requires.NotNull(type, "type");
            do
            {
                yield return type;
                type = type.BaseType;
            } while (type != typeof (object) && type != null);
            yield return type;
        }
    }
}