namespace Ecommerce.Web.ViewModels
{
    public class ShippingAddressViewModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Address line 1 is required")]
        public string AddressLine1 { get; set; } = string.Empty;

        public string? AddressLine2 { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "State is required")]
        public string State { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Pincode is required")]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^\d{6}$", ErrorMessage = "Enter a valid 6-digit pincode")]
        public string Pincode { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Phone is required")]
        [System.ComponentModel.DataAnnotations.Phone]
        public string Phone { get; set; } = string.Empty;
    }
}
