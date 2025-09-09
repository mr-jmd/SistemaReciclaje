using SistemaReciclaje.Application.Services;
using SistemaReciclaje.Application.Strategies;
using SistemaReciclaje.Domain.Interfaces;
using SistemaReciclaje.Infrastructure.Console;
using SistemaReciclaje.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Infrastructure.Console
{
    public class MenuConsola
    {
        private readonly ServicioReciclaje _servicioReciclaje;
        private readonly ServicioReportes _servicioReportes;
        private readonly Dictionary<string, IEstrategiaCalculoPuntos> _estrategias;

        public MenuConsola(ServicioReciclaje servicioReciclaje, ServicioReportes servicioReportes)
        {
            _servicioReciclaje = servicioReciclaje;
            _servicioReportes = servicioReportes;
            _estrategias = new Dictionary<string, IEstrategiaCalculoPuntos>
            {
                { "1", new CalculoPorPeso() },
                { "2", new CalculoPorTipo() },
                { "3", new CalculoMixto() }
            };
        }

        public void MostrarMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("=== SISTEMA DE GESTIÓN DE RESIDUOS Y RECICLAJE ===");
            System.Console.WriteLine("Contribuyendo al ODS 11: Ciudades y Comunidades Sostenibles\n");

            while (true)
            {
                try
                {
                    System.Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
                    System.Console.WriteLine("1. 👤 Gestión de Ciudadanos");
                    System.Console.WriteLine("2. 📍 Gestión de Zonas");
                    System.Console.WriteLine("3. ♻️  Registrar Depósito");
                    System.Console.WriteLine("4. 📊 Consultas y Reportes");
                    System.Console.WriteLine("5. ⚙️  Configuración");
                    System.Console.WriteLine("0. 🚪 Salir");
                    System.Console.Write("\nSeleccione una opción: ");

                    var opcion = System.Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            MenuCiudadanos();
                            break;
                        case "2":
                            MenuZonas();
                            break;
                        case "3":
                            RegistrarDeposito();
                            break;
                        case "4":
                            MenuReportes();
                            break;
                        case "5":
                            MenuConfiguracion();
                            break;
                        case "0":
                            System.Console.WriteLine("\n¡Gracias por contribuir al medio ambiente! 🌱");
                            return;
                        default:
                            System.Console.WriteLine("❌ Opción no válida");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"❌ Error: {ex.Message}");
                    System.Console.WriteLine("Presione Enter para continuar...");
                    System.Console.ReadLine();
                }
            }
        }

        private void MenuCiudadanos()
        {
            System.Console.WriteLine("\n--- GESTIÓN DE CIUDADANOS ---");
            System.Console.WriteLine("1. Registrar nuevo ciudadano");
            System.Console.WriteLine("2. Consultar ciudadano");
            System.Console.WriteLine("3. Listar todos los ciudadanos");
            System.Console.WriteLine("4. Ranking por puntos");
            System.Console.Write("Seleccione una opción: ");

            var opcion = System.Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    RegistrarCiudadano();
                    break;
                case "2":
                    ConsultarCiudadano();
                    break;
                case "3":
                    ListarCiudadanos();
                    break;
                case "4":
                    MostrarRanking();
                    break;
            }
        }

        private void RegistrarCiudadano()
        {
            System.Console.WriteLine("\n--- REGISTRO DE CIUDADANO ---");

            System.Console.Write("Cédula: ");
            var cedula = System.Console.ReadLine()?.Trim();

            System.Console.Write("Nombre completo: ");
            var nombre = System.Console.ReadLine()?.Trim();

            System.Console.Write("Email (opcional): ");
            var email = System.Console.ReadLine()?.Trim();

            System.Console.WriteLine("\nZonas disponibles:");
            var zonas = _servicioReciclaje.ListarZonas().ToList();
            if (!zonas.Any())
            {
                System.Console.WriteLine("❌ No hay zonas registradas. Registre una zona primero.");
                return;
            }

            foreach (var zona in zonas)
            {
                System.Console.WriteLine($"  {zona.Id} - {zona.Nombre}");
            }

            System.Console.Write("ID de la zona: ");
            var zonaId = System.Console.ReadLine()?.Trim();

            _servicioReciclaje.RegistrarCiudadano(cedula!, nombre!, email, zonaId!);
            System.Console.WriteLine("✅ Ciudadano registrado exitosamente");
        }

        private void ConsultarCiudadano()
        {
            System.Console.Write("Ingrese la cédula: ");
            var cedula = System.Console.ReadLine()?.Trim();

            var ciudadano = _servicioReciclaje.ConsultarCiudadano(cedula!);
            if (ciudadano == null)
            {
                System.Console.WriteLine("❌ Ciudadano no encontrado");
                return;
            }

            System.Console.WriteLine($"\n--- INFORMACIÓN DEL CIUDADANO ---");
            System.Console.WriteLine($"Cédula: {ciudadano.Cedula}");
            System.Console.WriteLine($"Nombre: {ciudadano.Nombre}");
            System.Console.WriteLine($"Email: {ciudadano.Email}");
            System.Console.WriteLine($"Zona: {ciudadano.Zona.Nombre}");
            System.Console.WriteLine($"Puntos totales: {ciudadano.PuntosTotales} 🏆");
            System.Console.WriteLine($"Depósitos realizados: {ciudadano.Depositos.Count}");

            if (ciudadano.Depositos.Any())
            {
                System.Console.WriteLine("\nÚltimos depósitos:");
                foreach (var deposito in ciudadano.Depositos.TakeLast(5))
                {
                    System.Console.WriteLine($"  • {deposito.FechaHora:dd/MM/yyyy HH:mm} - {deposito.Residuo} - {deposito.PuntosObtenidos} pts");
                }
            }
        }

        private void ListarCiudadanos()
        {
            var ciudadanos = _servicioReciclaje.ListarCiudadanos().ToList();

            System.Console.WriteLine($"\n--- LISTA DE CIUDADANOS ({ciudadanos.Count}) ---");
            foreach (var ciudadano in ciudadanos)
            {
                System.Console.WriteLine($"{ciudadano.Cedula} - {ciudadano.Nombre} | Zona: {ciudadano.Zona.Nombre} | Puntos: {ciudadano.PuntosTotales} 🏆");
            }
        }

        private void MostrarRanking()
        {
            var ranking = _servicioReportes.ObtenerRankingCiudadanos(10).ToList();

            System.Console.WriteLine("\n--- TOP 10 CIUDADANOS MÁS ACTIVOS ---");
            for (int i = 0; i < ranking.Count; i++)
            {
                var (nombre, puntos) = ranking[i];
                var medal = i switch { 0 => "🥇", 1 => "🥈", 2 => "🥉", _ => $"{i + 1}." };
                System.Console.WriteLine($"{medal} {nombre} - {puntos} puntos");
            }
        }

        private void MenuZonas()
        {
            System.Console.WriteLine("\n--- GESTIÓN DE ZONAS ---");
            System.Console.WriteLine("1. Registrar nueva zona");
            System.Console.WriteLine("2. Listar zonas");
            System.Console.Write("Seleccione una opción: ");

            var opcion = System.Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    RegistrarZona();
                    break;
                case "2":
                    ListarZonas();
                    break;
            }
        }

        private void RegistrarZona()
        {
            System.Console.WriteLine("\n--- REGISTRO DE ZONA ---");
            System.Console.Write("ID de la zona: ");
            var id = System.Console.ReadLine()?.Trim();

            System.Console.Write("Nombre: ");
            var nombre = System.Console.ReadLine()?.Trim();

            System.Console.Write("Descripción (opcional): ");
            var descripcion = System.Console.ReadLine()?.Trim();

            _servicioReciclaje.RegistrarZona(id!, nombre!, descripcion);
            System.Console.WriteLine("✅ Zona registrada exitosamente");
        }

        private void ListarZonas()
        {
            var zonas = _servicioReciclaje.ListarZonas().ToList();

            System.Console.WriteLine($"\n--- ZONAS REGISTRADAS ({zonas.Count}) ---");
            foreach (var zona in zonas)
            {
                System.Console.WriteLine($"{zona.Id} - {zona.Nombre}");
                if (!string.IsNullOrEmpty(zona.Descripcion))
                    System.Console.WriteLine($"    {zona.Descripcion}");
            }
        }

        private void RegistrarDeposito()
        {
            System.Console.WriteLine("\n--- REGISTRAR DEPÓSITO ---");

            System.Console.Write("Cédula del ciudadano: ");
            var cedula = System.Console.ReadLine()?.Trim();

            System.Console.WriteLine("\nTipos de residuo disponibles:");
            System.Console.WriteLine("  P - Plástico (2 pts/kg base)");
            System.Console.WriteLine("  PA - Papel (1.5 pts/kg)");
            System.Console.WriteLine("  V - Vidrio (3 pts/kg)");
            System.Console.WriteLine("  M - Metal (4 pts/kg)");
            System.Console.WriteLine("  O - Orgánico (1 pt/kg)");

            System.Console.Write("Tipo de residuo: ");
            var tipo = System.Console.ReadLine()?.Trim();

            System.Console.Write("Peso en kilogramos (0.1 - 50 kg): ");
            var pesoStr = System.Console.ReadLine()?.Trim();

            if (decimal.TryParse(pesoStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var peso))
            {
                _servicioReciclaje.RegistrarDeposito(cedula!, tipo!, peso);
                System.Console.WriteLine("✅ Depósito registrado exitosamente");

                // Mostrar puntos obtenidos
                var ciudadano = _servicioReciclaje.ConsultarCiudadano(cedula!);
                var ultimoDeposito = ciudadano?.Depositos.LastOrDefault();
                if (ultimoDeposito != null)
                {
                    System.Console.WriteLine($"🏆 Puntos obtenidos: {ultimoDeposito.PuntosObtenidos}");
                    System.Console.WriteLine($"🏆 Total de puntos: {ciudadano.PuntosTotales}");
                }
            }
            else
            {
                System.Console.WriteLine("❌ Peso no válido");
            }
        }

        private void MenuReportes()
        {
            System.Console.WriteLine("\n--- CONSULTAS Y REPORTES ---");
            System.Console.WriteLine("1. Reporte por zona");
            System.Console.WriteLine("2. Ranking general");
            System.Console.Write("Seleccione una opción: ");

            var opcion = System.Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    GenerarReporteZona();
                    break;
                case "2":
                    MostrarRanking();
                    break;
            }
        }

        private void GenerarReporteZona()
        {
            System.Console.Write("ID de la zona: ");
            var zonaId = System.Console.ReadLine()?.Trim();

            try
            {
                var reporte = _servicioReportes.GenerarReportePorZona(zonaId!);

                System.Console.WriteLine($"\n--- REPORTE ZONA: {reporte.NombreZona} ---");
                System.Console.WriteLine($"👥 Total ciudadanos: {reporte.TotalCiudadanos}");
                System.Console.WriteLine($"📦 Total depósitos: {reporte.TotalDepositos}");
                System.Console.WriteLine($"⚖️  Peso total: {reporte.PesoTotalKg:F2} kg");
                System.Console.WriteLine($"🏆 Puntos totales: {reporte.PuntosTotales}");

                System.Console.WriteLine("\nResumen por tipo de residuo:");
                foreach (var kvp in reporte.ResiduosPorTipo)
                {
                    System.Console.WriteLine($"  • {kvp.Key}: {kvp.Value:F2} kg");
                }

                System.Console.WriteLine("\nCiudadanos más activos:");
                foreach (var ciudadano in reporte.CiudadanosMasActivos)
                {
                    System.Console.WriteLine($"  🏆 {ciudadano}");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        private void MenuConfiguracion()
        {
            System.Console.WriteLine("\n--- CONFIGURACIÓN ---");
            System.Console.WriteLine("1. Cambiar estrategia de cálculo de puntos");
            System.Console.WriteLine("2. Ver estrategia actual");
            System.Console.Write("Seleccione una opción: ");

            var opcion = System.Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    CambiarEstrategiaPuntos();
                    break;
                case "2":
                    // Para simplificar, asumimos que siempre usa CalculoPorTipo por defecto
                    System.Console.WriteLine("Estrategia actual: Cálculo por Tipo");
                    break;
            }
        }

        private void CambiarEstrategiaPuntos()
        {
            System.Console.WriteLine("\n--- ESTRATEGIAS DISPONIBLES ---");
            System.Console.WriteLine("1. Cálculo por Peso (1 pt/kg)");
            System.Console.WriteLine("2. Cálculo por Tipo (específico por material)");
            System.Console.WriteLine("3. Cálculo Mixto (combinado)");
            System.Console.Write("Seleccione estrategia: ");

            var opcion = System.Console.ReadLine();
            if (_estrategias.TryGetValue(opcion!, out var estrategia))
            {
                _servicioReciclaje.CambiarEstrategiaPuntos(estrategia);
                System.Console.WriteLine($"✅ Estrategia cambiada a: {estrategia.Nombre}");
            }
            else
            {
                System.Console.WriteLine("❌ Opción no válida");
            }
        }
    }
}
