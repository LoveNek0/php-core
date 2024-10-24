using System.Collections.Generic;
using Extreme.Mathematics;
using PHP.Core.Runtime.Exceptions;
using BigInteger = System.Numerics.BigInteger;

namespace PHP.Core.Runtime.Memory.Data
{
    public class MemoryNull : Memory
    {
        public new static readonly MemoryNull Null = new MemoryNull();
        
        private MemoryNull() : base(DataType.Null, null)
        {
        }

        public override MemoryBoolean ToMemoryBoolean() => Memory.False;
        public override MemoryInteger ToMemoryInteger() => new MemoryInteger(0);
        public override MemoryFloat ToMemoryFloat() => new MemoryFloat(0);
        public override MemoryString ToMemoryString() => new MemoryString("");
        public override MemoryArray ToMemoryArray() => new MemoryArray(this);
        public override MemoryObject ToMemoryObject() => new MemoryObject(this);

        public override bool ToBool() => false;
        public override ushort ToUShort() => 0;
        public override short ToShort() => 0;
        public override uint ToUInt() => 0;
        public override int ToInt() => 0;
        public override ulong ToULong() => 0;
        public override long ToLong() => 0;
        public override float ToFloat() => 0;
        public override double ToDouble() => 0;
        public override decimal ToDecimal() => 0;
        public override BigInteger ToBigInteger() => 0;
        public override BigFloat ToBigFloat() => 0;
        public override string ToString() => "";

        public override Memory GetValue(Memory key) => throw new RuntimeException("Cannot use null as an array.");

        public override void SetValue(Memory key, Memory value) => throw new RuntimeException("Cannot use null as an array.");

        public override List<Memory> GetKeys() => throw new RuntimeException("Cannot use null as an array.");

        public override KeyValuePair<Memory, Memory> GetKeyValuePairs() => throw new RuntimeException("Cannot use null as an array.");

        public override int Length() => 0;
        public override Memory Clone() => this;
        
        public override MemoryBoolean Equals(Memory memory)
        {
            switch (memory.Type)
            {
                case DataType.Null:
                    return Memory.True;
                case DataType.Boolean:
                    return !memory.ToBool();
                case DataType.Integer:
                case DataType.Float:
                case DataType.String:
                case DataType.Array:
                    return memory.ToNative() == 0;
            }

            throw new RuntimeException("Incompatible type for comparison with null.");
        }

        public override Memory Add(Memory right)
        {
            switch (right.Type)
            {
                case Memory.DataType.Null:
                    return new MemoryInteger(0);
                case Memory.DataType.Boolean:
                    return right.ToMemoryInteger();
                case Memory.DataType.Integer:
                    return right.Clone();
                case DataType.Float:
                    return right.ToMemoryFloat().Clone();
            }

            throw new RuntimeException($"Incompatible types for addition ({this.Type} and {right.Type}).");
        }
        public override Memory Sub(Memory right)
        {
            switch (right.Type)
            {
                case Memory.DataType.Null:
                    return new MemoryInteger(0);
                case Memory.DataType.Boolean:
                case Memory.DataType.Integer:
                case DataType.Float:
                    return right.Mul(new MemoryInteger(-1));
            }
            
            throw new RuntimeException($"Incompatible types for subtraction ({this.Type} and {right.Type}).");
        }
        public override Memory Mul(Memory right)
        {
            switch (right.Type)
            {
                case Memory.DataType.Null:
                case Memory.DataType.Boolean:
                case Memory.DataType.Integer:
                case DataType.Float:
                    return new MemoryInteger(0);
            }
            
            throw new RuntimeException($"Incompatible types for multiplication ({this.Type} and {right.Type}).");
        }
        public override Memory Div(Memory right) => throw new RuntimeException("It is impossible to divide by zero.");
        public override Memory Mod(Memory right)
        {
            switch (right.Type)
            {
                case DataType.Null:
                case DataType.Boolean:
                case DataType.Integer:
                case DataType.Float:
                    return new MemoryInteger(0);
            }
            
            throw new RuntimeException($"Incompatible types for modulation ({this.Type} and {right.Type}).");
        }
        public override Memory Pow(Memory right)
        {
            switch (right.Type)
            {
                case DataType.Null:
                case DataType.Boolean:
                case DataType.Integer:
                case DataType.Float:
                    return new MemoryInteger(0);
            }
            
            throw new RuntimeException($"Incompatible type for exponentiation with null ({this.Type} and {right.Type}).");
        }
        public override Memory Concat(Memory right) => new MemoryString(this.ToString() + right.ToString());
    }
}