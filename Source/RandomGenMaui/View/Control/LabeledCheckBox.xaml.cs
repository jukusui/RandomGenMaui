using System.Windows.Input;

namespace Jukusui.RandomGen.View.Control;

public partial class LabeledCheckBox : ContentView
{
    public LabeledCheckBox()
    {
        OnCheckCommand = new Command(() => IsChecked = !IsChecked);
        InitializeComponent();
    }


    public static BindableProperty IsCheckedProperty =
    BindableProperty.Create(
        nameof(IsChecked), typeof(bool), typeof(LabeledCheckBox), false, BindingMode.TwoWay);

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }


    public static BindableProperty LabelProperty =
    BindableProperty.Create(
        nameof(Label), typeof(string), typeof(LabeledCheckBox));

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public ICommand OnCheckCommand { get; }

}