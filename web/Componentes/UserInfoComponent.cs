using Microsoft.AspNetCore.Mvc;

namespace web.Componentes;

public class UserInfoComponent :ViewComponent
{
  public IViewComponentResult Invoke()
  {
    //  si el user no esta logueado retornar un fragmento generico
    return View("UserGenerico");
  }
}
