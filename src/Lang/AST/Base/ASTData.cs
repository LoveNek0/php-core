using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Base
{
    public abstract class ASTData : ASTNode
    {
        internal ASTData(TokenItem token) : base(token)
        {
        }

        public override string ToString() => Token.Data.ToString();
    }
}
