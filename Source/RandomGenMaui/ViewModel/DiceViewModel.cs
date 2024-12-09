using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Jukusui.RandomGen.Model;
using Jukusui.RandomGen.Properties;
namespace Jukusui.RandomGen.ViewModel;

public partial class DiceViewModel : ObservableObject
{
    private uint maximum = 6;
    public uint Maximum
    {
        get => maximum;
        set
        {
            if (SetProperty(ref maximum, value))
            {
                SetProperty(ref maximumText, maximum.ToString(), nameof(MaximumText));
                ErrorText = null;
            }
        }
    }

    private string? maximumText = null;
    public string MaximumText
    {
        get => maximumText ?? maximum.ToString();
        set
        {
            if (uint.TryParse(value, out var newValue))
            {
                if (newValue < RangeMin)
                {
                    SetProperty(ref maximumText, value);
                    ErrorText = Resources.Error_Smaller;
                }
                else if (RangeMax < newValue)
                {
                    SetProperty(ref maximumText, value);
                    ErrorText = Resources.Error_Bigger;
                }
                else
                {
                    SetProperty(ref maximum, newValue, nameof(Maximum));
                    SetProperty(ref maximumText, value);
                    ErrorText = null;
                }
            }
            else
            {
                SetProperty(ref maximumText, value);
                ErrorText = Resources.Error_NumberConvert;
            }
        }
    }

    [ObservableProperty]
    private string? errorText = null;

    public uint RangeMin { get; } = 2;
    public uint RangeMax { get; } = 1000000;

    private int resultNo = 0;
    private const int RESULT_MAX = 100;

    public ObservableCollection<KeyValuePair<int, uint>> Results { get; } = [];

    private readonly Dice dice = new();

    public ICommand SetValueCommand { get; }
    public ICommand RollCommand { get; }
    public ICommand DeleteCommand { get; }

    public DiceViewModel()
    {
        RollCommand = new Command(OnRoll);
        SetValueCommand = new Command(SetValue);
        DeleteCommand = new Command(OnDelete);
    }

    private void OnDelete()
    {
        Results.Clear();
        resultNo = 0;
    }

    private void OnRoll()
    {
        if (2 <= Maximum)
        {
            if (RESULT_MAX <= Results.Count)
            {
                Results.RemoveAt(0);
            }
            resultNo = (resultNo + 1) % RESULT_MAX;
            Results.Insert(Results.Count, new(resultNo,
                dice.Roll(Maximum) + 1));
        }
    }

    private void SetValue(object value)
    {
        if (value is string sValue)
        {
            Maximum = uint.Parse(sValue);
        }
    }
}
