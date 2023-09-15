

namespace Dominio.Entities
{
    public class UsuarioRoles
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}