using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Structures.Function;

public class ASTFunctionArgument
{
    public TokenItem? Type => _type;
    public bool IsPointer => _isPointer;
    public TokenItem Variable => _variable;
    public ASTNode? Default => _default;
    
    internal TokenItem? _type = null;
    internal bool _isPointer = false;
    internal TokenItem _variable;
    internal ASTNode? _default = null;

    public override string ToString() =>
        $"{(_type != null ? _type.Data + " " : "")}{(_isPointer ? "& " : "")}{_variable.Data}{(_default != null ? " = " + _default : "")}";
}