using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core.Lang;
using PHP.Core.Lang.AST;
using PHP.Core.Lang.Tokens;
using PHP.Core.Runtime.Memory.Data;
using PHP.Core.Runtime.Statements;
using PHP.Core.Runtime.Statements.Constructions;
using PHP.Core.Runtime.Statements.Containers;
using PHP.Core.Runtime.Statements.Operations;
using PHP.Core.Runtime.Statements.Operators;

namespace PHP.Core.Runtime
{
    public class Launcher
    {
        private static void Test(string file)
        {
            string path = @"..\..\tests\" + file;
            Console.WriteLine($"### Testing > {file}");
            Tokenizer tokenizer = new Tokenizer(File.ReadAllText(path));
            TokenItem[] tokens = tokenizer.GetTokens();
            //Console.WriteLine("Testing Tokenizer...");
            //foreach (TokenItem token in tokens)
            //    Console.WriteLine(token);
            //Console.WriteLine("Testing ASTBuilder...");
            ASTBuilder builder = new ASTBuilder(tokens);
            ASTRoot root = builder.Build();
            Console.WriteLine(root);
            Console.WriteLine($"### End for < {file}");
        }
        public static void Main(string[] args)
        {
            /*Test("expressions.php");
            Test("functions.php");
            Test("constructions.php");*/
            DataStatement a = new DataStatement(new MemoryInteger(10));
            DataStatement b = new DataStatement(new MemoryFloat(18.2));
            AddStatement c = new AddStatement(a, b);
            VariableStatement var = new VariableStatement("$result");
            AssignmentStatement assignmentStatement = new AssignmentStatement(var, c);
            VariableStatement var2 = new VariableStatement("$result");
            EchoStatement echoStatement1 = new EchoStatement(var2);
            EchoStatement echoStatement2 = new EchoStatement(new DataStatement(new MemoryString("\n")));
            ReturnStatement d = new ReturnStatement(var2);
            BlockStatement blockStatement = new BlockStatement(new List<AbstractStatement> {assignmentStatement, echoStatement1, echoStatement2, d});
            
            Environment environment = new Environment();
            
            int result = environment.Execute(blockStatement);
            Console.WriteLine(result);
        }
    }
}
