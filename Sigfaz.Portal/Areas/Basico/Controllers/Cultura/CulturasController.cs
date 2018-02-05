using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web.Mvc;
using MvcBreadCrumbs;
using Sigfaz.Infra.Mvc;
using Sigfaz.Portal.Areas.Basico.ViewModels.Cultura;

namespace Sigfaz.Portal.Areas.Basico.Controllers.Cultura
{
    [Authorize(Roles = "Administrador")]
    public class CulturasController : Controller
    {

        private readonly ICulturaAppService _appService;
        private readonly IMapper _mapper;

        public CulturasController(ICulturaAppService repositorio)
        {
            this._appService = repositorio;
            _mapper = AutoMapperConfig.Mapper;

        }


        // GET: Cidade
        public ActionResult Index()
        {
            BreadCrumb.Add(Url.Action("Index", "Culturas", "Cultura_Default"), "Cultura");
            var culturaViewModel = _mapper.Map<IEnumerable<Dominio.Entidades.Cultura>, IEnumerable<CulturaIndexViewModel>>(_appService.BuscaTodos());
           
            return View(culturaViewModel);
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
