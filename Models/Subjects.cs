namespace Postdb.Models;

public record Subjects
{
    public long SubId { get; set; }
    public string SubName { get; set; }


    public SubjectsDTO asDto => new SubjectsDTO
        {
          SubId = SubId,
          SubName = SubName,
          
        };



}