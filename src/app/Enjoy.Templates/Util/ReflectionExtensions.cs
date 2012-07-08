using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Enjoy.Util
{
    public static class ReflectionExtensions
    {
        public static MemberInfo GetMemberInfo<TModel, TMember>(this Expression<Func<TModel, TMember>> memberExpr)
        {
            MemberInfo member;
            try
            {
                member = ((MemberExpression) memberExpr.Body).Member;
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("Parameter must be a MemberExpression.", "memberExpr");
            }
            return member;
        }
    }
}