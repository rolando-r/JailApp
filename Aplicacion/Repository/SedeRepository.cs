using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class SedeRepository : GenericRepository<Sede>, ISedeRepository
{
    private readonly DbAppContext _context;
    public SedeRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}