using System;

namespace Calculator
{
    public record Result<T>(T? Val, string? Err)
    {
        public bool IsErr()
        {
            return Err != null;
        }

        public bool IsOk()
        {
            return Err == null;
        }

        public Result<T> Map(Func<T, T> func)
        {
            if (IsOk())
            {
                return Result<T>.NewOk(func(Val!));
            }
            else
            {
                return this;
            }
        }

        static public Result<T> NewErr(string err) => new Result<T>(default, err);
        static public Result<T> NewOk(T val) => new Result<T>(val, default);
    }
}
