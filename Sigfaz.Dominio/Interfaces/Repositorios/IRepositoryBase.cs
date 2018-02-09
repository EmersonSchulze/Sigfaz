﻿
using System.Collections.Generic;

namespace Sigfaz.Dominio.Interfaces.Repositorios
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Incluir(TEntity obj);

        TEntity BuscaId(int id);

        IEnumerable<TEntity> BuscaTodos();

        IEnumerable<TEntity> BuscaPrimeiros(int qtd);
        void Remover(TEntity obj);
        void Atualizar(TEntity obj);

        void Dispose();
    }
}
