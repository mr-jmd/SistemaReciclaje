using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReciclaje.Domain.Entities.Residuos
{
    public sealed class ResiduoOrganico : Residuo
    {
        public override string Tipo => "Orgánico";
        public override decimal PuntosPorKilo => 1.0m;

        public ResiduoOrganico(decimal peso) : base(peso) { }
    }
}
