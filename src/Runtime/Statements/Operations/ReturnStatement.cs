using PHP.Core.Runtime.Memory;

namespace PHP.Core.Runtime.Statements.Operations
{
    internal class ReturnStatement : AbstractStatement
    {
        public readonly AbstractStatement Statement;

        public ReturnStatement(AbstractStatement statement) => Statement = statement;
    }
}