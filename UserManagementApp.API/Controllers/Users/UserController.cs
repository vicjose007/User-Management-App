using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using UserManagementApp.Application.Users.Dtos;
using UserManagementApp.Application.Users.Interfaces;

namespace UserManagementApp.API.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly IUserService _service;
    public UserController(IUserService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            return Ok(await _service.GetAllAsync(cancellationToken));
        }
        catch (Exception e)
        {
            return BadRequest(new { mensaje = e.Message });
        }
    }

    [Authorize]
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await _service.GetByIdAsync(id, cancellationToken);

            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(new { mensaje = e.Message });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> AddAsync([FromBody] CreateUser create, CancellationToken cancellationToken = default)
    {
        try
        {

            await _service.AddAsync(create, cancellationToken);

            return Ok(create);
        }
        catch (Exception e)
        {
            return BadRequest(new { mensaje = e.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _service.Login(request, cancellationToken);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { mensaje = e.Message });
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateUser update, CancellationToken cancellationToken = default)
    {
        try
        {
            await _service.UpdateAsync(id, update, cancellationToken);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { mensaje = e.Message });
        }
    }

    [Authorize]
    [HttpPut("update-user-activation/{id}")]
    public async Task<IActionResult> UpdateAsync(string id, bool isActive, CancellationToken cancellationToken = default)
    {
        try
        {
            await _service.UpdateUserActivationAsync(id, isActive);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { mensaje = e.Message });
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _service.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { mensaje = e.Message });
        }
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _service.ForgotPasswordAsync(email);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new { mensaje = e.Message });
        }
    }
}
