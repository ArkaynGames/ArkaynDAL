using ArkaynDAL.Core;
using ArkaynDAL.Models;
using System;
using System.Threading.Tasks;

namespace ArkaynDAL.Interfaces
{
    public interface IArkaynConnection : IDisposable
    {
        Task OpenAsync();
        void Close();

        IArkaynCommand CreateCommand(string sql);

        Task<int> ExecuteNonQueryAsync(string sql, params ArkaynParameter[] parameters);
        Task<T> ExecuteScalarAsync<T>(string sql, params ArkaynParameter[] parameters);
        Task<IArkaynReader> ExecuteReaderAsync(string sql, params ArkaynParameter[] parameters);

        SchemaVersion GetSchemaVersion();
        void SetSchemaVersion(SchemaVersion version);
    }
}

