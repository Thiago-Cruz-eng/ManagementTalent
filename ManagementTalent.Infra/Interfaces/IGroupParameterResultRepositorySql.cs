using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IGroupParameterResultRepositorySql : IBaseRepositorySql<Guid, GroupParameterResult>
{
    public Task SaveRange(List<GroupParameterResult> groups);
}