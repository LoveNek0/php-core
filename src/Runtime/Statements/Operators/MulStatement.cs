namespace PHP.Core.Runtime.Statements.Operators
{
    internal class MulStatement
    {
        public readonly AbstractStatement Left;
        public readonly AbstractStatement Right;

        public MulStatement(AbstractStatement left, AbstractStatement right)
        {
            Left = left;
            Right = right;
        }
    }
}