

namespace Dominio.Interfaces;

    public interface IUnitOfWork
    {
         IUsuarioRepository Usuarios {get;}
         IRol Roles {get;}
         ICiudadRepository Ciudades {get;}
         IPaisRepository Paises {get;}
         ICrimenRepository Crimenes {get;}
         IPersonaRepository Personas {get;}
         ISedeRepository Sedes {get;}
        Task<int> SaveAsync();
    }

