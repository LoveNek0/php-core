using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Runtime.Memory.Data
{
    public class MemoryFloat : MemoryData
    {
        private dynamic value;
        public MemoryFloat(float value) : base(DataType.Float) => this.value = value;
        public MemoryFloat(double value) : base(DataType.Float)
        {
            if (value >= float.MinValue && value <= float.MaxValue)
                this.value = (float)value;
            else
                this.value = value;
        }
        public MemoryFloat(decimal value) : base(DataType.Float)
        {
            if (value >= Convert.ToDecimal(float.MinValue) && value <= Convert.ToDecimal(float.MaxValue))
                this.value = (float)value;
            else
                this.value = Convert.ToDecimal(value);
        }

        public override bool Equals(MemoryData data) => this.ToDecimal() == data.ToDecimal();
        public override MemoryData Clone() => new MemoryFloat(this.value);
        public override int GetHashCode() => this.value.GetHashCode();

        public override MemoryBoolean ToMemoryBoolean() => new MemoryBoolean(ToBool());
        public override MemoryInteger ToMemoryInteger() => new MemoryInteger(ToLong());
        public override MemoryFloat ToMemoryFloat() => this;
        public override MemoryString ToMemoryString() => new MemoryString(ToString());
        public override MemoryArray ToMemoryArray() => new MemoryArray(this);

        public override bool ToBool() => value == 0;
        public override short ToShort() => (short)value;
        public override ushort ToUShort() => (ushort)value;
        public override int ToInt() => (int)value;
        public override uint ToUInt() => (uint)value;
        public override long ToLong() => (long)value;
        public override ulong ToULong() => (ulong)value;
        public override float ToFloat() => (float)value;
        public override double ToDouble() => (double)value;
        public override decimal ToDecimal() => (decimal)value;
        public override string ToString() => value.ToString();
        public override IReadOnlyDictionary<MemoryData, MemoryData> ToDictionary() => ToMemoryArray().ToDictionary();

    }
}
