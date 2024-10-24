using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extreme.Mathematics;
using PHP.Core.Runtime.Exceptions;
using BigInteger = System.Numerics.BigInteger;

namespace PHP.Core.Runtime.Memory.Data
{
    public class MemoryBoolean : Memory
    {
        public new static readonly MemoryBoolean True = new MemoryBoolean(true);
        public new static readonly MemoryBoolean False = new MemoryBoolean(false);
        
        private MemoryBoolean(bool value) : base(DataType.Boolean, value){}

        public override MemoryBoolean ToMemoryBoolean() => this;
        public override MemoryInteger ToMemoryInteger() => new MemoryInteger(this.Data ? 1 : 0);
        public override MemoryFloat ToMemoryFloat() => new MemoryFloat(this.Data ? 1.0f : 0.0f);
        public override MemoryString ToMemoryString() => new MemoryString(this.Data ? "true" : "false");
        public override MemoryArray ToMemoryArray() => new MemoryArray(this.Clone());
        public override MemoryObject ToMemoryObject() => new MemoryObject(this.Clone());

        public override bool ToBool() => this.Data;
        public override ushort ToUShort() => (ushort)(this.Data ? 1 : 0);
        public override short ToShort() => (short)(this.Data ? 1 : 0);
        public override uint ToUInt() => (uint)(this.Data ? 1 : 0);
        public override int ToInt() => this.Data ? 1 : 0;
        public override ulong ToULong() => (ulong)(this.Data ? 1 : 0);
        public override long ToLong() => this.Data ? 1 : 0;
        public override float ToFloat() => ToInt();
        public override double ToDouble() => ToInt();
        public override decimal ToDecimal() => ToInt();
        public override BigInteger ToBigInteger() => ToInt();
        public override BigFloat ToBigFloat() => ToInt();
        public override string ToString() => this.Data ? "true" : "false";
        
        public override Memory GetValue(Memory key) => throw new RuntimeException("Cannot use boolean as an array.");
        public override void SetValue(Memory key, Memory value) => throw new RuntimeException("Cannot use boolean as an array.");
        public override List<Memory> GetKeys() => throw new RuntimeException("Cannot use boolean as an array.");
        public override KeyValuePair<Memory, Memory> GetKeyValuePairs() => throw new RuntimeException("Cannot use boolean as an array.");

        public override int Length() => ToInt();

        public override Memory Clone() => this;
        public override MemoryBoolean Equals(Memory memory) => Data == memory.ToBool();

        public override Memory Add(Memory right)
        {
            switch (right.Type)
            {
                case DataType.Null:
                    return this.ToMemoryInteger();
                case DataType.Boolean:
                    return new MemoryInteger(this.ToInt() + right.ToInt());
                case DataType.Integer:
                    return ToMemoryInteger().Add(right);
                case DataType.Float:
                    return ToMemoryFloat().Add(right);
            }
            
            throw new RuntimeException($"Incompatible types for addition ({this.Type} and {right.Type}).");
        }
        public override Memory Sub(Memory right)
        {
            switch (right.Type)
            {
                case DataType.Null:
                    return this.ToMemoryInteger();
                case DataType.Boolean:
                    return new MemoryInteger(this.ToInt() - right.ToInt());
                case DataType.Integer:
                    return ToMemoryInteger().Sub(right);
                case DataType.Float:
                    return ToMemoryFloat().Sub(right);
            }
            
            throw new RuntimeException($"Incompatible types for substraction ({this.Type} and {right.Type}).");
        }
        public override Memory Mul(Memory right)
        {
            switch (right.Type)
            {
                case DataType.Null:
                    return this.ToMemoryInteger();
                case DataType.Boolean:
                    return new MemoryInteger(this.ToInt() * right.ToInt());
                case DataType.Integer:
                    return ToMemoryInteger().Pow(right);
                case DataType.Float:
                    return ToMemoryFloat().Pow(right);
            }
            
            throw new RuntimeException($"Incompatible types for multiplication ({this.Type} and {right.Type}).");
        }
        public override Memory Div(Memory right)
        {
            switch (right.Type)
            {
                case DataType.Null:
                    throw new RuntimeException("It is impossible to divide by zero.");
                case DataType.Boolean:
                    if(right.ToInt() == 0)
                        throw new RuntimeException("It is impossible to divide by zero.");
                    return new MemoryInteger(this.ToInt() / right.ToInt());
                case DataType.Integer:
                    return ToMemoryInteger().Div(right);
                case DataType.Float:
                    return ToMemoryFloat().Div(right);
            }
            
            throw new RuntimeException($"Incompatible types for division ({this.Type} and {right.Type}).");
        }
        public override Memory Mod(Memory right)
        {
            switch (right.Type)
            {
                case DataType.Null:
                case DataType.Boolean:
                case DataType.Integer:
                    return ToMemoryInteger().Mod(right);
                case DataType.Float:
                    return ToMemoryFloat().Mod(right);
            }
            
            throw new RuntimeException($"Incompatible types for modulo ({this.Type} and {right.Type}).");
        }
        public override Memory Pow(Memory right)
        {
            switch (right.Type)
            {
                case DataType.Null:
                case DataType.Boolean:
                case DataType.Integer:
                    return ToMemoryInteger().Pow(right);
                case DataType.Float:
                    return ToMemoryFloat().Pow(right);
            }
            
            throw new RuntimeException($"Incompatible types for exponentiation ({this.Type} and {right.Type}).");
        }
        public override Memory Concat(Memory right) => new MemoryString(ToString() + right.ToString());
        
        public static implicit operator MemoryBoolean(bool value) => value ? True : False;
        public static bool operator ==(MemoryBoolean left, bool right) => left.Data == right;
        public static bool operator !=(MemoryBoolean left, bool right) => left != right;
    }
}
