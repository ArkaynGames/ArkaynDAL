using System;
using System.Data.SQLite;
using ArkaynDAL.Interfaces;

namespace ArkaynDAL.Core
{
    public class ArkaynReader : IArkaynReader
    {
        private readonly SQLiteDataReader _reader;

        public ArkaynReader(SQLiteDataReader reader)
        {
            _reader = reader;
        }

        public bool Read() => _reader.Read();

        public int GetInt32(int index) => _reader.GetInt32(index);
        public string GetString(int index) => _reader.GetString(index);
        public bool IsDBNull(int index) => _reader.IsDBNull(index);

        public object this[int index] => _reader[index];

        public int FieldCount => _reader.FieldCount;

        public string GetName(int index) => _reader.GetName(index);

        public void Dispose()
        {
            _reader?.Dispose();
        }
    }
}

