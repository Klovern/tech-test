using FluentValidation;
using UserService.Dtos;

namespace UserService.Validators
{
    public class UserValidator : AbstractValidator<UserCreateDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage(" Length of {PropertyName} is Invalid")
                .Must(BeValidName).WithMessage("{PropertyName} Contains Invalid Characters");
        }

        private bool BeValidName(string name)
        {
            var s = name.Replace("-", "")
                .Replace(" ", "");
            return s.All(Char.IsLetter);
        }
    }
}