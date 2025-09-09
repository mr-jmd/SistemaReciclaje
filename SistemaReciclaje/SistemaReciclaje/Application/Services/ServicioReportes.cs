using SistemaReciclaje.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Application.Services
{
    public class ReporteZona
    {
        public string ZonaId { get; set; }
        public string NombreZona { get; set; }
        public int TotalCiudadanos { get; set; }
        public int TotalDepositos { get; set; }
        public decimal PesoTotalKg { get; set; }
        public int PuntosTotales { get; set; }
        public Dictionary<string, decimal> ResiduosPorTipo { get; set; } = new();
        public List<string> CiudadanosMasActivos { get; set; } = new();
    }

    public class ServicioReportes
    {
        private readonly IRepositorioCiudadanos _repoCiudadanos;
        private readonly IRepositorioZonas _repoZonas;
        private readonly IRepositorioDepositos _repoDepositos;

        public ServicioReportes(
            IRepositorioCiudadanos repoCiudadanos,
            IRepositorioZonas repoZonas,
            IRepositorioDepositos repoDepositos)
        {
            _repoCiudadanos = repoCiudadanos;
            _repoZonas = repoZonas;
            _repoDepositos = repoDepositos;
        }

        public ReporteZona GenerarReportePorZona(string zonaId, DateTime? desde = null, DateTime? hasta = null)
        {
            var zona = _repoZonas.ObtenerPorId(zonaId);
            if (zona == null)
                throw new InvalidOperationException($"No existe la zona con ID: {zonaId}");

            var ciudadanosZona = _repoCiudadanos.ListarPorZona(zonaId).ToList();
            var depositosZona = _repoDepositos.ListarPorZona(zonaId, desde, hasta).ToList();

            var reporte = new ReporteZona
            {
                ZonaId = zona.Id,
                NombreZona = zona.Nombre,
                TotalCiudadanos = ciudadanosZona.Count,
                TotalDepositos = depositosZona.Count,
                PesoTotalKg = depositosZona.Sum(d => d.Residuo.Peso),
                PuntosTotales = depositosZona.Sum(d => d.PuntosObtenidos)
            };

            // Agrupar residuos por tipo
            reporte.ResiduosPorTipo = depositosZona
                .GroupBy(d => d.Residuo.Tipo)
                .ToDictionary(g => g.Key, g => g.Sum(d => d.Residuo.Peso));

            // Top 5 ciudadanos más activos
            reporte.CiudadanosMasActivos = ciudadanosZona
                .OrderByDescending(c => c.PuntosTotales)
                .Take(5)
                .Select(c => $"{c.Nombre} ({c.PuntosTotales} pts)")
                .ToList();

            return reporte;
        }

        public IEnumerable<(string Ciudadano, int Puntos)> ObtenerRankingCiudadanos(int top = 10)
        {
            return _repoCiudadanos.Listar()
                .OrderByDescending(c => c.PuntosTotales)
                .Take(top)
                .Select(c => (c.Nombre, c.PuntosTotales));
        }
    }
}
