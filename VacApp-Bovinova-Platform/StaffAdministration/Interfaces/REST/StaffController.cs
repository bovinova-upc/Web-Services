using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Queries;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Services;
using VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Resources;
using VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Transform;
using VacApp_Bovinova_Platform.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;

namespace VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST;

/// <summary>
/// API controller for managing staffs
/// </summary>
[Authorize]
[ApiController]
[Route("/api/v1/staff")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Staffs")]
public class StaffController(IStaffCommandService commandService,
    IStaffQueryService queryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateStaffs([FromBody] CreateStaffResource resource)
    {
        var user = HttpContext.Items["User"] as User;
        if (user is null) return Unauthorized();

        var command = CreateStaffCommandFromResourceAssembler.ToCommandFromResource(resource, user.Id);
        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();

        return CreatedAtAction(nameof(GetStaffById), new { id = result.Id },
            StaffResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all staffs",
        Description = "Get all staffs",
        OperationId = "GetAllStaff")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of staffs were found", typeof(IEnumerable<StaffResource>))]
    public async Task<IActionResult> GetAllStaff()
    {
        var user = HttpContext.Items["User"] as User;
        if (user is null) return Unauthorized();
        var staffs = await queryService.Handle(new GetAllStaffQuery(user.Id));
        var staffResources = staffs.Select(StaffResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(staffResources);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetStaffById(int id)
    {
        var getStaffById = new GetStaffByIdQuery(id);
        var result = await queryService.Handle(getStaffById);
        if (result is null) return NotFound();
        var resources = StaffResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resources);
    }

    [HttpGet("search-by-employee-status/{employeeStatus}")]
    [SwaggerOperation(
        Summary = "Get all staffs by employee status",
        Description = "Get all staffs by employee status",
        OperationId = "GetStaffByEmployeeStatus")]
    public async Task<ActionResult> GetStaffByEmployeeStatus(int employeeStatus)
    {
        var getStaffByEmployeeStatusQuery = new GetStaffByEmployeeStatusQuery(employeeStatus);
        var staffs = await queryService.Handle(getStaffByEmployeeStatusQuery);

        if (staffs == null || !staffs.Any())
            return NotFound();

        var staffResources = staffs.Select(StaffResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(staffResources);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStaff(int id, [FromBody] UpdateStaffResource resource)
    {
        var command = UpdateStaffCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();

        return Ok(StaffResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStaff(int id)
    {
        var command = new DeleteStaffCommand(id);
        var result = await commandService.Handle(command);

        if (result is null)
            return NotFound(new { message = "Staff not found" });

        return Ok(new { message = "Deleted successfully" });
    }
}