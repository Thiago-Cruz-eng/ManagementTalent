using ManagementTalent.Services.Services;
using ManagementTalent.Services.Services.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTalent.Controllers;

[ApiController]
[Route("[controller]")]
public class SupervisorController : ControllerBase
{
    private readonly SupervisorService _supervisorService;

    public SupervisorController(SupervisorService supervisorService)
    {
        _supervisorService = supervisorService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateExercise([FromBody] CreateSupervisorRequest createExerciseDto)
    {
        try
        {
            var training = await _supervisorService.CreateSupervisor(createExerciseDto);
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
            var training = await _supervisorService.GetAllSupervisor();
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
            var training = await _supervisorService.GetSupervisor(id);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExerciseById(Guid id, [FromBody] UpdateSupervisorRequest updateExerciseDto)
    {
        try
        {
            var training = await _supervisorService.UpdateSupervisor(id, updateExerciseDto);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on update Exercise",e);
        }
    }
    
    [HttpGet("get-by-name")]
    public async Task<IActionResult> GetJobRoleByName([FromQuery]string name)
    {
        try
        {
            var training = await _supervisorService.GetSupervisorByName(name);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExerciseById(Guid id)
    {
        try
        {
            await _supervisorService.DeleteSupervisorById(id);
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception("error on delete Exercise",e);
        }
    }
}