using System;
using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.Estado;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcBreadCrumbs;
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
            var estadoViewModel = mapper.Map<IEnumerable<Dominio.Entidades.Estado>, IEnumerable<EstadoIndexViewModel>>(appService.BuscaTodos());
            BreadCrumb.Add(Url.Action("Index", "Estados"), "Estado");
            return View(estadoViewModel);
             
        }

    }
}
