using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.Rng;
internal class SystemSecurity : RngBase
{
    private RandomNumberGenerator _random;

    public SystemSecurity()
    {
        _random = RandomNumberGenerator.Create();
    }

    public override void NextBytes(Span<byte> data)
    {
        _random.GetBytes(data);
    }
}
