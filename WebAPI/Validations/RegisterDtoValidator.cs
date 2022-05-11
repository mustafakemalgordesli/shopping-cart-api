using FluentValidation;
using WebAPI.DTOs;

namespace WebAPI.Validations;

public class RegisterDtoValidator : AbstractValidator<RegisterDTO>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.LastName).MaximumLength(255);
        RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(100);
        RuleFor(x => x.Password).NotEmpty();
    }
}
