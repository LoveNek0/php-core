namespace PHP.Core.Runtime.Statements.Operations
{
    internal class EchoStatement: AbstractStatement
    {
        public readonly AbstractStatement Statement;

        public EchoStatement(AbstractStatement statement) => Statement = statement;
    }
}