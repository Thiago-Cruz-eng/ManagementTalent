using ManagementTalent.Services.Services;
using ManagementTalent.Services.Services.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTalent.Controllers;

[ApiController]
[Route("[controller]")]
public class AssessmentResultController : ControllerBase
{
    private readonly AssessmentResultService _assessmentResultService;

    public AssessmentResultController(AssessmentResultService assessmentResultService)
    {
        _assessmentResultService = assessmentResultService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateExercise([FromBody] CreateAssessmentResultRequest createExerciseDto)
    {
        try
        {
            var training = await _assessmentResultService.CreateAssessmentResult(createExerciseDto);
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
            var training = await _assessmentResultService.GetAllAssessmentResult();
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
            var training = await _assessmentResultService.GetAssessmentResult(id);
            return Ok(training);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExerciseById(Guid id, [FromBody] UpdateAssessmentResultRequest updateExerciseDto)
    {
        try
        {
            var training = await _assessmentResultService.UpdateAssessmentResult(id, updateExerciseDto);
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
            await _assessmentResultService.DeleteAssessmentResultById(id);
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception("error on delete Exercise",e);
        }
    }
}