using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class JobParameterBaseService
{
    private readonly IJobParameterBaseRepositorySql _jobParameterBaseRepositorySql;
    private readonly IGroupParameterRepositorySql _groupParameterRepositorySql;

    public JobParameterBaseService(IJobParameterBaseRepositorySql jobParameterBaseRepositorySql, IGroupParameterRepositorySql groupParameterRepositorySql)
    {
        _jobParameterBaseRepositorySql = jobParameterBaseRepositorySql;
        _groupParameterRepositorySql = groupParameterRepositorySql;
    }

    public async Task<List<CreateJobParameterBaseResponse>> CreateJobParameterBase(List<CreateJobParameterBaseRequest> jobParameterBases)
    {
        var jobParam = new List<CreateJobParameterBaseResponse>();
        foreach (var jobParameterBaseDto in jobParameterBases)
        {
            var jobParameterBase = new JobParameterBase
            {
                JobParamTitle = jobParameterBaseDto.JobParamTitle,
                Description = jobParameterBaseDto.Description,
                Observation = jobParameterBaseDto.Observation,
                Weight = jobParameterBaseDto.Weight,
                Expected = jobParameterBaseDto.Expected
            };

            jobParameterBase.Validate();
        
            if(jobParameterBaseDto.GroupParameterIds?.Count > 0) jobParameterBase.IntegrateGroupParameter(jobParameterBaseDto.GroupParameterIds);
            if(jobParameterBaseDto.SenioritiesIds?.Count > 0) jobParameterBase.IntegrateSeniority(jobParameterBaseDto.SenioritiesIds);
            await _jobParameterBaseRepositorySql.Save(jobParameterBase);
            await _jobParameterBaseRepositorySql.SaveChange();
            jobParam.Add(new CreateJobParameterBaseResponse
            {
                Id = jobParameterBase.Id,
                JobParamTitle = jobParameterBase.JobParamTitle,
                Description = jobParameterBase.Description,
                Observation = jobParameterBase.Observation,
                Weight = jobParameterBase.Weight,
                Expected = jobParameterBase.Expected
            });
        }
        return jobParam;
    }
    
    public async Task<UpdateJobParameterBaseResponse> UpdateJobParameterBase(string id, UpdateJobParameterBaseRequest jobParameterBaseDto)
    {
        var jobParameterBase = await _jobParameterBaseRepositorySql.FindById(id);
        if (jobParameterBase == null) throw new ApplicationException("exercise not found");
        jobParameterBase.JobParamTitle = jobParameterBaseDto.JobParamTitle ?? jobParameterBase.JobParamTitle;
        jobParameterBase.Description = jobParameterBaseDto.Description ?? jobParameterBase.Description;
        jobParameterBase.Observation = jobParameterBaseDto.Observation ?? jobParameterBase.Observation;
        jobParameterBase.Weight = jobParameterBaseDto.Weight ?? jobParameterBase.Weight;
        jobParameterBase.Expected = jobParameterBaseDto.Expected ?? jobParameterBase.Expected;
        
        jobParameterBase.Validate();
 
        await _jobParameterBaseRepositorySql.Update(jobParameterBase);
        if(jobParameterBaseDto.GroupParameterIds?.Count > 0) jobParameterBase.IntegrateGroupParameter(jobParameterBaseDto.GroupParameterIds);
        if(jobParameterBaseDto.SenioritiesIds?.Count > 0) jobParameterBase.IntegrateSeniority(jobParameterBaseDto.SenioritiesIds);
        await _jobParameterBaseRepositorySql.SaveChange();
        return new UpdateJobParameterBaseResponse
        {
            Success = true
        };
    }
    
    public async Task<GetJobParameterBaseResponse> GetJobParameterBase(string id)
    {
        var jobParameterBase = await _jobParameterBaseRepositorySql.FindById(id);
        return new GetJobParameterBaseResponse
        {
            Id = jobParameterBase.Id,
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
                Id = x.Id,
                JobParamTitle = x.JobParamTitle,
                Description = x.Description,
                Observation = x.Observation,
                Weight = x.Weight,
                Expected = x.Expected
            });
        });
        return jobParameterBaseResponses;
    }
    
    public async Task<List<JobParameterBase>> GetAllJobParameterBaseByGroupId(List<string> groupsList, string seniorityId)
    {
        var jobs = new List<JobParameterBase>();
        foreach (var id in groupsList)
        {
            var allJobParamsByGroup = await _groupParameterRepositorySql.GetJobParameterByGroup(id);
            jobs = await _jobParameterBaseRepositorySql.GetActualJobParamByColabSeniority(allJobParamsByGroup, seniorityId);
        }

        return jobs;
    }
    
    
    public async Task DeleteJobParameterBaseById(string id)
    {
        var jobParameterBase = await _jobParameterBaseRepositorySql.FindById(id);
        _jobParameterBaseRepositorySql.Delete(jobParameterBase);
        await _jobParameterBaseRepositorySql.SaveChange();
    }
}