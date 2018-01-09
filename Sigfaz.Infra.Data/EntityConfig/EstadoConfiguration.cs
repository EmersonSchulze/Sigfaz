using Sigfaz.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigfaz.Infra.Data.EntityConfig
{
    public class EstadoConfiguration : EntityTypeConfiguration<Estado>
    {
        public EstadoConfiguration()
        {
            HasKey(e => e.EstadoId);

            Property(e => e.Nome).IsRequired();

            Property(e => e.Sigla).IsRequired();

            Property(e => e.CodigoIbge).IsRequired();

        }
    }
}
