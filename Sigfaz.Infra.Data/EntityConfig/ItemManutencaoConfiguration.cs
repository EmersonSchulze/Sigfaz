using Sigfaz.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace Sigfaz.Infra.Data.EntityConfig
{
    public class ItemManutencaoConfiguration : EntityTypeConfiguration<ItemManutencao>
    {
        public ItemManutencaoConfiguration()
        {
            HasKey(e => e.ItemManutencaoId);

            Property(e => e.Descricao).IsRequired();
            
        }
    }
}
