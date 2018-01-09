using AutoMapper;
using Sigfaz.Dominio.Entidades;
using Sigfaz.Dominio.Interfaces;
using Sigfaz.Infra.Data.Repositorios;
using Sigfaz.Portal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigfaz.Portal.Controllers
{
    public class EstadoController : Controller
    {
        private readonly IEstadoRepository repositorio;
       
        public EstadoController(IEstadoRepository repositorio)
        {
           this.repositorio = repositorio;
        }

        //private readonly EstadoRepository estadoRepository = new EstadoRepository();

        // GET: Estado
        public ActionResult Index()
        {
            var estadoViewModel = Mapper.Map<IEnumerable<Estado>, IEnumerable<EstadoViewModel>>(repositorio.BuscaTodos());
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
                var estadoDominio = Mapper.Map<EstadoViewModel, Estado>(viewModel);
                repositorio.Incluir(estadoDominio);

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
