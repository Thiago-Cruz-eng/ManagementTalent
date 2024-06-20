using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IJobRoleRepositorySql : IBaseRepositorySql<Guid, JobRole>
{
    Task<JobRole> GetJobRoleByName(string name);
}