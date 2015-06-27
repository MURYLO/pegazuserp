﻿using PegazusERP.Dominio.Base;
using System.Data.Entity;

namespace PegazusERP.Infraestrutura.Base
{
    public interface IQueryableUnitOfWork : IUnitOfWork
    {
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        void Attach<TEntity>(TEntity item) where TEntity : class;

        void SetModified<TEntity>(TEntity item) where TEntity : class;

        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;
    }
}
