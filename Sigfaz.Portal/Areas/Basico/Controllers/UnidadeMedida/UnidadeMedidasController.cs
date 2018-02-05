using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.UnidadeMedida;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sigfaz.Portal.Areas.Basico.Controllers.Estado
{
    public class UnidadeMedidasController : System.Web.Mvc.Controller
    {
        private readonly IUnidadeMedidaAppService _appService;
        private readonly IMapper _mapper;


        public UnidadeMedidasController(IUnidadeMedidaAppService repositorio)
        {
           this._appService = repositorio;
            _mapper = AutoMapperConfig.Mapper;
        }

        // GET: Estado
        public ActionResult Index()
        {
            var unidadeViewModel = _mapper.Map<IEnumerable<Dominio.Entidades.UnidadeMedida>, IEnumerable<UnidadeMedidaIndexViewModel>>(_appService.BuscaTodos());
            return View(unidadeViewModel);
        }


    }
}
