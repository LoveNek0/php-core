using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Loops
{

    public class ASTForConstruction : ASTNode
    {
        public readonly ASTNode InitialAction;
        public readonly ASTNode Condition;
        public readonly ASTNode PostAction;
        public readonly ASTNode Body;
        
        internal ASTForConstruction(TokenItem token, ASTNode initialAction, ASTNode condition, ASTNode postAction, ASTNode body) : base(token)
        {
            InitialAction = initialAction;
            Condition = condition;
            PostAction = postAction;
            Body = body;
        }

        public override string ToString() =>
            $"[for({(InitialAction == null ? "" : InitialAction.ToString())};{(Condition == null ? "" : Condition.ToString())};{(PostAction == null ? "" : PostAction.ToString())})\n{Body}]";
    }
}
