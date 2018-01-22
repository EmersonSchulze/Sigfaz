using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.Cidade;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sigfaz.Portal.Areas.Basico.Controllers.Cidade
{
    public class CidadesController : Controller
    {
        private readonly ICidadeAppService appService;
        private readonly IMapper mapper;

        public CidadesController(ICidadeAppService repositorio)
        {
            this.appService = repositorio;
            mapper = AutoMapperConfig.Mapper;

        }

        // GET: Cidade
        public ActionResult Index()
        {
            var cidadeViewModel = mapper.Map<IEnumerable<Dominio.Entidades.Cidade>, IEnumerable<CidadeIndexViewModel>>(appService.BuscaTodos());
            return View(cidadeViewModel);
        }

        // GET: Cidade/Details/5
        public ActionResult Detalhes(int id)
        {
            var cidade = appService.BuscaId(id);
            var cidadeModel = mapper.Map<Dominio.Entidades.Cidade, CidadeDetalheViewModel>(cidade);
            return View(cidadeModel);
        }

       
    }
}
