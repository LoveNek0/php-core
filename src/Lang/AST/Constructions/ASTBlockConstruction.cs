using System;
using System.Collections.Generic;
using System.Linq;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions
{
    public class ASTBlockConstruction : ASTNode
    {
        public readonly ASTNode[] Body;
        
        internal ASTBlockConstruction(TokenItem token, ASTNode[] body) : base(token)
        {
            Body = body;
        }

        public override string ToString() => $"{{\n{String.Join("\n", Body.Select(a => a))}\n}}";
    }
}
