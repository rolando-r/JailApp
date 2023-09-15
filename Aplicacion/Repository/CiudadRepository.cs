using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class CiudadRepository : GenericRepository<Ciudad>, ICiudadRepository
{
    private readonly DbAppContext _context;
    public CiudadRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Ciudad> GetByIdAsync(int id)
    {
        return await _context.Ciudades
        .Include(p => p.Sedes)
        .FirstOrDefaultAsync(x => x.Id == id);
    }

    public override async Task <(int totalRegistros, IEnumerable<Ciudad> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Ciudades as IQueryable<Ciudad>;
    if(!string.IsNullOrEmpty(search)) query=query.Where(p=>p.NombreCiudad.ToLower().Contains(search));
    var totalRegistros=await query.CountAsync();
    var registros = await query
        .Include(p=>p.Sedes)
        .Skip((pageIndex-1)*pageSize)
        .Take(pageSize)
        .ToListAsync();
    return (totalRegistros,registros);
    }
}