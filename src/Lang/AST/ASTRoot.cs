using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core.Lang.AST.Base;

namespace PHP.Core.Lang.AST
{
    public class ASTRoot
    {
        public ASTNode[] Children => _children.ToArray();

        internal List<ASTNode> _children = new List<ASTNode>();

        internal ASTRoot()
        {
        }
        
        public override string ToString() => String.Join("\n", _children);
    }
}
