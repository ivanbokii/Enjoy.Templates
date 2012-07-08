namespace Enjoy.Web.Views.Fields
{
    /// <summary>
    /// A single-line text field.
    /// </summary>
    public class TextField : Field
    {
        public virtual string Placeholder { get; set; }
        public virtual object Value { get; set; }
    }
}