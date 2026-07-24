using System;
using System.Collections.Generic;
using System.Text;

namespace ArkaynDAL.Logging
{
    public interface IDalLogger
    {
        void OnError(string message, Exception ex);
        void OnQueryExecuted(string sql, TimeSpan duration);
        void OnConnectionOpened();
        void OnConnectionClosed();
    }

}
