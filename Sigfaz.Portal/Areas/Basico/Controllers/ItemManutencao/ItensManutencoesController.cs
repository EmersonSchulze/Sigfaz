using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.ItemManutencao;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sigfaz.Portal.Areas.Basico.Controllers.ItemManutencao
{
    public class ItensManutencoesController : Controller
    {
        private readonly IItemManutencaoAppService appService;
        private readonly IMapper mapper;

        public ItensManutencoesController(IItemManutencaoAppService repositorio)
        {
            this.appService = repositorio;
            mapper = AutoMapperConfig.Mapper;

        }

        // GET: Cidade
        public ActionResult Index()
        {
            var itemViewModel = mapper.Map<IEnumerable<Dominio.Entidades.ItemManutencao>, IEnumerable<ItemManutencaoIndexViewModel>>(appService.BuscaTodos());
            return View(itemViewModel);
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
