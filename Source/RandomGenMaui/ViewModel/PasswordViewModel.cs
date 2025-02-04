using CommunityToolkit.Mvvm.ComponentModel;
using Jukusui.RandomGen.Model;
using Jukusui.RandomGen.Properties;
using Jukusui.RandomGen.Util;
using Microsoft.Maui.Animations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jukusui.RandomGen.ViewModel;
public partial class PasswordViewModel : ObservableObject
{

    private readonly Password password = new();

    #region Length

    public uint LengthMin { get; } = 1;
    public uint LengthMax { get; } = 128;

    private uint length = 8;
    public uint Length
    {
        get => length;
        set
        {
            if (SetProperty(ref length, value))
            {
                SetProperty(ref lengthText, length.ToString(), nameof(LengthText));
                ErrorText = null;
            }
        }
    }

    private string? lengthText = null;
    public string LengthText
    {
        get => lengthText ?? length.ToString();
        set
        {
            if (uint.TryParse(value, out var newValue))
            {
                if (newValue < LengthMin)
                {
                    SetProperty(ref lengthText, value);
                    ErrorText = Resources.Error_Smaller;
                }
                else if (LengthMax < newValue)
                {
                    SetProperty(ref lengthText, value);
                    ErrorText = Resources.Error_Bigger;
                }
                else
                {
                    SetProperty(ref length, newValue, nameof(Length));
                    SetProperty(ref lengthText, value);
                    ErrorText = null;
                }
            }
            else
            {
                SetProperty(ref lengthText, value);
                ErrorText = Resources.Error_NumberConvert;
            }
        }
    }

    [ObservableProperty]
    public partial string? ErrorText { get; set; } = null;
    #endregion

    [ObservableProperty]
    public partial bool IsPassword { get; set; } = true;

    private int resultNo = 0;
    private const int RESULT_MAX = 100;


    public CharacterGroup ChUpper { get; } =
        new CharacterGroup(null, Resources.Password_ChUpper, RangeBetween('A', 'Z'));

    public CharacterGroup ChLower { get; } =
        new CharacterGroup(null, Resources.Password_ChLower, RangeBetween('a', 'z'));

    public CharacterGroup ChNumber { get; } =
        new CharacterGroup(null, Resources.Password_ChNumber, RangeBetween('0', '9'));

    public CharacterGroup ChSymbol { get; } =
        new CharacterGroup(null, Resources.Password_ChSymbol, [
            "!","\"","#","$","%","&","'","()","*","+",",","-",".","/",
            ":",";","<>","=","?",
            "@",
            "[]","\\","^","_",
            "`",
            "{}","|","~"]);

    public CharacterGroup CharacterGroups { get; }


    public ObservableCollection<KeyValuePair<int, string>> Results { get; } = [];

    public ICommand RollCommand { get; }
    public ICommand VisibilityCommand { get; }
    public ICommand DeleteCommand { get; }
    public PasswordViewModel()
    {
        CharacterGroups = new CharacterGroup(null, "All",
            ChUpper, ChLower, ChNumber, ChSymbol);
        RollCommand = new Command(OnRoll);
        VisibilityCommand = new Command(ChangeVisibility);
        DeleteCommand = new Command(OnDelete);
    }

    private void ChangeVisibility()
    {
        IsPassword = !IsPassword;
    }

    private void OnDelete()
    {
        Results.Clear();
        resultNo = 0;
    }


    private async void OnRoll()
    {
        if (1 <= Length)
        {
            if (RESULT_MAX <= Results.Count)
            {
                Results.RemoveAt(0);
            }
            resultNo = (resultNo + 1) % RESULT_MAX;
            var pass = password.Generate(CharacterGroups.EnabledChars, (int)Length);
            Results.Insert(Results.Count, new(resultNo, pass));
            await Clipboard.SetTextAsync(pass);
            ToastManager.Show(Resources.GenShared_Copied);
        }
    }

    private static string GetCharRnage(char start, char end)
    {
        var length = end - start + 1;
        var res = string.Create(length, 0, (content, _) =>
        {
            var ptr = content.GetEnumerator();
            for (var ch = start; ch <= end; ch++)
            {
                ptr.MoveNext();
                ptr.Current = ch;
            }
        });
        return res;
    }

    static IEnumerable<char> RangeBetween(char from, char to)
    {
        return Enumerable.Range(from, to - from).Select(i => (char)i);
    }

}

public interface ICharacterFlag : INotifyPropertyChanged
{
    bool Flag { get; set; }
    string Title { get; }
    ICharacterFlag? Owner { get; }
    public string EnabledChars { get; }
    void NotifyFlagChanged();
}

public partial class CharacterGroup : ObservableObject, ICharacterFlag
{

    public CharacterGroup(ICharacterFlag? owner, string title, params CharacterGroup[] groups)
    {
        Owner = owner;
        Title = title;
        Flags = groups;
        foreach (var group in groups)
        {
            group.Owner = this;
        }
    }

    public CharacterGroup(ICharacterFlag? owner, string title, params IEnumerable<char> chars)
    {
        Owner = owner;
        Title = title;
        Flags = chars.Select(ch => new CharacterFlag(this, ch.ToString(), ch)).ToArray();
    }
    public CharacterGroup(ICharacterFlag? owner, string title, params IEnumerable<string> strings)
    {
        Owner = owner;
        Title = title;
        Flags = strings.Select(str => new CharacterGroup(this, str, str.ToArray())).ToArray();
    }


    public string Title { get; }
    public ICharacterFlag? Owner { get; private set; }


    public string EnabledChars { get => string.Join("", Flags.Select(flag => flag.EnabledChars)); }

    public IReadOnlyList<ICharacterFlag> Flags { get; }
    public bool Flag
    {
        get => Flags.Any(ch => ch.Flag);
        set
        {
            if (_flag != value)
            {
                foreach (var child in Flags)
                {
                    child.Flag = value;
                }
            }
            Owner?.NotifyFlagChanged();
        }
    }
    private bool _flag = true;

    public void NotifyFlagChanged()
    {
        SetProperty(ref _flag, Flags.Any(ch => ch.Flag), nameof(Flag));
        OnPropertyChanged(nameof(EnabledChars));
    }
}

public partial class CharacterFlag : ObservableObject, ICharacterFlag
{

    public char Character { get; init; }
    public ICharacterFlag? Owner { get; }
    public string EnabledChars { get => _flag ? Character.ToString() : ""; }
    public string Title { get; init; }

    private bool _flag;
    public bool Flag
    {
        get => _flag; set
        {
            if (SetProperty(ref _flag, value))
            {
                OnPropertyChanged(nameof(EnabledChars));
                Owner?.NotifyFlagChanged();
            }
        }
    }

    public CharacterFlag(ICharacterFlag? owner, string title, char character)
    {
        Owner = owner;
        Title = title;
        _flag = true;
        Character = character;
    }

    public void NotifyFlagChanged() { }
}
