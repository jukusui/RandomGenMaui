#if ANDROID
using Android.Widget;
using Jukusui.RandomGen.Util;
#endif
namespace Jukusui.RandomGen.View;

using Jukusui.RandomGen.Util;
using Res = Properties.Resources;

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
            ToastManager.Show(Res.GenShared_Copied);
        }
    }

}