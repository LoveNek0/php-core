using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST
{
    public class ASTData : ASTNode
    {
        internal ASTData(TokenItem token) : base(token)
        {
        }
    }
}
