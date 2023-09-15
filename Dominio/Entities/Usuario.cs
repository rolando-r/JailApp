

namespace Dominio.Entities
{
    public class Usuario:BaseEntity
    {
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Rol> Roles { get; set; } = new HashSet<Rol>();
    public ICollection<UsuarioRoles> UsuariosRoles { get; set; }
    }
}