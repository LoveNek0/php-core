using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Runtime.Memory.Data
{
    public class MemoryInteger : MemoryData
    {
        private dynamic value;
        public MemoryInteger(short value) : base(DataType.Integer) => this.value = value;
        public MemoryInteger(ushort value) : base(DataType.Integer) => this.value = value;
        public MemoryInteger(int value) : base(DataType.Integer) => this.value = value;
        public MemoryInteger(uint value) : base(DataType.Integer) => this.value = value;
        public MemoryInteger(long value) : base(DataType.Integer) => this.value = value;
        public MemoryInteger(ulong value) : base(DataType.Integer) => this.value = value;

        public override MemoryData Clone() => new MemoryInteger(this.value);
        public override bool Equals(MemoryData data)
        {
            switch (data.Type)
            {
                case DataType.Boolean:
                    return this.ToLong() == data.ToLong();
                case DataType.Integer:
                    return this.ToLong() == data.ToLong();
                case DataType.Float:
                    return this.ToDecimal() == data.ToDecimal();
                case DataType.String:
                    return this.ToString() == data.ToString();
                case DataType.Array:
                    return this.ToMemoryArray().Equals(data.ToMemoryArray());
            }
            return false;
        }
        public override int GetHashCode() => this.value.GetHashCode();

        public override MemoryBoolean ToMemoryBoolean() => new MemoryBoolean(ToBool());
        public override MemoryInteger ToMemoryInteger() => this;
        public override MemoryFloat ToMemoryFloat() => new MemoryFloat(ToDecimal());
        public override MemoryString ToMemoryString() => new MemoryString(value.ToString());
        public override MemoryArray ToMemoryArray() => new MemoryArray(this);

        public override bool ToBool() => value != 0;
        public override short ToShort() => value;
        public override ushort ToUShort() => value;
        public override int ToInt() => value;
        public override uint ToUInt() => value;
        public override long ToLong() => value;
        public override ulong ToULong() => value;
        public override float ToFloat() => value;
        public override double ToDouble() => value;
        public override decimal ToDecimal() => value;
        public override string ToString() => value.ToString();
        public override IReadOnlyDictionary<MemoryData, MemoryData> ToDictionary() => ToMemoryArray().ToDictionary();
    }
}
