using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Model.Queries;
using VacApp_Bovinova_Platform.RanchManagement.Domain.Services;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Resources;
using VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST.Transform;

namespace VacApp_Bovinova_Platform.RanchManagement.Interfaces.REST;

[Authorize]
[ApiController]
[Route("/api/v1/stables")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Stables")]
public class StableController(
   IStableCommandService commandService,
   IStableQueryService queryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new stable",
        Description = "Creates a new stable associated with the authenticated user.",
        OperationId = "CreateStables"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "Stable successfully created", typeof(StableResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
    public async Task<IActionResult> CreateStables([FromBody] CreateStableResource resource)
    {
        var user = HttpContext.Items["User"] as User;
        if (user is null)
            return Unauthorized("User not found in context.");

        var command = CreateStableCommandFromResourceAssembler.ToCommandFromResource(resource, user.Id);
        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetStableById), new { id = result.Id },
            StableResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all stables",
        Description = "Get all stables",
        OperationId = "GetAllStable")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of stables were found", typeof(IEnumerable<StableResource>))]
    public async Task<IActionResult> GetAllStable()
    {
        var user = HttpContext.Items["User"] as User;
        if (user is null)
            return Unauthorized("User not found in context.");

        var stables = await queryService.Handle(new GetAllStablesQuery(user.Id));
        var stableResources = stables.Select(StableResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(stableResources);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetStableById(int id)
    {
        var getStableById = new GetStablesByIdQuery(id);
        var result = await queryService.Handle(getStableById);
        if (result is null) return NotFound();
        var resources = StableResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resources);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStable(int id, [FromBody] UpdateStableResource resource)
    {
        var command = UpdateStableCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();
        return Ok(StableResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStable(int id)
    {
        var command = new DeleteStableCommand(id);
        var result = await commandService.Handle(command);
        if (result is null)
            return NotFound(new { message = "Stable not found" });
        return Ok(new { message = "Deleted successfully" });
    }
}
