using System.Net;
using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class UserSystemService
{
    private readonly IUserSystemRepositorySql _userSystemRepositorySql;

    public UserSystemService(IUserSystemRepositorySql userSystemRepositorySql)
    {
        _userSystemRepositorySql = userSystemRepositorySql;
    }

    public async Task<CreateUserSystemResponse> CreateUserSystem(CreateUserSystemRequest userSystemDto)
    {
        var exist = new GetUserSystemResponse();
        if(userSystemDto.Id is not null) exist = await GetUserSystemById(userSystemDto.Id);
        if (exist.Email is not null) return new CreateUserSystemResponse();
        var userSystem = new UserSystem
        {
            Email = userSystemDto.Email,
            Password = userSystemDto.Password,
            Role = userSystemDto.Role,
            ColabId = userSystemDto.ColabId,
            Active = true
        };
        
        userSystem.Validate();
 
        await _userSystemRepositorySql.Save(userSystem);
        await _userSystemRepositorySql.SaveChange();
        return new CreateUserSystemResponse
        {
            Id = userSystem.Id,
            Email = userSystem.Email,
            Password = userSystem.Password,
            Role = userSystem.Role,
            ColabId = userSystemDto.ColabId
        };
    }
    
    public async Task<UpdateUserSystemResponse> UpdateUserSystem(Guid id, UpdateUserSystemRequest userSystemDto)
    {
        var userSystem = await _userSystemRepositorySql.FindById(id.ToString());
        if (userSystem == null) throw new ApplicationException("exercise not found");
        userSystem.Email = userSystemDto.Email ?? userSystem.Email;
        userSystem.Password = userSystemDto.Password ?? userSystem.Password;
        userSystem.ColabId = userSystemDto.ColabId ?? userSystem.ColabId;
        userSystem.Role = userSystemDto.Role ?? userSystem.Role;
        
        userSystem.Validate();
 
        await _userSystemRepositorySql.Update(userSystem);
        await _userSystemRepositorySql.SaveChange();
        return new UpdateUserSystemResponse
        {
            Success = true
        };
    }
    
    public async Task<GetUserSystemResponse> GetUserSystemById(string id)
    {
        var userSystem = await _userSystemRepositorySql.FindById(id);
        if (userSystem is null) return new GetUserSystemResponse();
        return new GetUserSystemResponse
        {
            Id = userSystem.Id,
            Email = userSystem.Email,
            Password = userSystem.Password,
            Role = userSystem.Role,
            ColabId = userSystem.ColabId
        };
    }
    
    public async Task<GetUserSystemResponse> GetUserSystemByLogin(LoginUserSystemRequest login)
    {
        if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            return new GetUserSystemResponse();
        var userSystem = await _userSystemRepositorySql.GetLogin(login.Email, login.Password);
        if (userSystem is null) return new GetUserSystemResponse();
        return new GetUserSystemResponse
        {
            Id = userSystem.Id,
            Email = userSystem.Email,
            Password = userSystem.Password,
            Role = userSystem.Role,
            ColabId = userSystem.ColabId
        };
    }

    public async Task<List<GetUserSystemResponse>> GetAllUserSystem()
    {
        var userSystemResponses = new List<GetUserSystemResponse>();
        var userSystem = await _userSystemRepositorySql.FindAll();
        userSystem.ForEach(x =>
        {
            if (x.Active)
            {
                userSystemResponses.Add(new GetUserSystemResponse
                {
                    Id = x.Id,
                    Email = x.Email,
                    Password = x.Password,
                    Role = x.Role,
                    ColabId = x.ColabId
                });
            }
        });
        return userSystemResponses;
    }
    
    public async Task DeleteUserSystemById(Guid id)
    {
        var userSystem = await _userSystemRepositorySql.FindById(id.ToString());
        userSystem.Active = false;
        await _userSystemRepositorySql.Update(userSystem);
        await _userSystemRepositorySql.SaveChange();
    }
}