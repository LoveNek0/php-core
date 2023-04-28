using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Runtime.Memory.Data
{
    public class MemoryString : MemoryData
    {
        private string value;
        public MemoryString(string value = "") : base(DataType.String) => this.value = value;

        public override MemoryData Clone() => new MemoryString(value);
        public override bool Equals(MemoryData data) => ToString().Equals(data.ToString());
        public override int GetHashCode() => this.value.GetHashCode();

        public override MemoryBoolean ToMemoryBoolean() => new MemoryBoolean(ToBool());
        public override MemoryInteger ToMemoryInteger() => new MemoryInteger(ToLong());
        public override MemoryFloat ToMemoryFloat() => new MemoryFloat(ToDecimal());
        public override MemoryString ToMemoryString() => this;
        public override MemoryArray ToMemoryArray() => new MemoryArray(this);

        public override bool ToBool() => (new string[] { "true", "1", "on", "yes" }).Contains(value.ToLower());
        public override short ToShort() => short.TryParse(value, out short o) ? o : (short)0;
        public override ushort ToUShort() => ushort.TryParse(value, out ushort o) ? o : (ushort)0;
        public override int ToInt() => int.TryParse(value, out int o) ? o : (int)0;
        public override uint ToUInt() => uint.TryParse(value, out uint o) ? o : (uint)0;
        public override long ToLong() => long.TryParse(value, out long o) ? o : (long)0;
        public override ulong ToULong() => ulong.TryParse(value, out ulong o) ? o : (ulong)0;
        public override float ToFloat() => float.TryParse(value, out float o) ? o : (float)0;
        public override double ToDouble() => double.TryParse(value, out double o) ? o : (double)0;
        public override decimal ToDecimal() => decimal.TryParse(value, out decimal o) ? o : (decimal)0;
        public override string ToString() => value;
        public override IReadOnlyDictionary<MemoryData, MemoryData> ToDictionary() => ToMemoryArray().ToDictionary();
    }
}
