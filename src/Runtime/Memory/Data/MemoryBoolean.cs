using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Runtime.Memory.Data
{
    public class MemoryBoolean : MemoryData
    {
        private bool value;
        public MemoryBoolean(bool value) : base(DataType.Boolean) => this.value = value;

        public override MemoryData Clone() => new MemoryBoolean(this.value);
        public override bool Equals(MemoryData data) => value == data.ToBool();
        public override int GetHashCode() => value.GetHashCode();

        public override MemoryBoolean ToMemoryBoolean() => this;
        public override MemoryInteger ToMemoryInteger() => new MemoryInteger(ToLong());
        public override MemoryFloat ToMemoryFloat() => new MemoryFloat(ToDecimal());
        public override MemoryString ToMemoryString() => new MemoryString(ToString());
        public override MemoryArray ToMemoryArray() => new MemoryArray(this);

        public override bool ToBool() => value;
        public override short ToShort() => (short)(value ? 1 : 0);
        public override ushort ToUShort() => (ushort)(value ? 1 : 0);
        public override int ToInt() => (int)(value ? 1 : 0);
        public override uint ToUInt() => (uint)(value ? 1 : 0);
        public override long ToLong() => (long)(value ? 1 : 0);
        public override ulong ToULong() => (ulong)(value ? 1 : 0);
        public override float ToFloat() => (float)(value ? 1 : 0);
        public override double ToDouble() => (double)(value ? 1 : 0);
        public override decimal ToDecimal() => (decimal)(value ? 1 : 0);
        public override string ToString() => value ? "true" : "false";
        public override IReadOnlyDictionary<MemoryData, MemoryData> ToDictionary() => ToMemoryArray().ToDictionary();
    }
}
