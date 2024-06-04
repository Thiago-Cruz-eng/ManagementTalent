using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IAssessmentRepositorySql : IBaseRepositorySql<Guid, Assessment>
{
    
}