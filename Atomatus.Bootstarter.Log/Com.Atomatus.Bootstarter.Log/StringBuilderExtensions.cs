using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Com.Atomatus.Bootstarter
{
    internal static class StringBuilderExtensions
    {
        public static StringBuilder AppendLineIf([NotNull] this StringBuilder sb, bool condition, params object[] value)
        {
            return condition ? value.Aggregate(sb, (acc, curr) => acc.Append(curr)).AppendLine() : sb;
        }
    }
}
