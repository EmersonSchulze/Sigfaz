﻿using Sigfaz.Dominio.Interfaces.Repositorios;
using Sigfaz.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sigfaz.Infra.Data.Repositorios
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected SigfazContext Bd = new SigfazContext();

        public void Atualizar(TEntity obj)
        {

            Bd.Entry(obj).State = EntityState.Modified;
            Bd.SaveChanges();
        }

        public TEntity BuscaId(int id)
        {
            return Bd.Set<TEntity>().Find(id);
        }

      public IEnumerable <TEntity> BuscaTodos()
        {
            return Bd.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> BuscaPrimeiros(int qtd)
        {
            return Bd.Set<TEntity>().Take(qtd).AsEnumerable();
        }

        public void Dispose()
        {
           
        }

        public void Incluir(TEntity obj)
        {
            Bd.Set<TEntity>().Add(obj);
            Bd.SaveChanges();
        }

        public void Remover(TEntity obj)
        {
            Bd.Set<TEntity>().Remove(obj);
            Bd.SaveChanges();
        }
    }
}
