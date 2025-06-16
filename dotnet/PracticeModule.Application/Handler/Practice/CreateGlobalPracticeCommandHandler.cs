using AutoMapper;
using DocumentModule.Contracts.Commands;
using DocumentModule.Domain.Enums;
using MediatR;
using PracticeModule.Contracts.Commands;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Contracts.Queries;
using PracticeModule.Contracts.Repositories;
using PracticeModule.Domain.Entity;
using Shared.Domain.Exceptions;

namespace PracticeModule.Application.Handler.Practice;

public class CreateGlobalPracticeCommandHandler : IRequestHandler<CreateGlobalPracticeCommand, GlobalPractice>
{
    private readonly IGlobalPracticeRepository _globalPracticeRepository;
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CreateGlobalPracticeCommandHandler(IGlobalPracticeRepository globalPracticeRepository, ISender sender, IMapper mapper)
    {
        _globalPracticeRepository = globalPracticeRepository;
        _sender = sender;
        _mapper = mapper;
    }

    public async Task<GlobalPractice> Handle(CreateGlobalPracticeCommand command, CancellationToken cancellationToken)
    {
        GlobalPractice? globalPractice = (await _globalPracticeRepository.ListAllAsync()).FirstOrDefault(globalPractice => globalPractice.SemesterId == command.createRequest.semesterId && globalPractice.StreamId == command.createRequest.streamId);
        if (globalPractice != null) { throw new Conflict("Global practice with such attributes already exists"); }

        globalPractice = _mapper.Map<GlobalPractice>(command.createRequest);

        globalPractice.DiaryPatternDocumentId = await _sender.Send(new LoadDocumentCommand(DocumentType.PracticeDiary, command.createRequest.diaryPatternFile));
        globalPractice.CharacteristicsPatternDocumentId = await _sender.Send(new LoadDocumentCommand(DocumentType.StudentPracticeCharacteristic, command.createRequest.characteristicsPatternFile));

        List<Domain.Entity.Practice> potentialPractices = await _sender.Send(new SearchPotentialPracticeQuery(new SearchPotentialPracticeRequest()));

        foreach (var practice in potentialPractices)
        {
            practice.StudentId = practice.Student.Id;
            practice.CompanyId = practice.NewCompany?.Id ?? practice.Company.Id;
            practice.PositionId = practice.NewPosition?.Id ?? practice.Position.Id;
        }

        globalPractice.Practices = potentialPractices;

        await _globalPracticeRepository.AddAsync(globalPractice);

        return globalPractice;
    }
}