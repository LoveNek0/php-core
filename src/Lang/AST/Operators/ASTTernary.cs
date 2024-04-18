using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Operators
{
    public class ASTTernary : ASTNode
    {
        public readonly ASTNode Condition;
        public readonly ASTNode Left;
        public readonly ASTNode Right;

        public readonly TokenItem SecondToken;

        internal ASTTernary(TokenItem firstToken, TokenItem secondToken, ASTNode condition = null, ASTNode left = null, ASTNode right = null) : base(firstToken)
        {
            SecondToken = secondToken;
            Condition = condition;
            Left = left;
            Right = right;
        }

        public override string ToString() => $"({Condition} {Token.Data} {Left} {SecondToken.Data} {Right})";
    }
}