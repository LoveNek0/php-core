namespace PHP.Core.Runtime.Statements.Operators
{
    internal class ModStatement
    {
        public readonly AbstractStatement Left;
        public readonly AbstractStatement Right;

        public ModStatement(AbstractStatement left, AbstractStatement right)
        {
            Left = left;
            Right = right;
        }
    }
}