using ManagementTalent.Services.Services;
using ManagementTalent.Services.Services.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTalent.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupParameterController : ControllerBase
{
    private readonly GroupParameterService _groupParameterService;

    public GroupParameterController(GroupParameterService groupParameterService)
    {
        _groupParameterService = groupParameterService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroupParameter(
        [FromBody] CreateGroupParameterRequest createGroupParameterDto)
    {
        try
        {
            var groupParameter = await _groupParameterService.CreateGroupParameter(createGroupParameterDto);
            return Ok(groupParameter);
        }
        catch (Exception e)
        {
            throw new Exception("error on add GroupParameter", e);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetGroupParameter()
    {
        try
        {
            var groupParameter = await _groupParameterService.GetAllGroupParameter();
            return Ok(groupParameter);
        }
        catch (Exception e)
        {
            throw new Exception("error on get GroupParameter", e);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGroupParameterById(Guid id)
    {
        try
        {
            var groupParameter = await _groupParameterService.GetGroupParameter(id);
            return Ok(groupParameter);
        }
        catch (Exception e)
        {
            throw new Exception("error on get GroupParameter", e);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGroupParameterById(Guid id,
        [FromBody] UpdateGroupParameterRequest updateGroupParameterDto)
    {
        try
        {
            var groupParameter = await _groupParameterService.UpdateGroupParameter(id, updateGroupParameterDto);
            return Ok(groupParameter);
        }
        catch (Exception e)
        {
            throw new Exception("error on update GroupParameter", e);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroupParameterById(Guid id)
    {
        try
        {
            await _groupParameterService.DeleteGroupParameterById(id);
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception("error on delete GroupParameter", e);
        }
    }
}