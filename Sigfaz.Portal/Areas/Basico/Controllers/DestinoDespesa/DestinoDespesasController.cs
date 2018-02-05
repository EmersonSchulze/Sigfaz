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
        private readonly IDestinoDespesaAppService _appService;
        private readonly IMapper _mapper;

        public DestinoDespesasController(IDestinoDespesaAppService repositorio)
        {
            this._appService = repositorio;
            _mapper = AutoMapperConfig.Mapper;

        }

        // GET: Cidade
        public ActionResult Index()
        {
            var despesaViewModel = _mapper.Map<IEnumerable<Dominio.Entidades.DestinoDespesa>, IEnumerable<DestinoDespesaIndexViewModel>>(_appService.BuscaTodos());
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
