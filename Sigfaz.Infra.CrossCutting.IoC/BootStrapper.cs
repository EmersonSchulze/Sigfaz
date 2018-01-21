using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sigfaz.Aplicacao;
using Sigfaz.Aplicacao.Especializadas;
using Sigfaz.Aplicacao.Interfaces;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;
using Sigfaz.Dominio.Servicos;
using Sigfaz.Infra.CrossCutting.Identity.Configuration;
using Sigfaz.Infra.CrossCutting.Identity.Context;
using Sigfaz.Infra.CrossCutting.Identity.Model;
using Sigfaz.Infra.Data.Repositorios;
using SimpleInjector;


namespace Sigfaz.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            #region Identity
            container.Register<ApplicationDbContext>();
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()), Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(new ApplicationDbContext()), Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>();
            container.Register<ApplicationUserManager>();
            container.Register<ApplicationSignInManager>();

            container.Register<IUsuarioRepository, UsuarioRepository>();
            #endregion
            
            #region AppServices
            container.Register(typeof(IAppServiceBase<>), (typeof(AppServiceBase<>)), Lifestyle.Scoped);
            container.Register<ICidadeAppService, CidadeAppService>();
            container.Register<IEstadoAppService, EstadoAppService>();
            container.Register<IUnidadeMedidaAppService, UnidadeMedidaAppService>();
            container.Register<IClassificacaoLoteAppService, ClassificacaoLoteAppService>();
            container.Register<ICulturaAppService, CulturaAppService>();
            container.Register <IDestinoDespesaAppService, DestinoDespesaAppService>();
            container.Register <IGrupoAppService,GrupoAppService>();
            container.Register<IItemManutencaoAppService,ItemManutencaoAppService>();
            container.Register<IRacaAppService,RacaAppService>();
            container.Register<ITipoSanidadeAppService,TipoSanidadeAppService>();
            #endregion

            #region Services
            container.Register(typeof(IServiceBase<>), (typeof(ServiceBase<>)), Lifestyle.Scoped);
            container.Register<ICidadeService, CidadeService>();
            container.Register<IEstadoService, EstadoService>();
            container.Register<IUnidadeMedidaService, UnidadeMedidaService>();
            container.Register<IClassificacaoLoteService,ClassificacaoLoteService>();
            container.Register<ICulturaService,CulturaService>();
            container.Register<IDestinoDespesaService,DestinoDespesaService>();
            container.Register<IGrupoService,GrupoService>();
            container.Register<IItemManutencaoService,ItemManutencaoService>();
            container.Register<IRacaService,RacaService>();
            container.Register<ITipoSanidadeService,TipoSanidadeService>();
            #endregion

            #region Repositorios
            container.Register(typeof(IRepositoryBase<>), (typeof(RepositoryBase<>)), Lifestyle.Scoped);
            container.Register<ICidadeRepository, CidadeRepository>();
            container.Register<IEstadoRepository, EstadoRepository>();
            container.Register<IUnidadeMedidaRepository, UnidadeMedidaRepository>();
            container.Register<IClassificacaoLoteRepository,ClassificacaoLoteRepository>();
            container.Register<ICulturaRepository,CulturaRepository>();
            container.Register<IDestinoDespesaRepository,DestinoDespesaRepository>();
            container.Register<IGrupoRepository,GrupoRepository>();
            container.Register<IItemManutencaoRepository,ItemManutencaoRepository>();
            container.Register<IRacaRepository,RacaRepository>();
            container.Register<ITipoSanidadeRepository,TipoSanidadeRepository>();
            #endregion
        } 
    }
}