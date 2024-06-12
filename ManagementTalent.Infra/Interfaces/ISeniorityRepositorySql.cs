using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface ISeniorityRepositorySql : IBaseRepositorySql<string, Seniority>
{
    public Task<List<Seniority>> GetAssessmentByJobRole(string jobRoleId);
}