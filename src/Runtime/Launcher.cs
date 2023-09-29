using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core.Lang;
using PHP.Core.Lang.AST;
using PHP.Core.Lang.Tokens;
using PHP.Runtime.Memory;
using PHP.Runtime.Memory.Data;

namespace PHP.Runtime
{
    public class Launcher
    {
        private static void Test(string file)
        {
            string path = @".\tests\" + file;
            Console.WriteLine($"Testing > {file}");
            TokenItem[] tokens = Lexer.Tokenize(File.ReadAllText(path));
            foreach (TokenItem token in tokens)
                Console.WriteLine(token);
            Console.WriteLine($"End for > {file}");
        }
        public static void Main(string[] args)
        {
            Test("expressions.php");
            //Test("dowhile.php");
            //Test("for.php");
            //Test("foreach.php");
            Console.ReadKey();
            Console.WriteLine("End.");
        }
    }
}
