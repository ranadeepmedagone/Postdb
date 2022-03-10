using Microsoft.AspNetCore.Mvc;
using Postdb.DTOs;
using Postdb.Models;
using Postdb.Repositories;

namespace Postdb.Controllers;

[ApiController]
[Route("api/teacher")]
public class TeacherController : ControllerBase
{
    private readonly ILogger<TeacherController> _logger;
    private readonly ITeacherRepository _Teacher;
    private readonly IStudentRepository _Student;

    public TeacherController(ILogger<TeacherController> logger, ITeacherRepository Teacher, IStudentRepository student)
    {
        _logger = logger;
        _Teacher = Teacher;
        _Student = student;
    }
    [HttpGet]
    public async Task<ActionResult<List<TeacherDTO>>> GetAllTeachers()
    {
        var TeachersList = await _Teacher.GetList();

        var dtoList = TeachersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{teacher_id}")]

    public async Task<ActionResult<TeacherDTO>> GetById([FromRoute] long teacher_id)
    {
        var Teacher = await _Teacher.GetById(teacher_id);
        if (Teacher is null)
            return NotFound("No Teacher found with given employee number");
        var dto = Teacher.asDto;
        dto.Student = await _Student.GetList(Teacher.TeacherId);
        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<TeacherDTO>> CreateTeacher([FromBody] CreateTeacherDTO Data)
    {
        if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
            return BadRequest("Gender value is not recognized");



        var toCreateTeacher = new Teacher
        {
            FirstName = Data.FirstName.Trim(),
            LastName = Data.LastName.Trim(),
            Gender = Data.Gender.Trim(),
            BirthDay = Data.BirthDay.UtcDateTime,
            Mobile = Data.Mobile,
            Address = Data.Address,

            Email = Data.Email.Trim().ToLower(),
            DateOfJoin = Data.DateOfJoin.UtcDateTime,
            //   Email = Data.Email.Trim().ToLower(),
            Subject = Data.Subject,




        };
        var createdTeacher = await _Teacher.Create(toCreateTeacher);

        return StatusCode(StatusCodes.Status201Created, createdTeacher.asDto);
        // return CreateTeacher();


    }

    [HttpPut("{teacher_id}")]
    public async Task<ActionResult> UpdateTeacher([FromRoute] long TeacherId,
    [FromBody] TeacherUpdateDTO Data)
    {
        var existing = await _Teacher.GetById(TeacherId);
        if (existing is null)
            return NotFound("No Teacher found with given id");

        var toUpdateTeacher = existing with
        {

            LastName = Data.LastName?.Trim() ?? existing.LastName,
            Mobile = Data.Mobile ?? existing.Mobile,
            Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            Address = Data.Address?.Trim()?.ToLower() ?? existing.Address,
            Subject = Data.Subject?.Trim()?.ToLower() ?? existing.Subject,
            // BirthDay = existing.BirthDay.UtcDateTime,
        };

        var didUpdate = await _Teacher.Update(toUpdateTeacher);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{teacherId}")]
    public async Task<ActionResult> DeleteTeacher([FromRoute] long TeacherId)
    {
        var existing = await _Teacher.GetById(TeacherId);
        if (existing is null)
            return NotFound("No Teacher found with given id");
        await _Teacher.Delete(TeacherId);
        return NoContent();
    }



}
