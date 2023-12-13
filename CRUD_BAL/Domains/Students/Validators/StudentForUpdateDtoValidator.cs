namespace CRUD_BAL.Domains.Students.Validators
{
    public class StudentForUpdateDtoValidator : AbstractValidator<StudentForUpdateDto>
    {
        public StudentForUpdateDtoValidator()
    {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.EmailAddress).NotEmpty().NotNull().EmailAddress().MaximumLength(100);
            RuleFor(x => x.GPA).NotEmpty().NotNull().InclusiveBetween(0, 4);
            RuleFor(x => x.StudentImage).NotEmpty().NotNull();
        }
}
    
}
