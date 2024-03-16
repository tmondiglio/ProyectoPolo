using Microsoft.AspNetCore.Mvc;
using Entidades.Clientes;
using Datos.Contextos;
using System.Threading.Tasks;
using.Microsoft.EntityFrameworkCore;

namespace web.Controllers
{
    public class Direcciones : Controller
    {
        private readonly SecurityContext _context;

        public Direcciones(SecurityContext context)
        {
            _context = context;
        }

        // GET: Direcciones
        public async Task<IActionResult> Index()
        {
            //var userID = ObtenerIdUsuarioActual();
            //var direcciones = await _context.DireccionesClientes
                //.Where(d => d.UsuarioId == userId)
               // .ToListAsync();
            //return View(direcciones);
        }

        // GET: Direcciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionCliente = await _context.DireccionesClientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccionCliente == null)
            {
                return NotFound();
            }

            return View(direccionCliente);
        }

        // Otras acciones como Create, Edit, Delete, etc.
    }
}