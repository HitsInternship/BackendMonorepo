using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.DTOs.Requests;
using CompanyModule.Domain.Enums;
using DeanModule.Contracts.Commands.DeanMember;
using DeanModule.Contracts.Dtos.Requests;
using MediatR;
using StudentModule.Contracts.Commands.GroupCommands;
using StudentModule.Contracts.Commands.StreamCommands;
using StudentModule.Contracts.Commands.StudentCommands;
using StudentModule.Domain.Enums;
using UserModule.Contracts.DTOs.Requests;
using UserModule.Contracts.Queries;

namespace HitsInternship.Api.Extensions.GlobalInitializer
{
    public static class GlobalInitializer
    {
        public static async Task InitializeDatabases(this IServiceProvider services)
        {
            //Добавление тестовых пользователей
            using (var scope = services.CreateScope())
            {
                var sender = scope.ServiceProvider.GetRequiredService<ISender>();

                Guid? adminId = (await sender.Send(new GetListSearchUserQuery(new SearchUserRequest()
                    {
                        email = "admin@example.com"
                    }
                ))).FirstOrDefault()?.Id;

                if (adminId != null) return;

                //Добавление сотрудника деканата
                await sender.Send(new CreateDeanMemberCommand(new DeanMemberRequestDto()
                    {
                        Name = "Сотрудник",
                        Surname = "Деканата",
                        Email = "dean@example.com"
                    }
                ));

                //Добавление куратора
                Guid companyId = (await sender.Send(new AddCompanyCommand(new CompanyRequest()
                    {
                        name = "Херриот-Ватт",
                        description = "Описание для компании",
                        status = CompanyStatus.Partner,
                    }
                ))).Id;

                await sender.Send(new AddCuratorCommand(companyId, new CuratorRequest()
                    {
                        userRequest = new UserRequest()
                        {
                            name = "Куратор",
                            surname = "ХВ",
                            email = "curator@example.com"
                        },
                        telegram = "valera",
                        phone = "+7 912 493 34 34"
                    }
                ));


                //Добавление студента
                Guid streamId = (await sender.Send(new CreateStreamCommand()
                    {
                        StreamNumber = 9722,
                        Year = 2022,
                        Status = StreamStatus.Practice,
                        Course = 3
                    }
                )).id;

                Guid groupId = (await sender.Send(new CreateGroupCommand()
                    {
                        GroupNumber = 2,
                        StreamId = streamId,
                    }
                )).Id;

                await sender.Send(new CreateStudentCommand()
                    {
                        userRequest = new UserRequest()
                        {
                            name = "Студент",
                            surname = "Хитса",
                            email = "student@example.com"
                        },

                        Middlename = "Сергеевич",
                        Phone = "+7 348 343 83 83",
                        IsHeadMan = false,
                        Status = StudentStatus.InProcess,
                        GroupId = groupId,
                    }
                );

                //Добавление админского пользователя со всеми ролями
                await sender.Send(new CreateDeanMemberCommand(new DeanMemberRequestDto()
                    {
                        Name = "Админский",
                        Surname = "Аккаунт",
                        Email = "admin@example.com"
                    }
                ));

                adminId = (await sender.Send(new GetListSearchUserQuery(new SearchUserRequest()
                    {
                        email = "admin@example.com"
                    }
                ))).First().Id;

                await sender.Send(new AddCuratorCommand(companyId, new CuratorRequest()
                    {
                        userId = adminId,
                        telegram = "admin",
                        phone = "+7 875 439 54 23"
                    }
                ));
                await sender.Send(new CreateStudentCommand()
                {
                    userId = adminId,
                    Middlename = "Григорьевич",
                    Phone = "+7 875 439 54 23",
                    IsHeadMan = false,
                    Status = StudentStatus.OnAcademicLeave,
                    GroupId = groupId,
                });
            }
        }
    }
}