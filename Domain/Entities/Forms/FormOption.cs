using Domain.Common;
using Domain.Entities.Forms.Args;

namespace Domain.Entities.Forms
{
    /// <summary>
    /// این برای وقتی توی فرانت ما سلکت باکس یا چیزهای به این شکل داریم فرانت برای ما لیبل و ولیو میفرسته
    /// </summary>
    public class FormOption:BaseEntity
    {
        public Guid FormOptionId { get; private set; }


        public string Label { get; private set; }

        public string Value { get; private set; }

        public FormInput FormInput { get; private set; }

        private FormOption() { }

        public FormOption(FormOptionArgs args)
        {
            FormOptionId=Guid.NewGuid();
            Label = args.Label;
            Value = args.Value;
            FormInput = args.FormInput;
        }
        public static FormOption New(FormOptionArgs args)
        {
            return new FormOption(args);
        }

        public void Modify(FormOptionArgs args)
        {

            Label = args.Label;
            Value = args.Value;
            FormInput = args.FormInput;
        }


    }
}
