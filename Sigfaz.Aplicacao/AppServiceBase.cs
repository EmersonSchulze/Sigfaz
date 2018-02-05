using Sigfaz.Aplicacao.Interfaces;
using Sigfaz.Dominio.Interfaces.Servicos;
using System;
using System.Collections;
using System.Collections.Generic;
using Sigfaz.Dominio.Entidades;

namespace Sigfaz.Aplicacao
{
    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity> service)
        {
            _serviceBase = service;
        }

        public void Atualizar(TEntity obj)
        {
            _serviceBase.Atualizar(obj);
        }

        public TEntity BuscaId(int id)
        {
            return _serviceBase.BuscaId(id);
        }

        public IEnumerable<TEntity> BuscaTodos()
        {
            return _serviceBase.BuscaTodos();
        }

        public IEnumerable BuscaPrimeiros(int qtd)
        {
            return _serviceBase.BuscaTodos();
        }

        public void Dispose()
        {
            _serviceBase.Dispose();
        }

        public void Incluir(TEntity obj)
        {
            _serviceBase.Incluir(obj);
        }

        public void Remover(TEntity obj)
        {
            _serviceBase.Remover(obj);
        }
    }
}
