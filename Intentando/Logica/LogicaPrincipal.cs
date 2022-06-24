using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;


namespace Logica
{
    public class LogicaPrincipal
    {
        // SINGLETON

        private static LogicaPrincipal instance = null;

        private LogicaPrincipal()
        {

        }

        public static LogicaPrincipal Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogicaPrincipal();
                }
                return instance;
            }
        }

        //

        public Animal ObtenerAnimalId(int id)
        {
            var resultado = Lista.listaAnimales.Where(x => x.Id == id).FirstOrDefault();
            return resultado;
        }

        public List<Animal> ObtenerLista()
        {
            return Lista.listaAnimales;
        }

        public Animal CargarAnimal(Animal animal)
        {
            animal.Id = Lista.listaAnimales.Count + 1;
            animal.FechaCreacion = DateTime.Now;
            animal.FechaModificacion = DateTime.Now;
            Lista.listaAnimales.Add(animal);

            return animal;
        }
    }
}
