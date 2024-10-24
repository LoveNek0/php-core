namespace PHP.Core.Runtime.Statements.Operators
{
    internal class ConcatStatement
    {
        public readonly AbstractStatement Left;
        public readonly AbstractStatement Right;

        public ConcatStatement(AbstractStatement left, AbstractStatement right)
        {
            Left = left;
            Right = right;
        }
    }
}