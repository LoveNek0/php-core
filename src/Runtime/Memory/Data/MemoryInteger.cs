using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Runtime.Memory.Data
{
    public class MemoryInteger : Memory
    {
        private long _value;

        public MemoryInteger() : base(DataType.Integer) => this._value = 0;
        public MemoryInteger(long value) : base(DataType.Integer) => this._value = value;

        public override MemoryBoolean ToMemoryBoolean() => new MemoryBoolean(this._value != 0);

        public override MemoryInteger ToMemoryInteger() => this;

        public override MemoryFloat ToMemoryFloat() => new MemoryFloat(this._value);

        public override MemoryString ToMemoryString() => new MemoryString(this._value.ToString(CultureInfo.InvariantCulture));

        public override MemoryArray ToMemoryArray() => new MemoryArray(this.Clone());

        public override MemoryObject ToMemoryObject() => new MemoryObject(this.Clone());
        public override bool ToBool()
        {
            throw new NotImplementedException();
        }

        public override int ToInt() => (int)_value;
        public override float ToFloat()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override Memory Clone() => new MemoryInteger(this._value);
        public override bool Equals(Memory memory)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Memory))
                return false;
            Memory other = obj as Memory;
            switch (other.Type)
            {
                case DataType.Integer:
                    return this._value == (other as MemoryInteger)._value;
                case DataType.Float:
                    //return this._value == (other as MemoryFloat)._value;
                default:
                    return false;
        
            }
        }

        public override string ToDumpString() => this._value.ToString(CultureInfo.InvariantCulture);
        public override Memory Add(Memory right)
        {
            switch (right.Type)
            {
                case DataType.Null:
                    return Clone();
                case DataType.Boolean:
                    return new MemoryInteger(((MemoryBoolean)right).ToInt() + _value);
                case DataType.Integer:
                    return new MemoryInteger(((MemoryInteger)right)._value + _value);
                case DataType.Float:
                    return new MemoryFloat(((MemoryFloat)right).ToFloat() + _value);
            }
            
            return Memory.Null;
        }
    }
}
