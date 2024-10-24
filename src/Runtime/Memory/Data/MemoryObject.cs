using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extreme.Mathematics;
using BigInteger = System.Numerics.BigInteger;

namespace PHP.Core.Runtime.Memory.Data
{
    public class MemoryObject : Memory
    {
        public MemoryObject() : base(DataType.Object)
        {
        }

        public MemoryObject(Memory value) : base(DataType.Object)
        {
            
        }

        public override MemoryBoolean ToMemoryBoolean()
        {
            throw new NotImplementedException();
        }

        public override MemoryInteger ToMemoryInteger()
        {
            throw new NotImplementedException();
        }

        public override MemoryFloat ToMemoryFloat()
        {
            throw new NotImplementedException();
        }

        public override MemoryString ToMemoryString()
        {
            throw new NotImplementedException();
        }

        public override MemoryArray ToMemoryArray()
        {
            throw new NotImplementedException();
        }

        public override MemoryObject ToMemoryObject()
        {
            throw new NotImplementedException();
        }

        public override bool ToBool()
        {
            throw new NotImplementedException();
        }

        public override ushort ToUShort()
        {
            throw new NotImplementedException();
        }

        public override short ToShort()
        {
            throw new NotImplementedException();
        }

        public override uint ToUInt()
        {
            throw new NotImplementedException();
        }

        public override int ToInt()
        {
            throw new NotImplementedException();
        }

        public override ulong ToULong()
        {
            throw new NotImplementedException();
        }

        public override long ToLong()
        {
            throw new NotImplementedException();
        }

        public override float ToFloat()
        {
            throw new NotImplementedException();
        }

        public override double ToDouble()
        {
            throw new NotImplementedException();
        }

        public override decimal ToDecimal()
        {
            throw new NotImplementedException();
        }

        public override BigInteger ToBigInteger()
        {
            throw new NotImplementedException();
        }

        public override BigFloat ToBigFloat()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override Memory GetValue(Memory key)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(Memory key, Memory value)
        {
            throw new NotImplementedException();
        }

        public override List<Memory> GetKeys()
        {
            throw new NotImplementedException();
        }

        public override KeyValuePair<Memory, Memory> GetKeyValuePairs()
        {
            throw new NotImplementedException();
        }

        public override int Length()
        {
            throw new NotImplementedException();
        }

        public override Memory Clone()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(Memory memory)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override string ToDumpString()
        {
            throw new NotImplementedException();
        }

        public override Memory Add(Memory right)
        {
            throw new NotImplementedException();
        }

        public override Memory Sub(Memory right)
        {
            throw new NotImplementedException();
        }

        public override Memory Mul(Memory right)
        {
            throw new NotImplementedException();
        }

        public override Memory Div(Memory right)
        {
            throw new NotImplementedException();
        }

        public override Memory Mod(Memory right)
        {
            throw new NotImplementedException();
        }

        public override Memory Pow(Memory right)
        {
            throw new NotImplementedException();
        }

        public override Memory Concat(Memory right)
        {
            throw new NotImplementedException();
        }
    }
}
