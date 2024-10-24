using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Runtime.Memory.Data
{
    public class MemoryArray : Memory
    {
        private Dictionary<Memory, Memory> _values;
        public MemoryArray() : base(DataType.Array) => _values = new Dictionary<Memory, Memory>();
        public MemoryArray(KeyValuePair<Memory, Memory>[] values) : base(DataType.Array)
        {
            
        }

        public MemoryArray(Memory value) : base(DataType.Array)
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
    }
}
