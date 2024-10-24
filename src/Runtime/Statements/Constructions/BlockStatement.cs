using System.Collections.Generic;

namespace PHP.Core.Runtime.Statements.Constructions
{
    internal class BlockStatement : AbstractStatement
    {
        public readonly List<AbstractStatement> Statements;

        public BlockStatement(List<AbstractStatement> statements) => Statements = statements;
    }
}