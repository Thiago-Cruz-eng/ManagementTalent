using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class AssessmentResultService
{
    private readonly IAssessmentResultRepositorySql _assessmentResultRepositorySql;
    private readonly IColabRepositorySql _colabRepositorySql;
    private readonly IJobRoleRepositorySql _jobRoleRepositorySql;
    private readonly ISeniorityRepositorySql _seniorityRepositorySql;
    private readonly IAssessmentRepositorySql _assessmentRepositorySql;
    private readonly IGroupParameterRepositorySql _groupParameterRepositorySql;
    private readonly IGroupParameterResultRepositorySql _groupParameterResultRepositorySql;
    private readonly IJobParameterBaseRepositorySql _jobParameterBaseRepositorySql;
    private readonly IAssessmentParamResultRepositorySql _assessmentParamResultRepositorySql;

    public AssessmentResultService(IAssessmentResultRepositorySql assessmentResultRepositorySql, IColabRepositorySql colabRepositorySql, IJobRoleRepositorySql jobRoleRepositorySql, IAssessmentRepositorySql assessmentRepositorySql, IGroupParameterRepositorySql groupParameterRepositorySql, IGroupParameterResultRepositorySql groupParameterResultRepositorySql, IJobParameterBaseRepositorySql jobParameterBaseRepositorySql, ISeniorityRepositorySql seniorityRepositorySql, IAssessmentParamResultRepositorySql assessmentParamResultRepositorySql)
    {
        _assessmentResultRepositorySql = assessmentResultRepositorySql;
        _colabRepositorySql = colabRepositorySql;
        _jobRoleRepositorySql = jobRoleRepositorySql;
        _assessmentRepositorySql = assessmentRepositorySql;
        _groupParameterRepositorySql = groupParameterRepositorySql;
        _groupParameterResultRepositorySql = groupParameterResultRepositorySql;
        _jobParameterBaseRepositorySql = jobParameterBaseRepositorySql;
        _seniorityRepositorySql = seniorityRepositorySql;
        _assessmentParamResultRepositorySql = assessmentParamResultRepositorySql;
    }

    public async Task<CreateAssessmentResultResponse> CreateAssessmentResult(CreateAssessmentResultRequest assessmentResultDto)
    {
        var colab = await _colabRepositorySql.FindById(Guid.Parse(assessmentResultDto.CollaboratorId));
        var getJobRole = await _jobRoleRepositorySql.FindById(colab.JobRoleId);
        var seniority = await _seniorityRepositorySql.FindById(Guid.Parse(colab.SeniorityId));
        var getAssessmentByJobRole = await _assessmentRepositorySql.GetAssessmentByJobRole(getJobRole.Id.ToString());
        var getGroupParamsByAssessmentId =
            await _groupParameterRepositorySql.GetGroupParamsByAssessment(getAssessmentByJobRole.Id);

        await SaveActualGroupParamResultInAssessmentResult(getGroupParamsByAssessmentId);

        await SaveActualJobParamResultInAssessmentParamResult(getGroupParamsByAssessmentId, seniority);
       
        
        var assessmentResult = new AssessmentResult
        {
            CollaboratorId = assessmentResultDto.CollaboratorId,
            SupervisorId = colab.SupervisorId,
            NextAssessment = DateTime.UtcNow.AddYears(1)
        };
        
        assessmentResult.Validate();
        await _assessmentResultRepositorySql.Save(assessmentResult);
        await _assessmentResultRepositorySql.SaveChange();
        await _groupParameterResultRepositorySql.SaveChange();
        await _assessmentParamResultRepositorySql.SaveChange();
        
        return new CreateAssessmentResultResponse
        {
            Id = assessmentResult.Id,
            CollaboratorId = assessmentResult.CollaboratorId,
            SupervisorId = assessmentResult.SupervisorId
        };
    }

    private async Task SaveActualJobParamResultInAssessmentParamResult(List<GroupParameter> getGroupParamsByAssessmentId, Seniority seniority)
    {
        var groupsList = getGroupParamsByAssessmentId.Select(x => x.Id).ToList();
        
        foreach (var id in groupsList)
        {
            var allJobParamsByGroup = await _groupParameterRepositorySql.GetJobParameterByGroup(id);
            var jobParamsByActualSeniorityColab =
                await _jobParameterBaseRepositorySql.GetActualParamByColabSeniority(allJobParamsByGroup, seniority.Id);
            await SaveActualJobParamBase(jobParamsByActualSeniorityColab);
        }
    }

    private async Task SaveActualGroupParamResultInAssessmentResult(List<GroupParameter> getGroupParamsByAssessmentId)
    {
        var groupsParamToMap = new List<GroupParameterResult>();
        getGroupParamsByAssessmentId?.ForEach(x =>
        {
            groupsParamToMap.Add(new GroupParameterResult
            {
                GroupParamTitle = x.GroupParamTitle,
                Weight = x.Weight,
                AssessmentTamplateId = x.AssessmentId
            });
        });

        if (groupsParamToMap.Count > 0) await _groupParameterResultRepositorySql.SaveRange(groupsParamToMap);
    }

    private async Task SaveActualJobParamBase(List<JobParameterBase> getGroupParamsByAssessmentId)
    {
        var jobParamBaseToMap = new List<AssessmentParamResult>();
        getGroupParamsByAssessmentId?.ForEach(x =>
        {
            jobParamBaseToMap.Add(new AssessmentParamResult
            {
                JobParamTitle = x.JobParamTitle,
                Description = x.Description,
                Observation = x.Observation,
                Weight = x.Weight,
                RealityResult = 0,
                GroupParameterResultId = x.Id
            });
        });
        await _assessmentParamResultRepositorySql.SaveRange(jobParamBaseToMap);
    }

    public async Task<UpdateAssessmentResultResponse> UpdateAssessmentResult(Guid assessmentId, UpdateAssessmentResultRequest assessmentResultDto)
    {
        var colab = await _colabRepositorySql.FindById(Guid.Parse(assessmentResultDto.CollaboratorId));
        var assessmentResult = await _assessmentResultRepositorySql.FindById(assessmentId);
        if (assessmentResult == null) throw new ApplicationException("exercise not found");
        assessmentResult.CollaboratorId = colab.Id;
        assessmentResult.SupervisorId = colab.SupervisorId;
        assessmentResult.NextAssessment = assessmentResultDto.NextAssessment;
        assessmentResult.Result = assessmentResultDto.Result;    
        
        assessmentResult.Validate();
        await _assessmentResultRepositorySql.Update(assessmentResult);
        
        foreach (var jobParam in assessmentResultDto.JobParams)
        {
            await _assessmentParamResultRepositorySql.Update(new AssessmentParamResult
            {
                Id = Guid.Parse(jobParam.Id),
                RealityResult = jobParam.RealityResult
            });
        } 
        
        await _assessmentResultRepositorySql.SaveChange();
        await _assessmentParamResultRepositorySql.SaveChange();
        return new UpdateAssessmentResultResponse
        {
            Success = true
        };
    }
    
    public async Task<GetAssessmentResultResponse> GetAssessmentResult(Guid id)
    {
        var assessmentResult = await _assessmentResultRepositorySql.FindById(id);
        return new GetAssessmentResultResponse
        {
            Id = assessmentResult.Id,
            CollaboratorId = assessmentResult.CollaboratorId,
            SupervisorId =  assessmentResult.SupervisorId,
            Result = assessmentResult.Result,
            NextAssessment = assessmentResult.NextAssessment
        };
    }

    public async Task<List<GetAssessmentResultResponse>> GetAllAssessmentResult()
    {
        var assessmentResultResponses = new List<GetAssessmentResultResponse>();
        var assessmentResult = await _assessmentResultRepositorySql.FindAll();
        assessmentResult.ForEach(x =>
        {
            assessmentResultResponses.Add(new GetAssessmentResultResponse
            {
                Id = x.Id,
                CollaboratorId = x.CollaboratorId,
                SupervisorId = x.SupervisorId,
                Result = x.Result,
                NextAssessment = x.NextAssessment
            });
        });
        return assessmentResultResponses;
    }
    
    public async Task DeleteAssessmentResultById(Guid id)
    {
        var assessmentResult = await _assessmentResultRepositorySql.FindById(id);
        _assessmentResultRepositorySql.Delete(assessmentResult);
        await _assessmentResultRepositorySql.SaveChange();
    }
}