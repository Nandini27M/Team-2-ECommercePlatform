using Microsoft.AspNetCore.Mvc;
using PaymentService.DTOs;
using PaymentService.Models;
using PaymentService.Services;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentDto dto)
        {
            var payment = new Payment
            {
                OrderId = dto.OrderId,
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = "Pending",
                PaymentDate = DateTime.Now
            };

            var result = await _paymentService.CreatePaymentAsync(payment);

            return CreatedAtAction(nameof(GetPaymentById),
                new { id = result.PaymentId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePaymentDto dto)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);

            if (payment == null)
                return NotFound();

            payment.PaymentStatus = dto.PaymentStatus;
            payment.TransactionId = dto.TransactionId;

            await _paymentService.UpdatePaymentAsync(payment);

            return Ok("Payment Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);

            if (payment == null)
                return NotFound();

            await _paymentService.DeletePaymentAsync(id);

            return Ok("Payment Deleted Successfully");
        }
    }
}