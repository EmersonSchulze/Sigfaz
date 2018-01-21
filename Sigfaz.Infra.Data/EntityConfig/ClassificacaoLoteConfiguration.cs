using Sigfaz.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace Sigfaz.Infra.Data.EntityConfig
{
    public class ClassificacaoLoteConfiguration : EntityTypeConfiguration<ClassificacaoLote>
    {
        public ClassificacaoLoteConfiguration()
        {
            HasKey(e => e.ClassificacaoLoteId);

            Property(e => e.Descricao).IsRequired().HasMaxLength(255);

            Property(e => e.Periodo);
            
        }
    }
}
