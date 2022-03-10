using Microsoft.AspNetCore.Mvc;
using Postdb.DTO;
using Postdb.DTOs;
using Postdb.Models;
using Postdb.Repositories;

namespace Postdb.Controllers;

[ApiController]
[Route("api/classes")]
public class ClassesController : ControllerBase
{
    private readonly ILogger<ClassesController> _logger;
    private readonly IClassesRepository _classes;
    private readonly IStudentRepository _student;

    public ClassesController(ILogger<ClassesController> logger, IClassesRepository Classes, IStudentRepository Student)
    {
        _logger = logger;
        _classes = Classes;
        _student = Student;
    }
    [HttpGet]
    public async Task<ActionResult<List<ClassesDTO>>> GetAllClasses()
    {
        var ClassesList = await _classes.GetList();

        var dtoList = ClassesList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{class_id}")]

    public async Task<ActionResult<ClassesDTO>> GetClassById([FromRoute] long class_id)
    {
        var oneclass = await _classes.GetById(class_id);
        if (oneclass is null)
            return NotFound("No class found with given id");
        var dto = oneclass.asDto;
        dto.Student = await _student.GetList(oneclass.ClassId);
        // dto.Teacher = await _teacher.GetList(studentid);
        return Ok(dto);
    }

}

