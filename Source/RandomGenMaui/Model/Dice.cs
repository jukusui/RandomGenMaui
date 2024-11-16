using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jukusui.RandomGen.Rng;

namespace Jukusui.RandomGen.Model;
internal class Dice
{

    private readonly RngBase rng = RngFactory.Create();

    public uint Roll(uint max)
    {
        return rng.NextUInt32(max);
    }

}
