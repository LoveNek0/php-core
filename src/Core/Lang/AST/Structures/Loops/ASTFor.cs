using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Structures.Loops;

public class ASTFor : ASTBlock
{
    public ASTNode? InitialAction => _initialAction;
    public ASTNode? Condition => _condition;
    public ASTNode? PostAction => _postAction;

    internal ASTNode? _initialAction = null;
    internal ASTNode? _condition = null;
    internal ASTNode? _postAction = null;
    
    internal ASTFor(TokenItem token) : base(token)
    {
    }

    public override string ToString() =>
        $"for({(_initialAction == null ? "" : _initialAction)};{(_condition == null ? "" : _condition)};{(_postAction == null ? "" : _postAction)}){base.ToString()}";
}