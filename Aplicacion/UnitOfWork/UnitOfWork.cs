

using Aplicacion.Repository;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    CrimenRepository _crimen;
    PaisRepository _pais;
    PersonaRepository _persona;
    SedeRepository _sede;
    CiudadRepository _ciudad;
    RolRepository _rol;
    UsuarioRepository _usuario;
    private readonly DbAppContext _context;
    public UnitOfWork(DbAppContext context)
    {
        _context = context;
    }
    public IUsuarioRepository Usuarios
    {
        get
        {
            if (_usuario is not null)
            {
                return _usuario;
            }
            return _usuario = new UsuarioRepository(_context);
        }
    }
    public IRol Roles
    {
        get
        {
            if (_rol is not null)
            {
                return _rol;
            }
            return _rol = new RolRepository(_context);
        }
    }
    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public ICiudadRepository Ciudades
    {
        get
        {
            if (_ciudad is not null)
            {
                return _ciudad;
            }
            return _ciudad = new CiudadRepository(_context);
        }
    }
    public ICrimenRepository Crimenes
    {
        get
        {
            if (_crimen is not null)
            {
                return _crimen;
            }
            return _crimen = new CrimenRepository(_context);
        }
    }
    public IPaisRepository Paises
    {
        get
        {
            if (_pais is not null)
            {
                return _pais;
            }
            return _pais = new PaisRepository(_context);
        }
    }
    public IPersonaRepository Personas
    {
        get
        {
            if (_persona is not null)
            {
                return _persona;
            }
            return _persona = new PersonaRepository(_context);
        }
    }
    public ISedeRepository Sedes
    {
        get
        {
            if (_sede is not null)
            {
                return _sede;
            }
            return _sede = new SedeRepository(_context);
        }
    }
}