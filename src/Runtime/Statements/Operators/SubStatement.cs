namespace PHP.Core.Runtime.Statements.Operators
{
    internal class SubStatement: AbstractStatement
    {
        public readonly AbstractStatement Left;
        public readonly AbstractStatement Right;

        public SubStatement(AbstractStatement left, AbstractStatement right)
        {
            Left = left;
            Right = right;
        }
    }
}