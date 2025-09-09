using SistemaReciclaje.Domain.Entities;
using SistemaReciclaje.Domain.Entities.Residuos;
using SistemaReciclaje.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Application.Services
{
    public class ServicioReciclaje
    {
        private readonly IRepositorioCiudadanos _repoCiudadanos;
        private readonly IRepositorioZonas _repoZonas;
        private readonly IRepositorioDepositos _repoDepositos;
        private IEstrategiaCalculoPuntos _estrategiaPuntos;

        public ServicioReciclaje(
            IRepositorioCiudadanos repoCiudadanos,
            IRepositorioZonas repoZonas,
            IRepositorioDepositos repoDepositos,
            IEstrategiaCalculoPuntos estrategiaPuntos)
        {
            _repoCiudadanos = repoCiudadanos;
            _repoZonas = repoZonas;
            _repoDepositos = repoDepositos;
            _estrategiaPuntos = estrategiaPuntos;
        }

        public void CambiarEstrategiaPuntos(IEstrategiaCalculoPuntos nuevaEstrategia)
        {
            _estrategiaPuntos = nuevaEstrategia ?? throw new ArgumentNullException(nameof(nuevaEstrategia));
        }

        public void RegistrarCiudadano(string cedula, string nombre, string email, string zonaId)
        {
            var zona = _repoZonas.ObtenerPorId(zonaId);
            if (zona == null)
                throw new InvalidOperationException($"No existe la zona con ID: {zonaId}");

            var ciudadanoExistente = _repoCiudadanos.ObtenerPorCedula(cedula);
            if (ciudadanoExistente != null)
                throw new InvalidOperationException($"Ya existe un ciudadano con cédula: {cedula}");

            var ciudadano = new Ciudadano(cedula, nombre, email, zona);
            _repoCiudadanos.Agregar(ciudadano);
        }

        public void RegistrarDeposito(string cedula, string tipoResiduo, decimal peso)
        {
            var ciudadano = _repoCiudadanos.ObtenerPorCedula(cedula);
            if (ciudadano == null)
                throw new InvalidOperationException($"No existe ciudadano con cédula: {cedula}");

            var residuo = CrearResiduoPorTipo(tipoResiduo, peso);
            ciudadano.RealizarDeposito(residuo, _estrategiaPuntos);

            // Registrar en repositorio de depósitos para reportes
            var ultimoDeposito = ciudadano.Depositos.Last();
            _repoDepositos.Agregar(ultimoDeposito);
        }

        private Residuo CrearResiduoPorTipo(string tipo, decimal peso)
        {
            return tipo.ToUpperInvariant() switch
            {
                "PLASTICO" or "P" => new ResiduoPlastico(peso),
                "PAPEL" or "PA" => new ResiduoPapel(peso),
                "VIDRIO" or "V" => new ResiduoVidrio(peso),
                "METAL" or "M" => new ResiduoMetal(peso),
                "ORGANICO" or "O" => new ResiduoOrganico(peso),
                _ => throw new ArgumentException($"Tipo de residuo no válido: {tipo}")
            };
        }

        public Ciudadano? ConsultarCiudadano(string cedula)
        {
            return _repoCiudadanos.ObtenerPorCedula(cedula);
        }

        public void RegistrarZona(string id, string nombre, string descripcion = "")
        {
            var zonaExistente = _repoZonas.ObtenerPorId(id);
            if (zonaExistente != null)
                throw new InvalidOperationException($"Ya existe una zona con ID: {id}");

            var zona = new Zona(id, nombre, descripcion);
            _repoZonas.Agregar(zona);
        }

        public IEnumerable<Ciudadano> ListarCiudadanos()
        {
            return _repoCiudadanos.Listar().OrderByDescending(c => c.PuntosTotales);
        }

        public IEnumerable<Zona> ListarZonas()
        {
            return _repoZonas.Listar();
        }
    }
}
