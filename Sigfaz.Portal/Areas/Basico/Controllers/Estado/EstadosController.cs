using System;
using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.Estado;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcBreadCrumbs;
using Sigfaz.Infra.Mvc;
using Sigfaz.Portal.Areas.Basico.ViewModels.Cultura;
using Sigfaz.Portal.Controllers;
using Sigfaz.Portal.Domain;

namespace Sigfaz.Portal.Areas.Basico.Controllers.Estado
{
    [Authorize(Roles = "Administrador")]
    public class EstadosController : Controller
    {
        private readonly IEstadoAppService _appService;
        private readonly IMapper _mapper;

        
        public EstadosController(IEstadoAppService repositorio)
        {
            this._appService = repositorio;
            _mapper = AutoMapperConfig.Mapper;
        }

       
        // GET: Estado
        public ActionResult Index()
        {
            var estadoViewModel = _mapper.Map<IEnumerable<Dominio.Entidades.Estado>, IEnumerable<EstadoIndexViewModel>>(_appService.BuscaTodos());
            BreadCrumb.Add(Url.Action("Index", "Estados", "Estado_Default"), "Estado");
            return View(estadoViewModel);

        }


    }
}
