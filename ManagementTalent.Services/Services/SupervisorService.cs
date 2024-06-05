using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class SupervisorService
{
    private readonly ISupervisorRepositorySql _supervisorRepositorySql;

    public SupervisorService(ISupervisorRepositorySql supervisorRepositorySql)
    {
        _supervisorRepositorySql = supervisorRepositorySql;
    }

    public async Task<CreateSupervisorResponse> CreateSupervisor(CreateSupervisorRequest supervisorDto)
    {
        var sup = new Supervisor
        {
            Name = supervisorDto.Name,
            Age = supervisorDto.Age,
            SupFatherId = supervisorDto.SupFather,
            SinceAtInJob = supervisorDto.SinceAtInJob,
            Company = supervisorDto.Company
        };
        
        sup.Validate();
 
        await _supervisorRepositorySql.Save(sup);
        await _supervisorRepositorySql.SaveChange();
        return new CreateSupervisorResponse
        {
            SupFather = sup.SupFatherId,
            Name = sup.Name,
            Company = sup.Company,
            SinceAtInJob = sup.SinceAtInJob,
            Age = sup.Age,
        };
    }
    
    public async Task<UpdateSupervisorResponse> UpdateSupervisor(Guid id, UpdateSupervisorRequest supervisorDto)
    {
        var sup = await _supervisorRepositorySql.FindById(id);
        if (sup == null) throw new ApplicationException("exercise not found");
        sup.Name = supervisorDto.Name;
        sup.Age = supervisorDto.Age ?? 0;
        sup.SinceAtInJob = supervisorDto.SinceAtInJob ?? DateTime.MinValue;
        sup.Company = supervisorDto.Company;
        sup.Colabs = supervisorDto.Colabs ?? sup.Colabs;
        
        sup.Validate();
 
        await _supervisorRepositorySql.Update(sup);
        await _supervisorRepositorySql.SaveChange();
        return new UpdateSupervisorResponse
        {
            SupFather = sup.SupFatherId,
            Name = sup.Name,
            Company = sup.Company,
            SinceAtInJob = sup.SinceAtInJob,
            Age = sup.Age,
            Colabs = sup.Colabs
        };
    }
    
    public async Task<GetSupervisorResponse> GetSupervisor(Guid id)
    {
        var sup = await _supervisorRepositorySql.FindById(id);
        return new GetSupervisorResponse
        {
            SupFather = sup.SupFatherId,
            Name = sup.Name,
            Company = sup.Company,
            SinceAtInJob = sup.SinceAtInJob,
            Age = sup.Age
        };
    }

    public async Task<List<GetSupervisorResponse>> GetAllSupervisor()
    {
        var supResponse = new List<GetSupervisorResponse>();
        var sup = await _supervisorRepositorySql.FindAll();
        sup.ForEach(x =>
        {
            supResponse.Add(new GetSupervisorResponse
            {
                SupFather = x.SupFatherId,
                Name = x.Name,
                Company = x.Company,
                SinceAtInJob = x.SinceAtInJob,
                Age = x.Age,
            });
        });
        return supResponse;
    }
    
    public async Task DeleteSupervisorById(Guid id)
    {
        var sup = await _supervisorRepositorySql.FindById(id);
        _supervisorRepositorySql.Delete(sup);
        await _supervisorRepositorySql.SaveChange();
    }
}