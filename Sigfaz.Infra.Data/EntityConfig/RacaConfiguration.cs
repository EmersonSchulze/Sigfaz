using Sigfaz.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace Sigfaz.Infra.Data.EntityConfig
{
    public class RacaConfiguration : EntityTypeConfiguration<Raca>
    {
        public RacaConfiguration()
        {
            HasKey(e => e.RacaId);

            Property(e => e.Descricao).IsRequired().HasMaxLength(255);

            Property(e => e.Abreveatura).HasMaxLength(4);
            
        }
    }
}
