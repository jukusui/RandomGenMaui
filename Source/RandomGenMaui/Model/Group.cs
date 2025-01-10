using Jukusui.RandomGen.Rng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.Model;

internal class Group
{

    private readonly RngBase rng = RngFactory.Create();

    public bool isValid(IReadOnlyList<string> items, IReadOnlyList<int> groups)
        => 0 < items.Count && groups.Sum() == items.Count;

    public IReadOnlyList<IReadOnlyList<string>> Generate(IReadOnlyList<string> items, IReadOnlyList<int> groups)
    {
        if (!isValid(items, groups))
            return [];
        var cnt = items.Count;
        var results = groups.Select(len => new string[len]).ToArray();
        var resultCounts = new int[groups.Count];
        Array.Clear(resultCounts);
        var groupIds = new int[cnt];
        {
            var i = 0;
            for (int gi = 0; gi < groups.Count; gi++)
            {
                var group = groups[gi];
                for (var j = 0; j < group; i++, j++)
                    groupIds[i] = gi;
            }
        }

        for (int i = 0; i < cnt; i++)
        {
            var j = (int)rng.NextUInt32((uint)(cnt - 1 - i));
            var groupId = groupIds[j];
            results[groupId][resultCounts[groupId]] = items[i];
            resultCounts[groupId]++;
            groupIds[j] = groupIds[cnt - 1 - i];
        }

        //var buff = items.ToArray();
        //for (int i = groupIds.Length; i > 0; i--)
        //{
        //    var j = (int)rng.NextUInt32((uint)i);
        //    var groupId = groupIds[i];
        //    results[groupId][resultCounts[groupId]] = buff[j];
        //    resultCounts[groupId]++;
        //    buff[j] = buff[i - 1];
        //}

        return results;
    }
}
