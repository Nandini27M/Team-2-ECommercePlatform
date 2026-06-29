using FluentValidation;
using ShippingService.DTOs;

namespace ShippingService.Validators
{
    public class CreateShippingRequestValidator : AbstractValidator<CreateShippingRequest>
    {
        public CreateShippingRequestValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId is required");

            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("TransactionId is required");

            RuleFor(x => x.CustomerEmail)
                .NotEmpty().WithMessage("CustomerEmail is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.ShippingAddress)
                .NotEmpty().WithMessage("ShippingAddress is required")
                .MinimumLength(5).WithMessage("ShippingAddress must be at least 5 characters");
        }
    }
}
