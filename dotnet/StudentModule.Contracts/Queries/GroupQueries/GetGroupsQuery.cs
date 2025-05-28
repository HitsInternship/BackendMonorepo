using MediatR;
using StudentModule.Contracts.DTOs;


namespace StudentModule.Contracts.Queries.GroupQueries
{
    public record GetGroupsQuery() : IRequest<List<GroupDto>>;
}
