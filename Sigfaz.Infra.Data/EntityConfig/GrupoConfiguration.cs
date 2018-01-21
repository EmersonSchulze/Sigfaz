using Sigfaz.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace Sigfaz.Infra.Data.EntityConfig
{
    public class GrupoConfiguration : EntityTypeConfiguration<Grupo>
    {
        public GrupoConfiguration()
        {
            HasKey(e => e.GrupoId);

            Property(e => e.Descricao).IsRequired().HasMaxLength(120);

        }
    }
}
