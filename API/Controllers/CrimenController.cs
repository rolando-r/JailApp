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

// Crimen, crimen, Crimenes, crimenes

public class CrimenController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CrimenController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
   /* [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<Crimen>>> Get()
    {
        var regiones = await _unitOfWork.Crimenes.GetAllAsync();
        return Ok(regiones);
    }*/
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<CrimenDto>>> Get()
    {
        var crimenes = await _unitOfWork.Crimenes.GetAllAsync();
        return _mapper.Map<List<CrimenDto>>(crimenes);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CrimenDto>>> Get11([FromQuery] Params crimenParams)
    {
        var crimen = await _unitOfWork.Crimenes.GetAllAsync(crimenParams.PageIndex,crimenParams.PageSize,crimenParams.Search);
        var lstCrimenesDto = _mapper.Map<List<CrimenDto>>(crimen.registros);
        return new Pager<CrimenDto>(lstCrimenesDto,crimen.totalRegistros,crimenParams.PageIndex,crimenParams.PageSize,crimenParams.Search);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CrimenDto>> Get(int id)
    {
        var crimen = await _unitOfWork.Crimenes.GetByIdAsync(id);
        if (crimen == null){
            return NotFound();
        }
        return _mapper.Map<CrimenDto>(crimen);
    }
    /*[HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Crimen>> Post(Crimen crimen){
        this._unitOfWork.Crimenes.Add(crimen);
        await _unitOfWork.SaveAsync();
        if (crimen == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post),new {id= crimen.Id}, crimen);
    }*/
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Crimen>> Post(CrimenDto crimenDto){
        var crimen = _mapper.Map<Crimen>(crimenDto);
        this._unitOfWork.Crimenes.Add(crimen);
        await _unitOfWork.SaveAsync();
        if (crimen == null)
        {
            return BadRequest();
        }
        crimenDto.Id = crimen.Id;
        return CreatedAtAction(nameof(Post),new {id= crimenDto.Id}, crimenDto);
    }
    /*[HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Area>> Put(int id, [FromBody]Area crimen){
        if(crimen == null)
            return NotFound();
        _unitOfWork.Crimenes.Update(crimen);
        await _unitOfWork.SaveAsync();
        return crimen;
        
    }*/
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CrimenDto>> Put(int id, [FromBody]CrimenDto crimenDto){
        if(crimenDto == null)
            return NotFound();
        var crimenes = _mapper.Map<Crimen>(crimenDto);
        _unitOfWork.Crimenes.Update(crimenes);
        await _unitOfWork.SaveAsync();
        return crimenDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var crimen = await _unitOfWork.Crimenes.GetByIdAsync(id);
        if(crimen == null){
            return NotFound();
        }
        _unitOfWork.Crimenes.Remove(crimen);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}