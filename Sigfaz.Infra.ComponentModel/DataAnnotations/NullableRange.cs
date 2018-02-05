using System;
using System.ComponentModel.DataAnnotations;

namespace Sigfaz.Infra.ComponentModel.DataAnnotations
{
    public class NullableRange : RangeAttribute
    {
        public NullableRange(double minimum, double maximum)
            : base(minimum, maximum)
        { 
        }

        public NullableRange(int minimum, int maximum)
            : base(minimum, maximum)
        { 
        }

        public NullableRange(Type type, string minimum, string maximum)
            : base(type, minimum, maximum)
        {
        }

        public new object Maximum { get { return base.Maximum; } }
        
        public new object Minimum { get { return base.Minimum; } }
        
        public new Type OperandType { get { return base.OperandType; } }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            var tempInt = value as int?;
            if (tempInt != null && !tempInt.HasValue)
                return true;

            var tempDouble = value as double?;
            if (tempDouble != null && !tempDouble.HasValue)
                return true;

            return base.IsValid(value);
        }
    }
}
