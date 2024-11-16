using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jukusui.RandomGen.Rng;

namespace Jukusui.RandomGen.Model;
internal class Password
{

    private readonly RngBase rng = RngFactory.Create();

    public string Generate(IReadOnlyList<char> strings, int length)
    {
        if (length <= 0)
            return "";
        if (strings.Count <= 0)
            return "";
        var patterns = (uint)strings.Count;
        StringBuilder sb = new StringBuilder(length, length);
        for (int i = 0; i < length; i++)
        {

            sb[i] = strings[(int)rng.NextUInt32(patterns)];
        }
        return sb.ToString();
    }
}
