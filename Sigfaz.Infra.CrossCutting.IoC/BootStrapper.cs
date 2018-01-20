using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sigfaz.Aplicacao;
using Sigfaz.Aplicacao.Especializadas;
using Sigfaz.Aplicacao.Interfaces;
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

            container.Register<ApplicationDbContext>();
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()), Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(new ApplicationDbContext()), Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>();
            container.Register<ApplicationUserManager>();
            container.Register<ApplicationSignInManager>();

            container.Register<IUsuarioRepository, UsuarioRepository>();

            container.Register(typeof(IAppServiceBase<>), (typeof(AppServiceBase<>)), Lifestyle.Scoped);
            container.Register<ICidadeAppService, CidadeAppService>();
            container.Register<IEstadoAppService, EstadoAppService>();

            container.Register(typeof(IServiceBase<>), (typeof(ServiceBase<>)), Lifestyle.Scoped);
            container.Register<ICidadeService, CidadeService>();
            container.Register<IEstadoService, EstadoService>();

            container.Register(typeof(IRepositoryBase<>), (typeof(RepositoryBase<>)), Lifestyle.Scoped);
            container.Register<ICidadeRepository, CidadeRepository>();
            container.Register<IEstadoRepository, EstadoRepository>();

        } 
    }
}