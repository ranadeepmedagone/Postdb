using System.Text.Json.Serialization;
using Postdb.DTOs;

namespace Postdb.DTO;

public record ClassesDTO
{

    // public long SubId { get; set; }
    [JsonPropertyName("class_id")]
    public long ClassId { get; set; }

    [JsonPropertyName("class_name")]
    public string ClassName { get; set; }

    [JsonPropertyName("students_in_class")]
    public List<StudentDTO> Student { get; set; }


    


    



}