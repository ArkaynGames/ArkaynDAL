using ArkaynDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkaynDAL.Migrations
{
    public interface IMigration
    {
        int Order { get; }
        Task ApplyAsync(IArkaynConnection connection);
    }
}

