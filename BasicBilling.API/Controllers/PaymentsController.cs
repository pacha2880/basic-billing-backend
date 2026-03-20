using BasicBilling.Application.DTOs;
using BasicBilling.Application.Payments.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicBilling.API.Controllers;

[ApiController]
[Route("api/payments")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
    {
        var command = new ProcessPaymentCommand(
            request.ClientId,
            request.ServiceType,
            request.BillingPeriod);

        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
