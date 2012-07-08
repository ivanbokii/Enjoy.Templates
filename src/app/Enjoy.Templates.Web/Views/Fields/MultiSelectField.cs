using System.Collections.Generic;

namespace Enjoy.Web.Views.Fields
{
    /// <remarks>
    /// Can be displayed as a set of checkboxes or as a multi-select list.
    /// </remarks>
    public class MultiSelectField : ChoicesField
    {
        public virtual ISet<object> Selected { get; set; }
    }
}