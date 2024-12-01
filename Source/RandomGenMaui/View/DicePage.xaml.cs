#if ANDROID
using Android.Widget;
#endif
namespace Jukusui.RandomGen.View;

public partial class DicePage : ContentPage
{
    public DicePage()
    {
        InitializeComponent();
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