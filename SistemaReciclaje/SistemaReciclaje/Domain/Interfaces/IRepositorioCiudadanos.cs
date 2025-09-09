using SistemaReciclaje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Interfaces
{
    public interface IRepositorioCiudadanos
    {
        void Agregar(Ciudadano ciudadano);
        Ciudadano? ObtenerPorCedula(string cedula);
        IEnumerable<Ciudadano> Listar();
        IEnumerable<Ciudadano> ListarPorZona(string zonaId);
        void Actualizar(Ciudadano ciudadano);
    }
}
