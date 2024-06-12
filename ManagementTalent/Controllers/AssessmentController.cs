using ManagementTalent.Services.Services;
using ManagementTalent.Services.Services.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTalent.Controllers;

[ApiController]
[Route("[controller]")]
public class AssessmentController : ControllerBase
{
    private readonly AssessmentService _assessmentService;

    public AssessmentController(AssessmentService assessmentService)
    {
        _assessmentService = assessmentService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAssessment([FromBody] CreateAssessmentRequest createAssessmentDto)
    {
        try
        {
            var assessment = await _assessmentService.CreateAssessment(createAssessmentDto);
            return Ok(assessment);
        }
        catch (Exception e)
        {
            throw new Exception("error on add Assessment",e);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAssessment()
    {
        try
        {
            var assessment = await _assessmentService.GetAllAssessment();
            return Ok(assessment);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Assessment",e);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAssessmentById(Guid id)
    {
        try
        {
            var assessment = await _assessmentService.GetAssessment(id);
            return Ok(assessment);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Assessment",e);
        }
    }
    
    [HttpGet("jobrole/{id}")]
    public async Task<IActionResult> GetAssessmentByJobRole(string id)
    {
        try
        {
            var assessment = await _assessmentService.GetAssessmentByJobRoleId(id);
            return Ok(assessment);
        }
        catch (Exception e)
        {
            throw new Exception("error on get Assessment",e);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAssessmentById(Guid id, [FromBody] UpdateAssessmentRequest updateAssessmentDto)
    {
        try
        {
            var assessment = await _assessmentService.UpdateAssessment(id, updateAssessmentDto);
            return Ok(assessment);
        }
        catch (Exception e)
        {
            throw new Exception("error on update Assessment",e);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssessmentById(Guid id)
    {
        try
        {
            await _assessmentService.DeleteAssessmentById(id);
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception("error on delete Assessment",e);
        }
    }
}