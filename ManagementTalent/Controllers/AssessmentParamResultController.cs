using ManagementTalent.Services.Services;
using ManagementTalent.Services.Services.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTalent.Controllers;

[ApiController]
[Route("[controller]")]
public class AssessmentParamResultController : ControllerBase
{
    private readonly AssessmentParamResultService _assessmentParamResultService;

    public AssessmentParamResultController(AssessmentParamResultService assessmentParamResultService)
    {
        _assessmentParamResultService = assessmentParamResultService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateExercise([FromBody] CreateAssessmentParamResultRequest createExerciseDto)
    {
        try
        {
            var training = await _assessmentParamResultService.CreateAssessmentParamResult(createExerciseDto);
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
            var training = await _assessmentParamResultService.GetAllAssessmentParamResult();
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
            var training = await _assessmentParamResultService.GetAssessmentParamResult(id);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }
    
    [HttpGet("assessment-param-result/by-group/{id}")]
    public async Task<IActionResult> GetAssessmentParamResultByGroupParameterResultId(Guid id)
    {
        try
        {
            var training = await _assessmentParamResultService.GetAssessmentParamResultByGroupParameterResultId(id);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExerciseById(Guid id, [FromBody] UpdateAssessmentParamResultRequest updateExerciseDto)
    {
        try
        {
            var training = await _assessmentParamResultService.UpdateAssessmentParamResult(id, updateExerciseDto);
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
            await _assessmentParamResultService.DeleteAssessmentParamResultById(id);
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception("error on delete Exercise",e);
        }
    }
}