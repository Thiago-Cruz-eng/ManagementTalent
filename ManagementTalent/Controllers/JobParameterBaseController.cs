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
    public async Task<IActionResult> CreateExercise([FromBody] List<CreateJobParameterBaseRequest> createExerciseDto)
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
    public async Task<IActionResult> GetExerciseById(string id)
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
    
    [HttpPost("all-by-group/seniority/{seniorityId}")]
    public async Task<IActionResult> GetAllJobParameterBaseByJobParam(string seniorityId, [FromBody]List<string> groupId)
    {
        try
        {
            var training = await _jobParameterBaseService.GetAllJobParameterBaseByGroupId(groupId, seniorityId);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExerciseById(string id, [FromBody] UpdateJobParameterBaseRequest updateExerciseDto)
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
    public async Task<IActionResult> DeleteExerciseById(string id)
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