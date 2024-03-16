using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Clientes;

namespace Entidades.Seguridad;

public class Usuario
{
  public Usuario()
  {
    Perfiles = new HashSet<Perfil>();
  }

  public Guid Clave { get; set; }

  public string Login { get; set; }

  public string Nombre { get; set; }

  public TipoUsuario TipoUsuario { get; set; }

  public bool Habilitado { get; set; }

  public string Correo { get; set; }

  public DateTime? LastLogin { get; set; }

  public DateTime FechaAlta { get; set; }

  public DateTime Nacimiento { get; set; }

  public ISet<Perfil> Perfiles { get; set; }

  public ISet<DireccionCliente> Direcciones { get; set; } // Agregar la propiedad Direcciones
}
