#if ANDROID
using Android.Widget;
#endif

namespace Jukusui.RandomGen.View;

public partial class PasswordPage : ContentPage
{
    public PasswordPage()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty IsShownProperty =
    BindableProperty.CreateAttached("IsShown", typeof(bool), typeof(PasswordPage), true);

    public static bool GetIsShown(BindableObject view)
    {
        return (bool)view.GetValue(IsShownProperty);
    }

    public static void SetIsShown(BindableObject view, bool value)
    {
        view.SetValue(IsShownProperty, value);
    }

    private void OnSymbolBorderClicked(object sender, EventArgs e)
    {
        SetIsShown(SymbolBorder, !GetIsShown(SymbolBorder));
    }

    private async void OnOutputEntryTapped(object sender, TappedEventArgs e)
    {
        if (sender is Entry input)
        {
            await Clipboard.SetTextAsync(input.Text);
            ToastManager.Show("Copied");
        }
    }
}