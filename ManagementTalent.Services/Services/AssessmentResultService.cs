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
        
        var assessmentResult = new AssessmentResult //mapeamento da entidade de dominio
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
            Id = assessmentResult.Id
        };
    }

    private async Task SaveActualJobParamResultInAssessmentParamResult(List<GroupParameter> getGroupParamsByAssessmentId, Seniority seniority, GroupParameterResult groupParamResult)
    {
        var groupsList = getGroupParamsByAssessmentId;
        var jobParamBase = new List<JobParameterBase>();
        foreach (var gptamplate in groupsList)
        {
            var allJobParamsByGroup = await _groupParameterRepositorySql.GetJobParameterByGroup(gptamplate.Id);
            jobParamBase = await _jobParameterBaseRepositorySql.GetActualJobParamByColabSeniority(allJobParamsByGroup, seniority.Id);
      
            if(gptamplate.GroupParamTitle == groupParamResult.GroupParamTitle)
                await SaveActualJobParamBase(jobParamBase, groupParamResult.Id);
        }
    }

    private async Task<List<GroupParameterResult>> SaveActualGroupParamResultInAssessmentResult(List<GroupParameter> getGroupParamsByAssessmentId, string assessmentResultId)
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
        return groupsParamToMap;
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
                Expected = x.Expected,
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
        
        assessmentResult.Validate();
        await _assessmentResultRepositorySql.Update(assessmentResult);
        
        foreach (var jobParam in assessmentResultDto.JobParams)
        {
            var jobParamResult = await _assessmentParamResultRepositorySql.FindById(jobParam.Id);
            if (string.IsNullOrWhiteSpace(jobParamResult.JobParamTitle)) continue;
            jobParamResult.RealityResult = jobParam.RealityResult;
            jobParamResult.Observation = jobParam.Observation;
            await _assessmentParamResultRepositorySql.Update(jobParamResult);
        }

        var resultByGroup = new List<double?>();
        var groupList = await _groupParameterResultRepositorySql.FindByAssessmentId(assessmentResult.Id);
        foreach (var groupParameterResult in groupList)
        {
            var jobParams = await _assessmentParamResultRepositorySql.GetAssessmentParamResultByGroupParameterResul(Guid.Parse(groupParameterResult.Id));
            var sumOfParam = new List<double?>();
            foreach (var assessmentParamResult in jobParams)
            {
                var resultOfParam = assessmentParamResult.Weight *
                                    (assessmentParamResult.RealityResult / assessmentParamResult.Expected);
                sumOfParam.Add(resultOfParam);
            }

            resultByGroup.Add(sumOfParam.Sum() * groupParameterResult.Weight);
        }

        assessmentResult.Result = resultByGroup.Sum();
        
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
            var jobparam = await _assessmentParamResultRepositorySql.FindAll();
            var actualGruopInAssessment = groups.Where(x => x.AssessmentResultId == result.Id).ToList();
            foreach (var groupParameterResult in actualGruopInAssessment)
            {
                var actualJobParamResultInGroupParamResult =
                    jobparam.Where(x => x.GroupParameterResultId == groupParameterResult.Id).ToList();
                
                fullAssessmentResult.GroupJobParam.Add(groupParameterResult);
            }
            
            listFullAssessmentResult.Add(fullAssessmentResult);
        }

        using var memoryStream = new MemoryStream();
        var document = new iTextSharp.text.Document(PageSize.A4);
        var writer = PdfWriter.GetInstance(document, memoryStream);
        var par = new List<Paragraph>();
        document.Open();
        var firstRegisterToMakeMetrics =  listFullAssessmentResult.MaxBy(x => x.AssessmentResult.CreateAt)!;
        par.Add(new Paragraph("Relatorio de perfomance:", 
            new Font(Font.FontFamily.TIMES_ROMAN, 14f, Font.BOLD)));
       
        par.Add(new Paragraph($"Colaborador: {firstRegisterToMakeMetrics.AssessmentResult.Collaborator.Name}")
        {
            Alignment = Element.ALIGN_CENTER,
            Font = new Font(Font.FontFamily.TIMES_ROMAN, 18f)
        });
        par.Add(new Paragraph($"Supervisor: {firstRegisterToMakeMetrics.AssessmentResult.ActualSupervisorName}")
        {
            Alignment = Element.ALIGN_CENTER,
            Font = new Font(Font.FontFamily.TIMES_ROMAN, 18f)
        });
        par.Add(new Paragraph($"Cargo: {firstRegisterToMakeMetrics.AssessmentResult.ActualJobName} ")
        {
            Alignment = Element.ALIGN_CENTER,
            Font = new Font(Font.FontFamily.TIMES_ROMAN, 18f)
        });
        par.Add(new Paragraph($"Senioridade: {firstRegisterToMakeMetrics.AssessmentResult.ActualSeniorityName} ")
        {
            Alignment = Element.ALIGN_CENTER,
            Font = new Font(Font.FontFamily.TIMES_ROMAN, 18f)
        });
        par.Add(new Paragraph($"Ultima avaliação: {firstRegisterToMakeMetrics.AssessmentResult.NextAssessment!.Value.Date} ")
        {
            Alignment = Element.ALIGN_CENTER,
            Font = new Font(Font.FontFamily.TIMES_ROMAN, 18f)
        });
        par.Add(new Paragraph($"Resultado da ultlima avalição: {firstRegisterToMakeMetrics.AssessmentResult.Result}")
        {
            Alignment = Element.ALIGN_CENTER,
            Font = new Font(Font.FontFamily.TIMES_ROMAN, 18f)
        });
        par.ForEach(x => document.Add(x));
        CreateComparisonContent(document, listFullAssessmentResult);
        AddTableContent(document, listFullAssessmentResult);
        document.Close();
        writer.Close();

        return memoryStream.ToArray();
    }

    private void AddTableContent(iTextSharp.text.Document document, List<FullAssessmentResult> assessment)
    {
        foreach (var assessmentResult in assessment.OrderByDescending(x => x.AssessmentResult.CreateAt))
        {
            document.Add(new Paragraph("______________________________________________________________________________"));
            document.Add(new Paragraph($"Data: {assessmentResult.AssessmentResult.CreateAt}"));
            document.Add(new Paragraph($"Cargo: {assessmentResult.AssessmentResult.ActualJobName}"));
            document.Add(new Paragraph($"Senioridade: {assessmentResult.AssessmentResult.ActualSeniorityName}"));
            document.Add(new Paragraph($"Resultado: {assessmentResult.AssessmentResult.Result}"));
            CreateItemContent(document, assessmentResult);
        }
    }

    private void CreateComparisonContent(iTextSharp.text.Document document, List<FullAssessmentResult> assessment)
    {
        var sortedAssessments = assessment.OrderByDescending(x => x.AssessmentResult.CreateAt).ToList();
        
        if (sortedAssessments.Count < 2)
        {
            document.Add(new Paragraph("Não há avaliações suficientes para comparação."));
            return;
        }
        
        var currentAssessment = sortedAssessments[0];
        var previousAssessment = sortedAssessments[1];
        
        var comparisons = CompareAssessments(currentAssessment, previousAssessment);

        document.Add(new Paragraph("______________________________________________________________________________"));
        document.Add(new Paragraph($"Evolução em comparação com a última avaliação: {comparisons.Evolution}"));
        document.Add(new Paragraph($"Pilar com maior destaque: {comparisons.TopGroupParamTitle}"));
        document.Add(new Paragraph($"Parâmetro de maior destaque: {comparisons.TopEvolvedParamTitle}"));
        document.Add(new Paragraph($"Parâmetro de menor destaque: {comparisons.LeastEvolvedParamTitle}"));
    }

    private ComparisonResult CompareAssessments(FullAssessmentResult current, FullAssessmentResult previous)
    {
        var betterPillar = 0;
        var betterParamResult = 0;
        var worstParamResult = 999;
        double? diff = 0.0;
        var betterPillarName = "";
        var betterParamResultName = "";
        var worstParamResultName = "";
        
        foreach (var currentGroup in current.GroupJobParam)
        {
            var previousGroup = previous.GroupJobParam.FirstOrDefault(g => g.GroupParamTitle == currentGroup.GroupParamTitle);
            if (previousGroup != null)
            {
                var actualRealityResult = currentGroup.AssessmentParam.Sum(x => x.RealityResult);
                var prevRealityResult = previousGroup.AssessmentParam.Sum(x => x.RealityResult);
                if (actualRealityResult > betterPillar) betterPillarName = currentGroup.GroupParamTitle;
                diff = ((prevRealityResult - actualRealityResult) / prevRealityResult) * 100;

                var worstParam = currentGroup.AssessmentParam.MinBy(x => x.RealityResult);
                if(worstParamResult > worstParam?.RealityResult) worstParamResultName = worstParam.JobParamTitle;
                var betterParam = currentGroup.AssessmentParam.MaxBy(x => x.RealityResult);
                if(betterParamResult < betterParam?.RealityResult) betterParamResultName = betterParam.JobParamTitle;
            }
        }

        return new ComparisonResult
        {
            Evolution = diff,
            TopGroupParamTitle = betterPillarName,
            TopEvolvedParamTitle = betterParamResultName,
            LeastEvolvedParamTitle = worstParamResultName
        };
    }

    private void CreateItemContent(iTextSharp.text.Document document, FullAssessmentResult assessment)
    {
        foreach (var groupParameterJobParam in assessment.GroupJobParam)
        {
            var paragraphHeader = new Paragraph(groupParameterJobParam.GroupParamTitle + ":", 
                new Font(Font.FontFamily.TIMES_ROMAN, 14f, Font.BOLD));
            document.Add(paragraphHeader);

            foreach (var assessmentParamResult in groupParameterJobParam.AssessmentParam.Where(x => x.GroupParameterResultId == groupParameterJobParam.Id))
            {
                document.Add(new Paragraph($"{assessmentParamResult.JobParamTitle}", new Font(Font.FontFamily.TIMES_ROMAN, 16f, Font.BOLD)));
                document.Add(new Paragraph($"- Observação: {assessmentParamResult.Description}"));
                document.Add(new Paragraph($"- Observação: {assessmentParamResult.Observation}"));
                document.Add(new Paragraph($"- Esperado: {assessmentParamResult.Expected}"));
                document.Add(new Paragraph($"- Realizado: {assessmentParamResult.RealityResult}"));
            }
        }
    }
}

internal class FullAssessmentResult
{
    public AssessmentResult AssessmentResult { get; set; }
    public List<GroupParameterResult> GroupJobParam { get; set; } = new();
}

internal class ComparisonResult
{
    public double? Evolution { get; set; }
    public string TopGroupParamTitle { get; set; }
    public string TopEvolvedParamTitle { get; set; }
    public string LeastEvolvedParamTitle { get; set; }
}