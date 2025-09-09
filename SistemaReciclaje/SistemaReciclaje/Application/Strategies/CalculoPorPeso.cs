using SistemaReciclaje.Domain.Entities.Residuos;
using SistemaReciclaje.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Application.Strategies
{
    public class CalculoPorPeso : IEstrategiaCalculoPuntos
    {
        public string Nombre => "Cálculo por Peso";

        public int CalcularPuntos(Residuo residuo)
        {
            // Estrategia simple: 1 punto por cada kilogramo
            return (int)Math.Round(residuo.Peso);
        }
    }
}
