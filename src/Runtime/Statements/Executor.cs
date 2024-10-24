using PHP.Core.Runtime.Exceptions;
using PHP.Core.Runtime.Memory;
using PHP.Core.Runtime.Statements.Constructions;
using PHP.Core.Runtime.Statements.Containers;
using PHP.Core.Runtime.Statements.Operations;
using PHP.Core.Runtime.Statements.Operators;

namespace PHP.Core.Runtime.Statements
{
    public class Executor
    {
        private Environment _environment;
        
        private Memory.Memory _returnData = null;

        public Executor(Environment environment)
        {
            this._environment = environment;
        }
        
        internal int Exec(AbstractStatement root)
        {
            Memory.Memory result = ExecStatement(root);
            if (_returnData != null)
                result = _returnData;
            return result.ToInt();
        }
        
        private Memory.Memory ExecStatement(AbstractStatement statement) => ExecStatement(statement, new MemoryBlock());
        private Memory.Memory ExecStatement(AbstractStatement statement, MemoryBlock memory)
        {
            if (_returnData != null)
                return _returnData;
            
            if (statement.GetType() == typeof(BlockStatement))
                return ExecBlockStatement(statement, memory);
            
            
            if (statement.GetType() == typeof(AssignmentStatement))
                return ExecAssignmentStatement(statement, memory);
            
            if (statement.GetType() == typeof(AddStatement))
                return ExecAddStatement(statement, memory);
            if (statement.GetType() == typeof(SubStatement))
                return ExecSubStatement(statement, memory);
            if (statement.GetType() == typeof(MulStatement))
                return ExecMulStatement(statement, memory);
            if (statement.GetType() == typeof(DivStatement))
                return ExecDivStatement(statement, memory);
            if (statement.GetType() == typeof(ModStatement))
                return ExecModStatement(statement, memory);
            if (statement.GetType() == typeof(PowStatement))
                return ExecPowStatement(statement, memory);
            if (statement.GetType() == typeof(ConcatStatement))
                return ExecConcatStatement(statement, memory);
            
            if (statement.GetType() == typeof(DataStatement))
                return ExecDataStatement(statement);
            if (statement.GetType() == typeof(VariableStatement))
                return ExecVariableStatement(statement, memory);
            
            if (statement.GetType() == typeof(ReturnStatement))
                return ExecReturnStatement(statement, memory);
            if (statement.GetType() == typeof(EchoStatement))
                return ExecEchoStatement(statement, memory);
            

            throw new RuntimeException($"Unknown statement {statement}.");
        }

        private Memory.Memory ExecBlockStatement(AbstractStatement statement, MemoryBlock memory)
        {
            MemoryBlock memoryBlock = new MemoryBlock(memory);
            foreach (AbstractStatement currentStmt in ((BlockStatement)statement).Statements)
            {
                if (_returnData != null)
                    break;
                ExecStatement(currentStmt, memoryBlock);
            }

            return null;
        }

        private Memory.Memory ExecAssignmentStatement(AbstractStatement statement, MemoryBlock memory)
        {
            AssignmentStatement assignmentStatement = (AssignmentStatement)statement;
            VariableStatement variable = assignmentStatement.Variable;
            if (assignmentStatement.Statement.GetType() == typeof(RefVariableStatement))
            {
                memory.SetPointer(variable.Name,
                    GetVariablePointer((RefVariableStatement)assignmentStatement.Statement, memory));
                return memory.GetData(variable.Name);
            }

            Memory.Memory data = ExecStatement(assignmentStatement.Statement, memory);
            memory.SetData(variable.Name, data);
            return data;
        }
        private Memory.Memory ExecAddStatement(AbstractStatement statement, MemoryBlock memory)
        {
            AddStatement addStatement = (AddStatement)statement;
            Memory.Memory left = ExecStatement(addStatement.Left, memory);
            Memory.Memory right = ExecStatement(addStatement.Right, memory);
            if (left == null || right == null)
                throw new RuntimeException("Incorrect use of the addition operation.");
            return Memory.Memory.Add(left, right);
        }
        private Memory.Memory ExecSubStatement(AbstractStatement statement, MemoryBlock memory)
        {
            AddStatement addStatement = (AddStatement)statement;
            Memory.Memory left = ExecStatement(addStatement.Left, memory);
            Memory.Memory right = ExecStatement(addStatement.Right, memory);
            if (left == null || right == null)
                throw new RuntimeException("Incorrect use of the addition operation.");
            return Memory.Memory.Sub(left, right);
        }
        private Memory.Memory ExecMulStatement(AbstractStatement statement, MemoryBlock memory)
        {
            AddStatement addStatement = (AddStatement)statement;
            Memory.Memory left = ExecStatement(addStatement.Left, memory);
            Memory.Memory right = ExecStatement(addStatement.Right, memory);
            if (left == null || right == null)
                throw new RuntimeException("Incorrect use of the addition operation.");
            return Memory.Memory.Mul(left, right);
        }
        private Memory.Memory ExecDivStatement(AbstractStatement statement, MemoryBlock memory)
        {
            AddStatement addStatement = (AddStatement)statement;
            Memory.Memory left = ExecStatement(addStatement.Left, memory);
            Memory.Memory right = ExecStatement(addStatement.Right, memory);
            if (left == null || right == null)
                throw new RuntimeException("Incorrect use of the addition operation.");
            return Memory.Memory.Div(left, right);
        }
        private Memory.Memory ExecModStatement(AbstractStatement statement, MemoryBlock memory)
        {
            AddStatement addStatement = (AddStatement)statement;
            Memory.Memory left = ExecStatement(addStatement.Left, memory);
            Memory.Memory right = ExecStatement(addStatement.Right, memory);
            if (left == null || right == null)
                throw new RuntimeException("Incorrect use of the addition operation.");
            return Memory.Memory.Mod(left, right);
        }
        private Memory.Memory ExecPowStatement(AbstractStatement statement, MemoryBlock memory)
        {
            AddStatement addStatement = (AddStatement)statement;
            Memory.Memory left = ExecStatement(addStatement.Left, memory);
            Memory.Memory right = ExecStatement(addStatement.Right, memory);
            if (left == null || right == null)
                throw new RuntimeException("Incorrect use of the addition operation.");
            return Memory.Memory.Pow(left, right);
        }
        private Memory.Memory ExecConcatStatement(AbstractStatement statement, MemoryBlock memory)
        {
            AddStatement addStatement = (AddStatement)statement;
            Memory.Memory left = ExecStatement(addStatement.Left, memory);
            Memory.Memory right = ExecStatement(addStatement.Right, memory);
            if (left == null || right == null)
                throw new RuntimeException("Incorrect use of the addition operation.");
            return Memory.Memory.Concat(left, right);
        }

        private Memory.Memory ExecDataStatement(AbstractStatement statement)
        {
            return ((DataStatement)statement).Data;
        }
        private Memory.Memory ExecVariableStatement(AbstractStatement statement, MemoryBlock memory)
        {
            VariableStatement variableStatement = (VariableStatement)statement;
            return memory.GetData(variableStatement.Name);
        }

        private MemoryBlock.Pointer GetVariablePointer(RefVariableStatement statement, MemoryBlock memoryBlock)
        {
            return memoryBlock.GetPointer(statement.Name);
        }

        private Memory.Memory ExecReturnStatement(AbstractStatement statement, MemoryBlock memoryBlock)
        {
            _returnData = ExecStatement(((ReturnStatement)statement).Statement, memoryBlock);
            return _returnData;
        }

        private Memory.Memory ExecEchoStatement(AbstractStatement statement, MemoryBlock memoryBlock)
        {
            EchoStatement echoStatement = (EchoStatement)statement;
            _environment.StdOut.Write(ExecStatement(echoStatement.Statement, memoryBlock).ToString());
            return null;
        }
    }
}