using Microsoft.AspNetCore.Mvc;
using Postdb.DTOs;
using Postdb.Models;
using Postdb.Repositories;

namespace Postdb.Controllers;

[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
private readonly ILogger<StudentController>_logger;
private readonly IStudentRepository _student;
private readonly ITeacherRepository _teacher;
private readonly ISubjectsRepository _subjects;

public StudentController(ILogger<StudentController> logger,IStudentRepository Student,ITeacherRepository teacher,ISubjectsRepository subjects)
{
    _logger = logger;
    _student = Student;
    _teacher = teacher;
    _subjects = subjects;
}
[HttpGet]
public async Task<ActionResult<List<StudentDTO>>> GetAllStudents()
{
   var StudentsList = await _student.GetList();

   var dtoList = StudentsList.Select(x => x.asDto);

   return Ok(dtoList);
}

[HttpGet("{student_id}")]

public async Task<ActionResult<StudentDTO>> GetById([FromRoute] long student_id)
{
 var Student = await _student.GetById(student_id);
 if (Student is null)
 return NotFound("No Student found with given employee number");
 var dto = Student.asDto;
 dto.Teacher = await _teacher.GetList(Student.StudentId);
 dto.Subject = await _subjects.GetSubjects(Student.StudentId);
 return Ok(dto);
}

[HttpPost]

public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody] CreateStudentDTO Data)
{
  if (!(new string[] {"male","female"}.Contains(Data.Gender.Trim().ToLower())))
  return BadRequest ("Gender value is not recognized");

  var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
  if (subtractDate.TotalDays / 365 > 18.0)
  return BadRequest("student must be at least below 18 years old");

  var toCreateStudent = new Student
  {
      FirstName = Data.FirstName.Trim(),
      LastName = Data.LastName.Trim(),
      Gender = Data.Gender.Trim(),
      DateOfBirth = Data.DateOfBirth.UtcDateTime,
      Address = Data.Address,
      ParentMobile = Data.ParentMobile,
      Email = Data.Email.Trim().ToLower(),
      JoinDate = Data.JoinDate.UtcDateTime,
      ClassId = Data.ClassId,

      
      


  };
  var createdStudent =  await _student.Create(toCreateStudent);

  return StatusCode(StatusCodes.Status201Created,createdStudent.asDto);
  // return CreateStudent();


}

[HttpPut("{student_id}")]
public async Task<ActionResult> UpdateStudent([FromRoute] long StudentId,
[FromBody] StudentUpdateDTO Data)
{
 var existing = await _student.GetById(StudentId);
 if (existing is null)
 return NotFound("No Student found with given id");

 var toUpdateStudent = existing with
 {
Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
LastName = Data.LastName?.Trim() ?? existing.LastName,
ParentMobile = Data.Mobile ?? existing.ParentMobile,
Address = Data.Address?.Trim()?.ToLower() ?? existing.Address,
// DateOfBirth = existing.DateOfBirth.UtcDateTime,
 };

 var didUpdate = await _student.Update(toUpdateStudent);

 if (!didUpdate)
 return StatusCode(StatusCodes.Status500InternalServerError,"Could not update");
 return NoContent();
}

[HttpDelete("{student_id}")]
public async Task<ActionResult> DeleteStudent([FromRoute] long StudentId)
{
  var existing = await _student.GetById(StudentId);
 if (existing is null)
 return NotFound("No Student found with given id");
 await _student.Delete(StudentId);
 return NoContent();
}



}
