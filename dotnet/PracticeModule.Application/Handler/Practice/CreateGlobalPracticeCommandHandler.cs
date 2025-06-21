using AutoMapper;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using DocumentModule.Contracts.Commands;
using DocumentModule.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticeModule.Contracts.Commands;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Contracts.Queries;
using PracticeModule.Contracts.Repositories;
using PracticeModule.Domain.Entity;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PracticeModule.Application.Handler.PracticePart;

public class CreateGlobalPracticeCommandHandler : IRequestHandler<CreateGlobalPracticeCommand, GlobalPractice>
{
    private readonly ISemesterRepository _semesterRepository;
    private readonly IGlobalPracticeRepository _globalPracticeRepository;
    private readonly IPracticeRepository _practiceRepository;
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CreateGlobalPracticeCommandHandler(ISemesterRepository semesterRepository, IGlobalPracticeRepository globalPracticeRepository, IPracticeRepository practiceRepostitory, ISender sender, IMapper mapper)
    {
        _semesterRepository = semesterRepository;
        _globalPracticeRepository = globalPracticeRepository;
        _practiceRepository = practiceRepostitory;
        _sender = sender;
        _mapper = mapper;
    }

    public async Task<GlobalPractice> Handle(CreateGlobalPracticeCommand command, CancellationToken cancellationToken)
    {
        SemesterEntity? lastSemester = await _semesterRepository.GetByIdAsync(command.createRequest.lastSemesterId);
        if (lastSemester == null) { throw new NotFound("No last semester"); }

        GlobalPractice? lastGlobalPractice = (await _globalPracticeRepository.ListAllAsync()).Where(globalPractice => globalPractice.SemesterId == lastSemester.Id && command.createRequest.streamId == globalPractice.StreamId).FirstOrDefault();
        if (lastGlobalPractice != null)
        {
            (await _practiceRepository.ListAllAsync()).Where(practice => practice.GlobalPractice == lastGlobalPractice).ExecuteUpdate(practiceProperties => practiceProperties.SetProperty(practice => practice.IsDeleted, true));
            lastGlobalPractice.IsDeleted = true;

            await _globalPracticeRepository.UpdateAsync(lastGlobalPractice);
        }

        GlobalPractice? globalPractice = (await _globalPracticeRepository.ListAllAsync()).FirstOrDefault(globalPractice => globalPractice.SemesterId == command.createRequest.semesterId && globalPractice.StreamId == command.createRequest.streamId);
        if (globalPractice != null) { throw new Conflict("Global practice with such attributes already exists"); }

        globalPractice = _mapper.Map<GlobalPractice>(command.createRequest);

        globalPractice.DiaryPatternDocumentId = await _sender.Send(new LoadDocumentCommand(DocumentType.PracticeDiary, command.createRequest.diaryPatternFile));
        globalPractice.CharacteristicsPatternDocumentId = await _sender.Send(new LoadDocumentCommand(DocumentType.StudentPracticeCharacteristic, command.createRequest.characteristicsPatternFile));

        List<Domain.Entity.Practice> potentialPractices = await _sender.Send(new SearchPotentialPracticeQuery(new SearchPotentialPracticeRequest() { streamId = command.createRequest.streamId, lastSemesterId = command.createRequest.lastSemesterId }));

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