using Domain.Common;
using Domain.Entities.Forms.Args;
using Domain.Entities.Forms.Enums;

namespace Domain.Entities.Forms
{
    public class Form :BaseEntity
    {
        //public Guid FormId { get; private set; }
        /// <summary>
        /// ما اینو توی هندلر در صورت مادیفای شدن فورم یکی به عددش اضافه میکنیم تا بتونیم ورژن های مختلف فورم رو جدا کنیم
        /// </summary>
        /// 
        public Guid FormKey {  get; private set; }
        public int VersionNumber { get; private set; }
        public FormStatus Status { get; private set; } = FormStatus.Draft;
        public string Title { get; private set; } = string.Empty;
        private readonly List<FormInput> _formInputs = [];
        public IReadOnlyCollection<FormInput> FormInputs => _formInputs.AsReadOnly();

        private Form() { }

        public  Form(FormArgs args)
        {
            VersionNumber=args.VersionNumber;
            Title=args.Title;
            
        }
        public void AddInput(FormInput input)
        {
            _formInputs.Add(input);
        }

        public void Modify (FormArgs args)
        {
            VersionNumber = args.VersionNumber;
            Title = args.Title;
          

        }
        public void AddOption(Guid inputId, FormOption option)
        {
            if (inputId != Guid.Empty)
            {
                throw new DomainException("Input Id is invalid!");
            }
            FindActiveInput(inputId).AddOption(option);
        }
        private FormOption FindActiveOption(Guid optionId)
        {
            if (optionId == Guid.Empty)
            {
                throw new DomainException("Option Id is invalid");
            }
            var alloptions = _formInputs.Where(i => !i.IsDeleted)
                .SelectMany(i => i.FormOptions)
                .Where(op => !op.IsDeleted)
                .ToList();
            var option = alloptions.SingleOrDefault(o => o.Id == optionId) 
                ?? throw new DomainException("Option was not found!");
            return option;
        }
        private FormInput FindActiveInput(Guid inputId)
        {
            var form = _formInputs.SingleOrDefault(i => !i.IsDeleted && i.Id == inputId)
                ?? throw new DomainException("The input was not found!");
            return form;
        }
    }
}
