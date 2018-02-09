using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.Grupo;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sigfaz.Portal.Areas.Basico.Controllers.Grupo
{
    public class GruposController : Controller
    {
        private readonly IGrupoAppService _appService;
        private readonly IMapper _mapper;

        public GruposController(IGrupoAppService repositorio)
        {
            this._appService = repositorio;
            _mapper = AutoMapperConfig.Mapper;

        }

        // GET: Cidade
        public ActionResult Index()
        {
            var grupoViewModel = _mapper.Map<IEnumerable<Dominio.Entidades.Grupo>, IEnumerable<GrupoIndexViewModel>>(_appService.BuscaTodos());
            return View(grupoViewModel);
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
