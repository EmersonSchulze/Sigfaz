using Sigfaz.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace Sigfaz.Infra.Data.EntityConfig
{
    public class TipoSanidadeConfiguration : EntityTypeConfiguration<TipoSanidade>
    {
        public TipoSanidadeConfiguration()
        {
            HasKey(e => e.Descricao);

            Property(e => e.Descricao).IsRequired().HasMaxLength(200);

            Property(e => e.CarenciaMesses).IsRequired();
            
        }
    }
}
