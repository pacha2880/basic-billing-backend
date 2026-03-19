using System;
using System.Threading.Tasks;
using BasicBilling.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicBilling.API.Controllers;

[ApiController]
[Route("api/clients")]
[Authorize]
public class ClientsController : ControllerBase
{
    private readonly IBillingService _billingService;
    private readonly IPaymentService _paymentService;

    public ClientsController(IBillingService billingService, IPaymentService paymentService)
    {
        _billingService = billingService;
        _paymentService = paymentService;
    }

    [HttpGet("{id}/pending-bills")]
    public async Task<IActionResult> GetPendingBills(int id)
    {
        try
        {
            var result = await _billingService.GetPendingBillsAsync(id);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpGet("{id}/payment-history")]
    public async Task<IActionResult> GetPaymentHistory(int id)
    {
        try
        {
            var result = await _paymentService.GetPaymentHistoryAsync(id);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}
