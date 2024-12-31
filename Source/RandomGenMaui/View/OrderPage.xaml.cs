using Jukusui.RandomGen.Util;
using System.Globalization;
using Res = Jukusui.RandomGen.Properties.Resources;

namespace Jukusui.RandomGen.View;

public partial class OrderPage : ContentPage
{
    public OrderPage()
    {
        InitializeComponent();
    }

    private class TextJoinConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return string.Join(' ', value as IEnumerable<string> ?? []);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public static IValueConverter TextJoin { get; } = new TextJoinConverter();

    private async void OnOutputListTapped(object sender, TappedEventArgs e)
    {
        if (sender is Entry input)
        {
            await Clipboard.SetTextAsync(input.Text);
            ToastManager.Show(Res.GenShared_Copied);
        }
    }
}