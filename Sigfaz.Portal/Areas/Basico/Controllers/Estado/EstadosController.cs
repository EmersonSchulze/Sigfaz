using System;
using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.Estado;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sigfaz.Portal.Controllers;
using Sigfaz.Portal.Domain;

namespace Sigfaz.Portal.Areas.Basico.Controllers.Estado
{
    public class EstadosController : Controller
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
            var data = new Data();
            var estadoViewModel = mapper.Map<IEnumerable<Dominio.Entidades.Estado>, IEnumerable<EstadoIndexViewModel>>(appService.BuscaTodos());
            PartialView("_Navbar", data.navbarItems().ToList());
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
