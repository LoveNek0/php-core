using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Structures.Loops;

public class ASTForeach : ASTBlock
{
    public ASTNode Container => _container;
    public ASTNode? Key => _key;
    public ASTNode Value => _value;

    internal ASTNode _container;
    internal ASTNode? _key = null;
    internal ASTNode _value;
    
    internal ASTForeach(TokenItem token) : base(token)
    {
    }

    public override string ToString() =>
        $"foreach({_container} as {(_key == null ? "" : _key + " => ")}{_value}{base.ToString()}";
}