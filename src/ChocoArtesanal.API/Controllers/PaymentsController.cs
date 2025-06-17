using ChocoArtesanal.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChocoArtesanal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    [HttpPost("process")]
    [Authorize]
    public IActionResult ProcessPayment([FromBody] PaymentRequestDto paymentRequest)
    {
        // Simulación de procesamiento de pago
        if (string.IsNullOrEmpty(paymentRequest.CardNumber) || paymentRequest.Amount <= 0)
        {
            return BadRequest(new PaymentResponseDto(false, "", "Invalid payment data."));
        }

        var transactionId = Guid.NewGuid().ToString();
        var response = new PaymentResponseDto(true, transactionId, "Payment processed successfully.");

        return Ok(response);
    }
}