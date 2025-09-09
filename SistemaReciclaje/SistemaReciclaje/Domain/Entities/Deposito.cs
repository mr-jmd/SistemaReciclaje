using SistemaReciclaje.Domain.Entities.Residuos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Entities
{
    public class Deposito
    {
        public DateTime FechaHora { get; }
        public Residuo Residuo { get; }
        public int PuntosObtenidos { get; }

        public Deposito(DateTime fechaHora, Residuo residuo, int puntosObtenidos)
        {
            FechaHora = fechaHora;
            Residuo = residuo ?? throw new ArgumentNullException(nameof(residuo));
            PuntosObtenidos = Math.Max(0, puntosObtenidos);
        }
    }
}
