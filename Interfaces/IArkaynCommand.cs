using ArkaynDAL.Core;
using System.Threading.Tasks;

namespace ArkaynDAL.Interfaces
{
    public interface IArkaynCommand : IDisposable
    {
        void AddParameters(params ArkaynParameter[] parameters);

        Task<int> ExecuteNonQueryAsync();
        Task<T> ExecuteScalarAsync<T>();
        Task<IArkaynReader> ExecuteReaderAsync();
        IArkaynReader ExecuteReader();
    }
}


