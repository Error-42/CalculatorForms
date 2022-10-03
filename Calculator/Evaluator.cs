using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    static class Evaluator
    {
        public abstract record Result
        {
            public record Error(int Pos, string Info) : Result;
            public record Answer(double Value) : Result;
        }

        public static Result Evaluate(string equation)
        {
            throw new NotImplementedException();
        }
    }
}
