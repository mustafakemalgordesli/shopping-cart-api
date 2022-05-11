using FluentValidation;
using WebAPI.Entities;

namespace WebAPI.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Description).NotEmpty().MaximumLength(255);
            RuleFor(p => p.Price).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(p => p.CategoryId).NotEmpty();
            RuleFor(p => p.ImageURL).NotEmpty();
            RuleFor(p => p.Quantity).NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
