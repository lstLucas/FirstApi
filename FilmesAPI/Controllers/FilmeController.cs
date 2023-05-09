﻿using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto) 
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RetornaFilmePorId), new { id = filme.Id}, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RetornaFilmes([FromQuery]int skip=0, [FromQuery]int take=100) 
    {
        return _context.Filmes.Skip(skip).Take(take);
    }


    [HttpGet("{id}")]
    public IActionResult RetornaFilmePorId(int id) 
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();
        return Ok(filme);
        
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    { 
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaParteFilme(int id, JsonPatchDocument<UpdateFilmeDto> patch) 
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        var filmeToUpdate = _mapper.Map<UpdateFilmeDto>(filme);
        patch.ApplyTo(filmeToUpdate, ModelState);
        if (!TryValidateModel(filmeToUpdate))
            return ValidationProblem(ModelState);

        _mapper.Map(filmeToUpdate, filme);
        _context.SaveChanges();

        return NoContent();
    }
}