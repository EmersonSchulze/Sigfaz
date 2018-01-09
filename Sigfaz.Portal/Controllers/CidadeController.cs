using AutoMapper;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Infra.Data.Repositorios;
using Sigfaz.Portal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigfaz.Portal.Controllers
{
    public class CidadeController : Controller
    {
        private readonly CidadeRepository cidadeRepository = new CidadeRepository();

        // GET: Cidade
        public ActionResult Index()
        {
            var cidadeViewModel = Mapper.Map<IEnumerable<Cidade>, IEnumerable<CidadeViewModel>>(cidadeRepository.BuscaTodos());
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
