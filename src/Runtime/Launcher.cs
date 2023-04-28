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
            string path = @"D:\Projects\CSharp\PHPSharp\php-core\tests\" + file;
            Console.WriteLine($"Testing > {file}");
            Console.WriteLine(ASTBuilder.BuildFromFile(path));
            Console.WriteLine($"End for > {file}");
        }
        public static void Main(string[] args)
        {
            Test("test_1.php");
            //Test("dowhile.php");
            //Test("for.php");
            //Test("foreach.php");
            Console.ReadKey();
            Console.WriteLine("End.");
        }
    }
}
