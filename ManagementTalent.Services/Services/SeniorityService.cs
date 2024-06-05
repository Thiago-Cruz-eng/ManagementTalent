using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class SeniorityService
{
    private readonly ISeniorityRepositorySql _seniorityRepositorySql;

    public SeniorityService(ISeniorityRepositorySql seniorityRepositorySql)
    {
        _seniorityRepositorySql = seniorityRepositorySql;
    }

    public async Task<CreateSeniorityResponse> CreateSeniority(CreateSeniorityRequest seniorityDto)
    {
        var seniority = new Seniority
        {
            SeniorityName = seniorityDto.SeniorityName,
            SeniorityNumber = seniorityDto.SeniorityNumber,
            JobRoleId = seniorityDto.JobRoleId
        };
        
        seniority.Validate();
 
        await _seniorityRepositorySql.Save(seniority);
        await _seniorityRepositorySql.SaveChange();
        return new CreateSeniorityResponse
        {
            SeniorityName = seniority.SeniorityName,
            SeniorityNumber = seniority.SeniorityNumber,
            JobRoleName = seniority.JobRoleName
        };
    }
    
    public async Task<UpdateSeniorityResponse> UpdateSeniority(Guid id, UpdateSeniorityRequest seniorityDto)
    {
        var seniority = await _seniorityRepositorySql.FindById(id);
        if (seniority == null) throw new ApplicationException("exercise not found");
        seniority.JobRoleId = seniorityDto.JobRoleId ?? seniority.JobRoleId;
        seniority.SeniorityNumber = seniorityDto.SeniorityNumber ?? seniority.SeniorityNumber;
        seniority.SeniorityName = seniorityDto.SeniorityName;
        
        seniority.Validate();
 
        await _seniorityRepositorySql.Update(seniority);
        await _seniorityRepositorySql.SaveChange();
        return new UpdateSeniorityResponse
        {
            Success = true
        };
    }
    
    public async Task<GetSeniorityResponse> GetSeniority(Guid id)
    {
        var seniority = await _seniorityRepositorySql.FindById(id);
        return new GetSeniorityResponse
        {
            JobRoleId = seniority.JobRoleId,
            SeniorityNumber = seniority.SeniorityNumber,
            SeniorityName = seniority.SeniorityName
        };
    }

    public async Task<List<GetSeniorityResponse>> GetAllSeniority()
    {
        var seniorityResponses = new List<GetSeniorityResponse>();
        var seniority = await _seniorityRepositorySql.FindAll();
        seniority.ForEach(x =>
        {
            seniorityResponses.Add(new GetSeniorityResponse
            {
                JobRoleId = x.JobRoleId,
                SeniorityNumber = x.SeniorityNumber,
                SeniorityName = x.SeniorityName
            });
        });
        return seniorityResponses;
    }
    
    public async Task DeleteSeniorityById(Guid id)
    {
        var seniority = await _seniorityRepositorySql.FindById(id);
        _seniorityRepositorySql.Delete(seniority);
        await _seniorityRepositorySql.SaveChange();
    }
}