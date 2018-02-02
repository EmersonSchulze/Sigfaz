using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;

namespace Sigfaz.Dominio.Servicos
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> repository;

        public ServiceBase(IRepositoryBase<TEntity> _repository)
        {
            repository = _repository;
        }

        public void Atualizar(TEntity obj)
        {
            repository.Atualizar(obj);
        }

        public TEntity BuscaId(int id)
        {
            return repository.BuscaId(id);
        }

        public IEnumerable<TEntity> BuscaTodos()
        {
            return repository.BuscaTodos();
        }

        public IEnumerable<TEntity> BuscaPrimeiros(int qtd)
        {
            return repository.BuscaPrimeiros(qtd);
        }

        public void Dispose()
        {
            repository.Dispose();
        }

        public void Incluir(TEntity obj)
        {
            repository.Incluir(obj);
        }

        public void Remover(TEntity obj)
        {
            repository.Remover(obj);
        }
    }
}
