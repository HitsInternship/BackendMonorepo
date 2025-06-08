using MediatR;
using SelectionModule.Domain.Entites;
using StudentModule.Domain.Entities;

namespace SelectionModule.Contracts.Commands.Selection;

public record CreateSelectionCommand(GlobalSelection GlobalSelection, List<StudentEntity> Students)
    : IRequest<Unit>;