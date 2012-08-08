using System;
using System.Reflection;
using Wandering.Monads.Maybe;

namespace Enjoy.Templates.Mvc.Specs.TestClasses
{
    public sealed class TestMemberViewProvider : IMemberViewProvider
    {
        public object View { get; set; }

        public Maybe<object> ForMember(MemberInfo member, Func<object> instanceAccessor)
        {
            View = new object();
            return View;
        }
    }
}