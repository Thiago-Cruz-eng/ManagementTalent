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
            SeniorityRelevanceInWorkDay = seniorityDto.SeniorityRelevanceInWorkDay,
            JobRoleId = seniorityDto.JobRoleId
        };
        if(seniorityDto.JobParamIds?.Count > 0) seniority.IntegrateSeniority(seniorityDto.JobParamIds);
        
        seniority.Validate();
 
        await _seniorityRepositorySql.Save(seniority);
        await _seniorityRepositorySql.SaveChange();
        return new CreateSeniorityResponse
        {
            Id = seniority.Id,
            SeniorityName = seniority.SeniorityName,
            SeniorityRelevanceInWorkDay = seniority.SeniorityRelevanceInWorkDay,
            JobRoleName = seniority.JobRoleName
        };
    }
    
    public async Task<UpdateSeniorityResponse> UpdateSeniority(Guid id, UpdateSeniorityRequest seniorityDto)
    {
        var seniority = await _seniorityRepositorySql.FindById(id.ToString());
        if (seniority == null) throw new ApplicationException("exercise not found");
        seniority.JobRoleId = seniorityDto.JobRoleId ?? seniority.JobRoleId;
        seniority.SeniorityRelevanceInWorkDay = seniorityDto.SeniorityRelevanceInWorkDay ?? seniority.SeniorityRelevanceInWorkDay;
        seniority.SeniorityName = seniorityDto.SeniorityName;
        if(seniorityDto.JobParamIds?.Count > 0) seniority.IntegrateSeniority(seniorityDto.JobParamIds);
        
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
        var seniority = await _seniorityRepositorySql.FindById(id.ToString());
        return new GetSeniorityResponse
        {
            Id = seniority.Id,
            JobRoleId = seniority.JobRoleId,
            SeniorityRelevanceInWorkDay = seniority.SeniorityRelevanceInWorkDay,
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
                Id = x.Id,
                JobRoleId = x.JobRoleId,
                SeniorityRelevanceInWorkDay = x.SeniorityRelevanceInWorkDay,
                SeniorityName = x.SeniorityName
            });
        });
        return seniorityResponses;
    }
    
    public async Task<List<GetSeniorityResponse>> GetAllSeniorityByJobRole(string jobroleId)
    {
        var seniorityResponses = new List<GetSeniorityResponse>();
        var seniority = await _seniorityRepositorySql.GetAssessmentByJobRole(jobroleId);
        seniority.ForEach(x =>
        {
            seniorityResponses.Add(new GetSeniorityResponse
            {
                Id = x.Id,
                JobRoleId = x.JobRoleId,
                SeniorityRelevanceInWorkDay = x.SeniorityRelevanceInWorkDay,
                SeniorityName = x.SeniorityName
            });
        });
        return seniorityResponses;
    }
    
    public async Task DeleteSeniorityById(Guid id)
    {
        var seniority = await _seniorityRepositorySql.FindById(id.ToString());
        _seniorityRepositorySql.Delete(seniority);
        await _seniorityRepositorySql.SaveChange();
    }
}