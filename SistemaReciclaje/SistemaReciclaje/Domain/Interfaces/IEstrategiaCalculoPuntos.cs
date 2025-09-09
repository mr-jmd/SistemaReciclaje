using SistemaReciclaje.Domain.Entities.Residuos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Interfaces
{
    public interface IEstrategiaCalculoPuntos
    {
        string Nombre { get; }
        int CalcularPuntos(Residuo residuo);
    }
}
