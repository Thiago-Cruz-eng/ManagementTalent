using ManagementTalent.Services.Services;
using ManagementTalent.Services.Services.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTalent.Controllers;

[ApiController]
[Route("[controller]")]
public class SeniorityController : ControllerBase
{
    private readonly SeniorityService _seniorityService;

    public SeniorityController(SeniorityService seniorityService)
    {
        _seniorityService = seniorityService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateExercise([FromBody] CreateSeniorityRequest createExerciseDto)
    {
        try
        {
            var training = await _seniorityService.CreateSeniority(createExerciseDto);
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
            var training = await _seniorityService.GetAllSeniority();
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
            var training = await _seniorityService.GetSeniority(id);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }
    
    [HttpGet("get-by-jobroleid/{id}")]
    public async Task<IActionResult> GetAllSeniorityByJobRole(string id)
    {
        try
        {
            var training = await _seniorityService.GetAllSeniorityByJobRole(id);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExerciseById(Guid id, [FromBody] UpdateSeniorityRequest updateExerciseDto)
    {
        try
        {
            var training = await _seniorityService.UpdateSeniority(id, updateExerciseDto);
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
            await _seniorityService.DeleteSeniorityById(id);
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception("error on delete Exercise",e);
        }
    }
}