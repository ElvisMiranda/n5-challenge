using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using N5.Challenge.Api.Model;
using N5.Challenge.Application.Permissions.GetPermission;
using N5.Challenge.Application.Permissions.GetPermissions;
using N5.Challenge.Application.Permissions.ModifyPermission;
using N5.Challenge.Application.Permissions.RegisterPermissionIndex;
using N5.Challenge.Application.Permissions.RequestPermission;

namespace N5.Challenge.Api.Controllers;

[ApiController]
[Route("api/permissions")]
public class PermissionController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;

    public PermissionController(ISender sender, IPublisher publisher)
    {
        _sender = sender;
        _publisher = publisher;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPermission(int id, CancellationToken cancellationToken)
    {
        var query = new GetPermissionQuery(id);
        var response = await _sender.Send(query, cancellationToken);

        if (response.IsFailure)
        {
            return NotFound(response.Error.Description);
        }

        await _publisher.Publish(new RegisterIndexNotification(id), cancellationToken);
        
        return Ok(response.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetPermissions(CancellationToken cancellationToken)
    {
        var query = new GetPermissionsQuery();
        var response = await _sender.Send(query, cancellationToken);

        if (response.IsFailure)
        {
            return BadRequest(response.IsFailure);
        }

        return Ok(response.Value);
    }

    [HttpPost]
    public async Task<IActionResult> RequestPermission(RequestPermission request, CancellationToken cancellationToken)
    {
        var command = new RequestPermissionCommand(request.Forename, request.Surname, request.PermissionTypeId);

        var response = await _sender.Send(command, cancellationToken);

        if (response.IsFailure)
        {
            return BadRequest(response.Error.Description);
        }
        
        await _publisher.Publish(new RegisterIndexNotification(response.Value), cancellationToken);

        return CreatedAtAction(nameof(GetPermission), new { id = response.Value }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyPermission(int id, RequestPermission request, CancellationToken cancellationToken)
    {
        var command = new ModifyPermissionCommand(id, request.Forename, request.Surname, request.PermissionTypeId);

        var response = await _sender.Send(command, cancellationToken);

        if (response.IsFailure)
        {
            return BadRequest(response.Error.Description);
        }
        
        await _publisher.Publish(new RegisterIndexNotification(id), cancellationToken);

        return NoContent();
    }
}