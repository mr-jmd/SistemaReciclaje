using SistemaReciclaje.Domain.Entities.Residuos;
using SistemaReciclaje.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Application.Strategies
{
    public class CalculoMixto : IEstrategiaCalculoPuntos
    {
        public string Nombre => "Cálculo Mixto (Peso + Tipo)";

        public int CalcularPuntos(Residuo residuo)
        {
            var puntosPorTipo = residuo.CalcularPuntosBase();
            var puntosPorPeso = (int)Math.Round(residuo.Peso);

            // Combina ambos enfoques con ponderación
            return (int)Math.Round((puntosPorTipo * 0.7) + (puntosPorPeso * 0.3));
        }
    }
}
