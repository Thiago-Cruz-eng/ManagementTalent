using ManagementTalent.Domain.Entity;
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
            SeniorityNumber = seniorityDto.SeniorityNumber
        };
        
        seniority.Validate();
 
        await _seniorityRepositorySql.Save(seniority);
        await _seniorityRepositorySql.SaveChange();
        return new CreateSeniorityResponse
        {
            SeniorityName = seniority.SeniorityName,
            SeniorityNumber = seniority.SeniorityNumber
        };
    }
    
    public async Task<UpdateSeniorityResponse> UpdateSeniority(Guid id, UpdateSeniorityRequest seniorityDto)
    {
        var seniority = await _seniorityRepositorySql.FindById(id);
        if (seniority == null) throw new ApplicationException("exercise not found");
        seniority.SeniorityName = seniorityDto.SeniorityName ?? seniority.SeniorityName;
        seniority.SeniorityNumber = seniorityDto.SeniorityNumber ?? seniority.SeniorityNumber;
        
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
            SeniorityName = seniority.SeniorityName,
            SeniorityNumber = seniority.SeniorityNumber
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
                SeniorityName = x.SeniorityName,
                SeniorityNumber = x.SeniorityNumber
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