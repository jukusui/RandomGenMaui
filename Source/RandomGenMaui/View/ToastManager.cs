#if ANDROID
using Android.Views.Inspectors;
using Android.Widget;
using System.Runtime.CompilerServices;
#endif

namespace Jukusui.RandomGen.View;

public class ToastManager : BindableObject
{


    public static BindableProperty TextProperty =
    BindableProperty.CreateAttached(
        "Text", typeof(string), typeof(ToastManager),
        null, propertyChanged: OnTestChanged);

    private static void OnTestChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Show(newValue as string);
    }


    public static bool GetText(BindableObject view)
    {
        return (bool)view.GetValue(TextProperty);
    }

    public static void SetText(BindableObject view, string value)
    {
        view.SetValue(TextProperty, value);
    }



#if ANDROID

    public static void Show(string? text)
    {
        lock (mutex)
        {
            Toast? newToast = null;
            if (text != null && text != "")
            {
                newToast = Toast.MakeText(Android.App.Application.Context, text, ToastLength.Short);
                if (newToast == null)
                    return;
            }
            oldToast?.Cancel();
            oldToast = newToast;

            newToast?.Show();
        }
    }

    private static Toast? oldToast;
    private static object mutex = new();
#else
    public static void Show(string? _) {}
#endif

}
