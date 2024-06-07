using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class ColabService
{
    private readonly IColabRepositorySql _colabRepositorySql;

    public ColabService(IColabRepositorySql colabRepositorySql)
    {
        _colabRepositorySql = colabRepositorySql;
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
        return new CreateColabResponse
        {
            Name = colab.Name,
            StartAt = colab.StartAt,
            SeniorityId = colab.SeniorityId,
            JobRoleId = colab.JobRoleId,
            SupervisorId = colabDto.SupervisorId
        };
    }
    
    public async Task<UpdateColabResponse> UpdateColab(Guid id, UpdateColabRequest colabDto)
    {
        var colab = await _colabRepositorySql.FindById(id);
        if (colab == null) throw new ApplicationException("exercise not found");
        colab.Name = colabDto.Name ?? colab.Name;
        colab.StartAt = colabDto.StartAt ?? colab.StartAt;
        colab.SeniorityId = colabDto.SeniorityId.ToString() ?? colab.SeniorityId;
        colab.JobRoleId = colabDto.JobRoleId ?? colab.JobRoleId;
        colab.SupervisorId = colabDto.SupervisorId;
        
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
        var colab = await _colabRepositorySql.FindById(id);
        return new GetColabResponse
        {
            Name = colab.Name,
            StartAt = colab.StartAt,
            SeniorityId = colab.SeniorityId,
            JobRoleId = colab.JobRoleId,
            SupervisorId = colab.SupervisorId 
        };
    }

    public async Task<List<GetColabResponse>> GetAllColab()
    {
        var colabResponse = new List<GetColabResponse>();
        var colab = await _colabRepositorySql.FindAll();
        colab.ForEach(x =>
        {
            colabResponse.Add(new GetColabResponse
            {
                Name = x.Name,
                StartAt = x.StartAt,
                SeniorityId = x.SeniorityId,
                JobRoleId = x.JobRoleId,
                SupervisorId = x.SupervisorId 
            });
        });
        return colabResponse;
    }
    
    public async Task DeleteColabById(Guid id)
    {
        var colab = await _colabRepositorySql.FindById(id);
        _colabRepositorySql.Delete(colab);
        await _colabRepositorySql.SaveChange();
    }
}