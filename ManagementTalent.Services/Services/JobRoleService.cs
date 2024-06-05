using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class JobRoleService
{
    private readonly IJobRoleRepositorySql _jobRoleRepositorySql;

    public JobRoleService(IJobRoleRepositorySql jobRoleRepositorySql)
    {
        _jobRoleRepositorySql = jobRoleRepositorySql;
    }

    public async Task<CreateJobRoleResponse> CreateJobRole(CreateJobRoleRequest jobRoleDto)
    {
        var jobRole = new JobRole
        {
            JobTitle = jobRoleDto.JobTitle,
        };
        
        jobRole.Validate();
 
        await _jobRoleRepositorySql.Save(jobRole);
        await _jobRoleRepositorySql.SaveChange();
        return new CreateJobRoleResponse
        {
            JobTitle = jobRole.JobTitle
        };
    }
    
    public async Task<UpdateJobRoleResponse> UpdateJobRole(Guid id, UpdateJobRoleRequest jobRoleDto)
    {
        var jobRole = await _jobRoleRepositorySql.FindById(id);
        if (jobRole == null) throw new ApplicationException("exercise not found");
        jobRole.JobTitle = jobRoleDto.JobTitle ?? jobRole.JobTitle;
        
        jobRole.Validate();
 
        await _jobRoleRepositorySql.Update(jobRole);
        await _jobRoleRepositorySql.SaveChange();
        return new UpdateJobRoleResponse
        {
            Success = true
        };
    }
    
    public async Task<GetJobRoleResponse> GetJobRole(Guid id)
    {
        var jobRole = await _jobRoleRepositorySql.FindById(id);
        return new GetJobRoleResponse
        {
            JobTitle = jobRole.JobTitle,

        };
    }

    public async Task<List<GetJobRoleResponse>> GetAllJobRole()
    {
        var jobRoleResponses = new List<GetJobRoleResponse>();
        var jobRole = await _jobRoleRepositorySql.FindAll();
        jobRole.ForEach(x =>
        {
            jobRoleResponses.Add(new GetJobRoleResponse
            {
                JobTitle = x.JobTitle
            });
        });
        return jobRoleResponses;
    }
    
    public async Task DeleteJobRoleById(Guid id)
    {
        var jobRole = await _jobRoleRepositorySql.FindById(id);
        _jobRoleRepositorySql.Delete(jobRole);
        await _jobRoleRepositorySql.SaveChange();
    }
}