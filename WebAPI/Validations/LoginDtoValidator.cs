using FluentValidation;
using WebAPI.DTOs;

namespace WebAPI.Validations
{

    public class LoginDtoValidator : AbstractValidator<LoginDTO>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
