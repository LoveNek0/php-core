using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Data
{
    public class ASTNullData : ASTData
    {
        internal ASTNullData(TokenItem token) : base(token)
        {
        }
    }
}