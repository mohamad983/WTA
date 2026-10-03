using Domain.Entities.Forms.Enums;

namespace Domain.Entities.Forms.Args
{
    public class FormInputArgs
    {
        public string Name { get; set; } = string.Empty;

        public string Label { get; set; } = string.Empty;

        public InputTypesEnum InputType { get; set; }

        public ValueDataTypeEnum ValueDataType { get; set; }

        public bool IsRequired { get; set; }

        public short Order { get; set; } = 1;
        public short ColSpan { get; set; } = 12;
        public short RowNumber { get; set; } = 1;

        public string? PlaceHolder { get; set; }

        public string? DefaultValue { get; set; }

        public Guid FormId { get;  set; }

    }
}
