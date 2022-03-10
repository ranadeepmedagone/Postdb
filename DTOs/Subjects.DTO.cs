using System.Text.Json.Serialization;

public record SubjectsDTO
{

    [JsonPropertyName("sub_id")]
    public long SubId { get; set; }

    [JsonPropertyName("sub_name")]
    public string SubName { get; set; }

    [JsonPropertyName("teacher")]
    public List<TeacherDTO> Teacher { get; set; }



}
// public record GetSubjectsDTO
// {

//     [JsonPropertyName("sub_id")]
//     public long SubId { get; set; }

//     [JsonPropertyName("sub_name")]
//     public string SubName { get; set; }



// }