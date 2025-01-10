using Jukusui.RandomGen.Model;
using Jukusui.RandomGen.Util;
using System.Diagnostics;
using System.Globalization;
using Res = Jukusui.RandomGen.Properties.Resources;

namespace Jukusui.RandomGen.View;

public partial class GroupPage : ContentPage
{
    public GroupPage()
    {
        InitializeComponent();
    }
        private class TextJoinConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var items = value as IEnumerable<IEnumerable<string>> ?? [];
            return "[" + string.Join("] [", items.Select(v => string.Join(' ', v))) + "]";
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