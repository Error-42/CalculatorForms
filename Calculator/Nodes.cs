using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    abstract class Node
    {
        public int Start { get; }
        public int End { get; }

        public Node(int start, int end)
        {
            Start = start;
            End = end;
        }

        public abstract Result<double> Evaluate();

        public class Value : Node
        {
            double val;

            public Value(Token.Value token) : base(token.Start, token.End)
            {
                val = token.Val;
            }

            public override Result<double> Evaluate() => Result<double>.NewOk(val);
        }

        public class UnaryOp : Node
        {
            public enum Type
            {
                Neg,
            }

            Type ty;
            Node subNode;

            public UnaryOp(Type ty, Node subNode, int start, int end) : base(start, end)
            {
                this.ty = ty;
                this.subNode = subNode;
            }

            public override Result<double> Evaluate() => ty switch
            {
                Type.Neg => subNode.Evaluate().Map(x => -x),
            };
        }

        public class BinaryOp : Node
        {
            public enum Type
            {
                Add,
                Sub,
                Mul,
                Div,
            }

            Type ty;
            Node lhs;
            Node rhs;

            public BinaryOp(Node lhs, Type ty, Node rhs) : base(lhs.Start, rhs.End)
            {
                this.lhs = lhs;
                this.rhs = rhs;
                this.ty = ty;
            }

            public override Result<double> Evaluate()
            {
                Result<double> lres = lhs.Evaluate();
                if (lres.IsErr()) return lres;
                Result<double> rres = rhs.Evaluate();
                if (rres.IsErr()) return rres;

                // Todo: implement error for other possible failures e.g. sum of infinity.
                switch (ty)
                {
                    case Type.Add:
                        return Result<double>.NewOk(lres.Val + rres.Val);
                    case Type.Sub:
                        return Result<double>.NewOk(lres.Val - rres.Val);
                    case Type.Mul:
                        return Result<double>.NewOk(lres.Val * rres.Val);
                    case Type.Div:
                        if (rres.Val == 0)
                        {
                            return Result<double>.NewErr($"Division by zero at range [{Start}..{End}]");
                        }
                        return Result<double>.NewOk(lres.Val / rres.Val);
                    default:
                        // Needed, so compiler doesn't complain about all paths
                        // returning a value.
                        throw new Exception("unhandled ty");
                }
            }
        }
    }
}
