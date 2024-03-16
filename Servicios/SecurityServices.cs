using Entidades.Seguridad;
using Entidades;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Repositorios;
using Microsoft.Extensions.Logging;
using Utiles;

namespace Servicios;
public class SecurityServices : ISecurityServices
{
  private readonly ISecurityRepo _repo;
  private readonly ILogger<SecurityServices> _logger;

  public SecurityServices(ISecurityRepo repo, ILogger<SecurityServices> logger)
  {
    _repo = repo;
    _logger = logger;
  }

  public IEnumerable<Perfil> GetPerfiles()
  {
    return _repo.GetPerfiles();
  }

  public Usuario CrearEmpleado(string nombre, string mail, string login, string pwd, DateTime nacimiento,
    int[] perfiles)
  {
    Usuario nuevo = new Usuario
    {
      Nombre = nombre,
      Login = login,
      Correo = mail,
      Nacimiento = nacimiento,
      TipoUsuario = TipoUsuario.Empleado,
      FechaAlta = DateTime.Now
    };

    foreach (var perfil in perfiles)
      nuevo.Perfiles.Add(GetPerfiles().Single(p => p.ID == perfil));

    return _repo.CrearUsuario(nuevo, pwd);
  }

  public Usuario CrearCliente(string nombre, string mail, string login, string pwd, DateTime nacimiento)
  {
    Usuario nuevo = new Usuario
    {
      Nombre = nombre,
      Login = login,
      Correo = mail,
      Nacimiento = nacimiento,
      TipoUsuario = TipoUsuario.Cliente,
      FechaAlta = DateTime.Now
    };

    nuevo.Perfiles.Add(GetPerfiles().Single(p => p.Nombre == "Visitante"));

    return _repo.CrearUsuario(nuevo, pwd);
  }

  public bool SetearPassword(Guid id, string newPass)
  {
    //  que chequeos podriamos hacer?
    return true;
  }

  public Usuario Login(string login, string pass)
  {
    var user = _repo.GetUsuarioFromLogin(login);

    if (user != null)
    {
      if (!_repo.ValidarPassword(user.Clave, pass))
        throw new ApplicationException("Pass invalida...POSIBLE FRAUDE??? INCREMENTAR REINTENTOS");

      return user;
    }

    _logger.LogCritical("El usuario {login} no existe. Chequear fraudes", login);
    throw new ApplicationException($"El usuario {login} no existe. Chequear fraudes");
  }
}
