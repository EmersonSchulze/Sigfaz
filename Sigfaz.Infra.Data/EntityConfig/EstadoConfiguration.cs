using Sigfaz.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

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
