using System.Collections.Generic;

namespace Enjoy.Web.Views.Fields
{
    public abstract class ChoicesField : Field
    {
        public virtual IDictionary<string, object> Choices { get; set; }
    }
}