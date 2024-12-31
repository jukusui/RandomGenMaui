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

namespace Jukusui.RandomGen.ViewModel;

public partial class OrderViewModel : ObservableObject
{

    private const int MAX_COUNT = 10;

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
                    ErrorText = Resources.Error_NumberOrList;
                }
                else
                {
                    var spaces = emptyRegex.Matches(value);
                    if (MAX_COUNT -1< spaces.Count)
                    {
                        ItemsList = Array.Empty<string>();
                        ErrorText = Resources.Error_ListLonger;
                    }
                    else if (1 <= spaces.Count)
                    {
                        //スペースを含む文字列
                        var match = splitRegex.Match(value);
                        if (match.Success && 2 <= match.Groups.Count && match.Groups.Count <= 3)
                        {
                            ItemsList = match.Groups.Values.Skip(1).SelectMany(v => v.Captures).Select(c => c.Value).ToArray();
                            ErrorText = null;
                        }
                        else
                        {
                            ItemsList = Array.Empty<string>();
                            ErrorText = Resources.Error_ListConvert;
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
                                ErrorText = Resources.Error_Smaller;
                            }
                            else if (MAX_COUNT < num)
                            {
                                ItemsList = Array.Empty<string>();
                                ErrorText = Resources.Error_Bigger;
                            }
                            else
                            {
                                ItemsList = Enumerable.Range(1, num).Select(n => n.ToString()).ToList();
                                ErrorText = null;
                            }
                        }
                        else
                        {
                            //値が大きい場合
                            ItemsList = Array.Empty<string>();
                            ErrorText = Resources.Error_Bigger;
                        }
                    }
                    else
                    {
                        //数値以外のみの文字列
                        ItemsList = Array.Empty<string>();
                        ErrorText = Resources.Error_NumberOrList;
                    }
                }
            }
        }
    }

    private static Regex emptyOnlyRegex = new Regex(@"^\s+$");
    private static Regex emptyRegex = new Regex(@"\s+");
    private static Regex splitRegex = new Regex(@"^([^\s]+)(?:\s+([^\s]+))*$");
    private static Regex numberRegex = new Regex(@"^[0-9]+$");

    [ObservableProperty]
    public partial IReadOnlyList<string> ItemsList { get; set; } = Array.Empty<string>();

    [ObservableProperty]
    public partial ObservableCollection<KeyValuePair<int, IReadOnlyList<string>>> ResultLists { get; set; } = new();

    [ObservableProperty]
    public partial string? ErrorText { get; set; }

    private int resultNo = 0;
    private const int RESULT_MAX = 100;

    private readonly Order order = new();

    public ICommand RollCommand { get; }
    public ICommand DeleteCommand { get; }

    public OrderViewModel()
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
        if (2 <= ItemsList.Count)
        {
            if (RESULT_MAX <= ResultLists.Count)
            {
                ResultLists.RemoveAt(0);
            }
            resultNo = (resultNo + 1) % RESULT_MAX;
            ResultLists.Insert(ResultLists.Count, new(resultNo,
                order.Generate(ItemsList)));
        }
    }
}
