using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using StudentModule.Contracts.Commands.StreamCommands;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace StudentModule.Application.Handlers.StreamHandlers
{
    public class EditStreamCommandHandler : IRequestHandler<EditStreamCommand, StreamDto>
    {
        private readonly IStreamRepository _streamRepository;
        private readonly IUserRepository _userRepository;

        public EditStreamCommandHandler(IStreamRepository streamRepository, IUserRepository userRepository)
        {
            _streamRepository = streamRepository;
            _userRepository = userRepository;
        }

        public async Task<StreamDto> Handle(EditStreamCommand request, CancellationToken cancellationToken)
        {
            StreamEntity? stream = await _streamRepository.GetStreamByIdAsync(request.Id) 
                ?? throw new NotFound("Stream not found");
            
            if (await _streamRepository.IsStreamWithNumderExistsAsync(request.StreamNumber) && request.StreamNumber != stream.StreamNumber)
                throw new Conflict($"Stream with number {request.StreamNumber} already exists");

            if (request.StreamNumber < 0) 
                throw new BadRequest("Invalid stream number");

            if (request.Year < 2017 || request.Year > DateTime.Now.Year)
                throw new BadRequest("Invalid year");

            if (request.Course < 1 || request.Course > 4)
                throw new BadRequest("Invalid course");
            

            stream.StreamNumber = request.StreamNumber;
            stream.Status = request.Status;
            stream.Year = request.Year;
            stream.Course = request.Course;

            await _streamRepository.UpdateAsync(stream);



            return await GetStream(stream);
        }

        private async Task<StreamDto> GetStream(StreamEntity stream)
        {
            var groups = new List<GroupDto>();
            var students = new List<StudentDto>();

            foreach (var group in stream.Groups)
            {
                students = new List<StudentDto>();
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

            var streamDto = new StreamDto(stream);
            streamDto.groups = groups;

            return streamDto;
        }
    }
}
