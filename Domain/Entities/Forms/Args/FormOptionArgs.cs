namespace Domain.Entities.Forms.Args
{
    public class FormOptionArgs
    {
        public string Label { get;  set; }

        public string Value { get;  set; }

        public FormInput FormInput { get;  set; }

        public Guid FormInputId { get;  set; }
    }
}
