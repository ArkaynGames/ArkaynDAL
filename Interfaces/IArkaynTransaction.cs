using System;

namespace ArkaynDAL.Interfaces
{
    public interface IArkaynTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
