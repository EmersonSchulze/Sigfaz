using AutoMapper;
using Sigfaz.Aplicacao.Interfaces;
using Sigfaz.Portal.Areas.Basico.ViewModels.Estado;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sigfaz.Portal.Areas.Basico.Controllers.Estado
{
    public class EstadosController : System.Web.Mvc.Controller
    {
        private readonly IEstadoAppService appService;
        private readonly IMapper mapper;

        
        public EstadosController(IEstadoAppService repositorio)
        {
           this.appService = repositorio;
            mapper = AutoMapperConfig.Mapper;
        }

        // GET: Estado
        public ActionResult Index()
        {
            var estadoViewModel = mapper.Map<IEnumerable<Dominio.Entidades.Estado>, IEnumerable<EstadoIndexViewModel>>(appService.BuscaTodos());
            return View(estadoViewModel);
        }

        // GET: Estado/Details/5
        public ActionResult Detalhes(int id)
        {
            var estado = appService.BuscaId(id);
            var estadoModel = mapper.Map<Dominio.Entidades.Estado, EstadoDetalheViewModel>(estado);
            return View(estadoModel);
        }

    }
}
