
using Sigfaz.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace Sigfaz.Dominio.Interfaces.Repositorios

{
    public interface IUsuarioRepository : IDisposable
    {
        Usuario ObterPorId(string id);
        IEnumerable<Usuario> ObterTodos();
        void DesativarLock(string id);
    }
}
