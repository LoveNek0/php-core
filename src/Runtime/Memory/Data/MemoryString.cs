using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Runtime.Memory.Data
{
    public class MemoryString : Memory
    {
        private string _value;

        public MemoryString() : base(DataType.String) => this._value = "";
        public MemoryString(string value) : base(DataType.String) => this._value = value;

        public override MemoryBoolean ToMemoryBoolean() =>
            new MemoryBoolean(new[] { "true", "1", "yes", "on" }.Contains(this._value.ToLower(CultureInfo.InvariantCulture)));
        
        public override MemoryInteger ToMemoryInteger() => new MemoryInteger(long.TryParse(this._value, CultureInfo.InvariantCulture, out long value) ? value : 0);

        public override MemoryFloat ToMemoryFloat() => new MemoryFloat(double.TryParse(this._value, CultureInfo.InvariantCulture, out double value) ? value : 0);

        public override MemoryString ToMemoryString() => this;

        public override MemoryArray ToMemoryArray() => new MemoryArray(this.Clone());
        
        public override MemoryObject ToMemoryObject() => new MemoryObject(this.Clone());
        public override bool ToBool()
        {
            throw new NotImplementedException();
        }

        public override int ToInt()
        {
            throw new NotImplementedException();
        }

        public override float ToFloat()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return this._value;
        }

        public override Memory Clone() => new MemoryString(this._value);
        public override bool Equals(Memory memory)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override string ToDumpString() => $"\"{this._value}\"";
        public override Memory Add(Memory right)
        {
            throw new NotImplementedException();
        }
    }
}
