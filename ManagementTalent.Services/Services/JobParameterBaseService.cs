using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class JobParameterBaseService
{
    private readonly IJobParameterBaseRepositorySql _jobParameterBaseRepositorySql;

    public JobParameterBaseService(IJobParameterBaseRepositorySql jobParameterBaseRepositorySql)
    {
        _jobParameterBaseRepositorySql = jobParameterBaseRepositorySql;
    }

    public async Task<CreateJobParameterBaseResponse> CreateJobParameterBase(CreateJobParameterBaseRequest jobParameterBaseDto)
    {
        var jobParameterBase = new JobParameterBase
        {
            JobParamTitle = jobParameterBaseDto.JobParamTitle,
            Description = jobParameterBaseDto.Description,
            Observation = jobParameterBaseDto.Observation,
            Weight = jobParameterBaseDto.Weight
        };
        
        jobParameterBase.Validate();
 
        await _jobParameterBaseRepositorySql.Save(jobParameterBase);
        if(jobParameterBaseDto.GroupParameterIds.Count > 0) jobParameterBase.IntegrateGroupParameter(jobParameterBaseDto.GroupParameterIds);
        await _jobParameterBaseRepositorySql.SaveChange();
        return new CreateJobParameterBaseResponse
        {
            JobParamTitle = jobParameterBase.JobParamTitle,
            Description = jobParameterBase.Description,
            Observation = jobParameterBase.Observation,
            Weight = jobParameterBase.Weight
        };
    }
    
    public async Task<UpdateJobParameterBaseResponse> UpdateJobParameterBase(Guid id, UpdateJobParameterBaseRequest jobParameterBaseDto)
    {
        var jobParameterBase = await _jobParameterBaseRepositorySql.FindById(id);
        if (jobParameterBase == null) throw new ApplicationException("exercise not found");
        jobParameterBase.JobParamTitle = jobParameterBaseDto.JobParamTitle ?? jobParameterBase.JobParamTitle;
        jobParameterBase.Description = jobParameterBaseDto.Description ?? jobParameterBase.Description;
        jobParameterBase.Observation = jobParameterBaseDto.Observation ?? jobParameterBase.Observation;
        jobParameterBase.Weight = jobParameterBaseDto.Weight ?? jobParameterBase.Weight;
        
        jobParameterBase.Validate();
 
        await _jobParameterBaseRepositorySql.Update(jobParameterBase);
        if(jobParameterBaseDto.GroupParameterIds.Count > 0) jobParameterBase.IntegrateGroupParameter(jobParameterBaseDto.GroupParameterIds);
        await _jobParameterBaseRepositorySql.SaveChange();
        return new UpdateJobParameterBaseResponse
        {
            Success = true
        };
    }
    
    public async Task<GetJobParameterBaseResponse> GetJobParameterBase(Guid id)
    {
        var jobParameterBase = await _jobParameterBaseRepositorySql.FindById(id);
        return new GetJobParameterBaseResponse
        {
            JobParamTitle = jobParameterBase.JobParamTitle,
            Description = jobParameterBase.Description,
            Observation = jobParameterBase.Observation,
            Weight = jobParameterBase.Weight
        };
    }

    public async Task<List<GetJobParameterBaseResponse>> GetAllJobParameterBase()
    {
        var jobParameterBaseResponses = new List<GetJobParameterBaseResponse>();
        var jobParameterBase = await _jobParameterBaseRepositorySql.FindAll();
        jobParameterBase.ForEach(x =>
        {
            jobParameterBaseResponses.Add(new GetJobParameterBaseResponse
            {
                JobParamTitle = x.JobParamTitle,
                Description = x.Description,
                Observation = x.Observation,
                Weight = x.Weight
            });
        });
        return jobParameterBaseResponses;
    }
    
    public async Task DeleteJobParameterBaseById(Guid id)
    {
        var jobParameterBase = await _jobParameterBaseRepositorySql.FindById(id);
        _jobParameterBaseRepositorySql.Delete(jobParameterBase);
        await _jobParameterBaseRepositorySql.SaveChange();
    }
}