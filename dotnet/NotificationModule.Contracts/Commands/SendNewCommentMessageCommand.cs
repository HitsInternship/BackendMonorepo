using MediatR;
using NotificationModule.Domain.Enums;

namespace NotificationModule.Contracts.Commands;

public record SendNewCommentMessageCommand(
    string Email,
    CommentType CommentType,
    string Message,
    string Fullname,
    Guid Typeid) : IRequest<Unit>;