using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Entities.Residuos
{
    public sealed class ResiduoPlastico : Residuo
    {
        public override string Tipo => "Plástico";
        public override decimal PuntosPorKilo => 2.0m;

        public ResiduoPlastico(decimal peso) : base(peso) { }

        public override int CalcularPuntosBase()
        {
            // Los plásticos tienen un bonus del 10% si superan 1kg
            var puntosBase = base.CalcularPuntosBase();
            return Peso > 1.0m ? (int)(puntosBase * 1.1) : puntosBase;
        }
    }
}
