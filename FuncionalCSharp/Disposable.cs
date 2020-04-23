using System;
using System.Collections.Generic;
using System.Text;

namespace FuncionalCSharp
{
    public static class Disposable
    {
        public static TResult Using<TDisposable, TResult>
        (
            Func<TDisposable> factory,
            Func<TDisposable, TResult> fn
        ) where TDisposable : IDisposable
        {
            using (var disposabled = factory())
            {
                return fn(disposabled);
            }
        }
    }

}
