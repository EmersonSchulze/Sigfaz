using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.Cidade;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcBreadCrumbs;

namespace Sigfaz.Portal.Areas.Basico.Controllers.Cidade
{
    [Authorize(Roles = "Administrador")]
    public class CidadesController : Controller
    {
        private readonly ICidadeAppService _appService;
        private readonly IMapper _mapper;

        public CidadesController(ICidadeAppService repositorio)
        {
            this._appService = repositorio;
            _mapper = AutoMapperConfig.Mapper;

        }


        //GET: Cidade
        public ActionResult Index()
            {
                var cidadeViewModel = _mapper.Map<IEnumerable<Dominio.Entidades.Cidade>, IEnumerable<CidadeIndexViewModel>>(_appService.BuscaTodos());
                BreadCrumb.Add(Url.Action("Index", "Cidades", "Cidade_Default"), "Cidade");
                return View(cidadeViewModel);
            }

    }
}
