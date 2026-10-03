namespace Domain.Entities.Forms.Args
{
    public class FormOptionArgs
    {
        public string Label { get; set; } = string.Empty;

        public string Value { get;  set; } = string.Empty;

        public Guid FormInputId { get;  set; }
    }
}
