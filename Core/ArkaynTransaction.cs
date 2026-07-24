using ArkaynDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace ArkaynDAL.Core
{
    public class ArkaynTransaction : IArkaynTransaction, IDisposable
    {
        private readonly SQLiteTransaction _tx;

        public ArkaynTransaction(SQLiteTransaction tx)
        {
            _tx = tx;
        }

        public void Commit() => _tx.Commit();
        public void Rollback() => _tx.Rollback();

        public void Dispose() => _tx.Dispose();
    }

}
