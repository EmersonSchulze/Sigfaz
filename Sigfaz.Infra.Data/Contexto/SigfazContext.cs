using Sigfaz.Dominio.Entidades;
using Sigfaz.Infra.Data.EntityConfig;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Sigfaz.Infra.Data.Contexto
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]

    public class SigfazContext : DbContext
    {
        public SigfazContext()
            : base("SigfazConection")
        {
           
        }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
		public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UnidadeMedida> UnidadeMedidas { get; set; }
        public DbSet<ClassificacaoLote> ClassificacaoLote { get; set; }
        public DbSet<Cultura> Cultura { get; set; }
        public DbSet<DestinoDespesa> DestinoDespesa { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<ItemManutencao> ItemManutencao { get; set; }
        public DbSet<Raca> Raca { get; set; }
        public DbSet<TipoSanidade> TipoSanidade { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<SigfazContext>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                    .Where(p => p.Name == p.ReflectedType.Name + "Id")
                    .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(255));

            modelBuilder.Configurations.Add(new EstadoConfiguration());
            modelBuilder.Configurations.Add(new CidadeConfiguration());
			modelBuilder.Configurations.Add(new UsuarioConfig());
            modelBuilder.Configurations.Add(new UnidadeMedidaConfiguration());

        }

          public override int SaveChanges()
          {
            foreach(var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
                
            }
            return base.SaveChanges();
          }

    }
}
