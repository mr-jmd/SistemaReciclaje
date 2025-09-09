using SistemaReciclaje.Domain.Entities.Residuos;
using SistemaReciclaje.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Application.Strategies
{
    public class CalculoPorTipo : IEstrategiaCalculoPuntos
    {
        public string Nombre => "Cálculo por Tipo";

        public int CalcularPuntos(Residuo residuo)
        {
            // Usa la lógica específica de cada tipo de residuo
            return residuo.CalcularPuntosBase();
        }
    }
}
