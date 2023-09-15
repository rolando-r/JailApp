using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class PaisRepository : GenericRepository<Pais>, IPaisRepository
{
    private readonly DbAppContext _context;
    public PaisRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Pais> GetByIdAsync(int id)
        {
            return await _context.Paises
            .Include(p => p.Ciudades)
            .FirstOrDefaultAsync(x => x.Id == id);
        } 

    public override async Task<(int totalRegistros, IEnumerable<Pais> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Paises as IQueryable<Pais>;
        if(!string.IsNullOrEmpty(search)) query=query.Where(p=>p.NombrePais.ToLower().Contains(search));
        var totalRegistros=await query.CountAsync();
        var registros = await query
            .Include(p=>p.Ciudades)
            .Skip((pageIndex-1)*pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros,registros);
    }
}