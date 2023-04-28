using PHP.Runtime.Exceptions;
using PHP.Runtime.Memory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Runtime.Memory
{
    internal class MemoryBlock
    {
        public class Pointer
        {
            public MemoryData? Data;

            public Pointer(MemoryData? data = null) => this.Data = data;

            public override string ToString() => $"({Data})";
        }

        private Dictionary<string, Pointer> variables = new Dictionary<string, Pointer>();
        private MemoryBlock parent;
        public MemoryBlock(MemoryBlock parent = null) => this.parent = parent;

        public Pointer? GetPointer(string name)
        {
            if(variables.TryGetValue(name, out Pointer pointer))
                return pointer;
            if (parent != null && parent.variables.TryGetValue(name, out pointer))
                return pointer;
            return null;
        }
        public MemoryData? GetData(string name)
        {
            Pointer? p = GetPointer(name);
            if (p == null)
                throw new RuntimeException($"Trying to get value from variable \"{name}\" but it's not defined.");
            return p.Data;
        }
        public void SetData(string name, MemoryData? data)
        {
            Pointer? p = GetPointer(name);
            if(p == null)
            {
                p = new Pointer();
                this.variables.Add(name, p);
            }
            p.Data = data;
        }
        public void SetPointer(string name, Pointer pointer)
        {
            if(this.variables.ContainsKey(name))
                this.variables[name] = pointer;
            else
                if(this.parent != null && this.parent.variables.ContainsKey(name))
                    this.parent.variables[name] = pointer;
        }
    }
}
