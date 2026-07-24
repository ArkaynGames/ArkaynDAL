using System;

namespace ArkaynDAL.Interfaces
{
    public interface IArkaynReader : IDisposable
    {
        bool Read();

        int GetInt32(int index);
        string GetString(int index);
        bool IsDBNull(int index);

        object this[int index] { get; }

        int FieldCount { get; }
        string GetName(int index);
    }
}
