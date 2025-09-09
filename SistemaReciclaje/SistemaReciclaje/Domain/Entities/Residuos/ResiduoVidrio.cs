using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Entities.Residuos
{
    public sealed class ResiduoVidrio : Residuo
    {
        public override string Tipo => "Vidrio";
        public override decimal PuntosPorKilo => 3.0m;

        public ResiduoVidrio(decimal peso) : base(peso) { }
    }
}
