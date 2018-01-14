using AutoMapper;
using Sigfaz.Aplicacao.Interfaces;
using Sigfaz.Portal.AutoMapper;
using Sigfaz.Portal.Areas.Cidade.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sigfaz.Portal.Areas.Cidade.Controllers
{
    public class CidadeController : Controller
    {
        private readonly ICidadeAppService appService;
        private readonly IMapper mapper;

        public CidadeController(ICidadeAppService repositorio)
        {
            this.appService = repositorio;
            mapper = AutoMapperConfig.Mapper;

        }

        // GET: Cidade
        public ActionResult Index()
        {
            var cidadeViewModel = mapper.Map<IEnumerable<Cidade>, IEnumerable<CidadeViewModel>>(appService.BuscaTodos());
            return View(cidadeViewModel);
        }

        // GET: Cidade/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cidade/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cidade/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cidade/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cidade/Edit/5
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

        // GET: Cidade/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cidade/Delete/5
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
