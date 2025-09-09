using SistemaReciclaje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Interfaces
{
    public interface IRepositorioZonas
    {
        void Agregar(Zona zona);
        Zona? ObtenerPorId(string id);
        IEnumerable<Zona> Listar();
        void Actualizar(Zona zona);
    }
}
