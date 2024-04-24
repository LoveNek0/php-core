using System.Collections.Generic;
using System.Linq;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Data
{
    public class ASTArrayData : ASTNode
    {
        public readonly KeyValuePair<ASTNode, ASTNode>[] Values;
        
        internal ASTArrayData(TokenItem token, KeyValuePair<ASTNode, ASTNode>[] values) : base(token)
        {
            Values = values;
        }

        public override string ToString() => $"[{string.Join(", ", Values.Select((a) => $"{(a.Key != null ? $"{a.Key} => " : "")}{a.Value}"))}]";
    }
}