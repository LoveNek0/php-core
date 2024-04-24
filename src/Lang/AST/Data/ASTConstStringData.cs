using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Data
{
    public class ASTConstStringData : ASTData
    {
        internal ASTConstStringData(TokenItem token) : base(token)
        {
        }
    }
}