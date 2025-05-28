using StudentModule.Domain.Entities;


namespace StudentModule.Contracts.DTOs
{
    public class GroupDto
    {
        public Guid Id { get; set; }
        public int GroupNumber { get; set; }
        public Guid StreamId { get; set; }
        public List<StudentDto> Students { get; set; }

        public GroupDto() { }

        public GroupDto(GroupEntity group)
        {
            Id = group.Id;
            GroupNumber = group.GroupNumber;
            StreamId = group.StreamId;
            /*Students = group.Students
                       .Select(s => new StudentDto(s))
                       .ToList();*/
        }
    }
}
