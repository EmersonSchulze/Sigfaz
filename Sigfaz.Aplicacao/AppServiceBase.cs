using Sigfaz.Aplicacao.Interfaces;
using Sigfaz.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using Sigfaz.Dominio.Entidades;

namespace Sigfaz.Aplicacao
{
    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> serviceBase;

        public AppServiceBase(IServiceBase<TEntity> service)
        {
            serviceBase = service;
        }

        public void Atualizar(TEntity obj)
        {
            serviceBase.Atualizar(obj);
        }

        public TEntity BuscaId(int id)
        {
            return serviceBase.BuscaId(id);
        }

        public IEnumerable<TEntity> BuscaTodos()
        {
            return serviceBase.BuscaTodos();
        }

        public IEnumerable<TEntity> BuscaPrimeiros(int qtd)
        {
            return serviceBase.BuscaTodos();
        }

        public void Dispose()
        {
            serviceBase.Dispose();
        }

        public void Incluir(TEntity obj)
        {
            serviceBase.Incluir(obj);
        }

        public void Remover(TEntity obj)
        {
            serviceBase.Remover(obj);
        }
    }
}
