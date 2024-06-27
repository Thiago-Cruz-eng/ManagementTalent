using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Services.Services;
using ManagementTalent.Services.Services.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTalent.Controllers;

[ApiController]
[Route("[controller]")]
public class AssessmentResultController : ControllerBase
{
    private readonly AssessmentResultService _assessmentResultService;
    private readonly GroupParameterResultService _groupParameterResultService;

    public AssessmentResultController(AssessmentResultService assessmentResultService, GroupParameterResultService groupParameterResultService)
    {
        _assessmentResultService = assessmentResultService;
        _groupParameterResultService = groupParameterResultService;
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

    [HttpPut("{assessmentId}")]
    public async Task<IActionResult> UpdateExerciseById(Guid assessmentId, [FromBody] UpdateAssessmentResultRequest updateExerciseDto)
    {
        try
        {
            var training = await _assessmentResultService.UpdateAssessmentResult(assessmentId, updateExerciseDto);
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
    [HttpGet("metrics/colab/{id}")]
    public async Task<IActionResult> ReturnMetricsWithResultInPdf(Guid id)
    {
        try
        {
            var pdf = await _assessmentResultService.ReturnMetricsWithResultInPdf(id);
            return File(pdf, "application/pdf", $"{DateTime.Now}-{id}.pdf");
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }
    
    [HttpGet("group-br-assessment-result/{id}")]
    public async Task<IActionResult> GetGroupParameterByAssessmentResult(string id)
    {
        try
        {
            var pdf = await _groupParameterResultService.GetGroupParameterByAssessmentResult(id);
            return Ok(pdf);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Exercise",e);
        }
    }
}