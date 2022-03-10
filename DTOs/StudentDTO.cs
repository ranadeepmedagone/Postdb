
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Postdb.DTOs;
public record StudentDTO
{
    [JsonPropertyName("student_id")]
    public long StudentId { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }


    [JsonPropertyName("mobile")]

    public long ParentMobile { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("join_date")]
    public DateTimeOffset JoinDate { get; set; }


    [JsonPropertyName("class_id")]
    public long ClassId { get; set; }



    [JsonPropertyName("teachers_assigned")]
    public List<TeacherDTO> Teacher { get; set; }

    [JsonPropertyName("subjects_enrolled")]
    public List<SubjectsDTO> Subject { get; set; }



}

public record CreateStudentDTO
{


    [JsonPropertyName("first_name")]
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }


    [JsonPropertyName("gender")]
    [Required]
    [MaxLength(6)]
    public string Gender { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("mobile")]
    [Required]



    public long ParentMobile { get; set; }


    [JsonPropertyName("email")]

    [MaxLength(255)]
    public string Email { get; set; }


    [JsonPropertyName("date_of_birth")]
    [Required]



    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("join_date")]
    public DateTimeOffset JoinDate { get; set; }

    [JsonPropertyName("class_id")]
    public long ClassId { get; set; }

}


public record StudentUpdateDTO
{


    [JsonPropertyName("last_name")]

    [MaxLength(50)]


    public string LastName { get; set; }
    [JsonPropertyName("mobile")]



    public long? Mobile { get; set; } = null;
    [JsonPropertyName("email")]

    [MaxLength(255)]
    public string Email { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }




}




