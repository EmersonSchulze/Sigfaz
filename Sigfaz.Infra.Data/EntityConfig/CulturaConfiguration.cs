using Sigfaz.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace Sigfaz.Infra.Data.EntityConfig
{
    public class CulturaConfiguration : EntityTypeConfiguration<Cultura>
    {
        public CulturaConfiguration()
        {
            HasKey(e => e.CulturaId);

            Property(e => e.Descricao).IsRequired().HasMaxLength(255);

            Property(e => e.Apelido);
            
        }
    }
}
