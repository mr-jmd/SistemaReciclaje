using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Entities.Residuos
{
    public sealed class ResiduoMetal : Residuo
    {
        public override string Tipo => "Metal";
        public override decimal PuntosPorKilo => 4.0m;

        public ResiduoMetal(decimal peso) : base(peso) { }
    }
}
