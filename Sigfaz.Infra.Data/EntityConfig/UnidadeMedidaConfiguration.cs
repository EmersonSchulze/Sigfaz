using Sigfaz.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace Sigfaz.Infra.Data.EntityConfig
{
    public class UnidadeMedidaConfiguration : EntityTypeConfiguration<UnidadeMedida>
    {
        public UnidadeMedidaConfiguration()
        {
            HasKey(e => e.UnidadeMedidaId);

            Property(e => e.Descricao).IsRequired();

            Property(e => e.Sigla).IsRequired();
            
        }
    }
}
