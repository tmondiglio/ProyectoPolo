using Entidades.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using Utiles;
using web.Models;

namespace web.Controllers;

public class LoginController : Controller
{
  private readonly ISecurityServices _seguridad;
  private readonly ILogger<LoginController> _logger;

  public LoginController(ISecurityServices seguridad, ILogger<LoginController> logger)
  {
    _seguridad = seguridad;
    _logger = logger;
  }

  [HttpGet]
  public IActionResult Inicio()
  {
    return View();
  }

  [HttpPost]
  public IActionResult Inicio(string login, string hashedPass)
  {
    try
    {
      Usuario userConectado = _seguridad.Login(login, hashedPass);

      //  solo es critico para que se distinga en pantalla
      //
      if (userConectado != null)
        _logger.LogCritical("El usuario {user} se conecto correctamente", userConectado.Clave);

      return RedirectToAction("Index", "Home");
    }
    catch (Exception ex)
    {
      //  puede ser usuario y/o pass
      //
      _logger.LogCritical(ex,
        "Cuidado muchas repeticiones de esta excepcion puede significar que nos quieren atacar");

      FullErrorViewModel vm = new()
      {
        Titulo = "Parece que la fuerza no te acompaña...",
        Mensaje = "Las credenciales no son validas",
        Detalle = ex.Resumen(),
        Source = "Login Usuario",
        Comunicacion = "Que pasa que no puedo entrar??",
        TraceIdentifier = HttpContext.TraceIdentifier
      };
      return View("Errores/ErrorMejorado", vm);
    }
  }
}
