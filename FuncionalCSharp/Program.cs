using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncionalCSharp
{


    class Program
    {
        private static Func<IDictionary<int, string>, string> BuildSelectBox(string id, bool includeUnknown) =>
            options =>
                new StringBuilder()
                    .AppendFormattedLine("<select id=\"{0}\" name=\"{0}\">", id)
                    .AppendLineWhen(
                        () => includeUnknown,
                        sb => sb.AppendLine("\t<option>Unknown</option>"))
                    .AppendSequence(options, (sb, opt) =>
                        sb.AppendFormattedLine("\t<option value=\"{0}\">{1}</option>", opt.Key, opt.Value))
                    .AppendLine("</select>")
                    .ToString();

        private static void Main(string[] args)
        {
            var selectBox = Disposable
                    .Using(
                        StreamFactory.GetStream,
                        steam => new byte[steam.Length].Tee(b => steam.Read(b, 0, (int)steam.Length)))
                    .Map(Encoding.UTF8.GetString)
                    .Split(new[] { Environment.NewLine, }, StringSplitOptions.RemoveEmptyEntries)
                    .Select((s, ix) => Tuple.Create(ix, s))
                    .ToDictionary(k => k.Item1, v => v.Item2)
                    .Map(BuildSelectBox("theDoctors", true))
                    .Tee(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
