using System;
using System.Reflection;

namespace Enjoy.Web.Util
{
    internal static class ReflectionExtensions
    {
        public static Type GetMemberType(this MemberInfo member)
        {
            var property = member as PropertyInfo;
            if (property != null)
            {
                return property.PropertyType;
            }

            var field = member as FieldInfo;
            if (field != null)
            {
                return field.FieldType;
            }

            throw new InvalidOperationException("We're interested in properties and fields only.");
        }
    }
}