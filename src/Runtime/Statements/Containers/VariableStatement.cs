namespace PHP.Core.Runtime.Statements.Containers
{
    public class VariableStatement: AbstractStatement
    {
        public readonly string Name;

        public VariableStatement(string name) => Name = name;
    }
}