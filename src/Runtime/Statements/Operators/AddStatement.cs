using PHP.Core.Runtime.Memory;
using PHP.Core.Runtime.Memory.Data;

namespace PHP.Core.Runtime.Statements.Operators
{
    internal class AddStatement : AbstractStatement
    {
        public readonly AbstractStatement Left;
        public readonly AbstractStatement Right;

        public AddStatement(AbstractStatement left, AbstractStatement right)
        {
            Left = left;
            Right = right;
        }
    }
}