using BasicBilling.Application.Bills.Commands;
using BasicBilling.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicBilling.API.Controllers;

[ApiController]
[Route("api/bills")]
[Authorize]
public class BillsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BillsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBill([FromBody] CreateBillRequest request)
    {
        var command = new CreateBillCommand(
            request.ClientId,
            request.ServiceType,
            request.BillingPeriod,
            request.Amount);

        var created = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(ClientsController.GetPendingBills),
            "Clients",
            new { id = created.ClientId },
            created);
    }
}
