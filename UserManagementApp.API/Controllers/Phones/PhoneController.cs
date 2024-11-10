using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Application.Users.Interfaces;

namespace UserManagementApp.API.Controllers.Phones;

[Route("api/[controller]")]
[ApiController]
public class PhoneController : ControllerBase
{

    private readonly IPhoneService _service;
    public PhoneController(IPhoneService service)
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
            var phone = await _service.GetByIdAsync(id, cancellationToken);

            return Ok(phone);
        }
        catch (Exception e)
        {
            return BadRequest(new { mensaje = e.Message });
        }
    }

    [Authorize]
    [HttpGet("get-by-userId/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var phone = await _service.GetByUserId(userId, cancellationToken);

            return Ok(phone);
        }
        catch (Exception e)
        {
            return BadRequest(new { mensaje = e.Message });
        }
    }

    [Authorize]
    [HttpPost("add-async")]
    public async Task<IActionResult> AddAsync([FromBody] CreateUserPhone create, CancellationToken cancellationToken = default)
    {
        try
        {
            await _service.AddUserPhoneAsync(create, cancellationToken);

            return Ok(create);
        }
        catch (Exception e)
        {
            return BadRequest(new { mensaje = e.Message });
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdatePhone update, CancellationToken cancellationToken = default)
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
}
