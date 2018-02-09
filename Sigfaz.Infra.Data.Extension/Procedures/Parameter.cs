using System;
using System.Data;

namespace Sigfaz.Infra.Data.Extension.Procedures
{
    public class Parameter
    {
        public string Name { get; private set; }
        public object Value { get; set; }
        public ParameterDirection Direction { get; private set; }
        public Type Type { get; private set; }
        public int Size { get; private set; }
        public bool Success { get; set; }

        public Parameter(string name, object value, ParameterDirection direction, Type type, int size = 0)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
            this.Name = name;
            this.Value = value;
            this.Direction = direction;
            this.Type = type;
            this.Size = size;
            this.Success = false;
        }
    }
}
