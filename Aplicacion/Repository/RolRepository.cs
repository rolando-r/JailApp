

using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository
{
    public class RolRepository:GenericRepository<Rol>,IRol
    {
        private readonly DbAppContext _context;

    public RolRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
    }
}