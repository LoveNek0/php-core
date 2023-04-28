using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Structures.Function;

public class ASTFunction : ASTNode
{
    public TokenItem Name => _name;
    public ASTFunctionArgument[] Arguments => _arguments.ToArray();
    public TokenItem? ReturnType => _returnType;
    public ASTNode[] Lines => _lines.ToArray();
    
    internal TokenItem _name;
    internal List<ASTFunctionArgument> _arguments = new List<ASTFunctionArgument>();
    internal TokenItem? _returnType;
    internal List<ASTNode> _lines = new List<ASTNode>();
    
    internal ASTFunction(TokenItem token) : base(token)
    {
    }

    public override string ToString() =>
        $"function {_name.Data} ({String.Join(", ", _arguments)}){{\n{String.Join("\n", _lines)}\n}}";
}