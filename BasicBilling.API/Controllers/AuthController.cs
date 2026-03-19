using System;
using System.Threading.Tasks;
using BasicBilling.API.Models;
using BasicBilling.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicBilling.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("token")]
    public async Task<IActionResult> Token([FromBody] AuthRequest request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        try
        {
            var token = await _authService.GenerateTokenAsync(request.ClientId);
            return Ok(new { token });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
