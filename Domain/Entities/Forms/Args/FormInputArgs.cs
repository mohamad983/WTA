namespace Domain.Entities.Forms.Args
{
    public class FormInputArgs
    {
        public string Name { get; private set; }

        public string Label { get; set; }

        public short InputTypes { get; set; }

        public short ValueDataType { get; set; }

        public bool IsRequired { get; set; }

        public short Order { get; set; }
        public short ColSpan { get; set; }
        public short RowNumber { get; set; }

        public string PlaceHolder { get; set; }

        public string DefaultValue { get; set; }

        public string Value { get; set; }

        public Form Form { get; set; }

    }
}
