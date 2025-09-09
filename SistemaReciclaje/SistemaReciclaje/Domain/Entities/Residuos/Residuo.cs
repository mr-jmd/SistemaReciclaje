using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Entities.Residuos
{
    public abstract class Residuo
    {
        public decimal Peso { get; }
        public abstract string Tipo { get; }
        public abstract decimal PuntosPorKilo { get; }

        protected Residuo(decimal peso)
        {
            if (peso < 0.1m)
                throw new ArgumentException("El peso mínimo es 0.1 kg", nameof(peso));
            if (peso > 50m)
                throw new ArgumentException("El peso máximo es 50 kg", nameof(peso));

            Peso = peso;
        }

        public virtual int CalcularPuntosBase()
        {
            return (int)Math.Round(Peso * PuntosPorKilo);
        }

        public override string ToString()
        {
            return $"{Tipo} - {Peso}kg";
        }
    }
}
