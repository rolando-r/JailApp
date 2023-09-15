using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

// Persona, persona, Personas, personas

public class PersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
   /* [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<Persona>>> Get()
    {
        var regiones = await _unitOfWork.Personas.GetAllAsync();
        return Ok(regiones);
    }*/
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<Persona>>> Get()
    {
        var personas = await _unitOfWork.Personas.GetAllAsync();
        return _mapper.Map<List<Persona>>(personas);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<Persona>>> Get11([FromQuery] Params personaParams)
    {
        var persona = await _unitOfWork.Personas.GetAllAsync(personaParams.PageIndex,personaParams.PageSize,personaParams.Search);
        var lstPersonasDto = _mapper.Map<List<Persona>>(persona.registros);
        return new Pager<Persona>(lstPersonasDto,persona.totalRegistros,personaParams.PageIndex,personaParams.PageSize,personaParams.Search);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Persona>> Get(int id)
    {
        var persona = await _unitOfWork.Personas.GetByIdAsync(id);
        if (persona == null){
            return NotFound();
        }
        return _mapper.Map<Persona>(persona);
    }
    /*[HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Persona>> Post(Persona persona){
        this._unitOfWork.Personas.Add(persona);
        await _unitOfWork.SaveAsync();
        if (persona == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post),new {id= persona.Id}, persona);
    }*/
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Persona>> Post(Persona personaDto){
        var persona = _mapper.Map<Persona>(personaDto);
        this._unitOfWork.Personas.Add(persona);
        await _unitOfWork.SaveAsync();
        if (persona == null)
        {
            return BadRequest();
        }
        personaDto.Id = persona.Id;
        return CreatedAtAction(nameof(Post),new {id= personaDto.Id}, personaDto);
    }
    /*[HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Area>> Put(int id, [FromBody]Area persona){
        if(persona == null)
            return NotFound();
        _unitOfWork.Personas.Update(persona);
        await _unitOfWork.SaveAsync();
        return persona;
        
    }*/
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Persona>> Put(int id, [FromBody]Persona personaDto){
        if(personaDto == null)
            return NotFound();
        var personas = _mapper.Map<Persona>(personaDto);
        _unitOfWork.Personas.Update(personas);
        await _unitOfWork.SaveAsync();
        return personaDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var persona = await _unitOfWork.Personas.GetByIdAsync(id);
        if(persona == null){
            return NotFound();
        }
        _unitOfWork.Personas.Remove(persona);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}