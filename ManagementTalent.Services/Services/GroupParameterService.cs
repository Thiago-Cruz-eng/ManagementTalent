using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class GroupParameterService
{
    private readonly IGroupParameterRepositorySql _groupParameterRepositorySql;

    public GroupParameterService(IGroupParameterRepositorySql groupParameterRepositorySql)
    {
        _groupParameterRepositorySql = groupParameterRepositorySql;
    }

    public async Task<CreateGroupParameterResponse> CreateGroupParameter(CreateGroupParameterRequest groupParameterDto)
    {
        var groupParameter = new GroupParameter
        {
            GroupParamTitle = groupParameterDto.GroupParamTitle,
            Weight = groupParameterDto.Weight,
            Parameters = groupParameterDto.Parameters
        };
        
        groupParameter.Validate();
 
        await _groupParameterRepositorySql.Save(groupParameter);
        await _groupParameterRepositorySql.SaveChange();
        return new CreateGroupParameterResponse
        {
            GroupParamTitle = groupParameter.GroupParamTitle,
            Weight = groupParameter.Weight,
            Parameters = groupParameter.Parameters
        };
    }
    
    public async Task<UpdateGroupParameterResponse> UpdateGroupParameter(Guid id, UpdateGroupParameterRequest groupParameterDto)
    {
        var groupParameter = await _groupParameterRepositorySql.FindById(id);
        if (groupParameter == null) throw new ApplicationException("exercise not found");
        groupParameter.GroupParamTitle = groupParameterDto.GroupParamTitle ?? groupParameter.GroupParamTitle;
        groupParameter.Weight = groupParameterDto.Weight ?? groupParameter.Weight;
        groupParameter.Parameters = groupParameterDto.Parameters ?? groupParameter.Parameters;
        
        groupParameter.Validate();
 
        await _groupParameterRepositorySql.Update(groupParameter);
        await _groupParameterRepositorySql.SaveChange();
        return new UpdateGroupParameterResponse
        {
            Success = true
        };
    }
    
    public async Task<GetGroupParameterResponse> GetGroupParameter(Guid id)
    {
        var groupParameter = await _groupParameterRepositorySql.FindById(id);
        return new GetGroupParameterResponse
        {
            GroupParamTitle = groupParameter.GroupParamTitle,
            Weight = groupParameter.Weight,
            Parameters = groupParameter.Parameters
        };
    }

    public async Task<List<GetGroupParameterResponse>> GetAllGroupParameter()
    {
        var groupParameterResponses = new List<GetGroupParameterResponse>();
        var groupParameter = await _groupParameterRepositorySql.FindAll();
        groupParameter.ForEach(x =>
        {
            groupParameterResponses.Add(new GetGroupParameterResponse
            {
                GroupParamTitle = x.GroupParamTitle,
                Weight = x.Weight,
                Parameters = x.Parameters
            });
        });
        return groupParameterResponses;
    }
    
    public async Task DeleteGroupParameterById(Guid id)
    {
        var groupParameter = await _groupParameterRepositorySql.FindById(id);
        _groupParameterRepositorySql.Delete(groupParameter);
        await _groupParameterRepositorySql.SaveChange();
    }
}