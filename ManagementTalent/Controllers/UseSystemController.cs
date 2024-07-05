using ManagementTalent.Services.Services;
using ManagementTalent.Services.Services.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTalent.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSystemController : ControllerBase
{
    private readonly UserSystemService _userSystemService;

    public UserSystemController(UserSystemService userSystemService)
    {
        _userSystemService = userSystemService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUserSystem([FromBody] CreateUserSystemRequest createUserSystemDto)
    {
        try
        {
            var userSystem = await _userSystemService.CreateUserSystem(createUserSystemDto);
            return Ok(userSystem);
        }
        catch (Exception e)
        {
            throw new Exception("error on add UserSystem",e);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUserSystem()
    {
        try
        {
            var userSystem = await _userSystemService.GetAllUserSystem();
            return Ok(userSystem);
        }
        catch (Exception e)
        {
            throw new Exception("error on get UserSystem",e);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserSystemById(Guid id)
    {
        try
        {
            var userSystem = await _userSystemService.GetUserSystemById(id.ToString());
            return Ok(userSystem);
        }
        catch (Exception e)
        {
            throw new Exception("error on get UserSystem",e);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserSystemById(Guid id, [FromBody] UpdateUserSystemRequest updateUserSystemDto)
    {
        try
        {
            var userSystem = await _userSystemService.UpdateUserSystem(id, updateUserSystemDto);
            return Ok(userSystem);
        }
        catch (Exception e)
        {
            throw new Exception("error on update UserSystem",e);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserSystemById(Guid id)
    {
        try
        {
            await _userSystemService.DeleteUserSystemById(id);
            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception("error on delete UserSystem",e);
        }
    }
}