namespace Enjoy.Web.Views
{
    /// <summary>
    /// A Field is any single item on a Form.
    /// </summary>
    public abstract class Field
    {
        public virtual string Key { get; set; }
        public virtual string Class { get; set; }
    }
}