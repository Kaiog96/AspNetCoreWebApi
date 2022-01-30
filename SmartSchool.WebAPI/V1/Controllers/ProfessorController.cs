using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;
namespace SmartSchool.WebAPI.V1.Controllers
{ /// <summary>
  /// Versão 1 do meu controlador de Professores
  /// </summary>
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class ProfessorController : ControllerBase
  {

    private readonly IRepository _repo;
    private readonly IMapper _mapper;
    public ProfessorController(IRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var Professor = _repo.GetAllProfessor(true);
      return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(Professor));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var Professor = _repo.GetProfessorById(id, true);
      if (Professor == null) return BadRequest("O Professor não foi encontrado");

      var professorDto = _mapper.Map<ProfessorDto>(Professor);

      return Ok(Professor);
    }

    [HttpPost]
    public IActionResult Post(ProfessorRegistrarDto model)
    {
      var prof = _mapper.Map<Professor>(model);

      _repo.Add(prof);
      if (_repo.SaveChanges())
      {
        return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
      }

      return BadRequest("Professor não cadastrado");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, ProfessorRegistrarDto model)
    {
      var prof = _repo.GetProfessorById(id, false);
      if (prof == null) return BadRequest("Professor não encontrado");

      _mapper.Map(model, prof);

      _repo.Update(prof);
      if (_repo.SaveChanges())
      {
        return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
      }

      return BadRequest("Professor não Atualizado");
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, ProfessorRegistrarDto model)
    {
      var prof = _repo.GetProfessorById(id, false);
      if (prof == null) return BadRequest("Professor não encontrado");

      _mapper.Map(model, prof);

      _repo.Update(prof);
      if (_repo.SaveChanges())
      {
        return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
      }

      return BadRequest("Professor não Atualizado");
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