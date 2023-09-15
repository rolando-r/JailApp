using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class CrimenRepository : GenericRepository<Crimen>, ICrimenRepository
{
    private readonly DbAppContext _context;
    public CrimenRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}