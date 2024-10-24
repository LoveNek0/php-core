using PHP.Core.Runtime.Memory;

namespace PHP.Core.Runtime.Statements.Containers
{
    internal class DataStatement : AbstractStatement
    {
        public readonly Memory.Memory Data;

        public DataStatement(Memory.Memory data) => Data = data;

        public Memory.Memory Execute(MemoryBlock memory, Environment environment) => Data.Clone();
    }
}