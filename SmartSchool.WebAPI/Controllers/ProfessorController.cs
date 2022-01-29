using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;
namespace SmartSchool.WebAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfessorController : ControllerBase
  {

    private readonly IRepository _repo;

    public ProfessorController(IRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var result = _repo.GetAllProfessor(true);
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {

      var aluno = _repo.GetProfessorById(id, false);

      if (aluno == null) return BadRequest("O aluno não foi encontrado");

      return Ok(aluno);
    }

    [HttpPost]
    public IActionResult Post(Professor professor)
    {
      _repo.Add(professor);
      if (_repo.SaveChanges())
      {
        return Ok(professor);
      }

      return BadRequest("Professor não cadastrado");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Professor professor)
    {
      var professor1 = _repo.GetProfessorById(id, false);
      if (professor1 == null) return BadRequest("Professor não encontrado");
      _repo.Update(professor1);
      if (_repo.SaveChanges())
      {
        return Ok(professor1);
      }

      return BadRequest("Professor não atualizado");
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, Professor professor)
    {
      var professor1 = _repo.GetProfessorById(id, false);
      if (professor1 == null) return BadRequest("Professor não encontrado");
      _repo.Update(professor1);
      if (_repo.SaveChanges())
      {
        return Ok(professor1);
      }

      return BadRequest("Professor não atualizado");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var aluno = _repo.GetProfessorById(id, false);
      if (aluno == null) return BadRequest("Professor não encontrado");
      _repo.Delete(aluno);
      if (_repo.SaveChanges())
      {
        return Ok("Professor deletado");
      }

      return BadRequest("Professor não deletado");
    }

  }
}