using Sigfaz.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigfaz.Infra.Data.EntityConfig
{
    public class CidadeConfiguration : EntityTypeConfiguration<Cidade>
    {
        public CidadeConfiguration()
        {
            HasKey(c => c.CidadeId);

            Property(c => c.Nome).IsRequired().HasMaxLength(255);

            HasRequired(c => c.Estado)
                .WithMany()
                .HasForeignKey(c => c.EstadoId);

        }
    }
}
