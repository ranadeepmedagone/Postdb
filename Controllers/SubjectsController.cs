using Microsoft.AspNetCore.Mvc;
using Postdb.DTOs;
using Postdb.Models;
using Postdb.Repositories;

namespace Postdb.Controllers;

[ApiController]
[Route("api/subjects")]
public class SubjectsController : ControllerBase
{
    private readonly ILogger<SubjectsController> _logger;
    private readonly ISubjectsRepository _subjects;
    private readonly ITeacherRepository _teacher;

    public SubjectsController(ILogger<SubjectsController> logger, ISubjectsRepository Subjects, ITeacherRepository teacher)
    {
        _logger = logger;
        _subjects = Subjects;
        _teacher = teacher;
    }
    [HttpGet]
    public async Task<ActionResult<List<SubjectsDTO>>> GetAllSubjects()
    {
        var SubjectsList = await _subjects.GetList();

        var dtoList = SubjectsList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{sub_id}")]

    public async Task<ActionResult<SubjectsDTO>> GetById([FromRoute] long sub_id)
    {
        var subjects = await _subjects.GetById(sub_id);
        if (subjects is null)
            return NotFound("No subject found with given id");
        var dto = subjects.asDto;
        dto.Teacher = await _teacher.GetListBySubject(subjects.SubId);
        // dto.Teacher = await _teacher.GetList(studentid);
        return Ok(dto);
    }

}

