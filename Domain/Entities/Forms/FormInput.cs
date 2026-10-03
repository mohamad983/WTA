using Domain.Common;
using Domain.Entities.Forms.Args;
using Domain.Entities.Forms.Enums;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Domain.Entities.Forms
{
    public class FormInput:BaseEntity
    {
        //public Guid FormInputId { get; private set; }
        /// <summary>
        /// ما اینو توی هندلر در صورت مادیفای شدن فورم یکی به عددش اضافه میکنیم تا بتونیم ورژن های مختلف فورم رو جدا کنیم
        /// </summary>
        /// 
        private readonly Regex NamePattern = new Regex("^[A-Za-z][A-Za-z0-9_]{0-49}$",RegexOptions.Compiled);
        public string Name { get; private set; } = string.Empty;
        public string Label { get; private set; } = string.Empty;

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

        public string? PlaceHolder { get; private set; } = string.Empty;

        public string? DefaultValue { get; private set; } = string.Empty;

        public Form Form { get; private set; } = null!;

        public Guid FormId { get; private set; } 

        private readonly List<FormOption> _formOptions = [];
        public IReadOnlyCollection<FormOption> FormOptions => _formOptions.AsReadOnly();
        public bool HasOptions {  get; private set; }

        internal IEnumerable<FormOption> ActiveOptions => _formOptions.Where(fo => !fo.IsDeleted);
        private FormInput() { }

        public FormInput(FormInputArgs args)
        {
            Apply(args);
            FormId = args.FormId;
        }
        internal void AddOption(FormOption option)
        {
            //change needed
            if (option.FormInputId != Id)
                throw new DomainException("This option belongs to a different input.");
            if (option.IsDeleted)
                throw new DomainException("A deleted option can't be added.");
            if (ActiveOptions.Any(o => o.Id == option.Id))
                throw new DomainException("This option has already been added.");
            if (ActiveOptions.Any(o => string.Equals(o.Value, option.Value, StringComparison.Ordinal)))
                throw new DomainException($"An option with the value '{option.Value}' already exists.");

            _formOptions.Add(option);
        }
        internal void RemoveOption(Guid optionId)
        {
            var Option = _formOptions.SingleOrDefault(o => !o.IsDeleted && o.Id == optionId) 
                ?? throw new DomainException("The option was not found!");
            Option.MarkAsDeleted();
        }
        internal void Modify(FormInputArgs args)
        {
            Apply(args);
        }
        internal FormInputArgs CreateArgs()
        {
            var args = new FormInputArgs
            {
                FormId = this.FormId,
                Name = this.Name,
                Label = this.Label,
                ValueDataType = this.ValueDataType,
                InputType = InputTypes,
                IsRequired = this.IsRequired,
                Order = this.Order,
                RowNumber = this.RowNumber,
                ColSpan = this.ColSpan,
                DefaultValue = this.DefaultValue,
                PlaceHolder = this.PlaceHolder
            };
            return args;
        }
        private void Apply(FormInputArgs args)
        {
            if (args.Order < 1)
            {
                throw new DomainException("The order cannot be less than 0!");

            }
            if (args.ColSpan < 1)
            {
                throw new DomainException("The Column Span cannot be less than or equal 0!");
            }
            if (args.RowNumber < 1)
            {
                throw new DomainException("The Row number cannot be less than or equal to 0!");
            }
            if (!IsCompatible(args.InputType,args.ValueDataType))
            {
                throw new DomainException("Input Type and value data type are incompatible");
            }
            if (string.IsNullOrWhiteSpace(args.Name) || !NamePattern.IsMatch(args.Name))
            {
                throw new DomainException("The name is invalid!");
            }
            if (string.IsNullOrWhiteSpace(args.Label))
            {
                throw new DomainException("The label cannot be empty!");
            }
            if (!Enum.IsDefined<InputTypesEnum>(args.InputType))
            {
                throw new DomainException($"Invalid input type {args.InputType}");
            }
            if (Enum.IsDefined<ValueDataTypeEnum>(args.ValueDataType))
            {
                throw new DomainException($"Invalid input type {args.ValueDataType}");
            }
            Name = args.Name.Trim();
            Label = args.Label.Trim();
            Order = args.Order;
            IsRequired = args.IsRequired;
            InputTypes = args.InputType;
            ValueDataType = args.ValueDataType;
            RowNumber = args.RowNumber;
            PlaceHolder = args.PlaceHolder?.Trim() ?? string.Empty;
            DefaultValue = args.DefaultValue ?? string.Empty;
        }
        public IReadOnlyList<string> GetDefinitionProblems()
        {
            var problems = new List<string>();

            if (HasOptions)
            {
                var options = ActiveOptions.ToList();
                if (options.Count == 0)
                    problems.Add($"Input '{Name}' needs at least one option.");
                else if (DefaultValue is not null && !options.Any(o => o.Value == DefaultValue))
                    problems.Add($"The default value of '{Name}' is not one of its options.");
            }

            return problems;
        }
        public string? ValidateValue(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return IsRequired ? $"'{Label}' is required." : null;

            var trimmed = value.Trim();

            var typeError = CheckType(ValueDataType, trimmed);
            if (typeError is not null)
                return $"'{Label}': {typeError}";

            if (HasOptions && !ActiveOptions.Any(o => string.Equals(o.Value, trimmed, StringComparison.Ordinal)))
                return $"'{Label}': '{trimmed}' is not one of the available options.";

            return null;
        }
        private static string? CheckType(ValueDataTypeEnum valueDataType, string Value) => valueDataType switch
        {
            ValueDataTypeEnum.String => Value.Length > 4000 ? "The text is too long!" : null,
            ValueDataTypeEnum.Boolean => bool.TryParse(Value, out var actualValue) ? null : "'true' or 'false' is expected",
            ValueDataTypeEnum.Number => int.TryParse(Value, out var intvalue) ? null : "a whole number is expected",
            ValueDataTypeEnum.Decimal => decimal.TryParse(Value, out var decimalvalue) ? null : "A number is expected",
            ValueDataTypeEnum.DateTime => DateOnly.TryParseExact(Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                    ? null : "a date in the form yyyy-MM-dd is expected.",
            _ => "DataType Unknown!"
        };
        private static bool IsCompatible(InputTypesEnum inputType, ValueDataTypeEnum valueType) => inputType switch
        {
            InputTypesEnum.SearchBox or InputTypesEnum.Text or InputTypesEnum.TextArea => valueType == ValueDataTypeEnum.String,
            InputTypesEnum.Number => valueType == ValueDataTypeEnum.Number,
            InputTypesEnum.DateTime or InputTypesEnum.Date => valueType == ValueDataTypeEnum.DateTime,
            InputTypesEnum.Time => valueType == ValueDataTypeEnum.Time,
            InputTypesEnum.MultiSelect or InputTypesEnum.Radio or InputTypesEnum.FileUpload => valueType == ValueDataTypeEnum.String,
            InputTypesEnum.Checkbox => valueType == ValueDataTypeEnum.Boolean,
            _ => false
        };
       /* private static bool IsCompatible(InputTypesEnum inputType, ValueDataTypeEnum dataType) => inputType switch
        {
            implementation to be added later

        }*/ 
       //private 
    }
}
