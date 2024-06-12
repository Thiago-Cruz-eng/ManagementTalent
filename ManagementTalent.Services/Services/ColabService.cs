using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class ColabService
{
    private readonly IColabRepositorySql _colabRepositorySql;
    private readonly ISeniorityRepositorySql _seniorityRepositorySql;
    private readonly ISupervisorRepositorySql _supervisorRepositorySql;
    private readonly IJobRoleRepositorySql _jobRoleRepositorySql;

    public ColabService(IColabRepositorySql colabRepositorySql, ISeniorityRepositorySql seniorityRepositorySql, ISupervisorRepositorySql supervisorRepositorySql, IJobRoleRepositorySql jobRoleRepositorySql)
    {
        _colabRepositorySql = colabRepositorySql;
        _seniorityRepositorySql = seniorityRepositorySql;
        _supervisorRepositorySql = supervisorRepositorySql;
        _jobRoleRepositorySql = jobRoleRepositorySql;
    }

    public async Task<CreateColabResponse> CreateColab(CreateColabRequest colabDto)
    {
        var colab = new Colab()
        {
            Name = colabDto.Name,
            StartAt = colabDto.StartAt,
            SeniorityId = colabDto.SeniorityId.ToString(),
            JobRoleId = colabDto.JobRoleId,
            SupervisorId = colabDto.SupervisorId
        };
        
        colab.Validate();
 
        await _colabRepositorySql.Save(colab);
        await _colabRepositorySql.SaveChange();
        var colabSeniority = await _seniorityRepositorySql.FindById(colab.SeniorityId);
        var colabSup = await _supervisorRepositorySql.FindById(colab.SupervisorId);
        var colabJobRole = await _jobRoleRepositorySql.FindById(colab.JobRoleId);
        return new CreateColabResponse
        {
            Id = colab.Id,
            Name = colab.Name,
            StartAt = colab.StartAt,
            SeniorityId = colab.SeniorityId,
            JobRoleId = colab.JobRoleId,
            SupervisorId = colabDto.SupervisorId,
            SeniorityName = colabSeniority.SeniorityName,
            JobRoleName = colabJobRole.JobTitle,
            SupervisorName = colabSup.Name,
        };
    }
    
    public async Task<UpdateColabResponse> UpdateColab(Guid id, UpdateColabRequest colabDto)
    {
        var colab = await _colabRepositorySql.FindById(id.ToString());
        if (colab == null) throw new ApplicationException("exercise not found");
        colab.Name = colabDto.Name ?? colab.Name;
        colab.StartAt = colabDto.StartAt ?? colab.StartAt;
        colab.SeniorityId = colabDto.SeniorityId.ToString() ?? colab.SeniorityId;
        colab.JobRoleId = colabDto.JobRoleId ?? colab.JobRoleId;
        colab.SupervisorId = colabDto.SupervisorId ?? colab.SupervisorId;
        
        colab.Validate();
 
        await _colabRepositorySql.Update(colab);
        await _colabRepositorySql.SaveChange();
        return new UpdateColabResponse
        {
            Success = true
        };
    }
    
    public async Task<GetColabResponse> GetColab(Guid id)
    {
        var colab = await _colabRepositorySql.FindById(id.ToString());
        var colabSeniority = await _seniorityRepositorySql.FindById(colab.SeniorityId);
        var colabSup = await _supervisorRepositorySql.FindById(colab.SupervisorId);
        var colabJobRole = await _jobRoleRepositorySql.FindById(colab.JobRoleId);
        return new GetColabResponse
        {
            Id = colab.Id,
            Name = colab.Name,
            StartAt = colab.StartAt,
            SeniorityId = colab.SeniorityId,
            JobRoleId = colab.JobRoleId,
            SupervisorId = colab.SupervisorId,
            SeniorityName = colabSeniority.SeniorityName,
            JobRoleName = colabJobRole.JobTitle,
            SupervisorName = colabSup.Name,
        };
    }

    public async Task<List<GetColabResponse>> GetAllColab()
    {
        var colabResponse = new List<GetColabResponse>();
        var colabo = await _colabRepositorySql.FindAll();
        foreach (var x in colabo)
        {
            var colabSeniority = await _seniorityRepositorySql.FindById(x.SeniorityId);
            var colabSup = await _supervisorRepositorySql.FindById(x.SupervisorId);
            var colabJobRole = await _jobRoleRepositorySql.FindById(x.JobRoleId);
            colabResponse.Add(new GetColabResponse
            {
                Id = x.Id,
                Name = x.Name,
                StartAt = x.StartAt,
                SeniorityId = x.SeniorityId,
                JobRoleId = x.JobRoleId,
                SupervisorId = x.SupervisorId,
                SeniorityName = colabSeniority.SeniorityName,
                JobRoleName = colabJobRole.JobTitle,
                SupervisorName = colabSup.Name,
            });
        }
        return colabResponse;
    }
    
    public async Task DeleteColabById(Guid id)
    {
        var colab = await _colabRepositorySql.FindById(id.ToString());
        _colabRepositorySql.Delete(colab);
        await _colabRepositorySql.SaveChange();
    }
}