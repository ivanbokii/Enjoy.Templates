using System.Collections.Generic;
using Enjoy.Web.Views;
using Fasterflect;
using Wandering.Monads.Maybe;

namespace Enjoy.Web
{
    /// <summary>
    /// Builds a FieldSet, calling into its MemberViewProviders for each object's member.
    /// </summary>
    public sealed class SimpleObjectViewProvider : IObjectViewProvider
    {
        private readonly IEnumerable<IMemberViewProvider> memberViewProviders;

        public SimpleObjectViewProvider(IEnumerable<IMemberViewProvider> memberViewProviders)
        {
            this.memberViewProviders = memberViewProviders;
        }

        public Maybe<object> For(object instance)
        {
            var fieldSet = new FieldSet();
            var members = instance.GetType().FieldsAndProperties(Flags.InstancePublic);
            foreach (var member in members)
            {
                var view = memberViewProviders.For(instance, member);
                fieldSet.Add(view.IsJust() ? view.FromJust() : member.Get(instance));
            }
            return fieldSet;
        }
    }
}
