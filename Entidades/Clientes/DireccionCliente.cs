using Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Clientes
{
    public class DireccionCliente
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        // Otros campos de dirección...
        public int UsuarioId { get; set; } // Clave foránea para la relación con Usuario

        public Usuario Usuario { get; set; } // Propiedad de navegación hacia el usuario asociado
    }
}

