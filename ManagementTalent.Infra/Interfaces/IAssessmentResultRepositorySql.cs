using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IAssessmentResultRepositorySql : IBaseRepositorySql<Guid, AssessmentResult>
{
    
}