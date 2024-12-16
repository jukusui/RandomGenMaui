using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.Util;
public static class AssemblyInfoManager
{

    static AssemblyInfoManager()
    {
        var asm = Assembly.GetCallingAssembly();
        var name = asm.GetName();
        Version = name.Version?.ToString() ?? "?.?.?";
    }

    public static string Version { get; }
}
