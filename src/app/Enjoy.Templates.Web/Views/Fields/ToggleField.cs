namespace Enjoy.Web.Views.Fields
{
    /// <summary>
    /// This is a single on-off field.
    /// </summary>
    /// <remarks>Can be displayed as a checkbox or a button.</remarks>
    public class ToggleField : Field
    {
        public virtual bool Toggled { get; set; }
    }
}