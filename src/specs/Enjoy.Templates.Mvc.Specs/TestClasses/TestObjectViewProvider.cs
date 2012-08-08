using Wandering.Monads.Maybe;

namespace Enjoy.Templates.Mvc.Specs.TestClasses
{
    public sealed class TestObjectViewProvider : IObjectViewProvider
    {
        public object Instance { get; set; }
        public object View { get; set; }

        public Maybe<object> For(object instance)
        {
            Instance = instance;
            View = new object();
            return new Just<object>(View);
        }
    }
}