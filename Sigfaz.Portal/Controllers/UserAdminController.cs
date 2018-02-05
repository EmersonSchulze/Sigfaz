using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigfaz.Dominio.Interfaces.Repositorios;

namespace Sigfaz.Portal.Controllers
{
    public class UserAdminController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UserAdminController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        // GET: UserAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}