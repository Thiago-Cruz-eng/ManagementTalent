using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IAssessmentRepositorySql : IBaseRepositorySql<string, Assessment>
{
    public Task<Assessment> GetAssessmentByJobRole(string jobRoleId);
}