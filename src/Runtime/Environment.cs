using System;
using System.Collections.Generic;
using System.IO;
using PHP.Core.Runtime.Exceptions;
using PHP.Core.Runtime.Memory;
using PHP.Core.Runtime.Memory.Data;
using PHP.Core.Runtime.Statements;
using PHP.Core.Runtime.Statements.Operations;
using PHP.Core.Runtime.Statements.Operators;

namespace PHP.Core.Runtime
{
    public class Environment
    {
        public TextReader StdIn = Console.In;
        public TextWriter StdOut = Console.Out;

        public Dictionary<string, Memory.Memory> Constraints = new Dictionary<string, Memory.Memory>();
        
        
        
        public Environment()
        {
            
        }

        internal int Execute(AbstractStatement statement)
        {
            Executor executor = new Executor(this);
            return executor.Exec(statement);
        }
        
        

        
    }
}