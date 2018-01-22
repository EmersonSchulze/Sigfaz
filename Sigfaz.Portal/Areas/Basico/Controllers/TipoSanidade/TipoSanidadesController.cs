using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.TipoSanidade;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sigfaz.Portal.Areas.Basico.Controllers.TipoSanidade
{
    public class TipoSanidadesController : Controller
    {
        private readonly ITipoSanidadeAppService appService;
        private readonly IMapper mapper;

        public TipoSanidadesController(ITipoSanidadeAppService repositorio)
        {
            this.appService = repositorio;
            mapper = AutoMapperConfig.Mapper;

        }

        // GET: Cidade
        public ActionResult Index()
        {
            var sanidadeViewModel = mapper.Map<IEnumerable<Dominio.Entidades.TipoSanidade>, IEnumerable<TipoSanidadeIndexViewModel>>(appService.BuscaTodos());
            return View(sanidadeViewModel);
        }

        // GET: Cidade/Details/5
        //public ActionResult Detalhes(int id)
        //{
        //    var cultura = appService.BuscaId(id);
        //    var culturaModel = mapper.Map<Dominio.Entidades.Cultura, CulturaDetalheViewModel>(cultura);
        //    return View(culturaModel);
        //}

       
    }
}
