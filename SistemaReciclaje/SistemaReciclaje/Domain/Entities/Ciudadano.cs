using SistemaReciclaje.Domain.Entities.Residuos;
using SistemaReciclaje.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Entities
{
    public class Ciudadano
    {
        public string Cedula { get; }
        public string Nombre { get; private set; }
        public string Email { get; private set; }
        public Zona Zona { get; private set; }

        private readonly List<Deposito> _depositos = new();
        public IReadOnlyCollection<Deposito> Depositos => _depositos.AsReadOnly();

        public int PuntosTotales => _depositos.Sum(d => d.PuntosObtenidos);

        public Ciudadano(string cedula, string nombre, string email, Zona zona)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                throw new ArgumentException("La cédula es requerida", nameof(cedula));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es requerido", nameof(nombre));
            if (zona == null)
                throw new ArgumentException("La zona es requerida", nameof(zona));

            Cedula = cedula;
            Nombre = nombre;
            Email = email ?? "";
            Zona = zona;
        }

        public void RealizarDeposito(Residuo residuo, IEstrategiaCalculoPuntos estrategia)
        {
            if (residuo == null) throw new ArgumentNullException(nameof(residuo));
            if (estrategia == null) throw new ArgumentNullException(nameof(estrategia));

            var puntos = estrategia.CalcularPuntos(residuo);
            var deposito = new Deposito(DateTime.Now, residuo, puntos);

            _depositos.Add(deposito);
        }

        public void ActualizarDatos(string nombre, string email)
        {
            if (!string.IsNullOrWhiteSpace(nombre))
                Nombre = nombre;
            if (email != null)
                Email = email;
        }
    }
}
