using Wandering.Monads.Maybe;

namespace Enjoy
{
    /// <summary>
    /// ObjectViewProviders take an Object and yield a View.
    /// </summary>
    public interface IObjectViewProvider
    {
        /// <summary>
        /// Builds a View from an Object.
        /// </summary>
        Maybe<object> For(object instance);
    }
}
