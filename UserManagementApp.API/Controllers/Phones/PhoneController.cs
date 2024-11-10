﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        return Ok(await _service.GetAllAsync(cancellationToken));
    }


    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var phone = await _service.GetByIdAsync(id, cancellationToken);

        if (phone is null)
            return NotFound();

        return Ok(phone);
    }

    [HttpGet("get-by-userId/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId, CancellationToken cancellationToken = default)
    {
        var phone = await _service.GetByUserId(userId, cancellationToken);

        if (phone is null)
            return NotFound();

        return Ok(phone);
    }

    [HttpPost("add-async")]
    public async Task<IActionResult> AddAsync([FromForm] CreatePhone create, CancellationToken cancellationToken = default)
    {
        try
        {
            await _service.AddAsync(create, cancellationToken);

            return Ok(create);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromForm] UpdatePhone update, CancellationToken cancellationToken = default)
    {
        try
        {
            await _service.UpdateAsync(id, update, cancellationToken);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

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
            return BadRequest(e.Message);
        }
    }
}
