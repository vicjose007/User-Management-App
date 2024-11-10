﻿using Microsoft.AspNetCore.Authorization;
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
        return Ok(await _service.GetAllAsync(cancellationToken));
    }

    [Authorize]
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var user = await _service.GetByIdAsync(id, cancellationToken);

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> AddAsync([FromBody] CreateUser create, CancellationToken cancellationToken = default)
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

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _service.Login(request, cancellationToken);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
            return BadRequest(e.Message);
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
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            await _service.ForgotPasswordAsync(email);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
