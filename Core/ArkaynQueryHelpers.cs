using ArkaynDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArkaynDAL.Core
{
    public static class ArkaynQueryHelpers
    {
        public static async Task<T> ExecuteSingleAsync<T>(
            IArkaynConnection conn,
            string sql,
            params ArkaynParameter[] parameters)
            where T : new()
        {
            using var reader = await conn.ExecuteReaderAsync(sql, parameters);

            if (!reader.Read())
                return default;

            return ArkaynMapper.MapReaderToModel<T>(reader);
        }

        public static async Task<List<T>> ExecuteListAsync<T>(
            IArkaynConnection conn,
            string sql,
            params ArkaynParameter[] parameters)
            where T : new()
        {
            var list = new List<T>();

            using var reader = await conn.ExecuteReaderAsync(sql, parameters);

            while (reader.Read())
            {
                list.Add(ArkaynMapper.MapReaderToModel<T>(reader));
            }

            return list;
        }

        public static async Task<bool> ExecuteExistsAsync(
            IArkaynConnection conn,
            string sql,
            params ArkaynParameter[] parameters)
        {
            var result = await conn.ExecuteScalarAsync<object>(sql, parameters);
            return result != null && result != DBNull.Value;
        }

        public static async Task<T> ExecuteScalarOrDefaultAsync<T>(
            IArkaynConnection conn,
            string sql,
            params ArkaynParameter[] parameters)
        {
            var result = await conn.ExecuteScalarAsync<object>(sql, parameters);

            if (result == null || result == DBNull.Value)
                return default;

            return (T)Convert.ChangeType(result, typeof(T));
        }
    }
}
