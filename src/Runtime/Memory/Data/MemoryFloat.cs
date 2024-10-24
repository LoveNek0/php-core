using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Runtime.Memory.Data
{
    public class MemoryFloat : Memory
    {
        private double _value;

        public MemoryFloat() : base(DataType.Float) => this._value = 0;
        public MemoryFloat(float value) : base(DataType.Float) => this._value = value;
        public MemoryFloat(double value) : base(DataType.Float) => this._value = value;

        public override MemoryBoolean ToMemoryBoolean() => new MemoryBoolean(this._value != 0);

        public override MemoryInteger ToMemoryInteger() => new MemoryInteger((long)this._value);

        public override MemoryFloat ToMemoryFloat() => this;

        public override MemoryString ToMemoryString() => new MemoryString(this._value.ToString(CultureInfo.InvariantCulture));

        public override MemoryArray ToMemoryArray() => new MemoryArray(this.Clone());

        public override MemoryObject ToMemoryObject() => new MemoryObject(this.Clone());
        public override bool ToBool() => _value != 0;

        public override int ToInt() => (int)_value;

        public override float ToFloat() => (float)_value;

        public override string ToString() => _value.ToString(CultureInfo.InvariantCulture);

        public override Memory Clone() => new MemoryFloat(this._value);
        public override bool Equals(Memory memory)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override string ToDumpString() => this._value.ToString(CultureInfo.InvariantCulture);
        public override Memory Add(Memory right)
        {
            throw new NotImplementedException();
        }
    }
}
