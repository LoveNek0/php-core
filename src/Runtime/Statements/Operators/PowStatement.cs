namespace PHP.Core.Runtime.Statements.Operators
{
    internal class PowStatement
    {
        public readonly AbstractStatement Left;
        public readonly AbstractStatement Right;

        public PowStatement(AbstractStatement left, AbstractStatement right)
        {
            Left = left;
            Right = right;
        }
    }
}