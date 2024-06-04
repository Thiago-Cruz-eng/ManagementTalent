using ManagementTalent.Services.Services;
using ManagementTalent.Services.Services.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTalent.Controllers;

[ApiController]
[Route("[controller]")]
public class JobRoleController : ControllerBase
{
    private readonly JobRoleService _jobRoleService;

    public JobRoleController(JobRoleService jobRoleService)
    {
        _jobRoleService = jobRoleService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateExercise([FromBody] CreateJobRoleRequest createExerciseDto)
    {
        try
        {
            var training = await _jobRoleService.CreateJobRole(createExerciseDto);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on add Exercise",e);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetExercise()
    {
        try
        {
            var training = await _jobRoleService.GetAllJobRole();
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExerciseById(Guid id)
    {
        try
        {
            var training = await _jobRoleService.GetJobRole(id);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExerciseById(Guid id, [FromBody] UpdateJobRoleRequest updateExerciseDto)
    {
        try
        {
            var training = await _jobRoleService.UpdateJobRole(id, updateExerciseDto);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on update Exercise",e);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExerciseById(Guid id)
    {
        try
        {
            await _jobRoleService.DeleteJobRoleById(id);
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception("error on delete Exercise",e);
        }
    }
}