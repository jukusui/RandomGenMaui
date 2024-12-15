using Jukusui.RandomGen.Properties;
using Jukusui.RandomGen.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.ViewModel;

public class LicenseViewModel
{



    public IReadOnlyList<LicenseGroup> Licenses { get; set; } = [
         new ( Resources.License_Group_Font,
            [
                new ("material-design-icons","2.801","Material Design icons by Google (Material Symbols)",
                    "https://github.com/google/material-design-icons","Google",
                    true,"Apache License 2.0","https://github.com/google/material-design-icons/blob/master/LICENSE",null,null)
            ]
        ),
        new( Resources.License_Group_Nuget,LicenseManager.LicenseViewModels)
        ];

}

public class LicenseGroup(string name, IReadOnlyList<LicenseData> list) : IReadOnlyList<LicenseData>
{
    private readonly IReadOnlyList<LicenseData> list = list;

    public LicenseData this[int index] => list[index];

    public string Name { get; } = name;

    public int Count => list.Count;

    public IEnumerator<LicenseData> GetEnumerator() => list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)list).GetEnumerator();
}
