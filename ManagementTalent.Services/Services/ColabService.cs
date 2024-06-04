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
            Sup = colabDto.Sup,
            Name = colabDto.Name,
            StartAt = colabDto.StartAt,
            Seniority = colabDto.Seniority,
            JobRole = colabDto.JobRole 
        };
        
        colab.Validate();
 
        await _colabRepositorySql.Save(colab);
        await _colabRepositorySql.SaveChange();
        return new CreateColabResponse
        {
            Sup = colab.Sup,
            Name = colab.Name,
            StartAt = colab.StartAt,
            Seniority = colab.Seniority,
            JobRole = colab.JobRole 
        };
    }
    
    public async Task<UpdateColabResponse> UpdateColab(Guid id, UpdateColabRequest colabDto)
    {
        var colab = await _colabRepositorySql.FindById(id);
        if (colab == null) throw new ApplicationException("exercise not found");
        colab.Sup = colabDto.Sup ?? colab.Sup;
        colab.Name = colabDto.Name ?? colab.Name;
        colab.StartAt = colabDto.StartAt ?? colab.StartAt;
        colab.Seniority = colabDto.Seniority ?? colab.Seniority;
        colab.JobRole = colabDto.JobRole ?? colab.JobRole; 
        
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
            Supervisor = colab.Sup?.Name,
            Name = colab.Name,
            StartAt = colab.StartAt,
            Seniority = colab.Seniority,
            JobRole = colab.JobRole 
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
                Supervisor = x.Sup.Name,
                Name = x.Name,
                StartAt = x.StartAt,
                Seniority = x.Seniority,
                JobRole = x.JobRole 
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