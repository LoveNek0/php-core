using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST
{
    public class ASTRoot
    {
        public ASTNode[] Children => Children;

        internal List<ASTNode> _children = new List<ASTNode>();

        internal ASTRoot()
        {
        }
        
        public override string ToString() => String.Join("\n", _children);
    }
}
