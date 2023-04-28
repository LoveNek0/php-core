using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Runtime.Memory.Data
{
    public class MemoryObject : MemoryData
    {
        public MemoryObject(DataType type) : base(type)
        {
        }

        public override MemoryData Clone()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(MemoryData data)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool ToBool()
        {
            throw new NotImplementedException();
        }

        public override decimal ToDecimal()
        {
            throw new NotImplementedException();
        }

        public override IReadOnlyDictionary<MemoryData, MemoryData> ToDictionary()
        {
            throw new NotImplementedException();
        }

        public override double ToDouble()
        {
            throw new NotImplementedException();
        }

        public override float ToFloat()
        {
            throw new NotImplementedException();
        }

        public override int ToInt()
        {
            throw new NotImplementedException();
        }

        public override long ToLong()
        {
            throw new NotImplementedException();
        }

        public override MemoryArray ToMemoryArray()
        {
            throw new NotImplementedException();
        }

        public override MemoryBoolean ToMemoryBoolean()
        {
            throw new NotImplementedException();
        }

        public override MemoryFloat ToMemoryFloat()
        {
            throw new NotImplementedException();
        }

        public override MemoryInteger ToMemoryInteger()
        {
            throw new NotImplementedException();
        }

        public override MemoryString ToMemoryString()
        {
            throw new NotImplementedException();
        }

        public override short ToShort()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override uint ToUInt()
        {
            throw new NotImplementedException();
        }

        public override ulong ToULong()
        {
            throw new NotImplementedException();
        }

        public override ushort ToUShort()
        {
            throw new NotImplementedException();
        }
    }
}
