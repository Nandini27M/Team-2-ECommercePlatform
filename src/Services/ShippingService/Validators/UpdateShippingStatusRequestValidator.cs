using FluentValidation;
using ShippingService.DTOs;

namespace ShippingService.Validators
{
    public class UpdateShippingStatusRequestValidator : AbstractValidator<UpdateShippingStatusRequest>
    {
        public UpdateShippingStatusRequestValidator()
        {
            RuleFor(x => x.ShippingStatus)
                .NotEmpty().WithMessage("ShippingStatus is required")
                .Must(status =>
                    status == "Pending" ||
                    status == "Shipped" ||
                    status == "InTransit" ||
                    status == "Delivered" ||
                    status == "Cancelled")
                .WithMessage("Invalid shipping status");
        }
    }
}
