using Jukusui.RandomGen.Rng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.Model;

internal class Order
{

    private readonly RngBase rng = RngFactory.Create();


    public IReadOnlyList<string> Generate(IReadOnlyList<string> items)
    {
        var count = items.Count;
        var results = new string[count];
        var buff = items.ToArray();

        for (int i = count; i > 0; i--)
        {
            var j = (int)rng.NextUInt32((uint)i);
            results[i - 1] = buff[j];
            buff[j] = buff[i - 1];
        }
        return results;
    }
}
