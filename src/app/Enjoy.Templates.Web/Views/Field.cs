namespace Enjoy.Web.Views
{
    /// <summary>
    /// A Field is any single item on a Form.
    /// </summary>
    public abstract class Field
    {
        public virtual string Key { get; set; }
        public virtual string Label { get; set; }

        // NOTE: I consider certain things ("IsRequired," for example) to be part of validation concerns.
        // We may incorporate these later, but they will have to be handled carefully.
    }
}