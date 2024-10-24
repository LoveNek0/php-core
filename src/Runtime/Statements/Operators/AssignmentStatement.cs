using PHP.Core.Runtime.Statements.Containers;

namespace PHP.Core.Runtime.Statements.Operators
{
    internal class AssignmentStatement: AbstractStatement
    {
        public readonly VariableStatement Variable;
        public readonly AbstractStatement Statement;

        public AssignmentStatement(VariableStatement variable, AbstractStatement statement)
        {
            Variable = variable;
            Statement = statement;
        }
    }
}