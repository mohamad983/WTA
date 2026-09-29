using Domain.Common;
using Domain.Entities.Forms.Args;

namespace Domain.Entities.Forms
{
    public class Form :BaseEntity
    {
        public Guid FormId { get; private set; }
        /// <summary>
        /// ما اینو توی هندلر در صورت مادیفای شدن فورم یکی به عددش اضافه میکنیم تا بتونیم ورژن های مختلف فورم رو جدا کنیم
        /// </summary>
        public int VersionNumber { get; private set; }

     

        public string Title { get; private set; }

        private readonly List<FormInput> _formInputs = [];
     

        public IReadOnlyCollection<FormInput> FormInputs => _formInputs.AsReadOnly();

        private Form() { }

        public  Form(FormArgs args)
        {
            FormId=Guid.NewGuid();
            VersionNumber=args.VersionNumber;
            Title=args.Title;
            
        }

        public static Form New(FormArgs args)
        {
            return new Form(args);
        }
        public void AddInput(FormInput input)
        {
            _formInputs.Add(input);
        }

        public void ClearInputs()
        {
            _formInputs.Clear();
        }

        public void Modify (FormArgs args)
        {
            VersionNumber = args.VersionNumber;
            Title = args.Title;
          

        }
    }
}
