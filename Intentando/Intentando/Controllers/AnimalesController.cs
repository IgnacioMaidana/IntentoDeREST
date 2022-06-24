using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//Quitar referencia a entidades??
using Logica;
using Entidades;

namespace Intentando.Controllers
{
    public class AnimalServicio
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bue pone nombre capo")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Que especie es capo")]
        public string Especie { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Eliminado { get; set; }
    }

    public static class Conversores
    {
        public static AnimalServicio ConvertirServicio(this Animal animal)
        {
            AnimalServicio animalServicio = new AnimalServicio()
            {
                Id = animal.Id,
                Nombre = animal.Nombre,
                Especie = animal.Especie,
                FechaCreacion = animal.FechaCreacion,
                FechaModificacion = animal.FechaModificacion,
                Eliminado = animal.Eliminado
                
            };
            return animalServicio;
        }

        public static Animal ConvertirLogica(this AnimalServicio animalServicio)
        {
            Animal animal = new Animal()
            {
                Id = animalServicio.Id,
                Nombre = animalServicio.Nombre,
                Especie = animalServicio.Especie,
                FechaCreacion = animalServicio.FechaCreacion,
                FechaModificacion = animalServicio.FechaModificacion,
                Eliminado = animalServicio.Eliminado
            };
            return animal;
        }

    }
    [Route("Animales")]
    public class AnimalesController : ApiController
    {
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            Animal animal = LogicaPrincipal.Instancia.ObtenerAnimalId(id);
            if (animal == null)
                return NotFound();

            return Ok(animal.ConvertirServicio());
        }

        [Route("Lista")]
        public IHttpActionResult Get()
        {
            List<AnimalServicio> listaReal = new List<AnimalServicio>();
            List<Animal> lista = LogicaPrincipal.Instancia.ObtenerLista();
            foreach (Animal item in lista)
            {
                listaReal.Add(item.ConvertirServicio());
            }

            return Ok(listaReal);
        }

        [Route("Nuevo")]
        public IHttpActionResult Post([FromBody] AnimalServicio animalServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(LogicaPrincipal.Instancia.CargarAnimal(animalServicio.ConvertirLogica()).ConvertirServicio());
        }

        public IHttpActionResult Put(int id, string nombre)
        {
            Animal animalEditao = LogicaPrincipal.Instancia.ObtenerLista().Where(x => x.Id == id).FirstOrDefault();
            animalEditao.Nombre = nombre;

            return Ok(animalEditao.ConvertirServicio());
        }











        // -----------------------------------------------------------------------------------------------------------------------------------------------------
        //public static List<Animal> listaAnimales = new List<Animal>()
        //{
        //    new Animal(){Id= 0, Nombre = "coco", Especie = "duarte", FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Eliminado = false },
        //    new Animal(){Id= 1, Nombre = "teta", Especie = "mono", FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Eliminado = false }
        //};

        //[Route("Lista")]
        //public IHttpActionResult Get()
        //{
        //    List<Animal> listaFiltrada = new List<Animal>();
        //    listaFiltrada = listaAnimales.Where(x => x.Eliminado == false).ToList();
        //    return Ok(listaFiltrada);
        //}

        //[Route("Buscar/{nombre}")]
        //public IHttpActionResult Get(string nombre, string especie)
        //{
        //    List<Animal> buscado = new List<Animal>();
        //    buscado = listaAnimales.Where(x => x.Nombre.Contains(nombre)).ToList();
        //    return Ok(buscado);
        //}

        //[Route("Buscar/{id}")]
        //public IHttpActionResult Get(int id)
        //{
        //    Animal buscado = new Animal();
        //    buscado = listaAnimales.Where(x => x.Id == id).FirstOrDefault();
        //    return Ok(buscado);
        //}

        //[Route("Nuevo")]
        //public IHttpActionResult Post([FromBody] Animal animal)
        //{
        //    Animal animalNuevo = new Animal();
        //    if (ModelState.IsValid)
        //    {
        //        try //USAR MODELSTATE.ISVALID
        //        {
        //            animalNuevo.Id = listaAnimales.Count + 1;
        //            animalNuevo.Nombre = animal.Nombre;
        //            animalNuevo.Especie = animal.Especie;
        //            animalNuevo.FechaCreacion = DateTime.Now;
        //            animalNuevo.FechaModificacion = DateTime.Now;
        //            animal.Eliminado = false;
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //        return BadRequest("Campos enviados en formato erróneo");

        //    listaAnimales.Add(animalNuevo);
        //    return Ok(listaAnimales);
        //}

        //[Route("Eliminar/{id}")]
        //public IHttpActionResult Delete(int id)
        //{
        //    Animal animal = listaAnimales.Where(x => x.Id == id).FirstOrDefault();
        //    if (animal == null)
        //        return BadRequest("La ID ingresada no existe.");
        //    animal.Eliminado = true;
        //    return Ok(animal);
        //}
    }
}
