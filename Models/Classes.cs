using System.Text.Json.Serialization;
using Postdb.DTO;

namespace Postdb.Models;

public record Classes
{

    // public long SubId { get; set; }
    [JsonPropertyName("class_id")]
    public long ClassId { get; set; }

    [JsonPropertyName("class_name")]
    public string ClassName { get; set; }


    public ClassesDTO asDto => new ClassesDTO
        {
          ClassId = ClassId,
          ClassName = ClassName,

        };



}