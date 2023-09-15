

using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario> GetByUsernameAsync(string username);
    }
}