using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionSupport
{
    [Serializable()]
    public class Terminal
    {
        public object Value { get; set; }
        public Type Type { get; set; }
        public string Name { get; set; }
        public Terminal(string _name,object _value, Type _type)
        {
            this.Name = _name;
            this.Value = _value;
            this.Type = _type;
        }
    }
}
