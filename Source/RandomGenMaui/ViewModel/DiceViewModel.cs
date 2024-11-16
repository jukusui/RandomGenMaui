using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jukusui.RandomGen.Model;
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
                    ErrorText = "smaller";
                }
                else if (RangeMax < newValue)
                {
                    SetProperty(ref maximumText, value);
                    ErrorText = "bigger";
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
                ErrorText = "convert";
            }
        }
    }

    [ObservableProperty]
    private string? errorText = null;

    public uint RangeMin { get; } = 2;
    public uint RangeMax { get; } = 1000000;

    public ObservableCollection<KeyValuePair<int, uint>> Results { get; } = [];

    private readonly Dice dice = new();

    public ICommand SetValueCommand { get; }
    public ICommand RollCommand { get; }

    public DiceViewModel()
    {
        RollCommand = new Command(Roll);
        SetValueCommand = new Command(SetValue);
    }

    private void Roll()
    {
        if (2 <= Maximum)
        {
            var next = Results.Count + 1;
            if (100 <= Results.Count)
            {
                next = Results.First().Key + 1;
                Results.RemoveAt(0);
            }
            Results.Insert(Results.Count, new(next,
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
