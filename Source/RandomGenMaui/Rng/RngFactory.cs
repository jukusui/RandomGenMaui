using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.Rng;
internal static class RngFactory
{

    public static RngBase Create()
    {
        return new SystemSecurity();
    }

}
