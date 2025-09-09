using SistemaReciclaje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Interfaces
{
    public interface IRepositorioDepositos
    {
        void Agregar(Deposito deposito);
        IEnumerable<Deposito> ListarPorCiudadano(string cedula);
        IEnumerable<Deposito> ListarPorZona(string zonaId, DateTime? desde = null, DateTime? hasta = null);
        IEnumerable<Deposito> ListarPorPeriodo(DateTime desde, DateTime hasta);
    }
}
