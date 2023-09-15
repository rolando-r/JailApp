using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class PersonaRepository : GenericRepository<Persona>, IPersonaRepository
{
    private readonly DbAppContext _context;
    public PersonaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Persona> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Personas as IQueryable<Persona>;
        
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombrePersona.ToLower().Contains(search));
        }
        
        var totalRegistros = await query.CountAsync();

        var registros = await query
            .OrderBy(p => p.NombrePersona) // Ordenar por NombrePersona en orden alfabético
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}