using FluentValidation;
using WebAPI.Entities;

namespace WebAPI.Validations
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
        }
    }
}
