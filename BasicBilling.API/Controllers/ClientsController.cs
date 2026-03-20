using BasicBilling.Application.Clients.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicBilling.API.Controllers;

[ApiController]
[Route("api/clients")]
[Authorize]
public class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}/pending-bills")]
    public async Task<IActionResult> GetPendingBills(int id)
    {
        var result = await _mediator.Send(new GetPendingBillsQuery(id));
        return Ok(result);
    }

    [HttpGet("{id}/payment-history")]
    public async Task<IActionResult> GetPaymentHistory(int id)
    {
        var result = await _mediator.Send(new GetPaymentHistoryQuery(id));
        return Ok(result);
    }
}
