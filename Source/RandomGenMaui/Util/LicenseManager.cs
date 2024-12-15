using Jukusui.RandomGen.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.Util;
internal class LicenseManager
{

    static LicenseManager()
    {
        using var names = Assembly.GetCallingAssembly().GetManifestResourceStream("License.json");
        if (names != null)
        {
            LicenseViewModels = JsonSerializer.Deserialize<LicenseData[]>(names)?.OrderBy(info => info.ID)?.ToArray()!;
        }
        LicenseViewModels ??= [];
    }

    public static IReadOnlyList<LicenseData> LicenseViewModels { get; }

}

public record LicenseData(string? ID, string? Version, string? Title, string? ProjectUrl, string? Authors, bool? RequireLicenseAccept, string? License, string? LicenseUrl, string? LicenseText, string? Copyright);
