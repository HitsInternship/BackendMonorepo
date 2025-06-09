using MediatR;
using UserModule.Domain.Entities;

namespace UserModule.Contracts.Queries
{
    public record GetUsersByNameQuery : IRequest<List<User>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
