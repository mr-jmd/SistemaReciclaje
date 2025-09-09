using SistemaReciclaje.Domain.Entities;
using SistemaReciclaje.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Infrastructure.Repositories
{
    public class RepositorioEnMemoria : IRepositorioCiudadanos, IRepositorioZonas, IRepositorioDepositos
    {
        private readonly Dictionary<string, Ciudadano> _ciudadanos = new();
        private readonly Dictionary<string, Zona> _zonas = new();
        private readonly List<(string CedulaCiudadano, Deposito Deposito)> _depositos = new();

        // IRepositorioCiudadanos
        public void Agregar(Ciudadano ciudadano)
        {
            _ciudadanos[ciudadano.Cedula] = ciudadano;
        }

        public Ciudadano? ObtenerPorCedula(string cedula)
        {
            return _ciudadanos.TryGetValue(cedula, out var ciudadano) ? ciudadano : null;
        }

        public IEnumerable<Ciudadano> Listar()
        {
            return _ciudadanos.Values;
        }

        public IEnumerable<Ciudadano> ListarPorZona(string zonaId)
        {
            return _ciudadanos.Values.Where(c => c.Zona.Id == zonaId);
        }

        public void Actualizar(Ciudadano ciudadano)
        {
            _ciudadanos[ciudadano.Cedula] = ciudadano;
        }

        // IRepositorioZonas
        public void Agregar(Zona zona)
        {
            _zonas[zona.Id] = zona;
        }

        public Zona? ObtenerPorId(string id)
        {
            return _zonas.TryGetValue(id, out var zona) ? zona : null;
        }

        IEnumerable<Zona> IRepositorioZonas.Listar()
        {
            return _zonas.Values;
        }

        public void Actualizar(Zona zona)
        {
            _zonas[zona.Id] = zona;
        }

        // IRepositorioDepositos
        public void Agregar(Deposito deposito)
        {
            // Para simplificar, buscaremos la cédula del ciudadano asociado
            var ciudadano = _ciudadanos.Values.FirstOrDefault(c => c.Depositos.Contains(deposito));
            if (ciudadano != null)
            {
                _depositos.Add((ciudadano.Cedula, deposito));
            }
        }

        public IEnumerable<Deposito> ListarPorCiudadano(string cedula)
        {
            return _depositos
                .Where(d => d.CedulaCiudadano == cedula)
                .Select(d => d.Deposito);
        }

        public IEnumerable<Deposito> ListarPorZona(string zonaId, DateTime? desde = null, DateTime? hasta = null)
        {
            var ciudadanosZona = _ciudadanos.Values.Where(c => c.Zona.Id == zonaId);
            var cedulasZona = ciudadanosZona.Select(c => c.Cedula).ToHashSet();

            var depositos = _depositos
                .Where(d => cedulasZona.Contains(d.CedulaCiudadano))
                .Select(d => d.Deposito);

            if (desde.HasValue)
                depositos = depositos.Where(d => d.FechaHora >= desde.Value);

            if (hasta.HasValue)
                depositos = depositos.Where(d => d.FechaHora <= hasta.Value);

            return depositos;
        }

        public IEnumerable<Deposito> ListarPorPeriodo(DateTime desde, DateTime hasta)
        {
            return _depositos
                .Where(d => d.Deposito.FechaHora >= desde && d.Deposito.FechaHora <= hasta)
                .Select(d => d.Deposito);
        }
    }
}
