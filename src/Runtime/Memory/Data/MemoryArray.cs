using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Runtime.Memory.Data
{
    public class MemoryArray : MemoryData
    {
        private Dictionary<MemoryData, MemoryData> values = new Dictionary<MemoryData, MemoryData>();
        public MemoryArray() : base(DataType.Array) { }
        public MemoryArray(params MemoryData[] values) : base(DataType.Array)
        {
            foreach (var value in values)
                Add(value);
        }
        public MemoryArray(params KeyValuePair<MemoryData, MemoryData>[] values) : base(DataType.Array)
        {
            foreach(var value in values)
                Set(value.Key, value.Value);
        }

        public MemoryData Get(MemoryData key) => values.TryGetValue(key, out var value) ? value : null;
        public void Set(MemoryData key, MemoryData value) => values.Add(key, value);
        public void Add(MemoryData value) => values.Add(new MemoryInteger(values.Count()), value);
        public long Size() => values.Count();

        public override bool Equals(MemoryData data)
        {
            if (this.Type != data.Type)
                return false;
            MemoryArray array = data.ToMemoryArray();
            if (array.Size() != this.Size())
                return false;
            var a = this.values.ToArray();
            var b = array.values.ToArray();
            for (long i = 0; i < a.Length; i++)
                if (!a[i].Key.Equals(b[i].Key) || !a[i].Value.Equals(b[i].Value))
                    return false;
            return true;
        }
        public override MemoryData Clone()
        {
            MemoryArray clone = new MemoryArray();
            foreach (var v in values)
                clone.Set(v.Key.Clone(), v.Value.Clone());
            return Clone();
        }
        public override int GetHashCode() => this.values.GetHashCode();

        public override MemoryBoolean ToMemoryBoolean() => new MemoryBoolean(ToBool());
        public override MemoryInteger ToMemoryInteger() => new MemoryInteger(ToLong());
        public override MemoryFloat ToMemoryFloat() => new MemoryFloat(ToDecimal());
        public override MemoryString ToMemoryString() => new MemoryString(ToString());
        public override MemoryArray ToMemoryArray() => this;

        public override bool ToBool() => values.Count > 0;
        public override short ToShort() => values.Count == 0 ? (short)0 : values.First().Value.ToShort();
        public override ushort ToUShort() => values.Count == 0 ? (ushort)0 : values.First().Value.ToUShort();
        public override int ToInt() => values.Count == 0 ? 0 : values.First().Value.ToInt();
        public override uint ToUInt() => values.Count == 0 ? 0 : values.First().Value.ToUInt();
        public override long ToLong() => values.Count == 0 ? 0 : values.First().Value.ToLong();
        public override ulong ToULong()=> values.Count == 0 ? 0 : values.First().Value.ToULong();
        public override float ToFloat() => values.Count == 0 ? 0 : values.First().Value.ToInt();
        public override double ToDouble() => values.Count == 0 ? 0 : values.First().Value.ToInt();
        public override decimal ToDecimal() => values.Count == 0 ? 0 : values.First().Value.ToInt();
        public override string ToString() => values.Count == 0 ? "" : values.First().Value.ToString();
        public override IReadOnlyDictionary<MemoryData, MemoryData> ToDictionary() => values;
    }
}
