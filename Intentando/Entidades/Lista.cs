using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Lista
    {
        public static List<Animal> listaAnimales = new List<Animal>()
        {
            new Animal(){Id = 0, Nombre = "teta", Especie = "mono", FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Eliminado = false },
            new Animal(){Id = 1, Nombre = "coco", Especie = "duarte", FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Eliminado = false }
        };
    }
}
