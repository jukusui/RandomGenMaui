using CommunityToolkit.Mvvm.ComponentModel;
using Jukusui.RandomGen.Model;
using Jukusui.RandomGen.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Group = Jukusui.RandomGen.Model.Group;

namespace Jukusui.RandomGen.ViewModel;

public partial class GroupViewModel : ObservableObject
{

    private const int MAX_COUNT = 1000;

    private string listText = "";

    public string ListText
    {
        get => listText;
        set
        {
            if (SetProperty(ref listText, value))
            {
                if (string.IsNullOrEmpty(value) || emptyOnlyRegex.IsMatch(value))
                {
                    //スペースかnullのみ
                    ItemsList = Array.Empty<string>();
                    ListErrorText = Resources.Error_NumberOrList;
                }
                else
                {
                    var spaces = emptyRegex.Matches(value);
                    if (MAX_COUNT - 1 < spaces.Count)
                    {
                        ItemsList = Array.Empty<string>();
                        ListErrorText = Resources.Error_ListLonger;
                    }
                    else if (1 <= spaces.Count)
                    {
                        //スペースを含む文字列
                        var match = splitRegex.Match(value);
                        if (match.Success && 2 <= match.Groups.Count && match.Groups.Count <= 3)
                        {
                            ItemsList = match.Groups.Values.Skip(1).SelectMany(v => v.Captures).Select(c => c.Value).ToArray();
                            ListErrorText = null;
                        }
                        else
                        {
                            ItemsList = Array.Empty<string>();
                            ListErrorText = Resources.Error_ListConvert;
                        }
                    }
                    else if (numberRegex.IsMatch(value))
                    {
                        //数値のみの文字列
                        if (int.TryParse(value, out var num))
                        {
                            if (num < 2)
                            {
                                ItemsList = Array.Empty<string>();
                                ListErrorText = Resources.Error_Smaller;
                            }
                            else if (MAX_COUNT < num)
                            {
                                ItemsList = Array.Empty<string>();
                                ListErrorText = Resources.Error_Bigger;
                            }
                            else
                            {
                                ItemsList = Enumerable.Range(1, num).Select(n => n.ToString()).ToList();
                                ListErrorText = null;
                            }
                        }
                        else
                        {
                            //値が大きい場合
                            ItemsList = Array.Empty<string>();
                            ListErrorText = Resources.Error_Bigger;
                        }
                    }
                    else
                    {
                        //数値以外のみの文字列
                        ItemsList = Array.Empty<string>();
                        ListErrorText = Resources.Error_NumberOrList;
                    }
                }
                if (ItemsList.Any())
                {
                    UpdateGroupList();
                }
            }
        }
    }

    private string groupText = "";
    public string GroupText
    {
        get => groupText;
        set
        {
            if (SetProperty(ref groupText, value))
                UpdateGroupList();
        }
    }


    [GeneratedRegex(@"^\s+$")]
    private static partial Regex emptyOnlyRegex { get; }
    [GeneratedRegex(@"\s+")]
    private static partial Regex emptyRegex { get; }
    [GeneratedRegex(@"^([^\s]+)(?:\s+([^\s]+))*$")]
    private static partial Regex splitRegex { get; }
    [GeneratedRegex(@"^([0-9]+)(?:\s+([0-9]+))*$")]
    private static partial Regex splitNumberRegex { get; }
    [GeneratedRegex(@"^[0-9]+$")]
    private static partial Regex numberRegex { get; }

    [ObservableProperty]
    public partial IReadOnlyList<string> ItemsList { get; set; } = Array.Empty<string>();

    [ObservableProperty]
    public partial IReadOnlyList<int> GroupList { get; set; } = Array.Empty<int>();

    [ObservableProperty]
    public partial string? ListErrorText { get; set; }
    [ObservableProperty]
    public partial string? GroupErrorText { get; set; }


    private void UpdateGroupList()
    {
        if (string.IsNullOrEmpty(groupText) || emptyOnlyRegex.IsMatch(groupText))
        {
            //スペースかnullのみ
            GroupList = Array.Empty<int>();
            GroupErrorText = Resources.Error_NumberOrList;
        }
        else
        {
            var spaces = emptyRegex.Matches(groupText);
            if (MAX_COUNT - 1 < spaces.Count)
            {
                GroupList = Array.Empty<int>();
                GroupErrorText = Resources.Error_ListLonger;
            }
            else if (1 <= spaces.Count)
            {
                //スペースを含む文字列
                var match = splitRegex.Match(groupText);
                if (match.Success && 2 <= match.Groups.Count && match.Groups.Count <= 3)
                {
                    var items = match.Groups.Values.Skip(1).SelectMany(v => v.Captures).Select(c => c.Value);
                    var numItems = items.Select(s => int.TryParse(s, out var num) ? num : -1).ToArray();
                    if (numItems.Any(v => v == -1))
                    {
                        GroupList = Array.Empty<int>();
                        GroupErrorText = Resources.Error_NumberConvert;
                    }
                    else if (numItems.Any(v => v == 0))
                    {
                        GroupList = Array.Empty<int>();
                        GroupErrorText = Resources.Error_Smaller;
                    }
                    else
                    {
                        var sum = numItems.Sum();
                        if (ItemsList.Count < sum)
                        {
                            GroupList = Array.Empty<int>();
                            GroupErrorText = Resources.Error_Bigger;
                        }
                        else if (sum < ItemsList.Count)
                        {
                            GroupList = Array.Empty<int>();
                            GroupErrorText = Resources.Error_Smaller;
                        }
                        else
                        {
                            GroupList = numItems;
                            GroupErrorText = null;
                        }
                    }
                }
                else
                {
                    GroupList = Array.Empty<int>();
                    GroupErrorText = Resources.Error_ListConvert;
                }
            }
            else if (numberRegex.IsMatch(groupText))
            {
                //数値のみの文字列
                if (int.TryParse(groupText, out var num))
                {
                    if (num < 2)
                    {
                        GroupList = Array.Empty<int>();
                        GroupErrorText = Resources.Error_Smaller;
                    }
                    else if (MAX_COUNT < num || ItemsList.Count < num)
                    {
                        GroupList = Array.Empty<int>();
                        GroupErrorText = Resources.Error_Bigger;
                    }
                    else
                    {
                        var div = Math.DivRem(ItemsList.Count, num, out var rem);
                        GroupList = Enumerable.Repeat(div + 1, rem).Concat(Enumerable.Repeat(div, num - rem)).ToArray();
                        GroupErrorText = null;
                    }
                }
                else
                {
                    //値が大きい場合
                    GroupList = Array.Empty<int>();
                    GroupErrorText = Resources.Error_Bigger;
                }
            }
            else
            {
                //数値以外のみの文字列
                GroupList = Array.Empty<int>();
                GroupErrorText = Resources.Error_NumberOrList;
            }
        }
    }


    [ObservableProperty]
    public partial ObservableCollection<KeyValuePair<int, IReadOnlyList<IReadOnlyList<string>>>> ResultLists { get; set; } = new();

    private int resultNo = 0;
    private const int RESULT_MAX = 100;

    private readonly Group group = new();

    public ICommand RollCommand { get; }
    public ICommand DeleteCommand { get; }

    public GroupViewModel()
    {
        RollCommand = new Command(OnRoll);
        DeleteCommand = new Command(OnDelete);
    }

    private void OnDelete()
    {
        ResultLists.Clear();
        resultNo = 0;
    }

    private void OnRoll()
    {
        if (2 <= ItemsList.Count && 2 <= GroupList.Count && group.isValid(ItemsList, GroupList))
        {
            if (RESULT_MAX <= ResultLists.Count)
            {
                ResultLists.RemoveAt(0);
            }
            resultNo = (resultNo + 1) % RESULT_MAX;
            ResultLists.Insert(ResultLists.Count, new(resultNo,
                group.Generate(ItemsList, GroupList)));
        }
    }

}
