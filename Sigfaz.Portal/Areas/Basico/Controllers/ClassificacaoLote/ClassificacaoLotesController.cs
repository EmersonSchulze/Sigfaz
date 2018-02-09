using AutoMapper;
using Sigfaz.Aplicacao.Interfaces.Especializadas;
using Sigfaz.Portal.Areas.Basico.ViewModels.ClassificacaoLote;
using Sigfaz.Portal.AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sigfaz.Portal.Areas.Basico.Controllers.ClassificacaoLote
{
    public class ClassificacaoLotesController : Controller
    {
        private readonly IClassificacaoLoteAppService _appService;
        private readonly IMapper _mapper;

        public ClassificacaoLotesController(IClassificacaoLoteAppService repositorio)
        {
            this._appService = repositorio;
            _mapper = AutoMapperConfig.Mapper;

        }
        // GET: Basico/ClassificacaoLotes
        public ActionResult Index()
        {
            var loteViewModel = _mapper.Map<IEnumerable<Dominio.Entidades.ClassificacaoLote>, IEnumerable<ClassificacaoLoteIndexViewModel>>(_appService.BuscaTodos());
            return View(loteViewModel);
        }

        // GET: Basico/ClassificacaoLotes/Details/5
        public ActionResult Detalhes(int id)
        {
            var lote = _appService.BuscaId(id);
            var loteModel = _mapper.Map<Dominio.Entidades.ClassificacaoLote, ClassificacaoLoteIndexViewModel>(lote);
            return View(loteModel);
        }

        // GET: Basico/ClassificacaoLotes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Basico/ClassificacaoLotes/Create
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

        // GET: Basico/ClassificacaoLotes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Basico/ClassificacaoLotes/Edit/5
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

        // GET: Basico/ClassificacaoLotes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Basico/ClassificacaoLotes/Delete/5
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
