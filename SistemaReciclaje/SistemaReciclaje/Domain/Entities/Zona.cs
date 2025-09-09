using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Entities
{
    public class Zona
    {
        public string Id { get; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }

        public Zona(string id, string nombre, string descripcion = "")
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El ID es requerido", nameof(id));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es requerido", nameof(nombre));

            Id = id;
            Nombre = nombre;
            Descripcion = descripcion ?? "";
        }

        public void ActualizarInfo(string nombre, string descripcion)
        {
            if (!string.IsNullOrWhiteSpace(nombre))
                Nombre = nombre;
            if (descripcion != null)
                Descripcion = descripcion;
        }
    }
}
