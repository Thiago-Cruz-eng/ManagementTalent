using ManagementTalent.Services.Services;
using ManagementTalent.Services.Services.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTalent.Controllers;

[ApiController]
[Route("[controller]")]
public class JobParameterBaseController : ControllerBase
{
    private readonly JobParameterBaseService _jobParameterBaseService;

    public JobParameterBaseController(JobParameterBaseService jobParameterBaseService)
    {
        _jobParameterBaseService = jobParameterBaseService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateExercise([FromBody] CreateJobParameterBaseRequest createExerciseDto)
    {
        try
        {
            var training = await _jobParameterBaseService.CreateJobParameterBase(createExerciseDto);
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
            var training = await _jobParameterBaseService.GetAllJobParameterBase();
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
            var training = await _jobParameterBaseService.GetJobParameterBase(id);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExerciseById(Guid id, [FromBody] UpdateJobParameterBaseRequest updateExerciseDto)
    {
        try
        {
            var training = await _jobParameterBaseService.UpdateJobParameterBase(id, updateExerciseDto);
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
            await _jobParameterBaseService.DeleteJobParameterBaseById(id);
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception("error on delete Exercise",e);
        }
    }
}