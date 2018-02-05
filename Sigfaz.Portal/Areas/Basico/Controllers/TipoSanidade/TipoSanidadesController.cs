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
        private readonly ITipoSanidadeAppService _appService;
        private readonly IMapper _mapper;

        public TipoSanidadesController(ITipoSanidadeAppService repositorio)
        {
            this._appService = repositorio;
            _mapper = AutoMapperConfig.Mapper;

        }

        // GET: Cidade
        public ActionResult Index()
        {
            var sanidadeViewModel = _mapper.Map<IEnumerable<Dominio.Entidades.TipoSanidade>, IEnumerable<TipoSanidadeIndexViewModel>>(_appService.BuscaTodos());
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
