using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncionalCSharp
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendFormattedLine(this StringBuilder @this, string format, params object[] arguments) =>
                @this.AppendFormat(format, arguments).AppendLine();

        public static StringBuilder AppendLineWhen(this StringBuilder @this, Func<bool> predicate, Func<StringBuilder, StringBuilder> fn) =>
            predicate() ? fn(@this) : @this;

        public static StringBuilder AppendSequence<T>(this StringBuilder @this, IEnumerable<T> sequence, Func<StringBuilder, T, StringBuilder> fn) =>
            sequence.Aggregate(@this, fn);

        public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> fn) =>
            fn(@this);

        public static T Tee<T>(this T @this, Action<T> act)
        {
            act(@this);
            return @this;
        }

    }
}
