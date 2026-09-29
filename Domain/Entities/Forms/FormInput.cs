using Domain.Common;
using Domain.Entities.Forms.Args;
using Domain.Entities.Forms.Enums;

namespace Domain.Entities.Forms
{
    public class FormInput:BaseEntity
    {
        public Guid FormInputId { get; private set; }
        /// <summary>
        /// ما اینو توی هندلر در صورت مادیفای شدن فورم یکی به عددش اضافه میکنیم تا بتونیم ورژن های مختلف فورم رو جدا کنیم
        /// </summary>
        public string Name { get; private set; }

        //i will add request type after finish 

        public string Label { get; private set; }

        public InputTypesEnum InputTypes { get; private set; }

        public ValueDataTypeEnum ValueDataType { get; private set; }

        public bool IsRequired { get; private set; }
        /// <summary>
        /// این پراپ برای اینه که ترتیب چینش فرم رو در محور ایکس ها نمایش بدیم 
        /// </summary>
        public short Order {  get; private set; }
        /// <summary>
        /// برای نمایش اندازه فرم 
        /// </summary>
        public short ColSpan { get; private set; }
        /// <summary>
        /// این پراپ برای اینه که ترتیب چینش فرم رو در محور وای ها نمایش بدیم
        /// </summary>
        public short RowNumber { get; private set; }

        public string PlaceHolder { get; private set; }

        public string DefaultValue { get; private set; }

        public string Value { get; private set; }

        public Form Form { get; private set; }

        private readonly List<FormOption> _formOptions = [];
        //public byte[] RowVersion { get; private set; } = null!;

        public IReadOnlyCollection<FormOption> FormOptions => _formOptions.AsReadOnly();

        private FormInput() { }

        public FormInput(FormInputArgs args)
        {
            FormInputId=Guid.NewGuid();
            Name = args.Name;
            Label = args.Label;
            InputTypes = (InputTypesEnum)args.InputTypes;
            ValueDataType = (ValueDataTypeEnum)args.ValueDataType;
            IsRequired = args.IsRequired;
            Order = args.Order;
            ColSpan = args.ColSpan;
            RowNumber = args.RowNumber;
            PlaceHolder = args.PlaceHolder;
            DefaultValue = args.DefaultValue;
            Form=args.Form;
            Value = args.Value;

        }
        public static FormInput New(FormInputArgs args)
        {
            return new FormInput(args);
        }
        public void AddOptions(FormOption option)
        {
            _formOptions.Add(option);
        }

        public void ClearOptions()
        {
            _formOptions.Clear();
        }

        public void Modify(FormInputArgs args)
        {
            Name = args.Name;
            Label = args.Label;
            InputTypes = (InputTypesEnum)args.InputTypes;
            ValueDataType = (ValueDataTypeEnum)args.ValueDataType;
            IsRequired = args.IsRequired;
            Order = args.Order;
            ColSpan = args.ColSpan;
            RowNumber = args.RowNumber;
            PlaceHolder = args.PlaceHolder;
            DefaultValue = args.DefaultValue;
            Value = args.Value;
            Form = args.Form;
            

        }
    }
}
