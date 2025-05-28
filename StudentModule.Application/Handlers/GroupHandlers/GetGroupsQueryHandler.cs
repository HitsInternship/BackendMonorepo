using MediatR;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Queries.GroupQueries;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace StudentModule.Application.Handlers.GroupHandlers
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, List<GroupDto>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupsQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<List<GroupDto>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetGroupAsync();
            var groupsDto = new List<GroupDto>(groups.Count);
           
            foreach (var group in groups)
            {
                groupsDto.Add(new GroupDto(group));
            }

            return groupsDto;
        }
    }
}
