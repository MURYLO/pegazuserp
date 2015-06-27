using System;

namespace PegazusERP.Dominio.Base
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void CommitAndRefreshChanges();

        void RollbackChanges();
    }
}
