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

// Sede, sede, Sedes, sedes

public class SedeController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SedeController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
   /* [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<Sede>>> Get()
    {
        var regiones = await _unitOfWork.Sedes.GetAllAsync();
        return Ok(regiones);
    }*/
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<SedeDto>>> Get()
    {
        var sedes = await _unitOfWork.Sedes.GetAllAsync();
        return _mapper.Map<List<SedeDto>>(sedes);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<SedeDto>>> Get11([FromQuery] Params sedeParams)
    {
        var sede = await _unitOfWork.Sedes.GetAllAsync(sedeParams.PageIndex,sedeParams.PageSize,sedeParams.Search);
        var lstSedesDto = _mapper.Map<List<SedeDto>>(sede.registros);
        return new Pager<SedeDto>(lstSedesDto,sede.totalRegistros,sedeParams.PageIndex,sedeParams.PageSize,sedeParams.Search);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SedeDto>> Get(int id)
    {
        var sede = await _unitOfWork.Sedes.GetByIdAsync(id);
        if (sede == null){
            return NotFound();
        }
        return _mapper.Map<SedeDto>(sede);
    }
    /*[HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Sede>> Post(Sede sede){
        this._unitOfWork.Sedes.Add(sede);
        await _unitOfWork.SaveAsync();
        if (sede == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post),new {id= sede.Id}, sede);
    }*/
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Sede>> Post(SedeDto sedeDto){
        var sede = _mapper.Map<Sede>(sedeDto);
        this._unitOfWork.Sedes.Add(sede);
        await _unitOfWork.SaveAsync();
        if (sede == null)
        {
            return BadRequest();
        }
        sedeDto.Id = sede.Id;
        return CreatedAtAction(nameof(Post),new {id= sedeDto.Id}, sedeDto);
    }
    /*[HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Area>> Put(int id, [FromBody]Area sede){
        if(sede == null)
            return NotFound();
        _unitOfWork.Sedes.Update(sede);
        await _unitOfWork.SaveAsync();
        return sede;
        
    }*/
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SedeDto>> Put(int id, [FromBody]SedeDto sedeDto){
        if(sedeDto == null)
            return NotFound();
        var sedes = _mapper.Map<Sede>(sedeDto);
        _unitOfWork.Sedes.Update(sedes);
        await _unitOfWork.SaveAsync();
        return sedeDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var sede = await _unitOfWork.Sedes.GetByIdAsync(id);
        if(sede == null){
            return NotFound();
        }
        _unitOfWork.Sedes.Remove(sede);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}