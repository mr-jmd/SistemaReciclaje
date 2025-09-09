using SistemaReciclaje.Application.Services;
using SistemaReciclaje.Application.Strategies;
using SistemaReciclaje.Infrastructure.Console;
using SistemaReciclaje.Infrastructure.Repositories;

namespace SistemaReciclaje
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Configuración de dependencias
                var repositorio = new RepositorioEnMemoria();
                var estrategiaInicial = new CalculoPorTipo();

                var servicioReciclaje = new ServicioReciclaje(
                    repositorio,
                    repositorio,
                    repositorio,
                    estrategiaInicial);

                var servicioReportes = new ServicioReportes(
                    repositorio,
                    repositorio,
                    repositorio);

                // Datos de prueba iniciales
                InicializarDatosPrueba(servicioReciclaje);

                // Iniciar aplicación
                var menu = new MenuConsola(servicioReciclaje, servicioReportes);
                menu.MostrarMenu();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"❌ Error fatal: {ex.Message}");
                System.Console.WriteLine("Presione Enter para salir...");
                System.Console.ReadLine();
            }
        }

        private static void InicializarDatosPrueba(ServicioReciclaje servicio)
        {
            try
            {
                // Crear zonas de prueba
                servicio.RegistrarZona("NORTE", "Zona Norte", "Incluye barrios del norte de la ciudad");
                servicio.RegistrarZona("SUR", "Zona Sur", "Incluye barrios del sur de la ciudad");
                servicio.RegistrarZona("CENTRO", "Zona Centro", "Centro histórico y comercial");

                // Crear ciudadanos de prueba
                servicio.RegistrarCiudadano("12345678", "Ana García López", "ana@email.com", "NORTE");
                servicio.RegistrarCiudadano("23456789", "Carlos Rodríguez", "carlos@email.com", "SUR");
                servicio.RegistrarCiudadano("34567890", "María Fernández", "maria@email.com", "CENTRO");

                // Crear algunos depósitos de prueba
                servicio.RegistrarDeposito("12345678", "PLASTICO", 2.5m);
                servicio.RegistrarDeposito("12345678", "PAPEL", 1.8m);
                servicio.RegistrarDeposito("23456789", "VIDRIO", 3.2m);
                servicio.RegistrarDeposito("34567890", "METAL", 1.5m);

                System.Console.WriteLine("✅ Sistema inicializado con datos de prueba");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"⚠️ Advertencia: No se pudieron inicializar datos de prueba: {ex.Message}");
            }

        }
    }
}
