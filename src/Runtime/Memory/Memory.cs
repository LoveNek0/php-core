using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata;
using Extreme.Mathematics;
using PHP.Core.Runtime.Exceptions;
using PHP.Core.Runtime.Memory.Data;
using BigInteger = System.Numerics.BigInteger;

namespace PHP.Core.Runtime.Memory
{
    public abstract class Memory
    {
        public enum DataType
        {
            Null,
            Boolean,
            Integer,
            Float,
            String,
            Array,
            Object
        }
        
        public static readonly MemoryNull Null = MemoryNull.Null;
        public static readonly MemoryBoolean True = MemoryBoolean.True;
        public static readonly MemoryBoolean False = MemoryBoolean.False;
        
        public readonly DataType Type;

        protected readonly dynamic Data;
        
        protected Memory(DataType type, dynamic data)
        {
            this.Type = type;
            this.Data = data;
        }

        //  Type conversion methods
        public abstract MemoryBoolean ToMemoryBoolean();
        public abstract MemoryInteger ToMemoryInteger();
        public abstract MemoryFloat ToMemoryFloat();
        public abstract MemoryString ToMemoryString();
        public abstract MemoryArray ToMemoryArray();
        public abstract MemoryObject ToMemoryObject();

        //  Native type getters
        public dynamic ToNative() => this.Data;
        public abstract bool ToBool();
        public abstract ushort ToUShort();
        public abstract short ToShort();
        public abstract uint ToUInt();
        public abstract int ToInt();
        public abstract ulong ToULong();
        public abstract long ToLong();
        public abstract float ToFloat();
        public abstract double ToDouble();
        public abstract decimal ToDecimal();
        public abstract BigInteger ToBigInteger();
        public abstract BigFloat ToBigFloat();
        public new abstract string ToString();
        
        //  Array and object operators
        public abstract Memory GetValue(Memory key);
        public abstract void SetValue(Memory key, Memory value);
        public abstract List<Memory> GetKeys();
        public abstract KeyValuePair<Memory, Memory> GetKeyValuePairs();

        public abstract int Length();
        public abstract Memory Clone();
        
        //  Basic operators
        public abstract MemoryBoolean Equals(Memory memory);
        public MemoryBoolean NotEquals(Memory memory) =>
            Equals(memory) == Memory.True;
        public MemoryBoolean Identical(Memory memory) => 
            Type == memory.Type && Equals(memory) == Memory.True;
        
        public abstract Memory Add(Memory right);
        public abstract Memory Sub(Memory right);
        public abstract Memory Mul(Memory right);
        public abstract Memory Div(Memory right);
        public abstract Memory Mod(Memory right);
        public abstract Memory Pow(Memory right);
        public abstract Memory Concat(Memory right);

        public static Memory Add(Memory left, Memory right) => left.Add(right);
        public static Memory Sub(Memory left, Memory right) => left.Sub(right);
        public static Memory Mul(Memory left, Memory right) => left.Mul(right);
        public static Memory Div(Memory left, Memory right) => left.Div(right);
        public static Memory Mod(Memory left, Memory right) => left.Mod(right);
        public static Memory Pow(Memory left, Memory right) => left.Pow(right);
        public static Memory Concat(Memory left, Memory right) => left.Concat(right);
    }
}