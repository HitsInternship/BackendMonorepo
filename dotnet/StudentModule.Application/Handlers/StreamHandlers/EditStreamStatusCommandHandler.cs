using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using StudentModule.Contracts.Commands.StreamCommands;
using UserModule.Contracts.Repositories;

namespace StudentModule.Application.Handlers.StreamHandlers
{
    public class EditStreamStatusCommandHandler : IRequestHandler<EditStreamStatusCommand, StreamDto>
    {
        private readonly IStreamRepository _streamRepository;
        private readonly IUserRepository _userRepository;

        public EditStreamStatusCommandHandler(IStreamRepository streamRepository, IUserRepository userRepository)
        {
            _streamRepository = streamRepository;
            _userRepository = userRepository;
        }

        public async Task<StreamDto> Handle(EditStreamStatusCommand request, CancellationToken cancellationToken)
        {
            StreamEntity? stream = await _streamRepository.GetByIdAsync(request.Id)
                                   ?? throw new NotFound("Stream not found");


            stream.Status = request.Status;

            //todo: create selection or practice

            await _streamRepository.UpdateAsync(stream);

            return await GetStream(stream);
        }

        private async Task<StreamDto> GetStream(StreamEntity stream)
        {
            var groups = new List<GroupDto>();

            foreach (var group in stream.Groups)
            {
                List<StudentDto> students = [];
                foreach (var student in group.Students)
                {
                    var user = await _userRepository.GetByIdAsync(student.UserId);
                    student.User = user;
                    students.Add(new StudentDto(student));
                }

                var groupDto = new GroupDto(group);
                groupDto.Students = students;
                groups.Add(groupDto);
            }

            var streamDto = new StreamDto(stream)
            {
                groups = groups
            };

            return streamDto;
        }
    }
}