namespace Enjoy.Web.Views.Fields
{
    /// <remarks>
    /// Can be displayed as a drop-down or as radio buttons.
    /// </remarks>
    public class SingleSelectField : ChoicesField
    {
        public virtual bool AllowOther { get; set; }
        public virtual object Selected { get; set; }
    }
}