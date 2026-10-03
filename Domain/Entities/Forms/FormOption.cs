using Domain.Common;
using Domain.Entities.Forms.Args;

namespace Domain.Entities.Forms
{
    /// <summary>
    /// این برای وقتی توی فرانت ما سلکت باکس یا چیزهای به این شکل داریم فرانت برای ما لیبل و ولیو میفرسته
    /// </summary>
    public class FormOption:BaseEntity
    {
        //public Guid FormOptionId { get; private set; }
        public string Label { get; private set; } = string.Empty;

        public string Value { get; private set; } = string.Empty;

        public FormInput FormInput { get; private set; } = null!;

        public Guid FormInputId { get; private set; }

        private FormOption() { }

        public FormOption(FormOptionArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Label))
            {
                throw new DomainException("The label is invalid!");
            }
            if (string.IsNullOrWhiteSpace(args.Value))
            {
                throw new DomainException("The value is empty!");
            }
            if (args.FormInputId == Guid.Empty)
            {
                throw new DomainException("The form input id is empty!");
            }
            Label = args.Label.Trim();
            Value = args.Value.Trim();
            FormInputId = args.FormInputId;
        }



    }
}
