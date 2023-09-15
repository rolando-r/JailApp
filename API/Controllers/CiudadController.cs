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

// Ciudad, ciudad, Ciudades, ciudades

public class CiudadController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CiudadController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
   /* [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<Ciudad>>> Get()
    {
        var regiones = await _unitOfWork.Ciudades.GetAllAsync();
        return Ok(regiones);
    }*/
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<CiudadDto>>> Get()
    {
        var ciudades = await _unitOfWork.Ciudades.GetAllAsync();
        return _mapper.Map<List<CiudadDto>>(ciudades);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CiudadDto>>> Get11([FromQuery] Params ciudadParams)
    {
        var ciudad = await _unitOfWork.Ciudades.GetAllAsync(ciudadParams.PageIndex,ciudadParams.PageSize,ciudadParams.Search);
        var lstCiudadesDto = _mapper.Map<List<CiudadDto>>(ciudad.registros);
        return new Pager<CiudadDto>(lstCiudadesDto,ciudad.totalRegistros,ciudadParams.PageIndex,ciudadParams.PageSize,ciudadParams.Search);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CiudadDto>> Get(int id)
    {
        var ciudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
        if (ciudad == null){
            return NotFound();
        }
        return _mapper.Map<CiudadDto>(ciudad);
    }
    /*[HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Ciudad>> Post(Ciudad ciudad){
        this._unitOfWork.Ciudades.Add(ciudad);
        await _unitOfWork.SaveAsync();
        if (ciudad == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post),new {id= ciudad.Id}, ciudad);
    }*/
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Ciudad>> Post(CiudadDto ciudadDto){
        var ciudad = _mapper.Map<Ciudad>(ciudadDto);
        this._unitOfWork.Ciudades.Add(ciudad);
        await _unitOfWork.SaveAsync();
        if (ciudad == null)
        {
            return BadRequest();
        }
        ciudadDto.Id = ciudad.Id;
        return CreatedAtAction(nameof(Post),new {id= ciudadDto.Id}, ciudadDto);
    }
    /*[HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Area>> Put(int id, [FromBody]Area ciudad){
        if(ciudad == null)
            return NotFound();
        _unitOfWork.Ciudades.Update(ciudad);
        await _unitOfWork.SaveAsync();
        return ciudad;
        
    }*/
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CiudadDto>> Put(int id, [FromBody]CiudadDto ciudadDto){
        if(ciudadDto == null)
            return NotFound();
        var ciudades = _mapper.Map<Ciudad>(ciudadDto);
        _unitOfWork.Ciudades.Update(ciudades);
        await _unitOfWork.SaveAsync();
        return ciudadDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var ciudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
        if(ciudad == null){
            return NotFound();
        }
        _unitOfWork.Ciudades.Remove(ciudad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}