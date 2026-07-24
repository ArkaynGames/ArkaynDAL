using ArkaynDAL.Interfaces;
using ArkaynDAL.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace ArkaynDAL.Core
{
    public class ArkaynCommand : IArkaynCommand, IDisposable
    {
        private readonly SQLiteCommand _cmd;
        private readonly IDalLogger _logger;

        public ArkaynCommand(SQLiteCommand cmd, string sql, IDalLogger logger)
        {
            _cmd = cmd;
            _cmd.CommandText = sql;
            _logger = logger;
        }

        public void AddParameters(params ArkaynParameter[] parameters)
        {
            foreach (var p in parameters)
                _cmd.Parameters.Add(new SQLiteParameter(p.Name, p.Value));
        }

        public async Task<int> ExecuteNonQueryAsync()
        {
            var start = DateTime.UtcNow;
            var result = await _cmd.ExecuteNonQueryAsync();
            _logger?.OnQueryExecuted(_cmd.CommandText, DateTime.UtcNow - start);
            return result;
        }

        public async Task<T> ExecuteScalarAsync<T>()
        {
            var start = DateTime.UtcNow;
            object value = await _cmd.ExecuteScalarAsync();
            _logger?.OnQueryExecuted(_cmd.CommandText, DateTime.UtcNow - start);

            if (value == null || value == DBNull.Value)
                return default;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public async Task<IArkaynReader> ExecuteReaderAsync()
        {
            var start = DateTime.UtcNow;
            var reader = await _cmd.ExecuteReaderAsync();
            _logger?.OnQueryExecuted(_cmd.CommandText, DateTime.UtcNow - start);

            return new ArkaynReader((SQLiteDataReader)reader);
        }

        // ⭐ NEW: synchronous reader implementation
        public IArkaynReader ExecuteReader()
        {
            var start = DateTime.UtcNow;
            var reader = _cmd.ExecuteReader();
            _logger?.OnQueryExecuted(_cmd.CommandText, DateTime.UtcNow - start);

            return new ArkaynReader(reader);
        }

        public void Dispose()
        {
            _cmd?.Dispose();
        }
    }


}
