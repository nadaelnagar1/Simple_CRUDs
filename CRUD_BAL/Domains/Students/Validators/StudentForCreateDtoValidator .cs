
namespace CRUD_BAL.Domains.Students.Validators
{
    public class StudentForCreateDtoValidator : AbstractValidator<StudentForCreateDto>
    {
        public StudentForCreateDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.EmailAddress).NotEmpty().NotNull().EmailAddress().MaximumLength(100);
            RuleFor(x => x.Gender).NotNull().IsInEnum();
            RuleFor(x => x.GPA).NotEmpty().NotNull().InclusiveBetween(0, 4); 
            RuleFor(x => x.StudentImage).NotEmpty().NotNull(); 
        }
    }
}
