using ArkaynDAL.Interfaces;
using ArkaynDAL.Logging;
using ArkaynDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace ArkaynDAL.Core
{
    public class ArkaynConnection : IArkaynConnection
    {
        private readonly string _connectionString;
        private SQLiteConnection _connection;
        private readonly IDalLogger _logger;

        public ArkaynConnection(string connectionString, IDalLogger logger = null)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task OpenAsync()
        {
            _connection = new SQLiteConnection(_connectionString);
            await _connection.OpenAsync();

            _logger?.OnConnectionOpened();
        }

        public void Close()
        {
            _connection?.Close();
            _logger?.OnConnectionClosed();
        }

        public IArkaynCommand CreateCommand(string sql)
        {
            return new ArkaynCommand(_connection.CreateCommand(), sql, _logger);
        }

        public async Task<int> ExecuteNonQueryAsync(string sql, params ArkaynParameter[] parameters)
        {
            using var cmd = CreateCommand(sql);
            cmd.AddParameters(parameters);
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, params ArkaynParameter[] parameters)
        {
            using var cmd = CreateCommand(sql);
            cmd.AddParameters(parameters);
            return await cmd.ExecuteScalarAsync<T>();
        }

        public async Task<IArkaynReader> ExecuteReaderAsync(string sql, params ArkaynParameter[] parameters)
        {
            var cmd = CreateCommand(sql);
            cmd.AddParameters(parameters);
            return await cmd.ExecuteReaderAsync();
        }

        public SchemaVersion GetSchemaVersion()
        {
            const string sql = "SELECT MajorVersion, MinorVersion, BuildVersion FROM ProgramInfo WHERE ComponentName = 'Database Schema'";

            using var cmd = CreateCommand(sql);
            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
                return null;

            return new SchemaVersion
            {
                Major = reader.GetInt32(0),
                Minor = reader.GetInt32(1),
                Build = reader.GetInt32(2)
            };
        }

        public void SetSchemaVersion(SchemaVersion version)
        {
            const string sql = @"
            UPDATE ProgramInfo
            SET MajorVersion = @major,
                MinorVersion = @minor,
                BuildVersion = @build
            WHERE ComponentName = 'Database Schema'";

            ExecuteNonQueryAsync(sql,
                new ArkaynParameter("@major", version.Major),
                new ArkaynParameter("@minor", version.Minor),
                new ArkaynParameter("@build", version.Build)
            ).Wait();
        }

        public void Dispose()
        {
            Close();
            _connection?.Dispose();
        }
    }

}
