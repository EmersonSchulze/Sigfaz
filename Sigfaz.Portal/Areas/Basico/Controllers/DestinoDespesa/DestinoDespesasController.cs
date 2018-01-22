using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.DestinoDespesa;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sigfaz.Portal.Areas.Basico.Controllers.DestinoDespesa
{
    public class DestinoDespesasController : Controller
    {
        private readonly IDestinoDespesaAppService appService;
        private readonly IMapper mapper;

        public DestinoDespesasController(IDestinoDespesaAppService repositorio)
        {
            this.appService = repositorio;
            mapper = AutoMapperConfig.Mapper;

        }

        // GET: Cidade
        public ActionResult Index()
        {
            var despesaViewModel = mapper.Map<IEnumerable<Dominio.Entidades.DestinoDespesa>, IEnumerable<DestinoDespesaIndexViewModel>>(appService.BuscaTodos());
            return View(despesaViewModel);
        }

        // GET: Cidade/Details/5
        //public ActionResult Detalhes(int id)
        //{
        //    var despesa = appService.BuscaId(id);
        //    var despesaModel = mapper.Map<Dominio.Entidades.DestinoDespesa, DestinoDespesaDetalheViewModel>(despesa);
        //    return View(despesaModel);
        //}

       
    }
}
