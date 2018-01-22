using Sigfaz.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace Sigfaz.Infra.Data.EntityConfig
{
    public class DestinoDespesaConfiguration : EntityTypeConfiguration<DestinoDespesa>
    {
        public DestinoDespesaConfiguration()
        {
            HasKey(e => e.DestinoDespesaId);

            Property(e => e.Descricao).IsRequired().HasMaxLength(200);

        }
    }
}
