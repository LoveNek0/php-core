namespace PHP.Core.Runtime.Statements.Operators
{
    internal class DivStatement
    {
        public readonly AbstractStatement Left;
        public readonly AbstractStatement Right;

        public DivStatement(AbstractStatement left, AbstractStatement right)
        {
            Left = left;
            Right = right;
        }
    }
}