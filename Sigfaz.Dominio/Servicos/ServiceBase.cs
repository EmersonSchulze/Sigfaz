using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;

namespace Sigfaz.Dominio.Servicos
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            this._repository = repository;
        }

        public void Atualizar(TEntity obj)
        {
            _repository.Atualizar(obj);
        }

        public TEntity BuscaId(int id)
        {
            return _repository.BuscaId(id);
        }

        public IEnumerable<TEntity> BuscaTodos()
        {
            return _repository.BuscaTodos();
        }

        public IEnumerable<TEntity> BuscaPrimeiros(int qtd)
        {
            return _repository.BuscaPrimeiros(qtd);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public void Incluir(TEntity obj)
        {
            _repository.Incluir(obj);
        }

        public void Remover(TEntity obj)
        {
            _repository.Remover(obj);
        }
    }
}
