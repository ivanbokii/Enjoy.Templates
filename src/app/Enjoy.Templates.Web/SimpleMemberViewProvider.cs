using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Enjoy.Web.Util;
using Enjoy.Web.Views.Fields;
using Fasterflect;
using Wandering.Monads.Maybe;

namespace Enjoy.Web
{
    public class SimpleMemberViewProvider : IMemberViewProvider
    {
        public Maybe<object> ForMember(MemberInfo member, Func<object> instanceAccessor)
        {
            Func<object> propertyAccessor = () => member.Get(instanceAccessor());
            var fieldType = member.GetMemberType();
            foreach (var builder in Builders.Where(builder => builder.Key(fieldType)))
            {
                return builder.Value(member, propertyAccessor);
            }
            return new Nothing<object>();
        }

        private static object ForString(MemberInfo member, Func<object> propertyAccessor)
        {
            var value = propertyAccessor();
            return new TextField
                {
                    Key = member.Name.ToLowerInvariant(),
                    Label = string.Join(" ", member.Name.SplitIdentifier()).ToLowerInvariant().FirstCharToUpper(),
                    Value = value
                };
        }

        private static readonly IDictionary<Func<Type, bool>, Func<MemberInfo, Func<object>, object>> Builders =
            new Dictionary<Func<Type, bool>, Func<MemberInfo, Func<object>, object>>
                {
                    {type => type == typeof (string), (member, propertyAccessor) => ForString(member, propertyAccessor)}
                };
    }
}