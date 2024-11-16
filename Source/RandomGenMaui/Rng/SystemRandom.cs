using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.Rng;
internal class SystemRandom
    : RngBase
{
    private Random _random;

    public SystemRandom()
    {
        _random = new Random();
    }

    public override void NextBytes(Span<byte> data)
    {
        _random.NextBytes(data);
    }
}
