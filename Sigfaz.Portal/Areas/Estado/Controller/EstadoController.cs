using AutoMapper;
using Sigfaz.Aplicacao.Interfaces;
using Sigfaz.Portal.Areas.Estado.ViewModels;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sigfaz.Portal.Areas.Estado.Controllers
{
    public class EstadoController : Controller
    {
        private readonly IEstadoAppService appService;
        private readonly IMapper mapper;

        
        public EstadoController(IEstadoAppService repositorio)
        {
           this.appService = repositorio;
            mapper = AutoMapperConfig.Mapper;
        }

        // GET: Estado
        public ActionResult Index()
        {
            var estadoViewModel = mapper.Map<IEnumerable<Estado>, IEnumerable<EstadoViewModel>>(appService.BuscaTodos());
          //  var estadoViewModel = Mapper.Map<IEnumerable<Estado>, IEnumerable<EstadoViewModel>>(appService.BuscaTodos());
            return View(estadoViewModel);
        }

        // GET: Estado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Estado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EstadoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var estadoDominio = mapper.Map<EstadoViewModel, Estado>(viewModel);
                appService.Incluir(estadoDominio);

                return RedirectToAction("Index");
            }

            return View(viewModel);
            
        }

        // GET: Estado/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Estado/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Estado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Estado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
