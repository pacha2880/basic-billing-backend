using System;
using System.Threading.Tasks;
using BasicBilling.Application.DTOs;
using BasicBilling.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicBilling.API.Controllers;

[ApiController]
[Route("api/bills")]
[Authorize]
public class BillsController : ControllerBase
{
    private readonly IBillingService _billingService;

    public BillsController(IBillingService billingService)
    {
        _billingService = billingService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBill([FromBody] CreateBillRequest request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        try
        {
            var created = await _billingService.CreateBillAsync(request);
            return CreatedAtAction(
                nameof(ClientsController.GetPendingBills),
                "Clients",
                new { id = created.ClientId },
                created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

}
