﻿using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.Cidade;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcBreadCrumbs;

namespace Sigfaz.Portal.Areas.Basico.Controllers.Cidade
{
   // [Authorize(Roles = "Administrador,")]
    public class CidadesController : Controller
    {
        private readonly ICidadeAppService appService;
        private readonly IMapper mapper;

        public CidadesController(ICidadeAppService repositorio)
        {
            this.appService = repositorio;
            mapper = AutoMapperConfig.Mapper;

        }


        //GET: Cidade
        public ActionResult Index()
            {
                var cidadeViewModel = mapper.Map<IEnumerable<Dominio.Entidades.Cidade>, IEnumerable<CidadeIndexViewModel>>(appService.BuscaPrimeiros(25));
                BreadCrumb.Add(Url.Action("Index", "Cidades"), "Cidade");
                return View(cidadeViewModel);
            }

    }
}
