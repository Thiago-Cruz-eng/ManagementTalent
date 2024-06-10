using iTextSharp.text;
using iTextSharp.text.pdf;
using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;
using Document = System.Reflection.Metadata.Document;

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
    private readonly ISupervisorRepositorySql _supervisorRepositorySql;

    public AssessmentResultService(IAssessmentResultRepositorySql assessmentResultRepositorySql, IColabRepositorySql colabRepositorySql, IJobRoleRepositorySql jobRoleRepositorySql, IAssessmentRepositorySql assessmentRepositorySql, IGroupParameterRepositorySql groupParameterRepositorySql, IGroupParameterResultRepositorySql groupParameterResultRepositorySql, IJobParameterBaseRepositorySql jobParameterBaseRepositorySql, ISeniorityRepositorySql seniorityRepositorySql, IAssessmentParamResultRepositorySql assessmentParamResultRepositorySql, ISupervisorRepositorySql supervisorRepositorySql)
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
        _supervisorRepositorySql = supervisorRepositorySql;
    }

    public async Task<CreateAssessmentResultResponse> CreateAssessmentResult(CreateAssessmentResultRequest assessmentResultDto)
    {
        var colab = await _colabRepositorySql.FindById(assessmentResultDto.CollaboratorId);
        var getJobRole = await _jobRoleRepositorySql.FindById(colab.JobRoleId);
        var seniority = await _seniorityRepositorySql.FindById(colab.SeniorityId);
        var supervisor = await _supervisorRepositorySql.FindById(colab.SupervisorId);
        var getAssessmentByJobRole = await _assessmentRepositorySql.GetAssessmentByJobRole(getJobRole.Id.ToString());
        var getGroupParamsByAssessmentId =
            await _groupParameterRepositorySql.GetGroupParamsByAssessment(getAssessmentByJobRole.Id);
        
        var assessmentResult = new AssessmentResult
        {
            CollaboratorId = assessmentResultDto.CollaboratorId,
            SupervisorId = colab.SupervisorId,
            NextAssessment = DateTime.UtcNow.AddYears(1),
            Result = 0,
            ActualJobName = getJobRole.JobTitle,
            ActualSeniorityName = seniority.SeniorityName,
            ActualSupervisorName = supervisor.Name
        };
        assessmentResult.Validate();
        
        await _assessmentResultRepositorySql.Save(assessmentResult);
        await _assessmentResultRepositorySql.SaveChange();

        var groupIds = await SaveActualGroupParamResultInAssessmentResult(getGroupParamsByAssessmentId, assessmentResult.Id);
        await _groupParameterResultRepositorySql.SaveChange();
        
        foreach (var groupId in groupIds)
        {
            await SaveActualJobParamResultInAssessmentParamResult(getGroupParamsByAssessmentId, seniority, groupId);
        }
        
        await _assessmentParamResultRepositorySql.SaveChange();
        
        return new CreateAssessmentResultResponse
        {
            Id = assessmentResult.Id,
            CollaboratorId = assessmentResult.CollaboratorId,
            SupervisorId = assessmentResult.SupervisorId
        };
    }

    private async Task SaveActualJobParamResultInAssessmentParamResult(List<GroupParameter> getGroupParamsByAssessmentId, Seniority seniority, string groupId)
    {
        var groupsList = getGroupParamsByAssessmentId.Select(x => x.Id).ToList();
        
        foreach (var id in groupsList)
        {
            var allJobParamsByGroup = await _groupParameterRepositorySql.GetJobParameterByGroup(id);
            var jobParamsByActualSeniorityColab =
                await _jobParameterBaseRepositorySql.GetActualJobParamByColabSeniority(allJobParamsByGroup, seniority.Id);
            await SaveActualJobParamBase(jobParamsByActualSeniorityColab, groupId);
        }
    }

    private async Task<List<string>> SaveActualGroupParamResultInAssessmentResult(List<GroupParameter> getGroupParamsByAssessmentId, string assessmentResultId)
    {
        var groupsParamToMap = new List<GroupParameterResult>();
        getGroupParamsByAssessmentId?.ForEach(x =>
        {
            groupsParamToMap.Add(new GroupParameterResult
            {
                GroupParamTitle = x.GroupParamTitle,
                Weight = x.Weight,
                AssessmentResultId = assessmentResultId,
                AssessmentTamplateId = x.AssessmentId
            });
        });

        if (groupsParamToMap.Count > 0) await _groupParameterResultRepositorySql.SaveRange(groupsParamToMap);
        return groupsParamToMap.Select(x => x.Id).ToList();
    }

    private async Task SaveActualJobParamBase(List<JobParameterBase> getGroupParamsByAssessmentId,  string groupId)
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
                RealityResult = 1,
                Expected = 0,
                GroupParameterResultId = groupId
            });
        });
        await _assessmentParamResultRepositorySql.SaveRange(jobParamBaseToMap);
    }

    public async Task<UpdateAssessmentResultResponse> UpdateAssessmentResult(Guid assessmentId, UpdateAssessmentResultRequest assessmentResultDto)
    {
        var colab = await _colabRepositorySql.FindById(assessmentResultDto.CollaboratorId);
        var assessmentResult = await _assessmentResultRepositorySql.FindById(assessmentId.ToString());
        if (assessmentResult == null) throw new ApplicationException("exercise not found");
        assessmentResult.CollaboratorId = colab.Id;
        assessmentResult.SupervisorId = colab.SupervisorId;
        assessmentResult.NextAssessment = assessmentResultDto.NextAssessment;
        assessmentResult.Result = assessmentResultDto.Result;    
        
        assessmentResult.Validate();
        await _assessmentResultRepositorySql.Update(assessmentResult);
        
        foreach (var jobParam in assessmentResultDto.JobParams)
        {
            var jobParamResult = await _assessmentParamResultRepositorySql.FindById(jobParam.Id);
            if (string.IsNullOrWhiteSpace(jobParamResult.JobParamTitle)) continue;
            jobParamResult.RealityResult = jobParam.RealityResult;
            jobParamResult.Expected = jobParamResult.Expected;
            await _assessmentParamResultRepositorySql.Update(jobParamResult);
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
        var assessmentResult = await _assessmentResultRepositorySql.FindById(id.ToString());
        return new GetAssessmentResultResponse
        {
            Id = assessmentResult.Id,
            CollaboratorId = assessmentResult.CollaboratorId,
            SupervisorId =  assessmentResult.SupervisorId,
            Result = assessmentResult.Result ?? 0,
            NextAssessment = assessmentResult.NextAssessment
        };
    }
    
    public async Task<List<AssessmentResult>> GetAssessmentResultByColabId(string colabId)
    {
       return await _assessmentResultRepositorySql.GetAssessmentResultByColabId(colabId);
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
                Result = x.Result ?? 0,
                NextAssessment = x.NextAssessment
            });
        });
        return assessmentResultResponses;
    }
    
    public async Task DeleteAssessmentResultById(Guid id)
    {
        var assessmentResult = await _assessmentResultRepositorySql.FindById(id.ToString());
        _assessmentResultRepositorySql.Delete(assessmentResult);
        await _assessmentResultRepositorySql.SaveChange();
    }

    public async Task<byte[]> ReturnMetricsWithResultInPdf(Guid colabId)
    {
        var listFullAssessmentResult = new List<FullAssessmentResult>();

        var colab = await _colabRepositorySql.FindById(colabId.ToString());
        var assessmentResult = await GetAssessmentResultByColabId(colab.Id);
        foreach (var result in assessmentResult)
        {
            var fullAssessmentResult = new FullAssessmentResult();
            fullAssessmentResult.AssessmentResult = result;
            var groups = await _groupParameterResultRepositorySql.FindAll();
            fullAssessmentResult.GroupParameterResults = groups.Where(x => x.AssessmentResultId == result.Id).ToList();
            foreach (var groupParameterResult in groups.Where(x => x.AssessmentResultId == result.Id))
            {
                var jobParams = await _assessmentParamResultRepositorySql.FindAll();
                fullAssessmentResult.AssessmentParamResults =
                    jobParams.Where(x => x.GroupParameterResultId == groupParameterResult.Id).ToList();
            }
            listFullAssessmentResult.Add(fullAssessmentResult);
        }

        using var memoryStream = new MemoryStream();
        var document = new iTextSharp.text.Document(PageSize.A4);
        var writer = PdfWriter.GetInstance(document, memoryStream);
        document.Open();
        var firstRegisterToMakeMetrics =  listFullAssessmentResult.FirstOrDefault()!;
        var paragraph = new Paragraph($"Colaborador: {firstRegisterToMakeMetrics.AssessmentResult.Collaborator.Name}" +
                                      $"Supervisor {firstRegisterToMakeMetrics.AssessmentResult.ActualSupervisorName}" +
                                      $"Cargo {firstRegisterToMakeMetrics.AssessmentResult.ActualJobName} " +
                                      $"Senioridade {firstRegisterToMakeMetrics.AssessmentResult.ActualSeniorityName} " +
                                      $"Ultima avaliação {firstRegisterToMakeMetrics.AssessmentResult.NextAssessment!.Value.Date} " +
                                      $"Resultado da ultlima avalição {firstRegisterToMakeMetrics.AssessmentResult.Result}")
        {
            Alignment = Element.ALIGN_CENTER,
            Font = new Font(Font.FontFamily.TIMES_ROMAN, 18f)
        };
        document.Add(paragraph);
        AddTableContent(document, listFullAssessmentResult);
        document.Close();
        writer.Close();

        return memoryStream.ToArray();
    }

    private void AddTableContent(iTextSharp.text.Document document, List<FullAssessmentResult> assessment)
    {
        foreach (var assessmentResult in assessment)
        {
            var table = CreateItemTable(assessmentResult);
        
            document.Add(table);
        }
    }
    
    private PdfPTable CreateItemTable(FullAssessmentResult assessment)
    {
        var table = new PdfPTable(7)
        {
            WidthPercentage = 100f
        };
        foreach (var groupParameter in assessment.GroupParameterResults)
        {
            table.AddCell(groupParameter.GroupParamTitle);
            foreach (var assessmentParamResult in assessment.AssessmentParamResults)
            {
                table.AddCell(assessmentParamResult.JobParamTitle);
                table.AddCell(assessmentParamResult.Description);
                table.AddCell(assessmentParamResult.Observation);
                table.AddCell(assessmentParamResult.Weight.ToString());
                table.AddCell(assessmentParamResult.Expected.ToString());
                table.AddCell(assessmentParamResult.RealityResult.ToString());
            }
        }

        return table;
    }
}

internal class FullAssessmentResult
{
    public AssessmentResult AssessmentResult { get; set; }
    public List<GroupParameterResult> GroupParameterResults { get; set; }
    public List<AssessmentParamResult> AssessmentParamResults { get; set; }
}