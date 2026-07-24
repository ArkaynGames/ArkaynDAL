using System;
using System.Collections.Generic;
using System.Text;

namespace ArkaynDAL.Core
{
    public class ArkaynParameter
    {
        public string Name { get; }
        public object Value { get; }

        public ArkaynParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }

}
