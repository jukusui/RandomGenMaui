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

    public string Generate(string chars, int length)
    {
        if (length <= 0)
            return "";
        if (chars.Length <= 0)
            return "";
        var patterns = (uint)chars.Length;
        StringBuilder sb = new(length, length);
        for (int i = 0; i < length; i++)
        {

            sb.Append(chars[(int)rng.NextUInt32(patterns)]);
        }
        return sb.ToString();
    }
}
