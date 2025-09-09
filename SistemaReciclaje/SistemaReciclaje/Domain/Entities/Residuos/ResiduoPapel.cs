using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Entities.Residuos
{
    public sealed class ResiduoPapel : Residuo
    {
        public override string Tipo => "Papel";
        public override decimal PuntosPorKilo => 1.5m;

        public ResiduoPapel(decimal peso) : base(peso) { }
    }
}
